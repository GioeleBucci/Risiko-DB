public class Queries
{
  // OP 2 
  public static string CREATE_MATCH = "INSERT INTO PARTITA (data)"
                                      + "VALUES(@date);";
  public static string CREATE_PLAYER = "INSERT INTO GIOCATORE (nickname, cognome, email, password)"
                                       + "VALUES(@nome, @cognome, @email, @password);";
  public static string GET_USERS = "SELECT codiceFiscale, nome, cognome FROM utente;";
}
