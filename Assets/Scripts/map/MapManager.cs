using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
  List<GameObject> territories;

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

  // public List<string>

  private void getTerritories()
  {
    territories = GameObject.FindGameObjectsWithTag("Territory").ToList();
  }
}
