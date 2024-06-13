using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackMenu : AbstractMenu
{
  private Button registerButton;
  private Button skipButton;
  private int matchID;
  private int playerID;
  private int turnNumber;
  public AttackMenu(MenuManager manager, VisualTreeAsset menu) : base(manager, menu) { }

  protected override void RecieveParameters(object[] args)
  {
    matchID = (int)args[0];
    playerID = (int)args[1];
    turnNumber = (int)args[2];
    Debug.Log($"(AttackMenu) Recieved: MatchID: {matchID}, PlayerID: {playerID}, TurnNumber: {turnNumber}");
  }

  protected override VisualElement[] FetchUIElements()
  {
    registerButton = root.Q<Button>("RegisterButton");
    skipButton = root.Q<Button>("SkipButton");
    return new VisualElement[] { registerButton, skipButton };
  }

  protected override void SetUICallbacks()
  {
    skipButton.clicked += GoToMovementMenu;
  }

  private void GoToMovementMenu()
  {
    ChangeMenu(manager.movementMenu, matchID, playerID, turnNumber);
  }
}
