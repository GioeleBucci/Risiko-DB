using System.Collections.Generic;
using System.Linq;
using MySqlConnector;
using UnityEngine;
using UnityEngine.UIElements;
public class NewTurnMenu : AbstractMenu
{
  private DropdownField matchSelector;
  private DropdownField playerSelector;
  private Button backButton;
  private Button okButton;
  private int playerID;
  public NewTurnMenu(MenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override VisualElement[] FetchUIElements()
  {
    matchSelector = root.Q<DropdownField>("DropdownMatch");
    playerSelector = root.Q<DropdownField>("DropdownPlayer");
    backButton = root.Q<Button>("BackButton");
    okButton = root.Q<Button>("OkButton");
    return new VisualElement[] { matchSelector, playerSelector, okButton };
  }

  protected override void SetUICallbacks()
  {
    SetDropdownLogic();
    backButton.clicked += OnBackButtonClicked;
    okButton.clicked += GoToMapSelector;
  }

  private void SetDropdownLogic()
  {
    List<int> matches = SqlUtils.ExecuteQuery(Queries.GET_MATCHES_IDS, reader => reader.GetInt32("codPartita"));
    matchSelector.choices = matches.ConvertAll(x => x.ToString());
    matchSelector.RegisterValueChangedCallback((evt) =>
    {
      Debug.Log("Selected new match: " + evt.newValue);
      List<string> nicknames = SqlUtils.ExecuteQuery(Queries.GET_PLAYERS_IN_MATCH,
        reader =>
        {
          string nickname = reader.GetString("nickname");
          playerID = reader.GetInt32("codGiocatore");
          return nickname;
        },
        new MySqlParameter[] { new MySqlParameter("matchID", evt.newValue) });
      playerSelector.choices = nicknames;
      playerSelector.value = playerSelector.choices[0];
    });
    matchSelector.value = matchSelector.choices[0];
  }

  private void GoToMapSelector()
  {
    // get the player latest turn number, if it's null then it's the first turn
    int turnNumber = SqlUtils.ExecuteQuery(Queries.GET_PLAYER_LATEST_TURN,
      reader => reader.IsDBNull(0) ? 1 : reader.GetInt32(0) + 1,
      new MySqlParameter[] { new("@playerID", playerID) }).First();
    int matchID = int.Parse(matchSelector.value);
    try
    {
      SqlUtils.ExecuteNonQuery(Queries.CREATE_TURN, new MySqlParameter[] {
      new("@playerID", playerID),
      new("@matchID", matchID),
      new("@turnNumber", turnNumber)
    });
    }
    catch (System.Exception ex)
    {
      manager.popupManager.ShowErrorPopup(ex.Message);
    }
    manager.ChangeMenu(manager.mapSelectMenu, matchID, playerID, turnNumber);
  }
}
