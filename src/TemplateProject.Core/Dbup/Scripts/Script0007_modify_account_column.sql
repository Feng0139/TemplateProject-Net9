ALTER TABLE `user_account`
    ADD COLUMN `level` INT,
    ADD COLUMN `source` INT;

UPDATE `user_account` SET `level` = 0 WHERE `level` IS NULL;
UPDATE `user_account` SET `source` = 0 WHERE `source` IS NULL;

ALTER TABLE `user_account`
    MODIFY COLUMN `level` INT NOT NULL DEFAULT 0,
    MODIFY COLUMN `source` INT NOT NULL DEFAULT 0;