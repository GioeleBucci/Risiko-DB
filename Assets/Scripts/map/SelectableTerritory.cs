using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableTerritory : MonoBehaviour
{
  private SpriteRenderer spriteRenderer;
  public bool isSelected { get; private set; }
  private Color originalColor;
  private Color selectedColor;
  private GameObject labelContainer;
  private TextMesh troopsLabel; // amount of troops on territory

  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    originalColor = spriteRenderer.color;
    selectedColor = originalColor + new Color(0.3f, 0.3f, 0.3f, 0);
  }

  void OnMouseDown()
  {
    if (EventSystem.current.IsPointerOverGameObject()) return;

    isSelected = !isSelected;
    spriteRenderer.color = isSelected ? selectedColor : originalColor;
    if (isSelected) ShowLabel();
    else HideLabel();
  }

  private void HideLabel()
  {
    labelContainer.SetActive(false);
  }

  private void ShowLabel()
  {
    if (labelContainer == null) CreateLabel();
    labelContainer.SetActive(true);
  }

  private void CreateLabel()
  {
    Vector3 center = transform.position;
    Vector3 labelPosition = center + new Vector3(0, 0, -1);
    labelContainer = new GameObject("Label");
    labelContainer.transform.position = labelPosition;
    labelContainer.transform.SetParent(transform);
    troopsLabel = labelContainer.AddComponent<TextMesh>();
    troopsLabel.text = "0";
    troopsLabel.anchor = TextAnchor.MiddleCenter;
    troopsLabel.alignment = TextAlignment.Center;
    troopsLabel.characterSize = 0.1f;
    troopsLabel.fontSize = 20;
    troopsLabel.color = Color.black;
    Collider2D collider = GetComponent<Collider2D>();
    Bounds bounds = collider.bounds;
    Vector3 colliderCenter = bounds.center;
    labelContainer.transform.position = colliderCenter;
    labelContainer.transform.position = colliderCenter + new Vector3(0, 0, -1);
  }
}
