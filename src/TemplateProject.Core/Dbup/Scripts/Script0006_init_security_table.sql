-- 创建用户角色表
CREATE TABLE user_role (
    id CHAR(36) NOT NULL PRIMARY KEY,
    user_id CHAR(36) NOT NULL,
    role_id CHAR(36) NOT NULL,
    deadline DATETIME NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NULL,
    created_by CHAR(36) NOT NULL,
    last_modified_by CHAR(36) NULL
);

CREATE INDEX IX_USER_ROLE_USER_ID ON user_role (user_id);
CREATE INDEX IX_USER_ROLE_ROLE_ID ON user_role (role_id);
CREATE INDEX IX_USER_ROLE_DEADLINE ON user_role (deadline);
CREATE UNIQUE INDEX IX_USER_ROLE_USER_ID_ROLE_ID ON user_role (user_id, role_id);

-- 创建角色表
CREATE TABLE role (
    id CHAR(36) NOT NULL PRIMARY KEY,
    pid CHAR(36) NOT NULL,
    name NVARCHAR(255) NOT NULL,
    description NVARCHAR(500) NOT NULL,
    code NVARCHAR(255) NOT NULL,
    sequence INT NOT NULL,
    status INT NOT NULL,
    built BIT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NULL,
    created_by CHAR(36) NOT NULL,
    last_modified_by CHAR(36) NULL
);

CREATE INDEX IX_ROLE_PID ON role (pid);
CREATE INDEX IX_ROLE_STATUS ON role (status);
CREATE INDEX IX_ROLE_SEQUENCE ON role (sequence);
CREATE INDEX IX_ROLE_CODE ON role (code);

-- 创建角色互斥表
CREATE TABLE role_conflict_matrix (
    id CHAR(36) NOT NULL PRIMARY KEY,
    role_a_id CHAR(36) NOT NULL,
    role_b_id CHAR(36) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NULL,
    created_by CHAR(36) NOT NULL,
    last_modified_by CHAR(36) NULL
);

CREATE INDEX IX_ROLE_CONFLICT_MATRIX_ROLE_A_ID ON role_conflict_matrix (role_a_id);
CREATE INDEX IX_ROLE_CONFLICT_MATRIX_ROLE_B_ID ON role_conflict_matrix (role_b_id);
CREATE UNIQUE INDEX IX_ROLE_CONFLICT_MATRIX_ROLE_A_ID_ROLE_B_ID ON role_conflict_matrix (role_a_id, role_b_id);

-- 创建角色权限表
CREATE TABLE role_permission (
    id CHAR(36) NOT NULL PRIMARY KEY,
    role_id CHAR(36) NOT NULL,
    permission_id CHAR(36) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NULL,
    created_by CHAR(36) NOT NULL,
    last_modified_by CHAR(36) NULL
);

CREATE INDEX IX_ROLE_PERMISSION_ROLE_ID ON role_permission (role_id);
CREATE INDEX IX_ROLE_PERMISSION_PERMISSION_ID ON role_permission (permission_id);
CREATE UNIQUE INDEX IX_ROLE_PERMISSION_ROLE_ID_PERMISSION_ID ON role_permission (role_id, permission_id);

-- 创建权限表
CREATE TABLE permission (
    id CHAR(36) NOT NULL PRIMARY KEY,
    pid CHAR(36) NOT NULL,
    type INT NOT NULL,
    name NVARCHAR(255) NOT NULL,
    description NVARCHAR(500) NOT NULL,
    code NVARCHAR(255) NOT NULL,
    sequence INT NOT NULL,
    status INT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NULL,
    created_by CHAR(36) NOT NULL,
    last_modified_by CHAR(36) NULL
);

CREATE INDEX IX_PERMISSION_PID ON permission (pid);
CREATE INDEX IX_PERMISSION_TYPE ON permission (type);
CREATE INDEX IX_PERMISSION_STATUS ON permission (status);
CREATE INDEX IX_PERMISSION_SEQUENCE ON permission (sequence);
CREATE INDEX IX_PERMISSION_CODE ON permission (code);

-- 创建权限菜单表
CREATE TABLE permission_menu (
    id CHAR(36) NOT NULL PRIMARY KEY,
    permission_id CHAR(36) NOT NULL,
    menu_id CHAR(36) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NULL,
    created_by CHAR(36) NOT NULL,
    last_modified_by CHAR(36) NULL
);

CREATE INDEX IX_PERMISSION_MENU_PERMISSION_ID ON permission_menu (permission_id);
CREATE INDEX IX_PERMISSION_MENU_MENU_ID ON permission_menu (menu_id);
CREATE UNIQUE INDEX IX_PERMISSION_MENU_PERMISSION_ID_MENU_ID ON permission_menu (permission_id, menu_id);

-- 创建菜单表
CREATE TABLE menu (
    id CHAR(36) NOT NULL PRIMARY KEY,
    pid CHAR(36) NOT NULL,
    type INT NOT NULL,
    name NVARCHAR(255) NOT NULL,
    description NVARCHAR(500) NOT NULL,
    path NVARCHAR(255) NOT NULL,
    icon NVARCHAR(255) NOT NULL,
    sequence INT NOT NULL,
    status INT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NULL,
    created_by CHAR(36) NOT NULL,
    last_modified_by CHAR(36) NULL
);

CREATE INDEX IX_MENU_PID ON menu (pid);
CREATE INDEX IX_MENU_TYPE ON menu (type);
CREATE INDEX IX_MENU_STATUS ON menu (status);
CREATE INDEX IX_MENU_SEQUENCE ON menu (sequence);

-- 创建权限API表
CREATE TABLE permission_api (
    id CHAR(36) NOT NULL PRIMARY KEY,
    permission_id CHAR(36) NOT NULL,
    endpoint NVARCHAR(255) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NULL,
    created_by CHAR(36) NOT NULL,
    last_modified_by CHAR(36) NULL
);

CREATE INDEX IX_PERMISSION_API_PERMISSION_ID ON permission_api (permission_id);
CREATE INDEX IX_PERMISSION_API_ENDPOINT ON permission_api (endpoint);