using UnityEngine.UIElements;

public class MainMenu : AbstractMenu
{
  private Button newGameButton;

  public MainMenu(MenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override VisualElement[] FetchUIElements()
  {
    newGameButton = root.Q<Button>("NewGameButton");
    return new VisualElement[] { newGameButton };
  }

  protected override void SetUICallbacks()
  {
    newGameButton.clicked += OnNewGameButtonClicked;
  }

  private void OnNewGameButtonClicked()
  {
    ChangeMenu(manager.newGameMenu);
  }
}
