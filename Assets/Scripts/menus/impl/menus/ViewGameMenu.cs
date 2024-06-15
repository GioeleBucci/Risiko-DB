using UnityEngine.UIElements;

class ViewGameMenu : AbstractMenu
{
  Button backButton;
  public ViewGameMenu(UIManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override VisualElement[] FetchUIElements()
  {
    backButton = root.Q<Button>("BackButton");
    return new VisualElement[] { backButton };
  }

  protected override void SetUICallbacks()
  {
    backButton.clicked += () => ChangeMenu(manager.mainMenu);
    manager.mapManager.TestColoredTerritories();
  }
}
