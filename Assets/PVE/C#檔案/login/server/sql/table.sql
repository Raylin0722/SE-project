CREATE TABLE users (
    username VARCHAR(50) NOT NULL PRIMARY KEY,
    token VARCHAR(64) NOT NULL UNIQUE,
    hash VARCHAR(64) NOT NULL
);

CREATE TABLE usersdata (
  updateTime datetime,
  playerName varchar(50),
  token varchar(64) NOT NULL PRIMARY KEY,
  money int,
  expLevel int,
  expTotal int,
  `character` varchar(200),
  lineup varchar(100),
  tear int,
  castleLevel int,
  slingshotLevel int,
  clearance varchar(200),
  energy int,
  volume int,
  backVolume int,
  shock tinyint(1),
  remind tinyint(1),
  chestTime datetime,
  props varchar(200),
  faction int
);
