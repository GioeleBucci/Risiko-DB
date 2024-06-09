using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]

public class PopupManager : MonoBehaviour
{
  [SerializeField] private VisualTreeAsset playerCreatedPopup;
  private UIDocument document;

  private void Awake()
  {
    document = GetComponent<UIDocument>();
    document.visualTreeAsset = null; 
  }

  public void ShowPlayerCreatedPopup(string army, string objective)
  {
    document.visualTreeAsset = playerCreatedPopup;
  }
}
