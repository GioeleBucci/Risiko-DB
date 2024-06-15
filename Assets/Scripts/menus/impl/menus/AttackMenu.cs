using System;
using System.Collections.Generic;
using MySqlConnector;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackMenu : AbstractMenu
{
  private DropdownField attacker;
  private DropdownField defender;
  private UnsignedIntegerField atkDeployed;
  private UnsignedIntegerField atkLost;
  private UnsignedIntegerField defDeployed;
  private UnsignedIntegerField defLost;
  private Toggle victory;
  private Button registerButton;
  private Button skipButton;
  private int matchID;
  private int playerID;
  private int turnNumber;
  public AttackMenu(UIManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override void RecieveParameters(object[] args)
  {
    matchID = (int)args[0];
    playerID = (int)args[1];
    turnNumber = (int)args[2];
    Debug.Log($"(AttackMenu) Recieved: MatchID: {matchID}, PlayerID: {playerID}, TurnNumber: {turnNumber}");
  }

  protected override VisualElement[] FetchUIElements()
  {
    attacker = root.Q<DropdownField>("AttackerTerr");
    defender = root.Q<DropdownField>("DefenderTerr");
    atkDeployed = root.Q<UnsignedIntegerField>("AtkDeployed");
    atkLost = root.Q<UnsignedIntegerField>("AtkLost");
    defDeployed = root.Q<UnsignedIntegerField>("DefDeployed");
    defLost = root.Q<UnsignedIntegerField>("DefLost");
    victory = root.Q<Toggle>("VictoryToggle");
    registerButton = root.Q<Button>("RegisterButton");
    skipButton = root.Q<Button>("SkipButton");
    return new VisualElement[] { attacker, defender, atkDeployed, atkLost, defDeployed,
                                  defLost, victory, registerButton, skipButton };
  }

  protected override void SetUICallbacks()
  {
    SetDropdownLogic();
    skipButton.clicked += GoToMovementMenu;
    registerButton.clicked += TryRegisterAttack;
  }

  private void SetDropdownLogic()
  {
    List<string> attackerTerritories = SqlUtils.ExecuteQuery(Queries.GET_CONTROLLED_TERRITORIES,
      reader => reader.GetString(0),
      new MySqlParameter[] {
        new("@matchID", matchID),
        new("@playerID", playerID),
        new("@turnNumber", turnNumber)
      });
    attacker.choices = attackerTerritories;
    attacker.RegisterValueChangedCallback((evt) =>
    {
      List<string> defenderTerritories = SqlUtils.ExecuteQuery(Queries.GET_ENEMY_NEIGHBOUR_TERRITORIES,
        reader => reader.GetString(0),
        new MySqlParameter[] {
          new("@territory", evt.newValue),
          new("@matchID", matchID),
          new("@playerID", playerID),
          new("@turnNumber", turnNumber - 1)
        });
      defender.choices = defenderTerritories;
      defender.value = defenderTerritories.Count > 0 ? defender.choices[0] : "";
    });
    attacker.value = attacker.choices[0];
  }

  private void TryRegisterAttack()
  {
    Debug.Log($"Attempting to register attack: {attacker.value}->{defender.value}\n(ATK: {atkDeployed.value}D: {atkLost.value}L) "
     + $"(DEF: {defDeployed.value}D: {defLost.value}L) VICTORY: {victory.value}");
    try
    {
      CheckValidAttack();
      SqlUtils.ExecuteTransaction(CreateAttack);
      GoToMovementMenu();
    }
    catch (Exception ex)
    {
      manager.popupManager.ShowErrorPopup("Error while registering attack: " + ex.Message);
      return;
    }
  }

  private void CheckValidAttack()
  {
    if (atkLost.value > atkDeployed.value || defLost.value > defDeployed.value)
      throw new Exception("Can't lose more troops than deployed");
  }

  private void CreateAttack(MySqlConnection conn, MySqlTransaction trans)
  {
    MySqlCommand createAttack = new(Queries.CREATE_ATTACK, conn, trans);
    createAttack.Parameters.AddRange(new MySqlParameter[] {
      new("@attacker", attacker.value),
      new("@defender", defender.value),
      new("@atkDeployed", atkDeployed.value),
      new("@atkLost", atkLost.value),
      new("@defDeployed", defDeployed.value),
      new("@defLost", defLost.value),
      new("@victory", victory.value),
    });
    createAttack.ExecuteNonQuery();
    MySqlCommand addAttackToTurn = new(Queries.ADD_ATTACK_TO_TURN, conn, trans);
    addAttackToTurn.Parameters.AddRange(new MySqlParameter[] {
      new("@matchID", matchID),
      new("@playerID", playerID),
      new("@turnNumber", turnNumber)
    });
    addAttackToTurn.ExecuteNonQuery();
  }

  private void GoToMovementMenu()
  {
    ChangeMenu(manager.movementMenu, matchID, playerID, turnNumber);
  }
}
