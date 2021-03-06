-- MySQL dump 10.13  Distrib 8.0.13, for Win64 (x86_64)
--
-- Host: localhost    Database: kvantcard
-- ------------------------------------------------------
-- Server version	8.0.13

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8mb4 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `document`
--

DROP TABLE IF EXISTS `document`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `document` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  `type_id` mediumint(9) DEFAULT NULL,
  `file_type_id` mediumint(9) DEFAULT NULL,
  `binary_file` blob,
  PRIMARY KEY (`ID`),
  KEY `type_id` (`type_id`),
  KEY `file_type_id` (`file_type_id`),
  CONSTRAINT `document_ibfk_1` FOREIGN KEY (`type_id`) REFERENCES `document_types` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `document_ibfk_2` FOREIGN KEY (`file_type_id`) REFERENCES `file_types` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `document_set`
--

DROP TABLE IF EXISTS `document_set`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `document_set` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `group_id` mediumint(9) DEFAULT NULL,
  `document_id` mediumint(9) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `group_id` (`group_id`),
  KEY `document_id` (`document_id`),
  CONSTRAINT `document_set_ibfk_1` FOREIGN KEY (`group_id`) REFERENCES `document_set_group` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `document_set_ibfk_2` FOREIGN KEY (`document_id`) REFERENCES `document` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `document_set_group`
--

DROP TABLE IF EXISTS `document_set_group`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `document_set_group` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `document_types`
--

DROP TABLE IF EXISTS `document_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `document_types` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `file_types`
--

DROP TABLE IF EXISTS `file_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `file_types` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `foreign_names`
--

DROP TABLE IF EXISTS `foreign_names`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `foreign_names` (
  `id` int(11) NOT NULL,
  `name` varchar(25) NOT NULL,
  `meaning` varchar(1000) NOT NULL,
  `gender` varchar(6) NOT NULL,
  `origin` varchar(24) NOT NULL,
  `PeoplesCount` int(11) DEFAULT NULL,
  `WhenPeoplesCount` datetime DEFAULT NULL,
  `Source` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `kvantum`
--

DROP TABLE IF EXISTS `kvantum`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `kvantum` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `kvantum_group`
--

DROP TABLE IF EXISTS `kvantum_group`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `kvantum_group` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `kvantum_mentors_hist`
--

DROP TABLE IF EXISTS `kvantum_mentors_hist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `kvantum_mentors_hist` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `mentor_id` mediumint(9) NOT NULL,
  `kvantum_id` mediumint(9) NOT NULL,
  `start_date` mediumint(9) NOT NULL,
  `end_date` mediumint(9) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `kvantum_id` (`kvantum_id`),
  KEY `mentor_id` (`mentor_id`),
  CONSTRAINT `kvantum_mentors_hist_ibfk_1` FOREIGN KEY (`kvantum_id`) REFERENCES `kvantum` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `kvantum_mentors_hist_ibfk_2` FOREIGN KEY (`mentor_id`) REFERENCES `mentor` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `levels`
--

DROP TABLE IF EXISTS `levels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `levels` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `mentor`
--

DROP TABLE IF EXISTS `mentor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `mentor` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `last_name` varchar(100) DEFAULT NULL,
  `first_name` varchar(100) DEFAULT NULL,
  `middle_name` varchar(100) DEFAULT NULL,
  `phone` varchar(10) DEFAULT NULL,
  `primary_kvantum_id` mediumint(9) DEFAULT NULL,
  `secondary_kvantum_id` mediumint(9) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `primary_kvantum_id` (`primary_kvantum_id`),
  KEY `secondary_kvantum_id` (`secondary_kvantum_id`),
  CONSTRAINT `mentor_ibfk_1` FOREIGN KEY (`primary_kvantum_id`) REFERENCES `kvantum` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `mentor_ibfk_2` FOREIGN KEY (`secondary_kvantum_id`) REFERENCES `kvantum` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `mentor_social_activity`
--

DROP TABLE IF EXISTS `mentor_social_activity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `mentor_social_activity` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `mentor_id` mediumint(9) NOT NULL,
  `project_id` mediumint(9) NOT NULL,
  `social_activity_id` mediumint(9) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `mentor_id` (`mentor_id`),
  KEY `project_id` (`project_id`),
  KEY `social_activity_id` (`social_activity_id`),
  CONSTRAINT `mentor_social_activity_ibfk_1` FOREIGN KEY (`mentor_id`) REFERENCES `mentor` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `mentor_social_activity_ibfk_2` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `mentor_social_activity_ibfk_3` FOREIGN KEY (`social_activity_id`) REFERENCES `social_activity` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `parent`
--

DROP TABLE IF EXISTS `parent`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `parent` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `last_name` varchar(100) DEFAULT NULL,
  `first_name` varchar(100) DEFAULT NULL,
  `middle_name` varchar(100) DEFAULT NULL,
  `phone` varchar(10) DEFAULT NULL,
  `status_id` mediumint(9) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `status_id` (`status_id`),
  CONSTRAINT `parent_ibfk_1` FOREIGN KEY (`status_id`) REFERENCES `parent_status` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `parent_status`
--

DROP TABLE IF EXISTS `parent_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `parent_status` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `personnel`
--

DROP TABLE IF EXISTS `personnel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `personnel` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `last_name` varchar(100) DEFAULT NULL,
  `first_name` varchar(100) DEFAULT NULL,
  `middle_name` varchar(100) DEFAULT NULL,
  `position_id` mediumint(9) DEFAULT NULL,
  `phone` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `position_id` (`position_id`),
  CONSTRAINT `personnel_ibfk_1` FOREIGN KEY (`position_id`) REFERENCES `positions` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `positions`
--

DROP TABLE IF EXISTS `positions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `positions` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `program`
--

DROP TABLE IF EXISTS `program`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `program` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `level_id` mediumint(9) NOT NULL,
  `kvantum_id` mediumint(9) NOT NULL,
  `group_id` mediumint(9) NOT NULL,
  `smena_id` mediumint(9) NOT NULL,
  `start_date` mediumint(9) NOT NULL,
  `end_date` mediumint(9) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `level_id` (`level_id`),
  KEY `kvantum_id` (`kvantum_id`),
  KEY `group_id` (`group_id`),
  KEY `smena_id` (`smena_id`),
  CONSTRAINT `program_ibfk_1` FOREIGN KEY (`level_id`) REFERENCES `levels` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `program_ibfk_2` FOREIGN KEY (`kvantum_id`) REFERENCES `kvantum` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `program_ibfk_3` FOREIGN KEY (`group_id`) REFERENCES `kvantum_group` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `program_ibfk_4` FOREIGN KEY (`smena_id`) REFERENCES `smena` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `project_status`
--

DROP TABLE IF EXISTS `project_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `project_status` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `project_type`
--

DROP TABLE IF EXISTS `project_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `project_type` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `projects`
--

DROP TABLE IF EXISTS `projects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `projects` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  `project_type_id` mediumint(9) NOT NULL,
  `document_set_group_id` mediumint(9) DEFAULT NULL,
  `project_status_id` mediumint(9) DEFAULT NULL,
  `start_date` date NOT NULL,
  `end_date` date DEFAULT NULL,
  `description` varchar(4000) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `document_set_group_id` (`document_set_group_id`),
  KEY `project_type_id` (`project_type_id`),
  KEY `project_status_id` (`project_status_id`),
  CONSTRAINT `projects_ibfk_1` FOREIGN KEY (`document_set_group_id`) REFERENCES `document_set_group` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `projects_ibfk_2` FOREIGN KEY (`project_type_id`) REFERENCES `project_type` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `projects_ibfk_3` FOREIGN KEY (`project_status_id`) REFERENCES `project_status` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `publict_type`
--

DROP TABLE IF EXISTS `publict_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `publict_type` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `russian_names`
--

DROP TABLE IF EXISTS `russian_names`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `russian_names` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `Name` varchar(20) DEFAULT NULL,
  `Sex` char(1) DEFAULT NULL,
  `PeoplesCount` smallint(6) DEFAULT NULL,
  `WhenPeoplesCount` timestamp NULL DEFAULT NULL,
  `Source` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=71432 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `russian_surnames`
--

DROP TABLE IF EXISTS `russian_surnames`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `russian_surnames` (
  `ID` int(11) NOT NULL,
  `Surname` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Sex` varchar(1) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `PeoplesCount` int(11) DEFAULT NULL,
  `WhenPeoplesCount` datetime DEFAULT NULL,
  `Source` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `smena`
--

DROP TABLE IF EXISTS `smena`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `smena` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `social_activity`
--

DROP TABLE IF EXISTS `social_activity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `social_activity` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `type_id` mediumint(9) NOT NULL,
  `publict_type_id` mediumint(9) NOT NULL,
  `project_id` mediumint(9) NOT NULL,
  `start_date` date NOT NULL,
  `end_date` date DEFAULT NULL,
  `document_set_group_id` mediumint(9) DEFAULT NULL,
  `description` varchar(4000) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `project_id` (`project_id`),
  KEY `type_id` (`type_id`),
  KEY `publict_type_id` (`publict_type_id`),
  CONSTRAINT `social_activity_ibfk_1` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `social_activity_ibfk_2` FOREIGN KEY (`type_id`) REFERENCES `social_activity_type` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `social_activity_ibfk_3` FOREIGN KEY (`publict_type_id`) REFERENCES `publict_type` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `social_activity_type`
--

DROP TABLE IF EXISTS `social_activity_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `social_activity_type` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `student`
--

DROP TABLE IF EXISTS `student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `student` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `last_name` varchar(100) DEFAULT NULL,
  `first_name` varchar(100) DEFAULT NULL,
  `middle_name` varchar(100) DEFAULT NULL,
  `birth_date` date DEFAULT NULL,
  `address` varchar(1000) DEFAULT NULL,
  `phone` varchar(10) DEFAULT NULL,
  `school` varchar(10) DEFAULT NULL,
  `parent1_id` mediumint(9) DEFAULT NULL,
  `parent2_id` mediumint(9) DEFAULT NULL,
  `document_set_group_id` mediumint(9) DEFAULT NULL,
  `register_date` date DEFAULT NULL,
  `current_program_id` mediumint(9) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `parent1_id` (`parent1_id`),
  KEY `parent2_id` (`parent2_id`),
  KEY `document_set_group_id` (`document_set_group_id`),
  KEY `current_program_id` (`current_program_id`),
  CONSTRAINT `student_ibfk_1` FOREIGN KEY (`parent1_id`) REFERENCES `parent` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `student_ibfk_2` FOREIGN KEY (`parent2_id`) REFERENCES `parent` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `student_ibfk_3` FOREIGN KEY (`document_set_group_id`) REFERENCES `document_set_group` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `student_ibfk_4` FOREIGN KEY (`current_program_id`) REFERENCES `program` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `student_program_hist`
--

DROP TABLE IF EXISTS `student_program_hist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `student_program_hist` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `student_id` mediumint(9) NOT NULL,
  `level_id` mediumint(9) NOT NULL,
  `kvantum_id` mediumint(9) NOT NULL,
  `group_id` mediumint(9) NOT NULL,
  `smena_id` mediumint(9) NOT NULL,
  `start_date` mediumint(9) NOT NULL,
  `end_date` mediumint(9) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `student_id` (`student_id`),
  KEY `level_id` (`level_id`),
  KEY `kvantum_id` (`kvantum_id`),
  KEY `group_id` (`group_id`),
  KEY `smena_id` (`smena_id`),
  CONSTRAINT `student_program_hist_ibfk_1` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `student_program_hist_ibfk_2` FOREIGN KEY (`level_id`) REFERENCES `levels` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `student_program_hist_ibfk_3` FOREIGN KEY (`kvantum_id`) REFERENCES `kvantum` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `student_program_hist_ibfk_4` FOREIGN KEY (`group_id`) REFERENCES `kvantum_group` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `student_program_hist_ibfk_5` FOREIGN KEY (`smena_id`) REFERENCES `smena` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `student_social_activity`
--

DROP TABLE IF EXISTS `student_social_activity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `student_social_activity` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `student_id` mediumint(9) NOT NULL,
  `project_id` mediumint(9) NOT NULL,
  `social_activity_id` mediumint(9) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `student_id` (`student_id`),
  KEY `project_id` (`project_id`),
  KEY `social_activity_id` (`social_activity_id`),
  CONSTRAINT `student_social_activity_ibfk_1` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `student_social_activity_ibfk_2` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `student_social_activity_ibfk_3` FOREIGN KEY (`social_activity_id`) REFERENCES `social_activity` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team`
--

DROP TABLE IF EXISTS `team`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `team` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_activity`
--

DROP TABLE IF EXISTS `team_activity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `team_activity` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `team_id` mediumint(9) NOT NULL,
  `project_id` mediumint(9) NOT NULL,
  `start_date` mediumint(9) NOT NULL,
  `end_date` mediumint(9) DEFAULT NULL,
  `document_set_group_id` mediumint(9) DEFAULT NULL,
  `description` varchar(4000) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `team_id` (`team_id`),
  KEY `project_id` (`project_id`),
  KEY `document_set_group_id` (`document_set_group_id`),
  CONSTRAINT `team_activity_ibfk_1` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `team_activity_ibfk_2` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `team_activity_ibfk_3` FOREIGN KEY (`document_set_group_id`) REFERENCES `document_set_group` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_build`
--

DROP TABLE IF EXISTS `team_build`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `team_build` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `team_id` mediumint(9) NOT NULL,
  `project_id` mediumint(9) NOT NULL,
  `student_id` mediumint(9) NOT NULL,
  `role_id` mediumint(9) NOT NULL,
  `start_date` mediumint(9) NOT NULL,
  `end_date` mediumint(9) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `team_id` (`team_id`),
  KEY `project_id` (`project_id`),
  KEY `role_id` (`role_id`),
  KEY `student_id` (`student_id`),
  CONSTRAINT `team_build_ibfk_1` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `team_build_ibfk_2` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `team_build_ibfk_3` FOREIGN KEY (`role_id`) REFERENCES `team_role` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `team_build_ibfk_4` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_mentor`
--

DROP TABLE IF EXISTS `team_mentor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `team_mentor` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `team_id` mediumint(9) NOT NULL,
  `project_id` mediumint(9) NOT NULL,
  `mentor_id` mediumint(9) NOT NULL,
  `start_date` mediumint(9) NOT NULL,
  `end_date` mediumint(9) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `team_id` (`team_id`),
  KEY `project_id` (`project_id`),
  KEY `mentor_id` (`mentor_id`),
  CONSTRAINT `team_mentor_ibfk_1` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `team_mentor_ibfk_2` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `team_mentor_ibfk_3` FOREIGN KEY (`mentor_id`) REFERENCES `mentor` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_role`
--

DROP TABLE IF EXISTS `team_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `team_role` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_social_activity`
--

DROP TABLE IF EXISTS `team_social_activity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `team_social_activity` (
  `ID` mediumint(9) NOT NULL AUTO_INCREMENT,
  `team_id` mediumint(9) NOT NULL,
  `project_id` mediumint(9) NOT NULL,
  `social_activity_id` mediumint(9) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `team_id` (`team_id`),
  KEY `project_id` (`project_id`),
  KEY `social_activity_id` (`social_activity_id`),
  CONSTRAINT `team_social_activity_ibfk_1` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `team_social_activity_ibfk_2` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `team_social_activity_ibfk_3` FOREIGN KEY (`social_activity_id`) REFERENCES `social_activity` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-01-23 15:35:06
