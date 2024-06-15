CREATE DATABASE  IF NOT EXISTS `RisiKoDB` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `RisiKoDB`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: RisiKoDB
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `armate_iniziali`
--

DROP TABLE IF EXISTS `armate_iniziali`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `armate_iniziali` (
  `numGiocatori` int NOT NULL,
  `numArmate` int NOT NULL,
  PRIMARY KEY (`numGiocatori`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `armate_iniziali`
--

LOCK TABLES `armate_iniziali` WRITE;
/*!40000 ALTER TABLE `armate_iniziali` DISABLE KEYS */;
INSERT INTO `armate_iniziali` VALUES (3,35),(4,30),(5,25),(6,20);
/*!40000 ALTER TABLE `armate_iniziali` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `attacco`
--

DROP TABLE IF EXISTS `attacco`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `attacco` (
  `codAttacco` int NOT NULL AUTO_INCREMENT,
  `vittoria` char(1) NOT NULL,
  `armateSchierate` int NOT NULL,
  `armatePerse` int NOT NULL,
  `difArmateSchierate` int NOT NULL,
  `difArmatePerse` int NOT NULL,
  `attaccante` varchar(48) NOT NULL,
  `difensore` varchar(48) NOT NULL,
  PRIMARY KEY (`codAttacco`),
  UNIQUE KEY `ID_ATTACCO_IND` (`codAttacco`),
  KEY `FKattaccante` (`attaccante`),
  KEY `FKdifensore` (`difensore`),
  CONSTRAINT `FKattaccante` FOREIGN KEY (`attaccante`) REFERENCES `territorio` (`nome`),
  CONSTRAINT `FKdifensore` FOREIGN KEY (`difensore`) REFERENCES `territorio` (`nome`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attacco`
--

LOCK TABLES `attacco` WRITE;
/*!40000 ALTER TABLE `attacco` DISABLE KEYS */;
INSERT INTO `attacco` VALUES (3,'1',4,1,1,1,'Venezuela','Brasile'),(4,'0',6,6,4,2,'Gran Bretagna','Europa Settentrionale'),(5,'0',3,1,1,1,'Islanda','Gran Bretagna'),(6,'1',4,1,1,1,'Kamchatka','Giappone'),(7,'1',4,2,2,2,'Africa del Nord','Egitto'),(8,'1',5,2,1,1,'Territori del Nord Ovest','Alaska'),(9,'0',5,5,3,0,'Islanda','Groenlandia'),(10,'1',4,1,1,1,'Indonesia','Siam'),(11,'1',4,2,2,2,'Africa Orientale','Madagascar'),(12,'1',8,6,5,5,'Groenlandia','Islanda'),(13,'1',3,2,1,1,'Europa Meridionale','Africa del Nord'),(14,'1',2,0,1,1,'India','Medio Oriente'),(15,'1',4,1,1,1,'Egitto','Africa del Nord'),(16,'1',3,0,1,1,'Alaska','Alberta');
/*!40000 ALTER TABLE `attacco` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `confine`
--

DROP TABLE IF EXISTS `confine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `confine` (
  `terrB` varchar(48) NOT NULL,
  `terrA` varchar(48) NOT NULL,
  PRIMARY KEY (`terrB`,`terrA`),
  UNIQUE KEY `ID_CONFINE_IND` (`terrB`,`terrA`),
  KEY `EQU_CONFI_TERRI_1_IND` (`terrA`),
  KEY `EQU_CONFI_TERRI_IND` (`terrB`),
  CONSTRAINT `EQU_CONFI_TERRI_1_FK` FOREIGN KEY (`terrA`) REFERENCES `territorio` (`nome`),
  CONSTRAINT `EQU_CONFI_TERRI_FK` FOREIGN KEY (`terrB`) REFERENCES `territorio` (`nome`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `confine`
--

LOCK TABLES `confine` WRITE;
/*!40000 ALTER TABLE `confine` DISABLE KEYS */;
INSERT INTO `confine` VALUES ('Cina','Afghanistan'),('Medio Oriente','Afghanistan'),('Ucraina','Afghanistan'),('Urali','Afghanistan'),('Africa Orientale','Africa del Nord'),('Brasile','Africa del Nord'),('Congo','Africa del Nord'),('Egitto','Africa del Nord'),('Europa Meridionale','Africa del Nord'),('Europa Occidentale','Africa del Nord'),('Africa Orientale','Africa Meridionale'),('Congo','Africa Meridionale'),('Madagascar','Africa Meridionale'),('Africa del Nord','Africa Orientale'),('Africa Meridionale','Africa Orientale'),('Congo','Africa Orientale'),('Egitto','Africa Orientale'),('Madagascar','Africa Orientale'),('Alberta','Alaska'),('Kamchatka','Alaska'),('Territori del Nord Ovest','Alaska'),('Ontario','Alberta'),('Stati Uniti Occidentali','Alberta'),('Stati Uniti Orientali','Alberta'),('Territori del Nord Ovest','Alberta'),('Stati Uniti Occidentali','America Centrale'),('Stati Uniti Orientali','America Centrale'),('Venezuela','America Centrale'),('Brasile','Argentina'),('Perù','Argentina'),('Australia Orientale','Australia Occidentale'),('Indonesia','Australia Occidentale'),('Nuova Guinea','Australia Occidentale'),('Australia Occidentale','Australia Orientale'),('Nuova Guinea','Australia Orientale'),('Africa del Nord','Brasile'),('Argentina','Brasile'),('Perù','Brasile'),('Venezuela','Brasile'),('Afghanistan','Cina'),('India','Cina'),('Medio Oriente','Cina'),('Mongolia','Cina'),('Siam','Cina'),('Siberia','Cina'),('Urali','Cina'),('Jacuzia','Cita'),('Kamchatka','Cita'),('Mongolia','Cita'),('Siberia','Cita'),('Africa del Nord','Congo'),('Africa Meridionale','Congo'),('Africa Orientale','Congo'),('Africa del Nord','Egitto'),('Africa Orientale','Egitto'),('Europa Meridionale','Egitto'),('Medio Oriente','Egitto'),('Africa del Nord','Europa Meridionale'),('Egitto','Europa Meridionale'),('Europa Occidentale','Europa Meridionale'),('Europa Settentrionale','Europa Meridionale'),('Medio Oriente','Europa Meridionale'),('Ucraina','Europa Meridionale'),('Africa del Nord','Europa Occidentale'),('Europa Meridionale','Europa Occidentale'),('Europa Settentrionale','Europa Occidentale'),('Gran Bretagna','Europa Occidentale'),('Europa Meridionale','Europa Settentrionale'),('Europa Occidentale','Europa Settentrionale'),('Gran Bretagna','Europa Settentrionale'),('Scandinavia','Europa Settentrionale'),('Ucraina','Europa Settentrionale'),('Kamchatka','Giappone'),('Mongolia','Giappone'),('Europa Occidentale','Gran Bretagna'),('Europa Settentrionale','Gran Bretagna'),('Islanda','Gran Bretagna'),('Scandinavia','Gran Bretagna'),('Islanda','Groenlandia'),('Ontario','Groenlandia'),('Quebec','Groenlandia'),('Territori del Nord Ovest','Groenlandia'),('Afghanistan','India'),('Cina','India'),('Medio Oriente','India'),('Siam','India'),('Australia Occidentale','Indonesia'),('Nuova Guinea','Indonesia'),('Siam','Indonesia'),('Gran Bretagna','Islanda'),('Groenlandia','Islanda'),('Scandinavia','Islanda'),('Cita','Jacuzia'),('Kamchatka','Jacuzia'),('Siberia','Jacuzia'),('Alaska','Kamchatka'),('Cita','Kamchatka'),('Giappone','Kamchatka'),('Jacuzia','Kamchatka'),('Mongolia','Kamchatka'),('Africa Meridionale','Madagascar'),('Africa Orientale','Madagascar'),('Afghanistan','Medio Oriente'),('Cina','Medio Oriente'),('Egitto','Medio Oriente'),('Europa Meridionale','Medio Oriente'),('India','Medio Oriente'),('Ucraina','Medio Oriente'),('Cina','Mongolia'),('Cita','Mongolia'),('Giappone','Mongolia'),('Kamchatka','Mongolia'),('Siberia','Mongolia'),('Australia Occidentale','Nuova Guinea'),('Australia Orientale','Nuova Guinea'),('Indonesia','Nuova Guinea'),('Alberta','Ontario'),('Groenlandia','Ontario'),('Quebec','Ontario'),('Stati Uniti Occidentali','Ontario'),('Stati Uniti Orientali','Ontario'),('Territori del Nord Ovest','Ontario'),('Argentina','Perù'),('Brasile','Perù'),('Venezuela','Perù'),('Groenlandia','Quebec'),('Ontario','Quebec'),('Stati Uniti Orientali','Quebec'),('Europa Settentrionale','Scandinavia'),('Gran Bretagna','Scandinavia'),('Islanda','Scandinavia'),('Ucraina','Scandinavia'),('Cina','Siam'),('India','Siam'),('Indonesia','Siam'),('Cina','Siberia'),('Cita','Siberia'),('Jacuzia','Siberia'),('Mongolia','Siberia'),('Urali','Siberia'),('Alberta','Stati Uniti Occidentali'),('America Centrale','Stati Uniti Occidentali'),('Ontario','Stati Uniti Occidentali'),('Stati Uniti Orientali','Stati Uniti Occidentali'),('America Centrale','Stati Uniti Orientali'),('Ontario','Stati Uniti Orientali'),('Quebec','Stati Uniti Orientali'),('Stati Uniti Occidentali','Stati Uniti Orientali'),('Alaska','Territori del Nord Ovest'),('Alberta','Territori del Nord Ovest'),('Groenlandia','Territori del Nord Ovest'),('Ontario','Territori del Nord Ovest'),('Afghanistan','Ucraina'),('Europa Meridionale','Ucraina'),('Europa Settentrionale','Ucraina'),('Medio Oriente','Ucraina'),('Scandinavia','Ucraina'),('Urali','Ucraina'),('Afghanistan','Urali'),('Cina','Urali'),('Siberia','Urali'),('Ucraina','Urali'),('America Centrale','Venezuela'),('Brasile','Venezuela'),('Peru','Venezuela');
/*!40000 ALTER TABLE `confine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `continente`
--

DROP TABLE IF EXISTS `continente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `continente` (
  `nome` varchar(16) NOT NULL,
  `bonusArmate` int NOT NULL,
  PRIMARY KEY (`nome`),
  UNIQUE KEY `ID_CONTINENTE_IND` (`nome`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `continente`
--

LOCK TABLES `continente` WRITE;
/*!40000 ALTER TABLE `continente` DISABLE KEYS */;
INSERT INTO `continente` VALUES ('Africa',3),('America del Nord',5),('America del Sud',2),('Asia',7),('Australia',2),('Europa',5);
/*!40000 ALTER TABLE `continente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `controllo_territorio`
--

DROP TABLE IF EXISTS `controllo_territorio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `controllo_territorio` (
  `codGiocatore` int NOT NULL,
  `codPartita` int NOT NULL,
  `numeroTurno` int NOT NULL,
  `territorio` varchar(48) NOT NULL,
  `numArmate` int NOT NULL,
  PRIMARY KEY (`codGiocatore`,`codPartita`,`numeroTurno`,`territorio`),
  UNIQUE KEY `ID_CONTROLLO_TERRITORIO_IND` (`codGiocatore`,`codPartita`,`numeroTurno`,`territorio`),
  UNIQUE KEY `terrain_cannot_be_controlled_by_multiple_players` (`codPartita`,`numeroTurno`,`territorio`),
  KEY `REF_CONTR_TERRI_IND` (`territorio`),
  CONSTRAINT `EQU_CONTR_TURNO` FOREIGN KEY (`codGiocatore`, `codPartita`, `numeroTurno`) REFERENCES `turno` (`codGiocatore`, `codPartita`, `numeroTurno`),
  CONSTRAINT `REF_CONTR_TERRI_FK` FOREIGN KEY (`territorio`) REFERENCES `territorio` (`nome`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `controllo_territorio`
--

LOCK TABLES `controllo_territorio` WRITE;
/*!40000 ALTER TABLE `controllo_territorio` DISABLE KEYS */;
INSERT INTO `controllo_territorio` VALUES (40,28,1,'Africa del Nord',9),(40,28,1,'Africa Meridionale',1),(40,28,1,'Argentina',1),(40,28,1,'Australia Occidentale',1),(40,28,1,'Cina',1),(40,28,1,'Jacuzia',1),(40,28,1,'Medio Oriente',4),(40,28,1,'Ontario',1),(40,28,1,'Perù',1),(40,28,1,'Stati Uniti Orientali',1),(40,28,1,'Venezuela',9),(40,28,2,'Africa del Nord',9),(40,28,2,'Africa Meridionale',3),(40,28,2,'Argentina',1),(40,28,2,'Australia Occidentale',2),(40,28,2,'Cina',1),(40,28,2,'Medio Oriente',4),(40,28,2,'Ontario',1),(40,28,2,'Perù',1),(40,28,2,'Siberia',1),(40,28,2,'Stati Uniti Orientali',1),(40,28,2,'Venezuela',9),(40,28,3,'Africa del Nord',3),(40,28,3,'Africa Meridionale',2),(40,28,3,'Africa Orientale',1),(40,28,3,'Argentina',1),(40,28,3,'Brasile',3),(40,28,3,'Cina',1),(40,28,3,'Congo',3),(40,28,3,'Egitto',2),(40,28,3,'Medio Oriente',4),(40,28,3,'Ontario',1),(40,28,3,'Perù',1),(40,28,3,'Siberia',1),(40,28,3,'Stati Uniti Orientali',1),(40,28,3,'Venezuela',6),(40,28,4,'Africa Meridionale',1),(40,28,4,'Africa Orientale',1),(40,28,4,'America Centrale',4),(40,28,4,'Argentina',1),(40,28,4,'Brasile',3),(40,28,4,'Congo',6),(40,28,4,'Egitto',7),(40,28,4,'Madagascar',1),(40,28,4,'Medio Oriente',1),(40,28,4,'Ontario',1),(40,28,4,'Perù',1),(40,28,4,'Stati Uniti Orientali',1),(40,28,4,'Venezuela',1),(40,28,5,'Africa del Nord',3),(40,28,5,'Africa Meridionale',1),(40,28,5,'Africa Orientale',5),(40,28,5,'America Centrale',5),(40,28,5,'Argentina',1),(40,28,5,'Brasile',3),(40,28,5,'Congo',2),(40,28,5,'Egitto',6),(40,28,5,'Madagascar',1),(40,28,5,'Ontario',1),(40,28,5,'Perù',1),(40,28,5,'Stati Uniti Orientali',1),(40,28,5,'Venezuela',1),(41,28,1,'Afghanistan',2),(41,28,1,'Congo',1),(41,28,1,'Giappone',1),(41,28,1,'Gran Bretagna',5),(41,28,1,'India',1),(41,28,1,'Indonesia',1),(41,28,1,'Quebec',8),(41,28,1,'Siberia',1),(41,28,1,'Territori del Nord Ovest',6),(41,28,1,'Ucraina',4),(41,28,2,'Afghanistan',2),(41,28,2,'Congo',1),(41,28,2,'Giappone',1),(41,28,2,'Gran Bretagna',1),(41,28,2,'India',1),(41,28,2,'Jacuzia',1),(41,28,2,'Quebec',8),(41,28,2,'Territori del Nord Ovest',6),(41,28,2,'Ucraina',4),(41,28,3,'Afghanistan',1),(41,28,3,'Alaska',4),(41,28,3,'Groenlandia',7),(41,28,3,'India',1),(41,28,3,'Quebec',1),(41,28,3,'Scandinavia',2),(41,28,3,'Territori del Nord Ovest',2),(41,28,3,'Ucraina',1),(41,28,4,'Afghanistan',1),(41,28,4,'Alaska',4),(41,28,4,'Groenlandia',2),(41,28,4,'India',1),(41,28,4,'Islanda',1),(41,28,4,'Quebec',1),(41,28,4,'Territori del Nord Ovest',2),(41,28,5,'Alaska',3),(41,28,5,'Alberta',2),(41,28,5,'Groenlandia',1),(41,28,5,'Quebec',2),(41,28,5,'Territori del Nord Ovest',2),(45,28,1,'Alberta',3),(45,28,1,'Brasile',1),(45,28,1,'Cita',3),(45,28,1,'Egitto',1),(45,28,1,'Europa Occidentale',1),(45,28,1,'Groenlandia',1),(45,28,1,'Kamchatka',8),(45,28,1,'Mongolia',6),(45,28,1,'Nuova Guinea',5),(45,28,1,'Stati Uniti Occidentali',1),(45,28,2,'Alberta',3),(45,28,2,'Brasile',1),(45,28,2,'Cita',3),(45,28,2,'Egitto',1),(45,28,2,'Europa Occidentale',1),(45,28,2,'Groenlandia',1),(45,28,2,'Indonesia',7),(45,28,2,'Kamchatka',8),(45,28,2,'Mongolia',6),(45,28,2,'Nuova Guinea',1),(45,28,2,'Stati Uniti Occidentali',1),(45,28,3,'Alberta',3),(45,28,3,'Australia Occidentale',2),(45,28,3,'Cita',3),(45,28,3,'Giappone',3),(45,28,3,'Indonesia',3),(45,28,3,'Jacuzia',4),(45,28,3,'Kamchatka',2),(45,28,3,'Mongolia',3),(45,28,3,'Nuova Guinea',1),(45,28,3,'Stati Uniti Occidentali',1),(45,28,4,'Alberta',3),(45,28,4,'Australia Occidentale',1),(45,28,4,'Australia Orientale',1),(45,28,4,'Cina',1),(45,28,4,'Cita',3),(45,28,4,'Giappone',1),(45,28,4,'Indonesia',1),(45,28,4,'Jacuzia',1),(45,28,4,'Kamchatka',4),(45,28,4,'Mongolia',1),(45,28,4,'Nuova Guinea',1),(45,28,4,'Siam',3),(45,28,4,'Siberia',3),(45,28,4,'Stati Uniti Occidentali',1),(45,28,5,'Afghanistan',1),(45,28,5,'Australia Occidentale',1),(45,28,5,'Australia Orientale',1),(45,28,5,'Cina',1),(45,28,5,'Cita',3),(45,28,5,'Giappone',1),(45,28,5,'India',1),(45,28,5,'Indonesia',1),(45,28,5,'Jacuzia',1),(45,28,5,'Kamchatka',4),(45,28,5,'Medio Oriente',1),(45,28,5,'Mongolia',1),(45,28,5,'Nuova Guinea',1),(45,28,5,'Siam',1),(45,28,5,'Siberia',2),(45,28,5,'Stati Uniti Occidentali',3),(45,28,5,'Urali',1),(45,28,6,'Afghanistan',1),(45,28,6,'Australia Occidentale',1),(45,28,6,'Australia Orientale',1),(45,28,6,'Cina',1),(45,28,6,'Cita',1),(45,28,6,'Europa Settentrionale',1),(45,28,6,'Giappone',1),(45,28,6,'Gran Bretagna',1),(45,28,6,'Groenlandia',4),(45,28,6,'India',1),(45,28,6,'Indonesia',1),(45,28,6,'Islanda',1),(45,28,6,'Jacuzia',1),(45,28,6,'Kamchatka',1),(45,28,6,'Medio Oriente',1),(45,28,6,'Mongolia',1),(45,28,6,'Nuova Guinea',1),(45,28,6,'Ontario',2),(45,28,6,'Scandinavia',1),(45,28,6,'Siam',1),(45,28,6,'Siberia',1),(45,28,6,'Stati Uniti Occidentali',3),(45,28,6,'Territori del Nord Ovest',1),(45,28,6,'Ucraina',1),(45,28,6,'Urali',4),(46,28,1,'Africa Orientale',1),(46,28,1,'Alaska',1),(46,28,1,'America Centrale',1),(46,28,1,'Australia Orientale',1),(46,28,1,'Europa Meridionale',6),(46,28,1,'Europa Settentrionale',4),(46,28,1,'Islanda',11),(46,28,1,'Scandinavia',3),(46,28,1,'Siam',1),(46,28,1,'Urali',1),(46,28,2,'Africa Orientale',1),(46,28,2,'Alaska',1),(46,28,2,'America Centrale',1),(46,28,2,'Australia Orientale',1),(46,28,2,'Europa Meridionale',5),(46,28,2,'Europa Settentrionale',2),(46,28,2,'Islanda',10),(46,28,2,'Madagascar',2),(46,28,2,'Scandinavia',3),(46,28,2,'Siam',1),(46,28,2,'Urali',1),(46,28,3,'America Centrale',1),(46,28,3,'Australia Orientale',1),(46,28,3,'Europa Meridionale',5),(46,28,3,'Europa Occidentale',3),(46,28,3,'Europa Settentrionale',1),(46,28,3,'Gran Bretagna',1),(46,28,3,'Islanda',6),(46,28,3,'Madagascar',2),(46,28,3,'Siam',1),(46,28,3,'Urali',1),(46,28,4,'Africa del Nord',1),(46,28,4,'Europa Meridionale',3),(46,28,4,'Europa Occidentale',3),(46,28,4,'Europa Settentrionale',1),(46,28,4,'Gran Bretagna',1),(46,28,4,'Scandinavia',1),(46,28,4,'Ucraina',1),(46,28,5,'Europa Meridionale',3),(46,28,5,'Europa Occidentale',1),(46,28,5,'Europa Settentrionale',1),(46,28,5,'Gran Bretagna',3),(46,28,5,'Islanda',1),(46,28,5,'Scandinavia',1),(46,28,5,'Ucraina',1);
/*!40000 ALTER TABLE `controllo_territorio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `esercito`
--

DROP TABLE IF EXISTS `esercito`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `esercito` (
  `codEsercito` int NOT NULL AUTO_INCREMENT,
  `colore` varchar(8) NOT NULL,
  PRIMARY KEY (`codEsercito`),
  UNIQUE KEY `ID_ESERCITO_IND` (`codEsercito`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `esercito`
--

LOCK TABLES `esercito` WRITE;
/*!40000 ALTER TABLE `esercito` DISABLE KEYS */;
INSERT INTO `esercito` VALUES (1,'Viola'),(2,'Blu'),(3,'Rosse'),(4,'Gialle'),(5,'Nere'),(6,'Verdi');
/*!40000 ALTER TABLE `esercito` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `giocatore`
--

DROP TABLE IF EXISTS `giocatore`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `giocatore` (
  `codGiocatore` int NOT NULL AUTO_INCREMENT,
  `nickname` char(16) NOT NULL,
  `codPartita` int NOT NULL,
  `codUtente` varchar(16) NOT NULL,
  `codObiettivo` int NOT NULL,
  `codEsercito` int NOT NULL,
  PRIMARY KEY (`codGiocatore`),
  UNIQUE KEY `ID_GIOCATORE_IND` (`codGiocatore`),
  UNIQUE KEY `unique_nickname_per_match` (`codPartita`,`nickname`),
  KEY `EQU_GIOCA_PARTI_IND` (`codPartita`),
  KEY `REF_GIOCA_UTENT_IND` (`codUtente`),
  KEY `REF_GIOCA_OBIET_IND` (`codObiettivo`),
  KEY `REF_GIOCA_ESERC_IND` (`codEsercito`),
  CONSTRAINT `EQU_GIOCA_PARTI_FK` FOREIGN KEY (`codPartita`) REFERENCES `partita` (`codPartita`),
  CONSTRAINT `REF_GIOCA_ESERC_FK` FOREIGN KEY (`codEsercito`) REFERENCES `esercito` (`codEsercito`),
  CONSTRAINT `REF_GIOCA_OBIET_FK` FOREIGN KEY (`codObiettivo`) REFERENCES `obiettivo` (`codObiettivo`),
  CONSTRAINT `REF_GIOCA_UTENT_FK` FOREIGN KEY (`codUtente`) REFERENCES `utente` (`codiceFiscale`),
  CONSTRAINT `chk_nickname_not_empty` CHECK ((`nickname` <> _utf8mb4''))
) ENGINE=InnoDB AUTO_INCREMENT=63 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `giocatore`
--

LOCK TABLES `giocatore` WRITE;
/*!40000 ALTER TABLE `giocatore` DISABLE KEYS */;
INSERT INTO `giocatore` VALUES (40,'pippo',28,'ABC1234567890123',5,6),(41,'pluto',28,'123',12,3),(45,'paperino',28,'test',9,1),(46,'rapepino',28,'24',2,2);
/*!40000 ALTER TABLE `giocatore` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `obiettivo`
--

DROP TABLE IF EXISTS `obiettivo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `obiettivo` (
  `codObiettivo` int NOT NULL AUTO_INCREMENT,
  `descrizione` varchar(255) NOT NULL,
  PRIMARY KEY (`codObiettivo`),
  UNIQUE KEY `ID_OBIETTIVO_IND` (`codObiettivo`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `obiettivo`
--

LOCK TABLES `obiettivo` WRITE;
/*!40000 ALTER TABLE `obiettivo` DISABLE KEYS */;
INSERT INTO `obiettivo` VALUES (1,'Conquistare 18 territori ed occuparne ciascuno con almeno due armate'),(2,'Conquistare 24 territori'),(3,'Conquistare la totalità del Nord America e dell\'Africa'),(4,'Conquistare la totalità del Nord America e dell\'Oceania'),(5,'Conquistare la totalità dell\'Asia e del Sud America'),(6,'Conquistare la totalità dell\'Asia e dell\'Africa'),(7,'Conquistare la totalità dell\'Europa, del Sud America e di un terzo continente a scelta'),(8,'Conquistare la totalità dell\'Europa, dell\'Oceania e di un terzo continente a scelta'),(9,'Devi distruggere LE ARMATE VIOLA. Se sei tu stesso il proprietario di tali armate o se il giocatore proprietario è eliminato da un\'altro giocatore, il tuo obiettivo diventa conquistare 24 TERRITORI'),(10,'Devi distruggere LE ARMATE BLU. Se sei tu stesso il proprietario di tali armate o se il giocatore proprietario è eliminato da un\'altro giocatore, il tuo obiettivo diventa conquistare 24 TERRITORI'),(11,'Devi distruggere LE ARMATE ROSSE. Se sei tu stesso il proprietario di tali armate o se il giocatore proprietario è eliminato da un\'altro giocatore, il tuo obiettivo diventa conquistare 24 TERRITORI'),(12,'Devi distruggere LE ARMATE GIALLE. Se sei tu stesso il proprietario di tali armate o se il giocatore proprietario è eliminato da un\'altro giocatore, il tuo obiettivo diventa conquistare 24 TERRITORI'),(13,'Devi distruggere LE ARMATE NERE. Se sei tu stesso il proprietario di tali armate o se il giocatore proprietario è eliminato da un\'altro giocatore, il tuo obiettivo diventa conquistare 24 TERRITORI'),(14,'Devi distruggere LE ARMATE VERDI. Se sei tu stesso il proprietario di tali armate o se il giocatore proprietario è eliminato da un\'altro giocatore, il tuo obiettivo diventa conquistare 24 TERRITORI');
/*!40000 ALTER TABLE `obiettivo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `partita`
--

DROP TABLE IF EXISTS `partita`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `partita` (
  `codPartita` int NOT NULL AUTO_INCREMENT,
  `data` date NOT NULL,
  PRIMARY KEY (`codPartita`),
  UNIQUE KEY `ID_PARTITA_IND` (`codPartita`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `partita`
--

LOCK TABLES `partita` WRITE;
/*!40000 ALTER TABLE `partita` DISABLE KEYS */;
INSERT INTO `partita` VALUES (28,'2024-06-10');
/*!40000 ALTER TABLE `partita` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `spostamento`
--

DROP TABLE IF EXISTS `spostamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `spostamento` (
  `codSpostamento` int NOT NULL AUTO_INCREMENT,
  `numArmate` int NOT NULL,
  `territorioArrivo` varchar(48) NOT NULL,
  `territorioPartenza` varchar(48) NOT NULL,
  PRIMARY KEY (`codSpostamento`),
  UNIQUE KEY `ID_SPOSTAMENTO_IND` (`codSpostamento`),
  KEY `REF_SPOST_TERRI_1_IND` (`territorioArrivo`),
  KEY `REF_SPOST_TERRI_IND` (`territorioPartenza`),
  CONSTRAINT `REF_SPOST_TERRI_1_FK` FOREIGN KEY (`territorioArrivo`) REFERENCES `territorio` (`nome`),
  CONSTRAINT `REF_SPOST_TERRI_FK` FOREIGN KEY (`territorioPartenza`) REFERENCES `territorio` (`nome`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `spostamento`
--

LOCK TABLES `spostamento` WRITE;
/*!40000 ALTER TABLE `spostamento` DISABLE KEYS */;
INSERT INTO `spostamento` VALUES (2,15,'Ucraina','India'),(3,15,'Ucraina','India'),(4,1,'Stati Uniti Occidentali','Quebec'),(5,15,'Ucraina','India'),(6,1,'Brasile','Argentina'),(7,1,'Africa Orientale','Africa del Nord'),(8,1,'Africa del Nord','Brasile'),(9,1,'Ucraina','Afghanistan'),(10,2,'Europa Settentrionale','Scandinavia'),(11,2,'Kamchatka','Giappone'),(12,3,'Egitto','Medio Oriente'),(13,2,'Stati Uniti Occidentali','Alberta'),(14,4,'Africa Orientale','Congo'),(15,1,'Quebec','Groenlandia'),(16,2,'Gran Bretagna','Europa Occidentale'),(17,2,'Siberia','Cita'),(18,4,'Egitto','Africa Orientale'),(19,1,'Alberta','Territori del Nord Ovest'),(20,2,'Europa Occidentale','Gran Bretagna');
/*!40000 ALTER TABLE `spostamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `territorio`
--

DROP TABLE IF EXISTS `territorio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `territorio` (
  `nome` varchar(48) NOT NULL,
  `continente` varchar(16) NOT NULL,
  PRIMARY KEY (`nome`),
  UNIQUE KEY `ID_TERRITORIO_IND` (`nome`),
  KEY `EQU_TERRI_CONTI_IND` (`continente`),
  CONSTRAINT `EQU_TERRI_CONTI_FK` FOREIGN KEY (`continente`) REFERENCES `continente` (`nome`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `territorio`
--

LOCK TABLES `territorio` WRITE;
/*!40000 ALTER TABLE `territorio` DISABLE KEYS */;
INSERT INTO `territorio` VALUES ('Africa del Nord','Africa'),('Africa Meridionale','Africa'),('Africa Orientale','Africa'),('Congo','Africa'),('Egitto','Africa'),('Madagascar','Africa'),('Alaska','America del Nord'),('Alberta','America del Nord'),('America Centrale','America del Nord'),('Groenlandia','America del Nord'),('Ontario','America del Nord'),('Quebec','America del Nord'),('Stati Uniti Occidentali','America del Nord'),('Stati Uniti Orientali','America del Nord'),('Territori del Nord Ovest','America del Nord'),('Argentina','America del Sud'),('Brasile','America del Sud'),('Perù','America del Sud'),('Venezuela','America del Sud'),('Afghanistan','Asia'),('Cina','Asia'),('Cita','Asia'),('Giappone','Asia'),('India','Asia'),('Jacuzia','Asia'),('Kamchatka','Asia'),('Medio Oriente','Asia'),('Mongolia','Asia'),('Siam','Asia'),('Siberia','Asia'),('Urali','Asia'),('Australia Occidentale','Australia'),('Australia Orientale','Australia'),('Indonesia','Australia'),('Nuova Guinea','Australia'),('Europa Meridionale','Europa'),('Europa Occidentale','Europa'),('Europa Settentrionale','Europa'),('Gran Bretagna','Europa'),('Islanda','Europa'),('Scandinavia','Europa'),('Ucraina','Europa');
/*!40000 ALTER TABLE `territorio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `turno`
--

DROP TABLE IF EXISTS `turno`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `turno` (
  `codGiocatore` int NOT NULL,
  `codPartita` int NOT NULL,
  `numeroTurno` int NOT NULL,
  `codSpostamento` int DEFAULT NULL,
  `codAttacco` int DEFAULT NULL,
  `territoriControllati` int DEFAULT '0',
  PRIMARY KEY (`codGiocatore`,`codPartita`,`numeroTurno`),
  UNIQUE KEY `ID_TURNO_IND` (`codGiocatore`,`codPartita`,`numeroTurno`),
  KEY `SID_TURNO_SPOST` (`codSpostamento`),
  KEY `SID_TURNO_ATTAC` (`codAttacco`),
  KEY `REF_TURNO_PARTI_IND` (`codPartita`),
  KEY `REF_TURNO_GIOCA_IND` (`codGiocatore`),
  CONSTRAINT `REF_TURNO_GIOCA_FK` FOREIGN KEY (`codGiocatore`) REFERENCES `giocatore` (`codGiocatore`),
  CONSTRAINT `REF_TURNO_PARTI_FK` FOREIGN KEY (`codPartita`) REFERENCES `partita` (`codPartita`),
  CONSTRAINT `SID_TURNO_ATTAC` FOREIGN KEY (`codAttacco`) REFERENCES `attacco` (`codAttacco`),
  CONSTRAINT `SID_TURNO_SPOST` FOREIGN KEY (`codSpostamento`) REFERENCES `spostamento` (`codSpostamento`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `turno`
--

LOCK TABLES `turno` WRITE;
/*!40000 ALTER TABLE `turno` DISABLE KEYS */;
INSERT INTO `turno` VALUES (40,28,1,NULL,NULL,11),(40,28,2,NULL,3,11),(40,28,3,12,7,14),(40,28,4,14,11,13),(40,28,5,18,15,13),(41,28,1,NULL,NULL,10),(41,28,2,9,4,9),(41,28,3,NULL,8,8),(41,28,4,15,12,7),(41,28,5,19,16,5),(45,28,1,NULL,NULL,10),(45,28,2,NULL,NULL,11),(45,28,3,11,6,10),(45,28,4,13,10,14),(45,28,5,17,14,17),(45,28,6,NULL,NULL,25),(46,28,1,NULL,NULL,10),(46,28,2,10,5,11),(46,28,3,NULL,9,10),(46,28,4,16,13,7),(46,28,5,20,NULL,7);
/*!40000 ALTER TABLE `turno` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `utente`
--

DROP TABLE IF EXISTS `utente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `utente` (
  `codiceFiscale` varchar(16) NOT NULL,
  `nome` varchar(20) NOT NULL,
  `cognome` varchar(20) NOT NULL,
  `vittorie` int DEFAULT '0',
  PRIMARY KEY (`codiceFiscale`),
  UNIQUE KEY `ID_UTENTE_IND` (`codiceFiscale`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `utente`
--

LOCK TABLES `utente` WRITE;
/*!40000 ALTER TABLE `utente` DISABLE KEYS */;
INSERT INTO `utente` VALUES ('123','Luca','Rossi',1),('24','3f','34',0),('ABC1234567890123','John','Doe',0),('test','nome','cognome',5);
/*!40000 ALTER TABLE `utente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vincitore`
--

DROP TABLE IF EXISTS `vincitore`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vincitore` (
  `codPartita` int NOT NULL,
  `codGiocatore` int NOT NULL,
  PRIMARY KEY (`codPartita`),
  UNIQUE KEY `ID_VINCI_PARTI_IND` (`codPartita`),
  KEY `SID_VINCI_GIOCA_IND` (`codGiocatore`),
  CONSTRAINT `ID_VINCI_PARTI_FK` FOREIGN KEY (`codPartita`) REFERENCES `partita` (`codPartita`),
  CONSTRAINT `SID_VINCI_GIOCA_FK` FOREIGN KEY (`codGiocatore`) REFERENCES `giocatore` (`codGiocatore`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vincitore`
--

LOCK TABLES `vincitore` WRITE;
/*!40000 ALTER TABLE `vincitore` DISABLE KEYS */;
INSERT INTO `vincitore` VALUES (28,45);
/*!40000 ALTER TABLE `vincitore` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'RisiKoDB'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-06-15 17:42:53
