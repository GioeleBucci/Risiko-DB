using UnityEngine.UIElements;

public class MainMenu : AbstractMenu
{
  private Button newGameButton;
  private Button newUserButton;

  public MainMenu(MenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override VisualElement[] FetchUIElements()
  {
    newGameButton = root.Q<Button>("NewGameButton");
    newUserButton = root.Q<Button>("NewUserButton");
    return new VisualElement[] { newGameButton, newUserButton };
  }

  protected override void SetUICallbacks()
  {
    newGameButton.clicked += () => ChangeMenu(manager.newGameMenu);
    newUserButton.clicked += () => ChangeMenu(manager.newUserMenu);
  }
}
