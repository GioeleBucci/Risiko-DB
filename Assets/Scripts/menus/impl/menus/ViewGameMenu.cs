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
    turnNumber = 1;
    SetPlayersLabel();
    ShowTurnMap();
    backButton.clicked += ResetMapAndGoBack;
    nextTurnButton.clicked += ShowNextTurn;
  }

  private void ShowNextTurn()
  {
    turnNumber++;
    ShowTurnMap();
  }

  private void ShowTurnMap()
  {
    manager.mapManager.ResetMap();
    turnLabel.text = $"Turn {turnNumber}";
    terrColorAndTroops = SqlUtils.ExecuteQuery(Queries.GET_TERRITORIES_AND_COLORS,
    r =>
      (r.GetString("territorio"), (r.GetString("colore"), r.GetInt32("numArmate"))),
    new MySqlParameter[] {
      new("@matchID", matchID),
      new("@turnNumber", turnNumber), // todo change this to the actual turn number 
    }).ToDictionary(k => k.Item1, v => (ArmyColors.GetArmyColor(v.Item2.Item1), v.Item2.Item2));
    manager.mapManager.ShowTurnMap(terrColorAndTroops);
  }

  private void SetPlayersLabel()
  {
    List<(string, string)> nicksAndColors =
      SqlUtils.ExecuteQuery(Queries.GET_NICKNAMES_AND_COLORS,
        r => (r.GetString("nickname"), r.GetString("colore")),
        new MySqlParameter[] { new("@matchID", matchID) });
    playersLabel.text = "";
    foreach (var pair in nicksAndColors)
    {
      playersLabel.text += $"\n<color={ArmyColors.GetRTColorTag(pair.Item2)}>{pair.Item1}</color>";
    }
  }

  private void ResetMapAndGoBack()
  {
    manager.mapManager.ResetMap();
    OnBackButtonClicked();
  }
}
