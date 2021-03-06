DROP Database weatherr;

CREATE DATABASE weatherr;
USE weatherr;


CREATE TABLE sensor (
  `Date` datetime NOT NULL,
  `Temperature` float DEFAULT NULL,
  `Pressure` float DEFAULT NULL,
  `Humidity` float DEFAULT NULL,
  `Brightness` float DEFAULT NULL,
   primary key(Date)
)


INSERT INTO sensor VALUES
('2016-01-16 18:00:00', 19, 1.005, 0.2, 0.6),
('2016-02-16 18:00:00', 13, 1.005, 0.2, 0.6),
('2016-03-16 18:00:00', 10, 1.005, 0.2, 0.6),
('2016-04-16 18:00:00', 10, 1.005, 0.2, 0.6),
('2016-05-16 18:00:00', 1, 1.005, 0.2, 0.6),
('2016-11-10 18:00:00', 14, 1.005, 0.2, 0.6),
('2016-11-16 13:00:00', 20, 1.005, 0.2, 0.6),
('2016-11-16 18:00:00', 20, 1.005, 0.2, 0.6),
('2016-11-17 18:00:00', 21, 0.998, 0.5, 0.6),
('2016-11-17 19:00:00', 19, 0.986, 0.4, 0.5),
('2016-11-17 20:00:00', 18, 0.975, 0.33, 0.4),
('2016-11-17 21:00:00', 15, 0.983, 0.2556, 0.3);
