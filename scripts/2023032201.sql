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
  CONSTRAINT `FK_EmailFolder_Email_EmailFolderId` FOREIGN KEY (`EmailFolderId`) REFERENCES `EmailFolder` (`FolderId`) ON DELETE NO ACTION ON UPDATE NO ACTION
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