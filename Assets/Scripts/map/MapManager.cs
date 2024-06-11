using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
  List<GameObject> territories;

  private void Awake()
  {
    getTerritories();
    setMapToInteractive(false);
  }

  public void setMapToInteractive(bool interactive)
  {
    territories.ForEach(t => t.GetComponent<PolygonCollider2D>().enabled = interactive);
  }

  private void getTerritories()
  {
    territories = GameObject.FindGameObjectsWithTag("Territory").ToList();
  }
}
