
-- ----------------------------
-- Table structure for tag
-- ----------------------------
DROP TABLE IF EXISTS `tag`;
CREATE TABLE `tag` (
  `TagId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `TagName` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Color` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Description` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `CreatedTime` date NOT NULL,
  `CreatedBy` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `UpdatedTime` date NOT NULL,
  `UpdatedBy` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT '0',
  `Status` smallint NOT NULL DEFAULT '0',
  PRIMARY KEY (`TagId`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Table structure for emailtags
-- ----------------------------
DROP TABLE IF EXISTS `emailtags`;
CREATE TABLE `emailtags` (
  `EmailId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `TagId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `CreatedTime` datetime DEFAULT NULL,
  `CreatedBy` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `UpdatedTime` datetime DEFAULT NULL,
  `UpdatedBy` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT '0'
  PRIMARY KEY (`EmailId`,`TagId`) USING BTREE,
  KEY `FK_emailtag_tagid` (`TagId`),
  CONSTRAINT `FK_emailtag_emailid` FOREIGN KEY (`EmailId`) REFERENCES `email` (`EmailId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_emailtag_tagid` FOREIGN KEY (`TagId`) REFERENCES `tag` (`TagId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;

SET FOREIGN_KEY_CHECKS = 1;
