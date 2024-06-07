using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]

public class MainMenuEvents : MonoBehaviour
{
  [SerializeField] private VisualTreeAsset newGameMenu;
  private VisualTreeAsset previousMenu;
  private UIDocument document;
  private Button newGameButton;

  private void Awake()
  {
    document = GetComponent<UIDocument>();
    newGameButton = document.rootVisualElement.Q<Button>("NewGameButton"); // todo this must be put in a method that gets called when the scene switches
    newGameButton.clicked += OnNewGameButtonClicked;
  }

  public void ChangeMenu(VisualTreeAsset newMenu)
  {
    previousMenu = document.visualTreeAsset;
    document.rootVisualElement.Clear();
    document.rootVisualElement.Add(newMenu.CloneTree());
    var backButton = document.rootVisualElement.Q<Button>("BackButton");
    if (backButton != null)
    {
      backButton.clicked += OnBackButtonClicked;
    }
  }

  public void OnBackButtonClicked()
  {
    ChangeMenu(previousMenu);
  }

  private void OnNewGameButtonClicked()
  {
    Debug.Log("click!");
    ChangeMenu(newGameMenu);
  }


}
