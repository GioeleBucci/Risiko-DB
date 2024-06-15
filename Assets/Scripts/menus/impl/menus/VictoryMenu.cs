using System.Collections.Generic;
using System.Linq;
using MySqlConnector;
using UnityEngine;
using UnityEngine.UIElements;

public class VictoryMenu : AbstractMenu
{
  public VictoryMenu(UIManager manager, VisualTreeAsset menu) : base(manager, menu) { }
  private DropdownField matchSelector;
  private DropdownField playerSelector;
  private Dictionary<string, int> nicksAndIDs; // player nicknames and IDs
  private Button backButton;
  private Button okButton;

  protected override VisualElement[] FetchUIElements()
  {
    matchSelector = root.Q<DropdownField>("DropdownMatch");
    playerSelector = root.Q<DropdownField>("DropdownPlayer");
    backButton = root.Q<Button>("BackButton");
    okButton = root.Q<Button>("OkButton");
    return new VisualElement[] { matchSelector, playerSelector, backButton, okButton };
  }

  protected override void SetUICallbacks()
  {
    SetDropdownLogic();
    backButton.clicked += OnBackButtonClicked;
    okButton.clicked += TryRegisterVictory;
  }

  private void SetDropdownLogic()
  {
    List<int> matches = SqlUtils.ExecuteQuery(Queries.GET_ONGOING_MATCHES_IDS, reader => reader.GetInt32("codPartita"));
    if (matches.Count == 0)
    {
      manager.popupManager.ShowInfoPopup("No ongoing matches found.");
      okButton.SetEnabled(false);
      return;
    }
    matchSelector.choices = matches.ConvertAll(x => x.ToString());
    matchSelector.RegisterValueChangedCallback((evt) =>
    {
      Debug.Log("Selected new match: " + evt.newValue);
      nicksAndIDs = SqlUtils.ExecuteQuery(Queries.GET_PLAYERS_IN_MATCH,
        reader => (reader.GetString("nickname"), reader.GetInt32("codGiocatore")),
      new MySqlParameter[] { new MySqlParameter("matchID", evt.newValue) })
        .ToDictionary(k => k.Item1, v => v.Item2);
      playerSelector.choices = nicksAndIDs.Keys.ToList();
      playerSelector.value = nicksAndIDs.Count > 0 ? playerSelector.choices[0] : "";
    });
    matchSelector.value = matches.Count > 0 ? matchSelector.choices[0] : "";
  }

  private void TryRegisterVictory()
  {
    try
    {
      int playerID = nicksAndIDs[playerSelector.value];
      Debug.Log($"Attempting to register {playerID} victory for match {matchSelector.value}.");
      SqlUtils.ExecuteNonQuery(Queries.CREATE_VICTORY,
        new MySqlParameter[] {
        new("@matchID", matchSelector.value),
        new("@playerID", playerID)
        }
      );
      manager.popupManager.ShowInfoPopup("Victory registered successfully!");
      ChangeMenu(manager.mainMenu);
    }
    catch (System.Exception ex)
    {
      manager.popupManager.ShowErrorPopup("Error while registering victory: " + ex.Message);
    }
  }
}
