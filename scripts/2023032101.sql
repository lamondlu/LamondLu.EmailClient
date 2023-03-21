DROP TABLE IF EXISTS `EmailFolder`;
CREATE TABLE `EmailFolder` (
  `FolderId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `EmailConnectorId` char(36) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `FolderFullPath` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `FolderName` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `IsDeleted` bit(1) DEFAULT NULL,
  `LastEmailId` bigint(20) DEFAULT NULL,
  `LastValidityId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`FolderId`) USING BTREE,
  KEY `FK_EmailConnecor_EmailFolder_EmailConnectorId` (`EmailConnectorId`),
  CONSTRAINT `FK_EmailConnecor_EmailFolder_EmailConnectorId` FOREIGN KEY (`EmailConnectorId`) REFERENCES `EmailConnector` (`EmailConnectorId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Table structure for EmailReceipt
-- ----------------------------
DROP TABLE IF EXISTS `EmailReceipt`;
CREATE TABLE `EmailReceipt` (
  `EmailReceiptId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `EmailId` char(36) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Email` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Type` tinyint(1) DEFAULT NULL,
  `DisplayName` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`EmailReceiptId`),
  KEY `FK_EmailReceipt_Email_EmailId` (`EmailId`),
  CONSTRAINT `FK_EmailReceipt_Email_EmailId` FOREIGN KEY (`EmailId`) REFERENCES `Email` (`EmailId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

SET FOREIGN_KEY_CHECKS = 1;
