using System.Linq;
using UnityEngine.UIElements;

public abstract class AbstractMenu
{
  public VisualTreeAsset menu { get; }

  protected VisualElement root { get => manager.document.rootVisualElement; }

  private AbstractMenuManager manager;

  /// <summary>
  /// Initializes a new instance of the <see cref="AbstractMenu"/> class.
  /// </summary>
  /// <param name="manager">The menu manager.</param>
  /// <param name="menu">The VisualTreeAsset representing the menu.</param>
  public AbstractMenu(AbstractMenuManager manager, VisualTreeAsset menu)
  {
    this.manager = manager;
    this.menu = menu;
  }

  /// <summary>
  /// Fetches the UI elements.
  /// </summary>
  /// <returns>An array of VisualElements that will be validated</returns>
  protected abstract VisualElement[] FetchUIElements();

  protected abstract void SetUICallbacks();

  public void Init()
  {
    if (!Validate(FetchUIElements()))
    {
      throw new System.ArgumentException("Validation failed. One or more parameters are null.");
    }
    SetUICallbacks();
  }

  /// <summary>
  /// Changes the current menu to a new menu.
  /// </summary>
  /// <param name="newMenu">The new menu to be displayed.</param>
  protected void ChangeMenu(AbstractMenu newMenu)
  {
    manager.oldMenu = this;
    manager.ChangeMenu(newMenu);
  }

  /// <summary>
  /// Handles the back button click event.
  /// </summary>
  protected void OnBackButtonClicked()
  {
    ChangeMenu(manager.oldMenu);
  }

  /// <summary>
  /// Validates the specified UI elements.
  /// </summary>
  /// <param name="args">UI elements to be validated.</param>
  /// <returns><c>true</c> if all the UI elements are not null</returns>
  protected bool Validate(params VisualElement[] args) => args.All(arg => arg != null);
}
