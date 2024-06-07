public class Queries
{
  // OP 2 
  public static string CREATE_MATCH = "INSERT INTO PARTITA (data)"
                                      + "VALUES(@data);";
  public static string CREATE_PLAYER = "INSERT INTO GIOCATORE (nickname, cognome, email, password)"
                                       + "VALUES(@nome, @cognome, @email, @password);";

}
