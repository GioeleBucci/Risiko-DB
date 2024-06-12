using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableTerritory : MonoBehaviour
{
  private SpriteRenderer spriteRenderer;
  public bool isSelected { get; private set; }
  private Color originalColor;
  private Color selectedColor;

  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    originalColor = spriteRenderer.color;
    selectedColor = originalColor + new Color(0.3f, 0.3f, 0.3f, 0);
  }

  void OnMouseDown()
  {
    isSelected = !isSelected;
    spriteRenderer.color = isSelected ? selectedColor : originalColor;
  }
}
