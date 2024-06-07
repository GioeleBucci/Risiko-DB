using MySqlConnector;

public static class SqlUtils
{
  const string connectionString = "server=localhost ; database=unitytest; user=root ; password= ; charset=utf8 ; SslMode=None;";
  public static MySqlConnection NewConnection()
  {
    return new MySqlConnection(connectionString);
  }
}
