CREATE DATABASE IF NOT EXISTS `weatherr` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `weatherr`;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `sensor`
--

CREATE TABLE `sensor` (
  `Date` datetime NOT NULL,
  `Temperature` float DEFAULT NULL,
  `Pressure` float DEFAULT NULL,
  `Humidity` float DEFAULT NULL,
  `Brightness` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Gegevens worden geëxporteerd voor tabel `sensor`
--

INSERT INTO `sensor` (`Date`, `Temperature`, `Pressure`, `Humidity`, `Brightness`) VALUES
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

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `sensor`
--
ALTER TABLE `sensor`
  ADD PRIMARY KEY (`Date`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
