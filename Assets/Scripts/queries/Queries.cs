public class Queries
{
  // OP 1 Create a new user
  public static string CREATE_USER = "INSERT INTO UTENTE (codiceFiscale, nome, cognome) "
                                    + "VALUES (@id, @name, @surname);";
  // OP 2 Create a new match
  public static string CREATE_MATCH = "INSERT INTO PARTITA (data)"
                                      + "VALUES(@date);";
  public static string GET_USERS = "SELECT * FROM utente;";

  public static string GET_ID_OF_LAST_MATCH_CREATED = "SELECT codPartita "
                                                    + "FROM PARTITA "
                                                    + "WHERE codPartita = (SELECT MAX(codPartita) FROM PARTITA);";
  public static string CREATE_PLAYER = "INSERT INTO GIOCATORE (nickname, codPartita, codUtente, codObiettivo, codEsercito) "
                                      + "VALUES (@nickname, @matchID, @userID, @objID, @armyID);";
  // OP 3 Get the number of troops each player has at the beginning of the match
  public static string GET_INITIAL_TROOPS = "SELECT numArmate FROM ARMATE_INIZIALI where numGiocatori = @playerCount;";
  // OP 4 Register a new turn in a certain match
  public static string GET_MATCHES_IDS = "SELECT codPartita FROM PARTITA;";

  public static string GET_PLAYERS_IN_MATCH = "SELECT * FROM GIOCATORE where codPartita = @matchID;";

  public static string GET_PLAYER_LATEST_TURN = "SELECT MAX(numeroTurno) " +
                                                "FROM turno " +
                                                "WHERE codGiocatore = @playerID;";
  // Actual turn creation
  public static string CREATE_TURN = "insert into turno(codGiocatore, codPartita, numeroTurno) " +
                                      "VALUES (@playerID, @matchID, @turnNumber);";

  public static string CREATE_TERRITORY_CONTROL = "insert into controllo_territorio (codGiocatore, codPartita, numeroTurno, territorio, numArmate) "
                                                + "VALUES (@playerID, @matchID, @turnNumber, @territory, @troops);";
}
