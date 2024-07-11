use emaildb;

ALTER TABLE Email ADD COLUMN `To` varchar(500) COLLATE utf8mb4_unicode_ci;
ALTER TABLE Email ADD COLUMN `Cc` varchar(500) COLLATE utf8mb4_unicode_ci;
ALTER TABLE Email ADD COLUMN `Bcc` varchar(500) COLLATE utf8mb4_unicode_ci;
ALTER TABLE Email ADD COLUMN `ReplyTo` varchar(500) COLLATE utf8mb4_unicode_ci;

