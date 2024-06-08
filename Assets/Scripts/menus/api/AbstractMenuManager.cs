using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]

public class AbstractMenuManager : MonoBehaviour
{
  public AbstractMenu oldMenu { get; set; }
  public UIDocument document { get; private set; }

  private void Awake()
  {
    document = GetComponent<UIDocument>();
  }

  public void ChangeMenu(AbstractMenu newMenu)
  {
    document.visualTreeAsset = newMenu.menu;
    newMenu.Init();
  }
}
