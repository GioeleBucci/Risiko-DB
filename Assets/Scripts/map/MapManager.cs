using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
  private List<SelectableTerritory> territories = new List<SelectableTerritory>();

  private void Start()
  {
    Init();
    Debug.Log($"(MapManager) Found {territories.Count} territories");
    setMapToInteractive(false);
  }

  public void setMapToInteractive(bool interactive)
  {
    territories.ForEach(t => t.Collider.enabled = interactive);
  }

  public void deselectTerritories()
  {
    territories.ForEach(t => t.Reset());
  }

  public List<(string, int)> GetTerritoriesAndArmies()
  {
    return territories
    .Where(t => t.isSelected)
    .Select(t => (t.name, t.troops)).ToList();
  }

  public void ShowTurnMap(Dictionary<string, (Color, int)> territoryColors)
  {
    territories.ForEach(t => t.Color = Color.white);
    foreach (var entry in territoryColors)
    {
      string territory = entry.Key;
      Color armyColor = entry.Value.Item1;
      SelectableTerritory selectedTerritory = territories.First(t => t.name == territory);
      selectedTerritory.Color = armyColor;
      selectedTerritory.SetTroopsLabel(entry.Value.Item2);
      selectedTerritory.SetTroopsLabelColor(Color.white);
    }
  }

  public void ResetMap()
  {
    territories.ForEach(t => t.Reset());
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
