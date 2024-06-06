using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using MySqlConnector;

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

      string query = "SELECT * FROM testTable";
      MySqlCommand command = new MySqlCommand(query, connection);
      MySqlDataReader reader = command.ExecuteReader();
      while (reader.Read())
      {
        for (int i = 0; i < reader.FieldCount; i++)
        {
          Debug.Log("Column " + i + ": " + reader[i]);
        }
      }

      // Don't forget to close the connection when you're done
      connection.Close();
    }
    catch (MySqlException ex)
    {
      // Handle any errors that occurred during the connection
      Debug.LogError("Error connecting to database: " + ex.Message);
    }
    // StartCoroutine(InsertDataCoroutine("name", 0));

  }
}
