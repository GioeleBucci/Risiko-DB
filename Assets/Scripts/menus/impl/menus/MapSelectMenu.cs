using System;
using System.Collections.Generic;
using MySqlConnector;
using UnityEngine;
using UnityEngine.UIElements;

public class MapSelectMenu : AbstractMenu
{
  public MapSelectMenu(MenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }
  private Button okButton;
  private int playerID;
  private int matchID;
  private int turnNumber;

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
  }

  protected override VisualElement[] FetchUIElements()
  {
    okButton = root.Q<Button>("OkButton");
    return new VisualElement[] { okButton };
  }

  protected override void SetUICallbacks()
  {
    okButton.clicked += OnOkButtonClicked;
  }

  private void OnOkButtonClicked()
  {
    manager.mapManager.setMapToInteractive(false);
    manager.mapManager.deselectTerritories();
    List<(string, int)> territoryArmyPairs = manager.mapManager.GetTerritoriesAndArmies();
    AddTerritoryControlToDB(territoryArmyPairs);
    manager.ChangeMenu(manager.mainMenu);
  }

  private void AddTerritoryControlToDB(List<(string, int)> territoryArmyPairs)
  {
    try
    {
      foreach (var pair in territoryArmyPairs)
      {
        string territory = pair.Item1;
        int troops = pair.Item2;
        Debug.Log($"Adding Territory: {territory}, Armies: {troops}");
        SqlUtils.ExecuteNonQuery(Queries.CREATE_TERRITORY_CONTROL,
          new MySqlParameter[] {
            new("@playerID", playerID),
            new("@matchID", matchID),
            new("@turnNumber", turnNumber),
            new("@territory", territory),
            new("@troops", troops)
          });
      }
      manager.popupManager.ShowInfoPopup("Succesfully added to DB!"); // TODO we probably need a success popup
    }
    catch (Exception ex)
    {
      manager.popupManager.ShowErrorPopup(ex.Message);
    }
  }
}
