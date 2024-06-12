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
  /// Returns a MySqlCommand object with the given query and parameters
  /// </summary>
  public static MySqlCommand GetCommand(string query, params MySqlParameter[] parameters)
  {
    MySqlConnection connection = NewConnection();
    connection.Open();
    MySqlCommand command = new MySqlCommand(query, connection);
    command.Parameters.AddRange(parameters);
    return command;
  }

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

