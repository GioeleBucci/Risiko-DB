using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    GetComponent<SpriteRenderer>().color = HexToColor("D42222");
  }

  public Color HexToColor(string hex)
  {
    hex = hex.TrimStart('#');
    int intValue = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
    float r = ((intValue & 0xFF0000) >> 16) / 255f;
    float g = ((intValue & 0x00FF00) >> 8) / 255f;
    float b = (intValue & 0x0000FF) / 255f;
    return new Color(r, g, b);
  }

}
