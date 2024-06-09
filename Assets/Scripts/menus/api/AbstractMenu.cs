using System.Linq;
using UnityEngine.UIElements;

public abstract class AbstractMenu
{
  public VisualTreeAsset visualTree { get; }

  protected VisualElement root { get => manager.document.rootVisualElement; }

  protected MenuManager manager { get; }

  /// <summary>
  /// Initializes a new instance of the <see cref="AbstractMenu"/> class.
  /// </summary>
  /// <param name="manager">The menu manager.</param>
  /// <param name="menu">The VisualTreeAsset representing the menu.</param>
  public AbstractMenu(MenuManager manager, VisualTreeAsset menu)
  {
    this.manager = manager;
    this.visualTree = menu;
  }

  /// <summary>
  /// An implementation of menu may need to recieve some data from the previous menu.
  /// </summary>
  protected virtual void RecieveParameters(object[] args) { }

  /// <summary>
  /// Fetches the UI elements.
  /// </summary>
  /// <returns>An array of VisualElements that will be validated</returns>
  protected abstract VisualElement[] FetchUIElements();

  protected abstract void SetUICallbacks();

  public void Init(object[] args)
  {
    if (!Validate(FetchUIElements()))
    {
      throw new System.ArgumentException("Validation failed. One or more parameters are null.");
    }
    RecieveParameters(args);
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
  protected bool Validate(VisualElement[] args) => args.All(arg => arg != null);
}