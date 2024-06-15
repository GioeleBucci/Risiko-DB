using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MySqlConnector;
using UnityEngine;
using UnityEngine.UIElements;

public class MapSelectMenu : AbstractMenu
{
  public MapSelectMenu(UIManager manager, VisualTreeAsset menu) : base(manager, menu) { }
  private Button selectButton;
  private Button backButton;
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
    ShowTroopsToPlacePopup();
    manager.mapManager.setMapToInteractive(true);
  }

  private void ShowTroopsToPlacePopup()
  {
    string startTurnMessage = $"Registering turn {turnNumber}.";
    // on the first turn, the player's troops are based off the total number of players
    if (turnNumber == 1)
    {
      int players = SqlUtils.ExecuteQuery(Queries.GET_PLAYERS_IN_MATCH,
        reader => reader.GetInt32(0),
        new MySqlParameter[] { new("@matchID", matchID) }).Count;
      int troops = SqlUtils.ExecuteQuery(Queries.GET_INITIAL_TROOPS,
        reader => reader.GetInt32("numArmate"),
        new MySqlParameter[] { new("@playerCount", players) }).First();
      manager.popupManager.ShowInfoPopup($"{startTurnMessage}" +
                                        $"\nYou have {troops} troops to place.");
      return;
    }
    // on subsequent turns, the player's troops are based off the territories they control
    int territoryTroops = SqlUtils.ExecuteQuery(Queries.GET_TERRITORIES_BONUS,
      reader => reader.GetInt32(0),
      new MySqlParameter[] {
        new("@playerID", playerID),
        new("@matchID", matchID),
        new("@turnNumber", turnNumber - 1)
      }).First();
    int continentTroops = SqlUtils.ExecuteQuery(Queries.GET_CONTINENTS_BONUS,
      reader => reader.GetInt32(0),
      new MySqlParameter[] {
        new("@playerID", playerID),
        new("@matchID", matchID),
        new("@turnNumber", turnNumber - 1)
      }).First();
    manager.popupManager.ShowInfoPopup($"{startTurnMessage}" +
                                  $"\nYou have {territoryTroops + continentTroops} troops to place." +
                                  $"\n({territoryTroops} from territories and {continentTroops} from continents)");
  }

  protected override VisualElement[] FetchUIElements()
  {
    selectButton = root.Q<Button>("SelectButton");
    backButton = root.Q<Button>("BackButton");
    return new VisualElement[] { selectButton, backButton };
  }

  protected override void SetUICallbacks()
  {
    selectButton.clicked += TrySelectTerritories;
    backButton.clicked += GoBackToMainMenu;
  }

  private void GoBackToMainMenu()
  {
    DisableInteractiveMap();
    ChangeMenu(manager.mainMenu);
  }

  private void TrySelectTerritories()
  {
    territoryArmyPairs = manager.mapManager.GetTerritoriesAndArmies();
    if (territoryArmyPairs.Count == 0)
    {
      manager.popupManager.ShowErrorPopup("You must select at least one territory!");
      return;
    }
    DisableInteractiveMap();
    try
    {
      SqlUtils.ExecuteTransaction(CreateTurnAndControlledTerritories);
      manager.popupManager.ShowInfoPopup("Territories registered successfully!");
      ChangeMenu(manager.attackMenu, matchID, playerID, turnNumber);
    }
    catch (Exception ex)
    {
      manager.popupManager.ShowErrorPopup("An error occourred while registering territories: " + ex.Message);
      ChangeMenu(manager.mainMenu);
    }
  }

  private void DisableInteractiveMap()
  {
    manager.mapManager.setMapToInteractive(false);
    manager.mapManager.deselectTerritories();
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
