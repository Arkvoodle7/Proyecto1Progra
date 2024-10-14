CREATE DATABASE  IF NOT EXISTS `pagosmovilessocketexterno` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `pagosmovilessocketexterno`;
-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: localhost    Database: pagosmovilessocketexterno
-- ------------------------------------------------------
-- Server version	8.0.39

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
-- Table structure for table `bitacora`
--

DROP TABLE IF EXISTS `bitacora`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bitacora` (
  `id_bitacora` int NOT NULL AUTO_INCREMENT,
  `fecha_hora` datetime DEFAULT CURRENT_TIMESTAMP,
  `trama_recibida` text,
  `trama_respuesta` text,
  PRIMARY KEY (`id_bitacora`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bitacora`
--

LOCK TABLES `bitacora` WRITE;
/*!40000 ALTER TABLE `bitacora` DISABLE KEYS */;
INSERT INTO `bitacora` VALUES (1,'2024-10-13 15:22:36','{\"telefono\":\"34343434\",\"monto\":10,\"descripcion\":\"sdf\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(2,'2024-10-13 16:30:48','{\"telefono\":\"21212121\",\"monto\":10,\"descripcion\":\"dgdf\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(3,'2024-10-13 19:18:50','{\"telefono\":\"65656565\",\"monto\":10,\"descripcion\":\"hjb\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(4,'2024-10-13 19:42:05','{\"telefono\":\"21212121\",\"monto\":10,\"descripcion\":\"fgd\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(5,'2024-10-13 19:48:21','{\"telefono\":\"56655656\",\"monto\":20,\"descripcion\":\"gwsgws\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(6,'2024-10-13 19:48:34','{\"telefono\":\"64564521\",\"monto\":123,\"descripcion\":\"sdf\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(7,'2024-10-13 19:51:55','{\"telefono\":\"42424242\",\"monto\":20,\"descripcion\":\"fhg\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(8,'2024-10-13 19:53:22','{\"telefono\":\"22222224\",\"monto\":90,\"descripcion\":\"erg\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(9,'2024-10-13 19:53:31','{\"telefono\":\"55555552\",\"monto\":60,\"descripcion\":\"ery\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(10,'2024-10-13 20:09:23','{\"telefono\":\"44444441\",\"monto\":500,\"descripcion\":\"kjyh\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(11,'2024-10-13 20:09:28','{\"telefono\":\"44444445\",\"monto\":200,\"descripcion\":\"dfgd\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(12,'2024-10-13 20:19:35','{\"telefono\":\"55555551\",\"monto\":20,\"descripcion\":\"hydf\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(13,'2024-10-13 20:58:38','{\"telefono\":\"55555553\",\"monto\":20,\"descripcion\":\"jkin\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(14,'2024-10-13 21:13:46','{\"telefono\":\"77777771\",\"monto\":50,\"descripcion\":\"jtyh\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(15,'2024-10-13 21:22:13','{\"telefono\":\"11111112\",\"monto\":30,\"descripcion\":\"tery\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(16,'2024-10-13 21:30:30','{\"telefono\":\"66666664\",\"monto\":10,\"descripcion\":\"fgh\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}'),(17,'2024-10-13 21:35:57','{\"telefono\":\"44444443\",\"monto\":50,\"descripcion\":\"dgf\"}','{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}');
/*!40000 ALTER TABLE `bitacora` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-10-14  9:39:32
