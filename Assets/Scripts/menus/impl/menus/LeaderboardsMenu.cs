using System;
using System.Collections.Generic;
using MySqlConnector;
using UnityEngine.UIElements;

public class LeaderboardsMenu : AbstractMenu
{
  private DropdownField leaderboardDropdown;
  private Button backButton;
  public LeaderboardsMenu(UIManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override VisualElement[] FetchUIElements()
  {
    leaderboardDropdown = root.Q<DropdownField>("LeaderboardDropdown");
    backButton = root.Q<Button>("BackButton");
    return new VisualElement[] { leaderboardDropdown, backButton };
  }

  protected override void SetUICallbacks()
  {
    LoadLeaderboard();
    backButton.clicked += OnBackButtonClicked;
  }

  private void LoadLeaderboard()
  {
    List<string> leaderboards = SqlUtils.ExecuteQuery(Queries.GET_LEADERBOARDS,
      reader => $"{reader.GetString("nome")} {reader.GetString("cognome")}: {reader.GetInt32("vittorie")} Victories");
    leaderboardDropdown.choices = leaderboards;
  }
}
