using System;
using MySqlConnector;
using UnityEngine.UIElements;

public class NewUserMenu : AbstractMenu
{
  private Button backButton;
  private Button okButton;
  private TextField idField;
  private TextField nameField;
  private TextField surnameField;
  public NewUserMenu(UIManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override VisualElement[] FetchUIElements()
  {
    backButton = root.Q<Button>("BackButton");
    okButton = root.Q<Button>("OkButton");
    idField = root.Q<TextField>("IDField");
    nameField = root.Q<TextField>("NameField");
    surnameField = root.Q<TextField>("SurnameField");
    return new VisualElement[] { backButton, okButton, idField, nameField, surnameField };
  }

  protected override void SetUICallbacks()
  {
    backButton.clicked += OnBackButtonClicked;
    okButton.clicked += createUser;
  }

  private void createUser()
  {
    string id = idField.value;
    string name = nameField.value;
    string surname = surnameField.value;
    if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
    {
      manager.popupManager.ShowErrorPopup("Please fill all the required fields.");
      return;
    }
    try
    {
      SqlUtils.ExecuteNonQuery(Queries.CREATE_USER, new MySqlParameter[]{
        new("@id", id),
        new("@name", name),
        new("@surname", surname)
      });
    }
    catch (Exception ex)
    {
      manager.popupManager.ShowErrorPopup("Error creating user: " + ex.Message);
    }
  }
}
