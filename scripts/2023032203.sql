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

 Date: 23/03/2023 00:07:43
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for EmailAttachment
-- ----------------------------
DROP TABLE IF EXISTS `EmailAttachment`;
CREATE TABLE `EmailAttachment` (
  `EmailAttachmentId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `EmailId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `FileName` varchar(255) utf8mb4_unicode_ci NOT NULL,
  `FileSize` varchar(20) NOT NULL,
  `SourceFileName` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`EmailAttachmentId`),
  KEY `FK_EmailAttachment_Email_EmailId` (`EmailId`),
  CONSTRAINT `FK_EmailAttachment_Email_EmailId` FOREIGN KEY (`EmailId`) REFERENCES `Email` (`EmailId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

SET FOREIGN_KEY_CHECKS = 1;
