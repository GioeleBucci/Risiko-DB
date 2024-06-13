using System;
using System.Linq;
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
      MySqlParameter dateParam = new MySqlParameter("@date", parsedDate);
      SqlUtils.ExecuteNonQuery(Queries.CREATE_MATCH, dateParam);
      // Tell how many troops each player has (OP 3)
      int troopCount = SqlUtils.ExecuteQuery(Queries.GET_INITIAL_TROOPS,
        reader => reader.GetInt32("numArmate"),
        new MySqlParameter[] { new("@playerCount", playerCount) }).First();
      manager.popupManager.ShowInfoPopup($"Match created successfully!\nEach player will starts with {troopCount} troops.");
      ChangeMenu(manager.newPlayerMenu, playerCount); 
    }
    else
    {
      dateTextField.value = "Invalid date";
    }
  }
}
