using UnityEngine;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;
using System;

public class DatabaseManager : MonoBehaviour
{
  public void Awake()
  {
    string connectionString = "server=localhost ; database=unitytest; user=root ; password= ; charset=utf8 ; SslMode=None;";
    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
      connection.Open();
      // Connection successful, you can now execute queries or perform other database operations
      Debug.Log("Connected to database.");

      string query = "SELECT * FROM obiettivo";
      List<string> results = new List<string>();
      MySqlCommand command = new MySqlCommand(query, connection);
      MySqlDataReader reader = command.ExecuteReader();
      while (reader.Read())
      {
        results.Add(reader.GetString(reader.GetOrdinal("descrizione")));
      }
      List<string> objectives = GetObjectivePool(results, 3);
      connection.Close();

      DateTime currentDate = DateTime.Now;
      connection = SqlUtils.NewConnection();
      connection.Open();
      MySqlCommand insertCommand = new MySqlCommand(Queries.CREATE_MATCH, connection);
      insertCommand.Parameters.AddWithValue("@data", currentDate.ToString("yyyy-MM-dd"));
      insertCommand.ExecuteNonQuery();

    }
    catch (MySqlException ex)
    {
      // Handle any errors that occurred during the connection
      Debug.LogError("Error connecting to database: " + ex.Message);
    }
    // StartCoroutine(InsertDataCoroutine("name", 0));

  }

  private List<string> GetObjectivePool(List<string> objectives, int poolSize)
  {
    return objectives.OrderBy(x => UnityEngine.Random.value).Take(poolSize).ToList();
  }
}
