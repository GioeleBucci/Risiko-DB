using System.Collections.Generic;
using MySqlConnector;
using UnityEngine;
using UnityEngine.UIElements;
public class NewTurnMenu : AbstractMenu
{
  private DropdownField matchSelector;
  private DropdownField playerSelector;
  private Button backButton;
  public NewTurnMenu(MenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override VisualElement[] FetchUIElements()
  {
    matchSelector = root.Q<DropdownField>("DropdownMatch");
    playerSelector = root.Q<DropdownField>("DropdownPlayer");
    backButton = root.Q<Button>("BackButton");
    List<int> matches = SqlUtils.ExecuteQuery(Queries.GET_MATCHES_IDS, reader => reader.GetInt32("codPartita"));
    matchSelector.choices = matches.ConvertAll(x => x.ToString());
    matchSelector.RegisterValueChangedCallback((evt) =>
    {
      Debug.Log("Selected new match: " + evt.newValue);
      List<string> nicknames = SqlUtils.ExecuteQuery(Queries.GET_PLAYERS_NICKS_IN_MATCH,
        reader => reader.GetString("nickname"),
        new MySqlParameter[] { new MySqlParameter("matchID", evt.newValue) });
      Debug.Log("Fetched nicknames: " + string.Join(", ", nicknames));
      playerSelector.choices = nicknames;
      playerSelector.value = playerSelector.choices[0];
    });
    matchSelector.value = matchSelector.choices[0];
    return new VisualElement[] { matchSelector, playerSelector };
  }

  protected override void SetUICallbacks()
  {
    backButton.clicked += OnBackButtonClicked;
  }
}
