public class Queries
{
  // OP 1
  public static string CREATE_USER = "INSERT INTO UTENTE (codiceFiscale, nome, cognome) "
                                    + "VALUES (@id, @name, @surname);";
  // OP 2 
  public static string CREATE_MATCH = "INSERT INTO PARTITA (data)"
                                      + "VALUES(@date);";
  public static string GET_USERS = "SELECT codiceFiscale, nome, cognome FROM utente;";

  public static string GET_ID_OF_LAST_MATCH_CREATED = "SELECT codPartita "
                                                    + "FROM PARTITA "
                                                    + "WHERE codPartita = (SELECT MAX(codPartita) FROM PARTITA);";
  public static string CREATE_PLAYER = "INSERT INTO GIOCATORE (nickname, codPartita, codUtente, codObiettivo, codEsercito) "
                                      + "VALUES (@nickname, @matchID, @userID, @objID, @armyID);";
  // OP 3
  public static string GET_INITIAL_TROOPS = "SELECT numArmate FROM ARMATE_INIZIALI where numGiocatori = @playerCount;";
}
