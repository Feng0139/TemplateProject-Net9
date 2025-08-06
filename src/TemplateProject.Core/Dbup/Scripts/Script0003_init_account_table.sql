CREATE TABLE IF NOT EXISTS `user_account` (
    `id` CHAR(36) NOT NULL,
    `name` VARCHAR(64) NOT NULL,
    `suffix` CHAR(4) NOT NULL default '0000',
    `password` VARCHAR(255) NOT NULL,
    `created_at` DATETIME NOT NULL,
    `update_at` DATETIME NOT NULL,
    PRIMARY KEY (`id`)
) CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `user_third_party`
(
    `id` CHAR(36) NOT NULL,
    `user_id` CHAR(36) NOT NULL,
    `provider` VARCHAR(255) NOT NULL,
    `third_party_user_id` VARCHAR(255) NOT NULL,
    `access_token` VARCHAR(512) NOT NULL DEFAULT '',
    `refresh_token` VARCHAR(512) NOT NULL DEFAULT '',
    `expires_at` DATETIME NOT NULL,
    `extra_data` VARCHAR(2048) NOT NULL DEFAULT '',
    `created_at` DATETIME NOT NULL,
    `update_at` DATETIME NOT NULL,
    PRIMARY KEY (`id`)
) CHARSET=utf8mb4;