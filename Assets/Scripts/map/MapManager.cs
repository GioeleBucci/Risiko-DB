using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
  private List<GameObject> territories;

  private void Awake()
  {
    getTerritories();
    Debug.Log($"(MapManager) Found {territories.Count} territories");
    setMapToInteractive(false);
    territories.ForEach(t => t.AddComponent<SelectableTerritory>());
  }

  public void setMapToInteractive(bool interactive)
  {
    territories.ForEach(t => t.GetComponent<PolygonCollider2D>().enabled = interactive);
  }

  public void deselectTerritories()
  {
    territories.ForEach(t => t.GetComponent<SelectableTerritory>().Deselect());
  }

  public List<(string, int)> GetTerritoriesAndArmies()
  {
    return territories
    .Where(t => t.GetComponent<SelectableTerritory>().isSelected)
    .Select(t => (t.name, t.GetComponent<SelectableTerritory>().troops)).ToList();
  }

  public void TestColoredTerritories()
  {
    territories.ForEach(t => t.GetComponent<SpriteRenderer>().color = Color.white);
  }

  private void getTerritories()
  {
    territories = GameObject.FindGameObjectsWithTag("Territory").ToList();
  }
}
