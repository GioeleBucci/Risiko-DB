using UnityEngine.UIElements;

public class MapSelectMenu : AbstractMenu
{
  public MapSelectMenu(MenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  private int playerID;
  private int matchID;
  private int turnNumber;

  protected override void RecieveParameters(object[] args)
  {
    playerID = (int)args[0];
    matchID = (int)args[1];
    turnNumber = (int)args[2];
  }

  protected override VisualElement[] FetchUIElements()
  {
    throw new System.NotImplementedException();
  }

  protected override void SetUICallbacks()
  {
    throw new System.NotImplementedException();
  }
}
