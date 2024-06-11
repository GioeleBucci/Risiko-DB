using UnityEngine;
using UnityEngine.UIElements;

public class MapSelectMenu : AbstractMenu
{
  public MapSelectMenu(MenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }

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
    return new VisualElement[] { };
  }

  protected override void SetUICallbacks()
  {
  }
}
