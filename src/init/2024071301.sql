use emaildb;


DROP TABLE IF EXISTS `emailrule`;
CREATE TABLE `emailrule` (
  `EmailRuleId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `RuleName` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `RuleType` smallint NOT NULL,
  `CreatedTime` datetime DEFAULT NULL,
  `CreatedBy` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `UpdatedTime` datetime DEFAULT NULL,
  `UpdatedBy` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT '0',
  `TerminateIfMatch` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`EmailRuleId`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;


DROP TABLE IF EXISTS `replyemailrule`;
CREATE TABLE `replyemailrule` (
  `EmailRuleId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `EmailTemplateId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`EmailRuleId`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;


DROP TABLE IF EXISTS `forwardemailrule`;
CREATE TABLE `forwardemailrule` (
  `EmailRuleId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ConditionType` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ConditionValue` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ForwardEmails` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`EmailRuleId`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;


DROP TABLE IF EXISTS `classifyemailrule`;
CREATE TABLE `classifyemailrule` (
  `EmailRuleId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `TagId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`EmailRuleId`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;


DROP TABLE IF EXISTS `emailconnectorrule`;
CREATE TABLE `emailconnectorrule` (
  `EmailConnectorId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `EmailRuleId` char(36) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Order` smallint NOT NULL,
  `CreatedTime` datetime DEFAULT NULL,
  `CreatedBy` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `UpdatedTime` datetime DEFAULT NULL,
  `UpdatedBy` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`EmailConnectorId`,`EmailRuleId`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;