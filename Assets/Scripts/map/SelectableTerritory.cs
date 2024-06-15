using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]

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
  private Color _color;
  public Color Color
  {
    get { return _color; }
    set
    {
      _color = value;
      spriteRenderer.color = value;
    }
  }
  public PolygonCollider2D Collider { get; private set; }
  private Color originalColor;
  private Color selectedColor;
  private SpriteRenderer spriteRenderer;
  private GameObject labelContainer;
  private TextMesh troopsLabel;

  private void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    Collider = GetComponent<PolygonCollider2D>();
    originalColor = spriteRenderer.color;
    selectedColor = originalColor + new Color(0.3f, 0.3f, 0.3f, 0);
  }

  private void Update()
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

  private void HandleRightClick()
  {
    if (isSelected) { troops++; }
  }

  private void OnMouseDown()
  {
    if (EventSystem.current.IsPointerOverGameObject()) return;
    isSelected = !isSelected;
    if (isSelected)
    {
      Select();
    }
    else Reset();
  }

  public void Reset()
  {
    isSelected = false;
    spriteRenderer.color = originalColor;
    if (labelContainer != null)
    {
      labelContainer.SetActive(false);
      troopsLabel.color = Color.black;
    }
  }

  private void Select()
  {
    spriteRenderer.color = selectedColor;
    SetTroopsLabel(1);
  }

  public void SetTroopsLabel(int troops)
  {
    if (labelContainer == null) CreateLabel();
    this.troops = troops;
    labelContainer.SetActive(true);
  }

  public void SetTroopsLabelColor(Color color)
  {
    if (troopsLabel != null) troopsLabel.color = color;
  }

  private void CreateLabel()
  {
    bool colliderState = Collider.enabled; // the collider must be enabled to center the label
    Collider.enabled = true;
    Vector3 center = transform.position;
    Vector3 labelPosition = center + new Vector3(0, 0, -1);
    labelContainer = new GameObject("Label");
    labelContainer.transform.position = labelPosition;
    labelContainer.transform.SetParent(transform);
    troopsLabel = labelContainer.AddComponent<TextMesh>();
    troopsLabel.anchor = TextAnchor.MiddleCenter;
    troopsLabel.alignment = TextAlignment.Center;
    troopsLabel.characterSize = 0.1f;
    troopsLabel.fontSize = 22;
    troopsLabel.color = Color.black;
    troopsLabel.fontStyle = FontStyle.Bold;
    Collider2D collider = GetComponent<Collider2D>();
    Bounds bounds = collider.bounds;
    Vector3 colliderCenter = bounds.center;
    labelContainer.transform.position = colliderCenter;
    labelContainer.transform.position = colliderCenter + new Vector3(0, 0, -1);
    Collider.enabled = colliderState;
  }
}
