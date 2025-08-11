ALTER TABLE `user_account` MODIFY COLUMN `update_at` datetime(3) NULL;
ALTER TABLE `user_third_party` MODIFY COLUMN `update_at` datetime(3) NULL;

ALTER TABLE `user_third_party` CHANGE COLUMN `expires_at` `access_token_expires_at` datetime(3) NULL;
ALTER TABLE `user_third_party` ADD COLUMN `refresh_token_expires_at` datetime(3) NULL;