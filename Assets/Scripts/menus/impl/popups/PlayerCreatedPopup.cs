using UnityEngine.UIElements;

public class PlayerCreatedPopup : AbstractMenu
{
  private Button okButton;
  public PlayerCreatedPopup(UIManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override VisualElement[] FetchUIElements()
  {
    okButton = root.Q<Button>("OkButton");
    return new VisualElement[] { okButton };
  }

  protected override void SetUICallbacks()
  {
    throw new System.NotImplementedException();
  }
}
