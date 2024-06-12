using System;
using System.Collections.Generic;
using MySqlConnector;
using UnityEngine;

public static class SqlUtils
{
  const string connectionString = "server=localhost ; database=unitytest; user=root ; password= ; charset=utf8 ; SslMode=None;";
  public static MySqlConnection NewConnection()
  {
    return new MySqlConnection(connectionString);
  }

  /// <summary>
  /// Executes a SQL query and returns a list of results.
  /// </summary>
  /// <typeparam name="T">The type of the result objects.</typeparam>
  /// <param name="query">The SQL query to execute.</param>
  /// <param name="mapFunction">A function that maps the data from the SQL reader to the result objects.</param>
  /// <param name="parameters">Optional parameters to be used in the SQL query.</param>
  /// <returns>A list of results obtained from executing the SQL query.</returns>
  public static List<T> ExecuteQuery<T>(string query, Func<MySqlDataReader, T> mapFunction, params MySqlParameter[] parameters)
  {
    List<T> resultList = new List<T>();
    MySqlConnection connection = NewConnection();
    connection.Open();
    MySqlCommand command = new MySqlCommand(query, connection);
    command.Parameters.AddRange(parameters);
    MySqlDataReader reader = command.ExecuteReader();
    while (reader.Read())
    {
      T result = mapFunction(reader);
      resultList.Add(result);
    }
    connection.Close();
    return resultList;
  }

  /// <summary>
  /// Executes a SQL query that does not return any data.
  /// </summary>
  /// <param name="query">The SQL query to execute.</param>
  /// <param name="parameters">Optional parameters to be used in the query.</param>
  public static void ExecuteNonQuery(string query, params MySqlParameter[] parameters)
  {
    MySqlConnection connection = NewConnection();
    connection.Open();
    MySqlCommand command = new MySqlCommand(query, connection);
    command.Parameters.AddRange(parameters);
    command.ExecuteNonQuery();
    connection.Close();
  }

  /// <summary>
  /// Executes a transaction using the provided transaction function.
  /// </summary>
  /// <param name="transactionFunction">The function that defines the transaction logic.</param>
  public static void ExecuteTransaction(Action<MySqlConnection, MySqlTransaction> transactionFunction)
  {
    MySqlConnection connection = NewConnection();
    connection.Open();
    MySqlTransaction transaction = connection.BeginTransaction();
    try
    {
      transactionFunction(connection, transaction);
      transaction.Commit();
    }
    catch (Exception ex)
    {
      transaction.Rollback();
      connection.Close();
      throw ex;
    }
    connection.Close();
  }
}

