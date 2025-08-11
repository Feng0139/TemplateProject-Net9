CREATE TABLE system_licenses (
     id CHAR(36) NOT NULL PRIMARY KEY,
     api_key VARCHAR(255) NOT NULL,
     user_name VARCHAR(100) NOT NULL,
     created_by CHAR(36) NOT NULL,
     created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
     last_modified_by CHAR(36) NULL,
     updated_at DATETIME NULL
);

CREATE UNIQUE INDEX IX_system_licenses_api_key ON system_licenses (api_key);
CREATE INDEX IX_system_licenses_user_name ON system_licenses (user_name);