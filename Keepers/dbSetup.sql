CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

-- STUB Keeps!

DROP TABLE keeps;

TRUNCATE TABLE keeps;

CREATE TABLE IF NOT EXISTS keeps(
  id INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(255) NOT NULL,
  description TEXT NOT NULL,
  img VARCHAR(500) NOT NULL,
  views INT NOT NULL DEFAULT 0,
  kept INT NOT NULL DEFAULT 0,
  shares INT NOT NULL DEFAULT 0,
  creatorId VARCHAR(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO keeps
(name, description, img, creatorId)
VALUES
('Keep Dogs', 'In this keep we show pictures of dogs', 'https://images.unsplash.com/photo-1517849845537-4d257902454a?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8ZG9nfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60', '63055ac4695fb4011243e2ce');

INSERT INTO keeps
(name, description, img, creatorId)
VALUES
('Keep Cats', 'In this keep we show pictures of cats', 'https://images.unsplash.com/photo-1514888286974-6c03e2ca1dba?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8Y2F0fGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60', '63237c712d0c7a123937383b' );

-- NOTE get keeps by Id with everything attached
SELECT 
k.*,
a.*
FROM keeps k
JOIN accounts a ON k.creatorId = a.id
WHERE k.creatorId = '63237c712d0c7a123937383b';

-- STUB Vaults!
DROP TABLE vaults;

CREATE TABLE IF NOT EXISTS vaults(
  id INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(255) NOT NULL,
  description TEXT NOT NULL,
  img VARCHAR(500) NOT NULL,
  isPrivate BOOLEAN NOT NULL DEFAULT false,
  creatorId VARCHAR(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO vaults
(name, description, img, creatorId)
VALUES
('Vault O Cats', 'We got a vault of cat keeps', 'https://images.unsplash.com/photo-1495360010541-f48722b34f7d?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=736&q=80', '63237c712d0c7a123937383b' );

INSERT INTO vaults
(name, description, img, creatorId)
VALUES
('Vault O Dogs', 'We got a vault of dog keeps', 'https://images.unsplash.com/photo-1587300003388-59208cc962cb?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8NXx8ZG9nfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60', '63055ac4695fb4011243e2ce');

INSERT INTO vaults
(name, description, isPrivate, img, creatorId)
VALUES
('Test name', 'Test desc', true, 'https://images.unsplash.com/photo-1587300003388-59208cc962cb?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8NXx8ZG9nfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60', '63055ac4695fb4011243e2ce');
SELECT LAST_INSERT_ID();

SELECT 
v.*,
a.*
FROM vaults v
JOIN accounts a ON v.creatorId = a.id
WHERE v.creatorId = '63237c712d0c7a123937383b';


-- STUB Vaultkeeps!

DROP TABLE vaultkeeps;
CREATE TABLE IF NOT EXISTS vaultkeeps(
  id INT AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',

  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY(vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY(keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO vaultkeeps
(creatorId, vaultId, keepId)
VALUES
('63237c712d0c7a123937383b', 1, 2);

INSERT INTO vaultkeeps
(creatorId, vaultId, keepId)
VALUES
('63055ac4695fb4011243e2ce', 2, 1);

-- NOTE get vaultkeeps by Id with everything attached
SELECT
vk.*,
k.*,
a.*,
FROM vaultkeeps vk
JOIN accounts a ON vk.creatorId = a.id
JOIN keeps k ON vk.keepId = k.id
WHERE vk.vaultId = 47;

