using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySqlConnector;
using UnityEngine;
using UnityEngine.UIElements;

public class MapSelectMenu : AbstractMenu
{
  public MapSelectMenu(MenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }
  private Button okButton;
  private Button movementButton;
  private Button attackButton;
  private int playerID;
  private int matchID;
  private int turnNumber;
  private List<(string, int)> territoryArmyPairs;

  protected override void RecieveParameters(object[] args)
  {
    matchID = (int)args[0];
    playerID = (int)args[1];
    turnNumber = (int)args[2];
    Debug.Log($"(MapSelectorMenu) Recieved: MatchID: {matchID}, PlayerID: {playerID}, TurnNumber: {turnNumber}");
    Init();
  }

  private void Init()
  {
    manager.popupManager.ShowInfoPopup($"Registering turn {turnNumber}");
    manager.mapManager.setMapToInteractive(true);
    DisableMovementAndAttackButtons();
  }

  protected override VisualElement[] FetchUIElements()
  {
    okButton = root.Q<Button>("OkButton");
    movementButton = root.Q<Button>("MovementButton");
    attackButton = root.Q<Button>("AttackButton");
    return new VisualElement[] { okButton };
  }

  protected override void SetUICallbacks()
  {
    okButton.clicked += OnOkButtonClicked;
    movementButton.clicked += OnMovementButtonClicked;
    attackButton.clicked += OnAttackButtonClicked;
  }

  private void OnAttackButtonClicked()
  {
    manager.mapManager.setMapToInteractive(false);
    throw new NotImplementedException();
  }

  private void OnMovementButtonClicked()
  {
    manager.mapManager.setMapToInteractive(false);
    ChangeMenu(manager.movementMenu, matchID, playerID, turnNumber);
  }

  private void OnOkButtonClicked()
  {
    manager.mapManager.setMapToInteractive(false);
    territoryArmyPairs = manager.mapManager.GetTerritoriesAndArmies();
    manager.mapManager.deselectTerritories();
    try
    {
      SqlUtils.ExecuteTransaction(CreateTurnAndControlledTerritories);
      manager.popupManager.ShowInfoPopup("Territories registered successfully!");
    }
    catch (Exception ex)
    {
      manager.popupManager.ShowErrorPopup("An error occourred while registering territories: " + ex.Message);
      ChangeMenu(manager.mainMenu);
    }
    EnableMovementAndAttackButtons();
  }

  private void DisableMovementAndAttackButtons()
  {
    attackButton.SetEnabled(false);
    movementButton.SetEnabled(false);
  }

  private void EnableMovementAndAttackButtons()
  {
    attackButton.SetEnabled(true);
    movementButton.SetEnabled(true);
    okButton.text = "Finish";
    okButton.clicked -= OnOkButtonClicked;
    okButton.clicked += OnFinishButtonClicked;
  }

  private void OnFinishButtonClicked()
  {
    okButton.clicked -= OnFinishButtonClicked;
    manager.popupManager.ShowInfoPopup("Turn registered successfully!");
    ChangeMenu(manager.mainMenu);
  }

  // execute the queries together in a transaction to ensure integrity
  void CreateTurnAndControlledTerritories(MySqlConnection conn, MySqlTransaction trans)
  {
    MySqlCommand createTurn = new MySqlCommand(Queries.CREATE_TURN, conn, trans);
    createTurn.Parameters.AddRange(new MySqlParameter[] {
      new("@playerID", playerID),
      new("@matchID", matchID),
      new("@turnNumber", turnNumber)
    });
    createTurn.ExecuteNonQuery();
    foreach (var pair in territoryArmyPairs)
    {
      string territory = pair.Item1;
      int troops = pair.Item2;
      MySqlCommand createControls = new MySqlCommand(Queries.CREATE_TERRITORY_CONTROL, conn, trans);
      createControls.Parameters.AddRange(new MySqlParameter[] {
            new("@playerID", playerID),
            new("@matchID", matchID),
            new("@turnNumber", turnNumber),
            new("@territory", territory),
            new("@troops", troops)
      });
      createControls.ExecuteNonQuery();
    }
  }
}
