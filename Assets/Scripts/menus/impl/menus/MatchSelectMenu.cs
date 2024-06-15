using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

class MatchSelectMenu : AbstractMenu
{
  private DropdownField matchDropdown;
  private Button backButton;
  private Button okButton;
  public MatchSelectMenu(UIManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override VisualElement[] FetchUIElements()
  {
    matchDropdown = root.Q<DropdownField>("MatchDropdown");
    backButton = root.Q<Button>("BackButton");
    okButton = root.Q<Button>("OkButton");
    return new VisualElement[] { matchDropdown, backButton, okButton };
  }

  protected override void SetUICallbacks()
  {
    LoadMatches();
    backButton.clicked += () => ChangeMenu(manager.mainMenu);
    okButton.clicked += GoToMatch;
  }

  private void LoadMatches()
  {
    List<string> matches = SqlUtils.ExecuteQuery(Queries.GET_MATCHES_IDS, 
      r => r.GetInt32(0).ToString()
    );
    if (matches.Count == 0)
    {
      manager.popupManager.ShowInfoPopup("No matches found!");
      ChangeMenu(manager.mainMenu);
    }
    matchDropdown.choices = matches;
    matchDropdown.value = matches[0];
  }

  private void GoToMatch()
  {
    ChangeMenu(manager.viewGameMenu, int.Parse(matchDropdown.value));
  }
}
