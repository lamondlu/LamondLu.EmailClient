CREATE DATABASE IF NOT EXISTS `$DB_NAME` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

use $DB_NAME;

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
  `SMTPServer` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `SMTPPort` varchar(5) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `POP3Server` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `POP3Port` varchar(5) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `IMAPServer` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `IMAPPort` varchar(5) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
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

ALTER TABLE EmailConnector ADD COLUMN IsDeleted BIT(1) NOT NULL;


SET FOREIGN_KEY_CHECKS = 1;

DROP TABLE IF EXISTS `Email`;
CREATE TABLE `Email` (
  `EmailId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `EmailConnectorId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Subject` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ReceivedDate` datetime NOT NULL,
  `EmailFolderId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Id` int(11) NOT NULL,
  `Validity` int(11) NOT NULL,
  `CreatedTime` datetime NOT NULL,
  `Sender` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `MessageId` char(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`EmailId`),
  KEY `FK_EmailConnecoctor_Email_EmailConnectorId` (`EmailConnectorId`),
  KEY `FK_EmailFolder_Email_EmailFolderId` (`EmailFolderId`),
  KEY `Id` (`Id`),
  CONSTRAINT `FK_EmailConnecoctor_Email_EmailConnectorId` FOREIGN KEY (`EmailConnectorId`) REFERENCES `EmailConnector` (`EmailConnectorId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_EmailFolder_Email_EmailFolderId` FOREIGN KEY (`EmailFolderId`) REFERENCES `EmailFolder` (`EmailFolderId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Table structure for EmailBody
-- ----------------------------
DROP TABLE IF EXISTS `EmailBody`;
CREATE TABLE `EmailBody` (
  `EmailId` char(36) NOT NULL,
  `EmailBody` longtext COLLATE utf8mb4_unicode_ci,
  `EmailHtmlBody` longtext COLLATE utf8mb4_unicode_ci,
  PRIMARY KEY (`EmailId`),
  CONSTRAINT `FK_EmailBody_Email_EmailId` FOREIGN KEY (`EmailId`) REFERENCES `Email` (`EmailId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

ALTER TABLE Email ADD UNIQUE KEY `UN_MessageId` (`MessageId`) USING BTREE;

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for EmailAttachment
-- ----------------------------
DROP TABLE IF EXISTS `EmailAttachment`;
CREATE TABLE `EmailAttachment` (
  `EmailAttachmentId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `EmailId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `FileName` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `FileSize` varchar(20) NOT NULL,
  `SourceFileName` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`EmailAttachmentId`),
  KEY `FK_EmailAttachment_Email_EmailId` (`EmailId`),
  CONSTRAINT `FK_EmailAttachment_Email_EmailId` FOREIGN KEY (`EmailId`) REFERENCES `Email` (`EmailId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

SET FOREIGN_KEY_CHECKS = 1;

ALTER TABLE Email ADD COLUMN `IsRead` BIT NOT NULL DEFAULT 0;

DROP TABLE IF EXISTS `EmailRecipient`;
CREATE TABLE `EmailReceipt` (
  `EmailRecipientId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `EmailId` char(36) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Email` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Type` tinyint(1) DEFAULT NULL,
  `DisplayName` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`EmailRecipientId`),
  KEY `FK_EmailRecipient_Email_EmailId` (`EmailId`),
  CONSTRAINT `FK_EmailRecipient_Email_EmailId` FOREIGN KEY (`EmailId`) REFERENCES `Email` (`EmailId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

