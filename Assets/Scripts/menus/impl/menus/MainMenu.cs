using UnityEngine.UIElements;

public class MainMenu : AbstractMenu
{
  private Button newGameButton;
  private Button newUserButton;
  private Button newTurnButton;

  public MainMenu(UIManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override VisualElement[] FetchUIElements()
  {
    newGameButton = root.Q<Button>("NewGameButton");
    newUserButton = root.Q<Button>("NewUserButton");
    newTurnButton = root.Q<Button>("NewTurnButton");
    return new VisualElement[] { newGameButton, newUserButton, newTurnButton };
  }

  protected override void SetUICallbacks()
  {
    newGameButton.clicked += () => ChangeMenu(manager.newGameMenu);
    newUserButton.clicked += () => ChangeMenu(manager.newUserMenu);
    newTurnButton.clicked += () => ChangeMenu(manager.newTurnMenu);
  }
}
