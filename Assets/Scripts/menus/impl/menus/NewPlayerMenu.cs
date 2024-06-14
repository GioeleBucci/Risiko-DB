using System;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class NewPlayerMenu : AbstractMenu
{
  private int playersLeft; // Number of players left to add
  private int matchID;
  private List<((int, string), (int, string))> randomPool; // List of random objectives and armies
  private Button backButton;
  private Button okButton;
  private DropdownField dropdownField;
  private TextField nicknameField;
  public NewPlayerMenu(UIManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override void RecieveParameters(object[] args)
  {
    if (args.Length == 0)
    {
      throw new ArgumentException("Expected 1 argument (player count).");
    }
    playersLeft = (int)args[0];
    Debug.Log("Recieved player count: " + playersLeft);
    randomPool = GetRandomPool(playersLeft);
    // get ID of the latest match created
    matchID = SqlUtils.ExecuteQuery(Queries.GET_ID_OF_LAST_MATCH_CREATED, reader => reader.GetInt32("codPartita")).First();
  }

  protected override VisualElement[] FetchUIElements()
  {
    backButton = root.Q<Button>("BackButton");
    okButton = root.Q<Button>("OkButton");
    dropdownField = root.Q<DropdownField>("DropdownField");
    nicknameField = root.Q<TextField>("NicknameField");
    return new VisualElement[] { backButton, okButton, dropdownField };
  }

  protected override void SetUICallbacks()
  {
    SetDropdownLogic();
    backButton.clicked += OnBackButtonClicked;
    okButton.clicked += OnOkButtonClicked;
  }

  private void SetDropdownLogic()
  {
    // Fetch all users from the database and put name + surname in the dropdown field
    Dictionary<string, string> users = SqlUtils.ExecuteQuery(Queries.GET_USERS,
      reader => (reader.GetString("codiceFiscale"), reader["nome"] + " " + reader["cognome"]))
      .ToDictionary(k => k.Item1, v => v.Item2);
    dropdownField.choices = users.Select(x => $"{x.Value} ({x.Key})").ToList();
    dropdownField.value = dropdownField.choices[0]; // Set the first element as default value
  }

  /// stay in this menu and add players to the DB until playersLeft is 0
  private void OnOkButtonClicked()
  {
    if (playersLeft == 0)
    {
      ChangeMenu(manager.mainMenu);
      return;
    }
    string selectedLine = dropdownField.value;
    string userID = selectedLine.Substring(selectedLine.IndexOf("(") + 1, selectedLine.IndexOf(")") - selectedLine.IndexOf("(") - 1);
    // get random objective and army
    var armyAndObj = randomPool[playersLeft - 1];
    int armyID = armyAndObj.Item1.Item1;
    int objectiveID = armyAndObj.Item2.Item1;
    // Show a popup window with a message 
    manager.popupManager.ShowPlayerCreatedPopup(matchID, armyAndObj.Item1.Item2, armyAndObj.Item2.Item2);
    // Add player to the database
    try
    {
      SqlUtils.ExecuteNonQuery(Queries.CREATE_PLAYER, new MySqlParameter[] {
        new("@nickname", nicknameField.value),
        new("@matchID", matchID),
        new("@userID", userID),
        new("@objID", objectiveID),
        new("@armyID", armyID)
      });
      playersLeft--;
    }
    catch (Exception ex)
    {
      manager.popupManager.ShowErrorPopup("Error creating player: " + ex.Message);
    }
  }

  private List<((int, string), (int, string))> GetRandomPool(int poolSize)
  {
    List<(int, string)> objectives = SqlUtils.ExecuteQuery("SELECT * FROM obiettivo",
         reader => (reader.GetInt32("codObiettivo"), reader.GetString("descrizione")));
    List<(int, string)> armies = SqlUtils.ExecuteQuery("SELECT * FROM esercito",
             reader => (reader.GetInt32("codEsercito"), reader.GetString("colore")));
    var arm = armies.OrderBy(x => UnityEngine.Random.value).Take(poolSize).ToList();
    var obj = objectives.OrderBy(x => UnityEngine.Random.value).Take(poolSize).ToList();
    var res = new List<((int, string), (int, string))>();
    for (int i = 0; i < poolSize; i++)
    {
      res.Add((arm[i], obj[i]));
    }
    return res;
  }
}
