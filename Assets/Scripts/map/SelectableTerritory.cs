using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableTerritory : MonoBehaviour
{
  private SpriteRenderer spriteRenderer;

  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void OnMouseDown()
  {
    Debug.Log("Selected: " + gameObject.name);
    spriteRenderer.color = Color.black;
  }

  bool IsClickingOnSprite()
  {
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    return spriteRenderer.bounds.Contains(mousePosition);
  }

  private void SelectObject()
  {
    // Implement your selection logic here
    Debug.Log("Selected: " + gameObject.name);

    // Example: change color to indicate selection
    GetComponent<SpriteRenderer>().color = Color.white;
  }
}
