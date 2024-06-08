using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class NewGameMenu : AbstractMenu
{
  private Button backButton;
  private Button okButton;
  private SliderInt playerCountSlider;
  private TextField dateTextField;
  public NewGameMenu(AbstractMenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override VisualElement[] FetchUIElements()
  {
    backButton = root.Q<Button>("BackButton");
    okButton = root.Q<Button>("OkButton");
    playerCountSlider = root.Q<SliderInt>("PlayerCount");
    dateTextField = root.Q<TextField>("DateField");
    return new VisualElement[] { backButton, okButton, playerCountSlider, dateTextField };
  }

  protected override void SetUICallbacks()
  {
    backButton.clicked += OnBackButtonClicked;
    //TODO ADD REST
  }
}
