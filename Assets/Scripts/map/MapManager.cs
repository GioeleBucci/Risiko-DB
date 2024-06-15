using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
  private List<SelectableTerritory> territories = new List<SelectableTerritory>();

  private void Awake()
  {
    Init();
    Debug.Log($"(MapManager) Found {territories.Count} territories");
    setMapToInteractive(false);
  }

  public void setMapToInteractive(bool interactive)
  {
    territories.ForEach(t => t.enabled = interactive);
  }

  public void deselectTerritories()
  {
    territories.ForEach(t => t.Deselect());
  }

  public List<(string, int)> GetTerritoriesAndArmies()
  {
    return territories
    .Where(t => t.isSelected)
    .Select(t => (t.name, t.troops)).ToList();
  }

  public void TestColoredTerritories()
  {
    territories.ForEach(t => t.GetComponent<SpriteRenderer>().color = Color.white);
  }

  private void Init()
  {
    GameObject.FindGameObjectsWithTag("Territory").ToList().ForEach(t =>
    {
      var terr = t.AddComponent<SelectableTerritory>();
      territories.Add(terr);
    });
  }
}
