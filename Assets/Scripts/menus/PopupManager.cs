using UnityEngine;
using UnityEngine.UIElements;

public class PopupManager : MonoBehaviour
{
  [SerializeField] private VisualTreeAsset playerCreatedPopup;
  [SerializeField] private VisualTreeAsset errorPopup;
  private UIDocument document;
  private VisualElement root { get => document.rootVisualElement; }

  private void Awake()
  {
    document = GetComponent<UIDocument>();
    document.visualTreeAsset = null;
    // MenuManager menuManager = GetComponentInParent<MenuManager>();
  }

  public void ShowPlayerCreatedPopup(string army, string objective)
  {
    document.visualTreeAsset = playerCreatedPopup;
    Label armyLabel = root.Q<Label>("ArmyLabel");
    armyLabel.text = $"ARMY: {army}";
    Label objectiveLabel = root.Q<Label>("ObjectiveLabel");
    objectiveLabel.text = $"OBJECTIVE: {objective}";
    Button okButton = root.Q<Button>("OkButton");
    okButton.clicked += () => document.visualTreeAsset = null;
  }

  public void ShowErrorPopup(string message)
  {
    document.visualTreeAsset = errorPopup;
    Label errorLabel = root.Q<Label>("ErrorLabel");
    errorLabel.text = message;
    Button okButton = root.Q<Button>("OkButton");
    okButton.clicked += () => document.visualTreeAsset = null;
  }
}