using System;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector;
using UnityEngine;
using UnityEngine.UIElements;

class ViewGameMenu : AbstractMenu
{
  private Button nextTurnButton;
  private Button backButton;
  private Label playersLabel;
  private Label turnLabel;
  private int matchID;
  private int turnNumber;
  private int highestTurnNumber;
  Dictionary<string, (Color, int)> terrColorAndTroops; // territory name, color and troops
  public ViewGameMenu(UIManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override void RecieveParameters(object[] args)
  {
    if (args.Length == 0) throw new ArgumentException("Expected 1 argument (match ID).");
    matchID = (int)args[0];
  }

  protected override VisualElement[] FetchUIElements()
  {
    nextTurnButton = root.Q<Button>("NextTurnButton");
    backButton = root.Q<Button>("BackButton");
    playersLabel = root.Q<Label>("PlayersLabel");
    turnLabel = root.Q<Label>("TurnLabel");
    return new VisualElement[] { nextTurnButton, backButton, playersLabel, turnLabel };
  }

  protected override void SetUICallbacks()
  {
    SetTurnLogic();
    ShowTurnMap();
    backButton.clicked += ResetMapAndGoBack;
    nextTurnButton.clicked += ShowNextTurn;
  }

  private void SetTurnLogic()
  {
    turnNumber = 1;
    highestTurnNumber = SqlUtils.ExecuteQuery(Queries.GET_MATCH_HIGHEST_TURN_NUMBER,
      reader => reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
      new MySqlParameter[] { new("@matchID", matchID) }).First();
  }

  private void ShowNextTurn()
  {
    turnNumber++;
    ShowTurnMap();
  }

  private void ShowTurnMap()
  {
    UpdateTurnLeaderboard();
    manager.mapManager.ResetMap();
    turnLabel.text = $"Turn {turnNumber}";
    terrColorAndTroops = SqlUtils.ExecuteQuery(Queries.GET_TERRITORIES_AND_COLORS,
    r =>
      (r.GetString("territorio"), (r.GetString("colore"), r.GetInt32("numArmate"))),
    new MySqlParameter[] {
      new("@matchID", matchID),
      new("@turnNumber", turnNumber),
    }).ToDictionary(k => k.Item1, v => (ArmyColors.GetArmyColor(v.Item2.Item1), v.Item2.Item2));
    manager.mapManager.ShowTurnMap(terrColorAndTroops);
    if (turnNumber >= highestTurnNumber)
    {
      nextTurnButton.SetEnabled(false);
    }
  }

  private void UpdateTurnLeaderboard()
  {
    List<(int, string, string)> turnLeaderboard =
      SqlUtils.ExecuteQuery(Queries.GET_TURN_LEADERBOARD,
        r => (r.GetInt32("territoriControllati"), r.GetString("nickname"), r.GetString("colore")),
        new MySqlParameter[] { new("@matchID", matchID), new("@turnNumber", turnNumber) });
    playersLabel.text = "";
    foreach (var entry in turnLeaderboard)
    {
      playersLabel.text += $"\n<color={ArmyColors.GetRTColorTag(entry.Item3)}>{entry.Item2} {entry.Item1}</color>";
    }
  }

  private void ResetMapAndGoBack()
  {
    manager.mapManager.ResetMap();
    OnBackButtonClicked();
  }
}
