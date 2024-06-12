using System;
using System.Collections.Generic;
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
    // TODO register selected territories in the DB
  }

  private void OnOkButtonClicked()
  {
    manager.mapManager.setMapToInteractive(false);
    List<(string, int)> territoryArmyPairs = manager.mapManager.GetTerritoriesAndArmies();
    foreach (var pair in territoryArmyPairs)
    {
      Debug.Log($"Territory: {pair.Item1}, Armies: {pair.Item2}");
    }
  }
}
