using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckTerrNames : MonoBehaviour
{
  private void Start()
  {
    List<string> dbNames = SqlUtils.ExecuteQuery("select nome from territorio", r => r.GetString("nome"));

    List<string> mapNames = new List<string>();
    check(transform, mapNames);

    foreach (string name in mapNames)
    {
      Debug.Log($"Checking {name}");
      if (!dbNames.Contains(name))
      {
        // Debug.LogError($"Territorio {name} non presente nel database");
      }
    }
  }

  private void check(Transform parentTransform, List<string> outputList)
  {
    foreach (Transform child in parentTransform)
    {
      outputList.Add(child.gameObject.name);
      check(child, outputList);
    }
  }
}
