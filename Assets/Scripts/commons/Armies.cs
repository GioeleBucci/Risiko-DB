using UnityEngine;

public static class Armies
{
  public static Color PURPLE = new Color(0.5f, 0, 0.5f);
  public static Color BLUE = Color.blue;
  public static Color RED = Color.red;
  public static Color YELLOW = Color.yellow;
  public static Color BLACK = Color.black;
  public static Color GREEN = Color.green;

  public static Color GetArmyColor(string armyColor)
  {
    switch (armyColor.ToLower())
    {
      case "viola":
        return PURPLE;
      case "blu":
        return BLUE;
      case "rosse":
        return RED;
      case "gialle":
        return YELLOW;
      case "nere":
        return BLACK;
      case "verdi":
        return GREEN;
      default:
        throw new System.ArgumentException("Invalid color string");
    }
  }
}
