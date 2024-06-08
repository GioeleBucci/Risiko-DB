using System.Collections.Generic;
using UnityEngine.UIElements;

public class NewPlayerMenu : AbstractMenu
{
  private Button backButton;
  private Button okButton;
  private DropdownField dropdownField;
  private TextField nicknameField;
  public NewPlayerMenu(MenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }
  protected override VisualElement[] FetchUIElements()
  {
    backButton = root.Q<Button>("BackButton");
    okButton = root.Q<Button>("OkButton");
    dropdownField = root.Q<DropdownField>("DropdownField");
    nicknameField = root.Q<TextField>("NicknameField");
    // Fetch all users from the database and put name + surname in the dropdown field
    List<(string, string)> users = SqlUtils.ExecuteQuery(Queries.GET_USERS, reader => (reader.GetString("codiceFiscale"), reader["nome"] + " " + reader["cognome"]));
    dropdownField.choices = users.ConvertAll(x => x.Item2);
    return new VisualElement[] { backButton, okButton, dropdownField };
  }

  protected override void SetUICallbacks()
  {
    throw new System.NotImplementedException("TODO");
  }
}
