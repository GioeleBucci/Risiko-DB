using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class NewTurnMenu : AbstractMenu
{
  private DropdownField dropdownMatch;
  private DropdownField dropdownPlayer;
  public NewTurnMenu(MenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override VisualElement[] FetchUIElements()
  {
    dropdownMatch = root.Q<DropdownField>("DropdownMatch");
    dropdownPlayer = root.Q<DropdownField>("DropdownPlayer");
    List<int> matches = SqlUtils.ExecuteQuery(Queries.GET_MATCHES_IDS, reader => reader.GetInt32("codPartita"));
    dropdownMatch.choices = matches.ConvertAll(x => x.ToString());
    dropdownMatch.value = dropdownMatch.choices[0];
    return new VisualElement[] { dropdownMatch, dropdownPlayer };
  }

  protected override void SetUICallbacks()
  {
    throw new System.NotImplementedException();
  }
}
