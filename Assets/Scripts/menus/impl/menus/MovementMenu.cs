using System;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementMenu : AbstractMenu
{
  private DropdownField from;
  private DropdownField to;
  private SliderInt troops;
  private Button registerButton;
  private Button skipButton;
  private int matchID;
  private int playerID;
  private int turnNumber;

  public MovementMenu(MenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override void RecieveParameters(object[] args)
  {
    matchID = (int)args[0];
    playerID = (int)args[1];
    turnNumber = (int)args[2];
    Debug.Log($"(MovementMenu) Recieved: MatchID: {matchID}, PlayerID: {playerID}, TurnNumber: {turnNumber}");
  }

  protected override VisualElement[] FetchUIElements()
  {
    from = root.Q<DropdownField>("From");
    to = root.Q<DropdownField>("To");
    troops = root.Q<SliderInt>("Troops");
    registerButton = root.Q<Button>("RegisterButton");
    skipButton = root.Q<Button>("SkipButton");
    return new VisualElement[] { from, to, troops, registerButton, skipButton };
  }

  private void SetDropdownLogic()
  {
    List<string> territories = SqlUtils.ExecuteQuery(Queries.GET_CONTROLLED_TERRITORIES,
      reader => reader.GetString(0),
      new MySqlParameter[] {
        new("@matchID", matchID),
        new("@playerID", playerID),
        new("@turnNumber", turnNumber)
      });
    territories.ForEach(t => Debug.Log($"Territory fetched: {t}"));
    from.choices = territories;
    from.value = from.choices[0];
    to.choices = territories;
    to.value = to.choices[0];
  }

  protected override void SetUICallbacks()
  {
    SetDropdownLogic();
    registerButton.clicked += RegisterMovementAndGoToMainMenu;
    skipButton.clicked += GoToMainMenu;
  }

  private void RegisterMovementAndGoToMainMenu()
  {
    string fromTerritory = from.value;
    string toTerritory = to.value;
    int troopsToMove = troops.value;
    Debug.Log($"Registering movement from {fromTerritory} to {toTerritory} with {troopsToMove} troops");
    RegisterMovement(fromTerritory, toTerritory, troopsToMove);
    GoToMainMenu();
  }

  private void RegisterMovement(string fromTerritory, string toTerritory, int troopsToMove)
  {
    try
    {
      CheckValidMovement(fromTerritory, toTerritory, troopsToMove);
    }
    catch (Exception ex)
    {
      manager.popupManager.ShowInfoPopup("Error while registering movement: " + ex.Message);
    }
  }

  private void CheckValidMovement(string fromTerritory, string toTerritory, int troopsToMove)
  {
    if (fromTerritory.Equals(toTerritory))
    {
      throw new Exception("Cannot move troops to the same territory");
    }
    int armiesOnTerritory = SqlUtils.ExecuteQuery(Queries.GET_TROOPS_ON_TERRITORY,
      reader => reader.GetInt32(0),
      new MySqlParameter[] {
        new("@matchID", matchID),
        new("@playerID", playerID),
        new("@turnNumber", turnNumber),
        new("@territory", fromTerritory)
    }).First();
    Debug.Log($"Armies on territory {fromTerritory}: {armiesOnTerritory}");
    if (troopsToMove >= armiesOnTerritory)
    {
      throw new Exception("Not enough troops on the selected territory");
    }
  }

  private void GoToMainMenu()
  {
    manager.popupManager.ShowInfoPopup("Turn registered successfully!");
    ChangeMenu(manager.mainMenu);
  }
}
