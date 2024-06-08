using System;
using MySqlConnector;
using UnityEngine;
using UnityEngine.UIElements;

public class NewGameMenu : AbstractMenu
{
  private Button backButton;
  private Button okButton;
  private SliderInt playerCountSlider;
  private TextField dateTextField;
  public NewGameMenu(MenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }

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
    okButton.clicked += createMatch;
  }

  private void createMatch()
  {
    int playerCount = playerCountSlider.value;
    string date = dateTextField.value;
    DateTime parsedDate;
    if (DateTime.TryParse(date, out parsedDate))
    {
      string arg = parsedDate.ToString("yyyy-MM-dd");
      Debug.Log("Creating match with date: " + arg);
      MySqlParameter dateParam = new MySqlParameter("@date", MySqlDbType.Date) { Value = parsedDate };
      SqlUtils.ExecuteNonQuery(Queries.CREATE_MATCH, dateParam);
      manager.ChangeMenu(manager.newPlayerMenu); // TODO we need to pass the player count to the next menu
    }
    else
    {
      dateTextField.value = "Invalid date";
    }
  }
}
