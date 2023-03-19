/*
 Navicat Premium Data Transfer

 Source Server         : master
 Source Server Type    : MySQL
 Source Server Version : 50735
 Source Host           : localhost:3339
 Source Schema         : ecdb

 Target Server Type    : MySQL
 Target Server Version : 50735
 File Encoding         : 65001

 Date: 19/03/2023 16:53:55
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for EmailConnector
-- ----------------------------
DROP TABLE IF EXISTS `EmailConnector`;
CREATE TABLE `EmailConnector` (
  `EmailConnectorId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Name` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `EmailAddress` varchar(150) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `UserName` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Password` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL,
  `IPv4` varchar(15) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Port` varchar(5) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `IPv6` varchar(128) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `EnableSSL` bit(1) DEFAULT NULL,
  `Description` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Type` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`EmailConnectorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Table structure for EmailFolder
-- ----------------------------
DROP TABLE IF EXISTS `EmailFolder`;
CREATE TABLE `EmailFolder` (
  `EmailFolderId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `EmailConnectorId` char(36) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `FolderFullPath` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `FolderName` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `IsDeleted` bit(1) DEFAULT NULL,
  `LastEmailId` bigint(20) DEFAULT NULL,
  `LastValidityId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`EmailFolderId`),
  KEY `FK_EmailConnecor_EmailFolder_EmailConnectorId` (`EmailConnectorId`),
  CONSTRAINT `FK_EmailConnecor_EmailFolder_EmailConnectorId` FOREIGN KEY (`EmailConnectorId`) REFERENCES `EmailConnector` (`EmailConnectorId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

SET FOREIGN_KEY_CHECKS = 1;
0ea1b296-7d6e-4b33-bd95-a97b33cea751