using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableTerritory : MonoBehaviour
{
  public bool isSelected { get; private set; }
  private int _troops;
  public int troops
  {
    get { return _troops; }
    private set
    {
      if (value < 0) throw new ArgumentException("Troops cannot be negative");
      _troops = value;
      troopsLabel.text = value.ToString();
    }
  }
  private SpriteRenderer spriteRenderer;
  private Color originalColor;
  private Color selectedColor;
  private GameObject labelContainer;
  private TextMesh troopsLabel;

  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    originalColor = spriteRenderer.color;
    selectedColor = originalColor + new Color(0.3f, 0.3f, 0.3f, 0);
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(1))
    {
      Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
      if (hit.collider != null && hit.collider.gameObject == gameObject)
      {
        HandleRightClick();
      }
    }
  }

  void HandleRightClick()
  {
    if (isSelected) { troops++; }
  }

  void OnMouseDown()
  {
    if (EventSystem.current.IsPointerOverGameObject()) return;

    isSelected = !isSelected;
    if (isSelected)
    {
      Select();
    }
    else Deselect();
  }

  public void Deselect()
  {
    isSelected = false;
    spriteRenderer.color = originalColor;
    labelContainer?.SetActive(false);
  }

  private void Select()
  {
    spriteRenderer.color = selectedColor;
    if (labelContainer == null) CreateLabel();
    troops = 1;
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
    troopsLabel.anchor = TextAnchor.MiddleCenter;
    troopsLabel.alignment = TextAlignment.Center;
    troopsLabel.characterSize = 0.1f;
    troopsLabel.fontSize = 20;
    troopsLabel.color = Color.black;
    troopsLabel.fontStyle = FontStyle.Bold;
    Collider2D collider = GetComponent<Collider2D>();
    Bounds bounds = collider.bounds;
    Vector3 colliderCenter = bounds.center;
    labelContainer.transform.position = colliderCenter;
    labelContainer.transform.position = colliderCenter + new Vector3(0, 0, -1);
  }
}
