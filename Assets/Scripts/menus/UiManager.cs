using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]

public class UIManager : MonoBehaviour
{
  [SerializeField] private VisualTreeAsset mainMenuTree;
  [SerializeField] private VisualTreeAsset newUserMenuTree;
  [SerializeField] private VisualTreeAsset newGameMenuTree;
  [SerializeField] private VisualTreeAsset newPlayerMenuTree;
  [SerializeField] private VisualTreeAsset newTurnMenuTree;
  [SerializeField] private VisualTreeAsset mapSelectMenuTree;
  [SerializeField] private VisualTreeAsset attackMenuTree;
  [SerializeField] private VisualTreeAsset movementMenuTree;
  [SerializeField] private VisualTreeAsset victoryMenuTree;
  [SerializeField] private VisualTreeAsset matchSelectMenuTree;
  [SerializeField] private VisualTreeAsset viewGameMenuTree;
  [SerializeField] private VisualTreeAsset leaderboardsMenuTree;
  public PopupManager popupManager { get; private set; }
  public MapManager mapManager { get; private set; }
  public AbstractMenu oldMenu { get; set; }
  public UIDocument document { get; private set; }
  public AbstractMenu mainMenu { get; private set; }
  public AbstractMenu newUserMenu { get; private set; }
  public AbstractMenu newGameMenu { get; private set; }
  public AbstractMenu newPlayerMenu { get; private set; }
  public AbstractMenu newTurnMenu { get; private set; }
  public AbstractMenu mapSelectMenu { get; private set; }
  public AbstractMenu movementMenu { get; private set; }
  public AbstractMenu attackMenu { get; private set; }
  public AbstractMenu victoryMenu { get; private set; }
  public AbstractMenu macthSelectMenu { get; private set; }
  public AbstractMenu viewGameMenu { get; private set; }
  public AbstractMenu leaderboardsMenu { get; private set; }

  protected void Awake()
  {
    document = GetComponent<UIDocument>();
    popupManager = FindObjectOfType<PopupManager>();
    mapManager = FindObjectOfType<MapManager>();
    mainMenu = new MainMenu(this, mainMenuTree);
    newUserMenu = new NewUserMenu(this, newUserMenuTree);
    newGameMenu = new NewGameMenu(this, newGameMenuTree);
    newPlayerMenu = new NewPlayerMenu(this, newPlayerMenuTree);
    newTurnMenu = new NewTurnMenu(this, newTurnMenuTree);
    mapSelectMenu = new MapSelectMenu(this, mapSelectMenuTree);
    movementMenu = new MovementMenu(this, movementMenuTree);
    attackMenu = new AttackMenu(this, attackMenuTree);
    victoryMenu = new VictoryMenu(this, victoryMenuTree);
    macthSelectMenu = new MatchSelectMenu(this, matchSelectMenuTree);
    viewGameMenu = new ViewGameMenu(this, viewGameMenuTree);
    leaderboardsMenu = new LeaderboardsMenu(this, leaderboardsMenuTree);
    ChangeMenu(mainMenu);
  }

  public void ChangeMenu(AbstractMenu newMenu, params object[] args)
  {
    document.visualTreeAsset = newMenu.visualTree;
    newMenu.Init(args);
  }
}
