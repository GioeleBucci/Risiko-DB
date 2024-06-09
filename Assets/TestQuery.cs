using UnityEngine;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data;
using UnityEditor;

public class DatabaseManager : MonoBehaviour
{
  public void Awake()
  {
    try
    {
      List<int> objectives = SqlUtils.ExecuteQuery("SELECT * FROM obiettivo", reader => reader.GetInt32(0));
      List<int> armies = SqlUtils.ExecuteQuery("SELECT * FROM esercito", reader => reader.GetInt32(0));
      List<(int, int)> pool = GetObjectivePool(objectives, armies, 3); // TODO change pool size
      foreach (var item in pool)
      {
        Debug.Log($"Objective: {item.Item1}, Army: {item.Item2}");
      }
      // now that we have a random pool of objectives and armies we can create the players
      return;
      DateTime currentDate = DateTime.Now;
      MySqlConnection connection = SqlUtils.NewConnection();
      connection.Open();
      MySqlCommand insertCommand = new MySqlCommand(Queries.CREATE_MATCH, connection);
      insertCommand.Parameters.Add(currentDate.ToString("yyyy-MM-dd"));
      insertCommand.ExecuteNonQuery();
    }
    catch (MySqlException ex)
    {
      // Handle any errors that occurred during the connection
      Debug.LogError("Error connecting to database: " + ex.Message);
    }
    // StartCoroutine(InsertDataCoroutine("name", 0));

  }

  private List<(int, int)> GetObjectivePool(List<int> objectives, List<int> armies, int poolSize)
  {
    var obj = objectives.OrderBy(x => UnityEngine.Random.value).Take(poolSize).ToList();
    var arm = armies.OrderBy(x => UnityEngine.Random.value).Take(poolSize).ToList();
    var res = new List<(int, int)>();
    for (int i = 0; i < poolSize; i++)
    {
      res.Add((obj[i], arm[i]));
    }
    return res;
  }
}
