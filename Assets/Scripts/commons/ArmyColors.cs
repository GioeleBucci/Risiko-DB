using UnityEngine;

public static class ArmyColors
{
  private static Color PURPLE = new Color(.9f, 0, .9f);
  private static Color BLUE = Color.blue;
  private static Color RED = Color.red;
  private static Color YELLOW = Color.yellow;
  private static Color BLACK = Color.black;
  private static Color GREEN = Color.green;

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

  public static string GetRTColorTag(string armyColor)
  {
    switch (armyColor.ToLower())
    {
      case "viola":
        return "purple";
      case "blu":
        return "blue";
      case "rosse":
        return "red";
      case "gialle":
        return "yellow";
      case "nere":
        return "black";
      case "verdi":
        return "green";
      default:
        throw new System.ArgumentException("Invalid color string");
    }
  }
}
