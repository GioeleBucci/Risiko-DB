public class Queries
{
  // OP 1 Create a new user
  public static string CREATE_USER =
  @"INSERT INTO UTENTE (codiceFiscale, nome, cognome)
    VALUES (@id, @name, @surname);";

  // OP 2 Create a new match
  public static string CREATE_MATCH =
  @"INSERT INTO PARTITA (data)
    VALUES(@date);";
  public static string GET_USERS =
  @"SELECT * FROM utente;";

  public static string GET_ID_OF_LAST_MATCH_CREATED =
  @"SELECT codPartita 
    FROM PARTITA 
    WHERE codPartita = (SELECT MAX(codPartita) FROM PARTITA);";
  // TODO change @matchID for a LAST_INSERT_ID()
  public static string CREATE_PLAYER =
  @"INSERT INTO GIOCATORE (nickname, codPartita, codUtente, codObiettivo, codEsercito) 
    VALUES (@nickname, @matchID, @userID, @objID, @armyID);";

  // OP 3 Get the number of troops each player has at the beginning of the match
  public static string GET_INITIAL_TROOPS =
  @"SELECT numArmate FROM ARMATE_INIZIALI where numGiocatori = @playerCount;";

  // OP 4 Register a new turn in a certain match
  // Get only the matches that are still ongoing
  public static string GET_ONGOING_MATCHES_IDS =
  @"SELECT P.codPartita
    FROM PARTITA P
    WHERE P.codPartita NOT IN 
    (
      SELECT V.codPartita
      FROM VINCITORE V
    );";

  public static string GET_PLAYERS_IN_MATCH =
  @"SELECT * FROM GIOCATORE where codPartita = @matchID;";

  public static string GET_PLAYER_LATEST_TURN =
  @"SELECT MAX(numeroTurno) 
    FROM TURNO 
    WHERE codGiocatore = @playerID;";
  // Actual turn creation
  public static string CREATE_TURN =
  @"INSERT INTO TURNO(codGiocatore, codPartita, numeroTurno) 
    VALUES (@playerID, @matchID, @turnNumber);";

  public static string CREATE_TERRITORY_CONTROL =
  @"INSERT INTO CONTROLLO_TERRITORIO (codGiocatore, codPartita, numeroTurno, territorio, numArmate) 
    VALUES (@playerID, @matchID, @turnNumber, @territory, @troops);";

  // OP 5 Get the player that's controlling a territory at the n-th turn
  public static string GET_TERRITORY_CONTROL =
  @"SELECT codGiocatore FROM CONTROLLO_TERRITORIO 
    WHERE codPartita = @matchID 
    AND territorio = @territory 
    AND numeroTurno = @turnNumber;";

  // OP 6 Register a new attack 
  public static string GET_CONTROLLED_TERRITORIES =
  @"SELECT territorio FROM CONTROLLO_TERRITORIO 
    WHERE codPartita = @matchID 
    AND codGiocatore = @playerID 
    AND numeroTurno = @turnNumber;";
  public static string GET_TROOPS_ON_TERRITORY =
  @"SELECT numArmate FROM CONTROLLO_TERRITORIO 
    WHERE codPartita = @matchID 
    AND codGiocatore = @playerID 
    AND numeroTurno = @turnNumber 
    AND territorio = @territory;";
  public static string CREATE_ATTACK =
  @"INSERT INTO ATTACCO (attaccante, difensore, armateSchierate, armatePerse, difArmateSchierate, difArmatePerse, vittoria) 
    VALUES (@attacker, @defender, @atkDeployed, @atkLost, @defDeployed, @defLost, @victory);";
  public static string ADD_ATTACK_TO_TURN =
  @"UPDATE TURNO 
    SET codAttacco = LAST_INSERT_ID() 
    WHERE codPartita = @matchID AND codGiocatore = @playerID AND numeroTurno = @turnNumber;";
  /// <summary> 
  /// <b>Warning</b>: could return null
  /// </summary>
  public static string GET_ENEMY_NEIGHBOUR_TERRITORIES =
  @"SELECT conf.terrB 
    FROM CONFINE conf 
    WHERE conf.terrA = @territory AND conf.terrB IN 
    (
      SELECT contr.territorio 
      FROM CONTROLLO_TERRITORIO contr 
      WHERE contr.codPartita = @matchID 
      AND contr.codGiocatore != @playerID 
      AND contr.numeroTurno = @turnNumber 
    );";

  // OP 7 Register a new movement
  public static string CREATE_MOVEMENT =
  @"INSERT INTO SPOSTAMENTO (territorioPartenza, territorioArrivo, numArmate) 
    VALUES (@from, @to, @troops);";
  public static string ADD_MOVEMENT_TO_TURN =
  @"UPDATE TURNO 
    SET codSpostamento = LAST_INSERT_ID() 
    WHERE codPartita = @matchID AND codGiocatore = @playerID AND numeroTurno = @turnNumber;";
  public static string GET_ALLIED_TERRITORY_NEIGHBOURS =
  @"SELECT conf.terrB 
    FROM CONFINE conf 
    WHERE conf.terrA = @territory AND conf.terrB IN 
    (
      SELECT contr.territorio 
      FROM CONTROLLO_TERRITORIO contr 
      WHERE contr.codPartita = @matchID 
      AND contr.codGiocatore = @playerID 
      AND contr.numeroTurno = @turnNumber 
    );";

  // OP 8 Get the number of troops to assign to a player at the beginning of his turn
  public static string GET_TERRITORIES_BONUS =
  @"SELECT FLOOR(T.territoriControllati / 3) AS truppeBonus
    FROM TURNO T
    WHERE T.codPartita = @matchID
    		AND T.codGiocatore = @playerID
        AND T.numeroTurno = @turnNumber";
  public static string GET_CONTINENTS_BONUS =
  @"SELECT COALESCE(SUM(C1.bonusArmate), 0) AS bonusArmate -- COALESCE avoids null (if no continent is owned returns 0)
    FROM CONTINENTE C1
    WHERE C1.nome IN
    (
      SELECT TERR_TOTALI.continente
      FROM 
        (
        SELECT C.nome AS continente, COUNT(*) AS numTerrPerCont
        FROM CONTINENTE C, TERRITORIO T 
        WHERE T.continente = C.nome 
        GROUP BY C.nome
        ) AS TERR_TOTALI
      JOIN 
        (
        SELECT T.continente, COUNT(*) AS numTerrPerContPosseduti 
        FROM CONTROLLO_TERRITORIO CT, TERRITORIO T 
        WHERE CT.territorio = T.nome 
            AND CT.codPartita = @matchID 
            AND CT.codGiocatore = @playerID 
            AND CT.numeroTurno = @turnNumber
        GROUP BY T.continente
        ) AS TERR_GIOCATORE 
      ON 
        TERR_TOTALI.continente = TERR_GIOCATORE.continente
      WHERE 
        TERR_TOTALI.numTerrPerCont = TERR_GIOCATORE.numTerrPerContPosseduti
    );";

  // OP 9 Get the number of territories players control in a certain turn

  public static string GET_TURN_LEADERBOARD = 
  @"SELECT G.nickname, E.colore, T.territoriControllati
    FROM GIOCATORE G, TURNO T, ESERCITO E
    WHERE T.codPartita = @matchID
          AND T.numeroTurno = @turnNumber
          AND G.codEsercito = E.codEsercito
          AND G.codGiocatore = T.codGiocatore
    ORDER BY T.territoriControllati DESC;";

  // OP 10 show a match in a certain turn
  public static string GET_MATCHES_IDS =
  "SELECT P.codPartita FROM PARTITA P";

  public static string GET_TERRITORIES_AND_COLORS =
  @"SELECT CT.territorio, CT.numArmate, E.colore
    FROM CONTROLLO_TERRITORIO CT, GIOCATORE G, ESERCITO E
    WHERE CT.codGiocatore = G.codGiocatore
      AND G.codEsercito = E.codEsercito
      AND CT.codPartita = @matchID
      AND CT.numeroTurno = @turnNumber;";
  public static string GET_MATCH_HIGHEST_TURN_NUMBER =
  "SELECT MAX(numeroTurno) FROM TURNO WHERE codPartita = @matchID";

  // OP 11 register a victory
  public static string CREATE_VICTORY =
  "INSERT INTO VINCITORE (codPartita, codGiocatore) VALUES (@matchID, @playerID);";
  // OP 12 show leaderboards
  public static string GET_LEADERBOARDS =
  @"SELECT U.nome, U.cognome, U.vittorie 
    FROM UTENTE U
    ORDER BY U.vittorie DESC;";
}
