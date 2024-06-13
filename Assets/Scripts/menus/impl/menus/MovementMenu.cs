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
  private SliderInt troopsSlider;
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
    troopsSlider = root.Q<SliderInt>("Troops");
    registerButton = root.Q<Button>("RegisterButton");
    skipButton = root.Q<Button>("SkipButton");
    return new VisualElement[] { from, to, troopsSlider, registerButton, skipButton };
  }

  protected override void SetUICallbacks()
  {
    SetDropdownLogic();
    registerButton.clicked += RegisterMovement;
    skipButton.clicked += GoToMainMenu;
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
    from.choices = territories;
    from.value = from.choices[0];
    to.choices = territories;
    to.value = to.choices[0];
  }

  private void RegisterMovement()
  {
    Debug.Log($"Attempting to register movement from {from.value} to {to.value} with {troopsSlider.value} troops");
    try
    {
      CheckValidMovement(from.value, to.value, troopsSlider.value);
      SqlUtils.ExecuteTransaction(CreateMovement);
      GoToMainMenu();
    }
    catch (Exception ex)
    {
      manager.popupManager.ShowErrorPopup("Error while registering movement: " + ex.Message);
      return;
    }
  }

  private void CreateMovement(MySqlConnection conn, MySqlTransaction trans)
  {
    MySqlCommand createMovement = new(Queries.CREATE_MOVEMENT, conn, trans);
    createMovement.Parameters.AddRange(new MySqlParameter[] {
      new("@from", from.value),
      new("@to", to.value),
      new("@troops", troopsSlider.value)
    });
    createMovement.ExecuteNonQuery();
    MySqlCommand addMovementToTurn = new(Queries.ADD_MOVEMENT_TO_TURN, conn, trans);
    addMovementToTurn.Parameters.AddRange(new MySqlParameter[] {
      new("@matchID", matchID),
      new("@playerID", playerID),
      new("@turnNumber", turnNumber)
    });
    addMovementToTurn.ExecuteNonQuery();
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
    // TODO check if the territories are adjacent !!
  }

  private void GoToMainMenu()
  {
    manager.popupManager.ShowInfoPopup("Turn registered successfully!");
    ChangeMenu(manager.mainMenu);
  }
}
