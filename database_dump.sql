-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: university
-- ------------------------------------------------------
-- Server version	8.0.31

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
-- Table structure for table `course`
--

DROP TABLE IF EXISTS `course`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `course` (
  `idcourse` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `ects` tinyint NOT NULL,
  PRIMARY KEY (`idcourse`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `course`
--

LOCK TABLES `course` WRITE;
/*!40000 ALTER TABLE `course` DISABLE KEYS */;
INSERT INTO `course` VALUES (1,'C#',5),(2,'C++',4),(3,'Java',5),(4,'Java 8',5),(5,'New Java',4),(7,'Java 8',5);
/*!40000 ALTER TABLE `course` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `grade`
--

DROP TABLE IF EXISTS `grade`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `grade` (
  `idgrade` int NOT NULL AUTO_INCREMENT,
  `grade_date` timestamp NULL DEFAULT NULL,
  `idstudent` int DEFAULT NULL,
  `idcourse` int DEFAULT NULL,
  `grade` tinyint DEFAULT NULL,
  PRIMARY KEY (`idgrade`),
  KEY `student-grade_idx` (`idstudent`),
  KEY `course-grade_idx` (`idcourse`),
  CONSTRAINT `course-grade` FOREIGN KEY (`idcourse`) REFERENCES `course` (`idcourse`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `student-grade` FOREIGN KEY (`idstudent`) REFERENCES `student` (`idstudent`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `grade`
--

LOCK TABLES `grade` WRITE;
/*!40000 ALTER TABLE `grade` DISABLE KEYS */;
INSERT INTO `grade` VALUES (2,'2023-05-10 21:00:00',7,1,4),(3,'2023-05-10 21:00:00',7,2,5),(4,'2023-05-11 21:00:00',8,2,5);
/*!40000 ALTER TABLE `grade` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student`
--

DROP TABLE IF EXISTS `student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student` (
  `idstudent` int NOT NULL AUTO_INCREMENT,
  `fname` varchar(45) NOT NULL,
  `lname` varchar(45) NOT NULL,
  `username` varchar(45) NOT NULL,
  `password` varchar(255) NOT NULL,
  `address` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idstudent`),
  UNIQUE KEY `username_UNIQUE` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student`
--

LOCK TABLES `student` WRITE;
/*!40000 ALTER TABLE `student` DISABLE KEYS */;
INSERT INTO `student` VALUES (7,'Bill','Smith','user01','$2a$11$qdVjaM8n3QjsrxSpBosT8uDhjavo7YyZfBenJY6Ywbz4tOjpQhGNq',NULL),(8,'Jimmy','Jones','user02','$2a$11$vy7gdyvLwekIuFlch4JCB.qQH0zJnmG3hNHynpqGOPXwa/3z7G8cy','Test'),(11,'Jimmy','Jones','user09','$2a$11$wiRVA0TUAIO/HH12ZCaSqOA47wcXXQMuTW0d1Vrk1UbbV5ysslsoa','Test1'),(12,'Bill','Smith','user05','$2a$11$or9hHPRC8tn5oJGhrCW1WuRBY1xSBLXUShvcek6rADbr8hKM0pmNi',NULL),(14,'Bill','Smith','user11','$2a$11$1njw5D2Wzn/koQeAKyqj.OenF93WhfaIPJOuu7LYk94KAny2PwPZK',NULL),(15,'Bill','Smith','user12','$2a$11$AqAFP70RFDynoeKzPW60bOnq/.59.G4EEB4Nx4EWlNePhhsyvQ4ti',NULL);
/*!40000 ALTER TABLE `student` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-05-12 20:16:55
