using System;
using System.Collections.Generic;
using MySqlConnector;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementMenu : AbstractMenu
{
  private DropdownField from;
  private DropdownField to;
  private SliderInt troops;
  private Button backButton;
  private Button okButton;
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
    backButton = root.Q<Button>("BackButton");
    okButton = root.Q<Button>("OkButton");
    return new VisualElement[] { from, to, troops, backButton, okButton };
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
    backButton.clicked += OnBackButtonClicked;
    okButton.clicked += RegisterMovement;
  }

  private void RegisterMovement()
  {
    throw new NotImplementedException();
  }
}
