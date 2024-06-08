using System;
using System.Collections.Generic;
using MySqlConnector;

public static class SqlUtils
{
  const string connectionString = "server=localhost ; database=unitytest; user=root ; password= ; charset=utf8 ; SslMode=None;";
  public static MySqlConnection NewConnection()
  {
    return new MySqlConnection(connectionString);
  }

  public static List<T> ExecuteQuery<T>(string query, Func<MySqlDataReader, T> mapFunction)
  {
    List<T> resultList = new List<T>();
    MySqlConnection connection = NewConnection();
    connection.Open();
    MySqlCommand command = new MySqlCommand(query, connection);
    MySqlDataReader reader = command.ExecuteReader();
    while (reader.Read())
    {
      T result = mapFunction(reader);
      resultList.Add(result);
    }
    connection.Close();
    return resultList;
  }

  public static void ExecuteNonQuery(string query, params MySqlParameter[] parameters)
  {
    MySqlConnection connection = NewConnection();
    connection.Open();
    MySqlCommand command = new MySqlCommand(query, connection);
    command.Parameters.AddRange(parameters);
    command.ExecuteNonQuery();
    connection.Close();
  }
}
