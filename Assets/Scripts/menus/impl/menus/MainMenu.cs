using UnityEngine.UIElements;

public class MainMenu : AbstractMenu
{
  private Button newGameButton;
  private Button newUserButton;
  private Button newTurnButton;
  private Button victoryButton;
  private Button viewGameButton;
  private Button leaderboardsButton;

  public MainMenu(UIManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override VisualElement[] FetchUIElements()
  {
    newGameButton = root.Q<Button>("NewGameButton");
    newUserButton = root.Q<Button>("NewUserButton");
    newTurnButton = root.Q<Button>("NewTurnButton");
    victoryButton = root.Q<Button>("VictoryButton");
    viewGameButton = root.Q<Button>("ViewGameButton");
    leaderboardsButton = root.Q<Button>("LeaderboardsButton");
    return new VisualElement[] { newGameButton, newUserButton, newTurnButton, victoryButton, viewGameButton, leaderboardsButton };
  }

  protected override void SetUICallbacks()
  {
    newGameButton.clicked += () => ChangeMenu(manager.newGameMenu);
    newUserButton.clicked += () => ChangeMenu(manager.newUserMenu);
    newTurnButton.clicked += () => ChangeMenu(manager.newTurnMenu);
    victoryButton.clicked += () => ChangeMenu(manager.victoryMenu);
    viewGameButton.clicked += () => ChangeMenu(manager.macthSelectMenu);
    leaderboardsButton.clicked += () => ChangeMenu(manager.leaderboardsMenu);
  }
}
