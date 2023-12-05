-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Дек 05 2023 г., 17:43
-- Версия сервера: 8.0.30
-- Версия PHP: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `aksis`
--

-- --------------------------------------------------------

--
-- Структура таблицы `admin`
--

CREATE TABLE `admin` (
  `admin_id` int UNSIGNED NOT NULL,
  `login` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `pass` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `admin`
--

INSERT INTO `admin` (`admin_id`, `login`, `pass`) VALUES
(1, 'root', 'root');

-- --------------------------------------------------------

--
-- Структура таблицы `destomi_dalykai`
--

CREATE TABLE `destomi_dalykai` (
  `dalyko_pav` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `destomi_dalykai`
--

INSERT INTO `destomi_dalykai` (`dalyko_pav`) VALUES
('Anglu'),
('duomenu bazes'),
('Fizika'),
('Istorija'),
('Literatura'),
('Matematika'),
('Programavimas'),
('Sokiai'),
('Vokieciu');

-- --------------------------------------------------------

--
-- Структура таблицы `destytojas`
--

CREATE TABLE `destytojas` (
  `dest_id` int NOT NULL,
  `login` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `pass` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `destytojas`
--

INSERT INTO `destytojas` (`dest_id`, `login`, `pass`) VALUES
(10, 'Andrius', 'Andriunis'),
(14, 'sav', 'savul'),
(15, 'V', 'Liubinas'),
(16, 'T', 'Liogiene'),
(17, 'I', 'Katin'),
(18, 'R', 'Mikoliene'),
(19, 'R', 'Tumasonis'),
(21, 'M', 'Kiskyte');

-- --------------------------------------------------------

--
-- Структура таблицы `dest_dalykai`
--

CREATE TABLE `dest_dalykai` (
  `dest_dalykai` int NOT NULL,
  `dalyko_pav` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `dest_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `dest_dalykai`
--

INSERT INTO `dest_dalykai` (`dest_dalykai`, `dalyko_pav`, `dest_id`) VALUES
(15, 'Anglu', 10),
(16, 'duomenu bazes', 17),
(17, 'Fizika', 18),
(18, 'Istorija', 18),
(19, 'Literatura', 14),
(20, 'Matematika', 16),
(21, 'Programavimas', 15),
(22, 'Sokiai', 10),
(23, 'Vokieciu', 21);

-- --------------------------------------------------------

--
-- Структура таблицы `grupes`
--

CREATE TABLE `grupes` (
  `pavadinimas` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `grupes`
--

INSERT INTO `grupes` (`pavadinimas`) VALUES
('PI22B'),
('PI23B');

-- --------------------------------------------------------

--
-- Структура таблицы `grupes_dalykai`
--

CREATE TABLE `grupes_dalykai` (
  `grupes_dalykai_id` int NOT NULL,
  `pavadinimas` varchar(10) NOT NULL,
  `dalyko_pav` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `grupes_dalykai`
--

INSERT INTO `grupes_dalykai` (`grupes_dalykai_id`, `pavadinimas`, `dalyko_pav`) VALUES
(27, 'PI22B', 'Anglu'),
(29, 'PI22B', 'duomenu bazes'),
(31, 'PI22B', 'Fizika'),
(33, 'PI22B', 'Literatura'),
(35, 'PI22B', 'Programavimas'),
(28, 'PI23B', 'Anglu'),
(30, 'PI23B', 'duomenu bazes'),
(32, 'PI23B', 'Istorija'),
(34, 'PI23B', 'Matematika'),
(36, 'PI23B', 'Sokiai');

-- --------------------------------------------------------

--
-- Структура таблицы `grupes_studentai`
--

CREATE TABLE `grupes_studentai` (
  `grupes_studentai_id` int NOT NULL,
  `stud_id` int NOT NULL,
  `pavadinimas` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `grupes_studentai`
--

INSERT INTO `grupes_studentai` (`grupes_studentai_id`, `stud_id`, `pavadinimas`) VALUES
(11, 7, 'PI22B'),
(12, 8, 'PI22B'),
(13, 9, 'PI23B'),
(14, 11, 'PI22B'),
(15, 14, 'PI23B'),
(16, 12, 'PI22B'),
(17, 10, 'PI23B'),
(18, 13, 'PI23B'),
(19, 16, 'PI22B');

-- --------------------------------------------------------

--
-- Структура таблицы `pazymiai`
--

CREATE TABLE `pazymiai` (
  `pazymio_id` int NOT NULL,
  `dalyko_pav` varchar(50) NOT NULL,
  `stud_id` int NOT NULL,
  `pazymis` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `pazymiai`
--

INSERT INTO `pazymiai` (`pazymio_id`, `dalyko_pav`, `stud_id`, `pazymis`) VALUES
(19, 'Anglu', 11, 10),
(20, 'Anglu', 7, 10),
(21, 'Anglu', 11, 10),
(22, 'Istorija', 14, 2),
(24, 'Anglu', 8, 10),
(25, 'Anglu', 11, 1),
(26, 'Anglu', 12, 1),
(27, 'Anglu', 9, 1),
(28, 'Anglu', 14, 1),
(29, 'Anglu', 10, 1),
(30, 'Anglu', 13, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `studentas`
--

CREATE TABLE `studentas` (
  `stud_id` int NOT NULL,
  `login` varchar(50) NOT NULL,
  `pass` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `studentas`
--

INSERT INTO `studentas` (`stud_id`, `login`, `pass`) VALUES
(16, 'Adamas', 'Jevaitis'),
(11, 'Arnas', 'Arnaitis'),
(7, 'Arturas', 'Sinkevic'),
(14, 'Emilija', 'Emiliaite'),
(12, 'Juozas', 'Juozaitis'),
(10, 'Laurinas', 'Laurinaitis'),
(8, 'Lukas', 'Lukaitis'),
(13, 'Oksana', 'Oksanaite'),
(9, 'Tomas', 'Tomaitis');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `admin`
--
ALTER TABLE `admin`
  ADD UNIQUE KEY `ID_admin` (`admin_id`);

--
-- Индексы таблицы `destomi_dalykai`
--
ALTER TABLE `destomi_dalykai`
  ADD UNIQUE KEY `dalyko_pav` (`dalyko_pav`);

--
-- Индексы таблицы `destytojas`
--
ALTER TABLE `destytojas`
  ADD UNIQUE KEY `dest_id` (`dest_id`);

--
-- Индексы таблицы `dest_dalykai`
--
ALTER TABLE `dest_dalykai`
  ADD UNIQUE KEY `dest_dalykai` (`dest_dalykai`),
  ADD UNIQUE KEY `dalyko_id` (`dalyko_pav`) USING BTREE,
  ADD KEY `dest_id` (`dest_id`);

--
-- Индексы таблицы `grupes`
--
ALTER TABLE `grupes`
  ADD PRIMARY KEY (`pavadinimas`);

--
-- Индексы таблицы `grupes_dalykai`
--
ALTER TABLE `grupes_dalykai`
  ADD UNIQUE KEY `grupes_dalykai_id` (`grupes_dalykai_id`) USING BTREE,
  ADD UNIQUE KEY `pavadinimas_2` (`pavadinimas`,`dalyko_pav`) USING BTREE,
  ADD KEY `pavadinimas` (`pavadinimas`) USING BTREE,
  ADD KEY `dalyko_pav` (`dalyko_pav`) USING BTREE;

--
-- Индексы таблицы `grupes_studentai`
--
ALTER TABLE `grupes_studentai`
  ADD PRIMARY KEY (`grupes_studentai_id`),
  ADD KEY `stud_id` (`stud_id`),
  ADD KEY `grupes_id` (`pavadinimas`);

--
-- Индексы таблицы `pazymiai`
--
ALTER TABLE `pazymiai`
  ADD PRIMARY KEY (`pazymio_id`),
  ADD KEY `dalyko_pav` (`dalyko_pav`),
  ADD KEY `stud_id` (`stud_id`);

--
-- Индексы таблицы `studentas`
--
ALTER TABLE `studentas`
  ADD UNIQUE KEY `stud_id` (`stud_id`),
  ADD UNIQUE KEY `login` (`login`,`pass`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `admin`
--
ALTER TABLE `admin`
  MODIFY `admin_id` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT для таблицы `destytojas`
--
ALTER TABLE `destytojas`
  MODIFY `dest_id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT для таблицы `dest_dalykai`
--
ALTER TABLE `dest_dalykai`
  MODIFY `dest_dalykai` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT для таблицы `grupes_dalykai`
--
ALTER TABLE `grupes_dalykai`
  MODIFY `grupes_dalykai_id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- AUTO_INCREMENT для таблицы `grupes_studentai`
--
ALTER TABLE `grupes_studentai`
  MODIFY `grupes_studentai_id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT для таблицы `pazymiai`
--
ALTER TABLE `pazymiai`
  MODIFY `pazymio_id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT для таблицы `studentas`
--
ALTER TABLE `studentas`
  MODIFY `stud_id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `dest_dalykai`
--
ALTER TABLE `dest_dalykai`
  ADD CONSTRAINT `dest_dalykai_ibfk_2` FOREIGN KEY (`dest_id`) REFERENCES `destytojas` (`dest_id`),
  ADD CONSTRAINT `dest_dalykai_ibfk_3` FOREIGN KEY (`dalyko_pav`) REFERENCES `destomi_dalykai` (`dalyko_pav`);

--
-- Ограничения внешнего ключа таблицы `grupes_dalykai`
--
ALTER TABLE `grupes_dalykai`
  ADD CONSTRAINT `grupes_dalykai_ibfk_3` FOREIGN KEY (`pavadinimas`) REFERENCES `grupes` (`pavadinimas`),
  ADD CONSTRAINT `grupes_dalykai_ibfk_4` FOREIGN KEY (`dalyko_pav`) REFERENCES `destomi_dalykai` (`dalyko_pav`);

--
-- Ограничения внешнего ключа таблицы `grupes_studentai`
--
ALTER TABLE `grupes_studentai`
  ADD CONSTRAINT `grupes_studentai_ibfk_1` FOREIGN KEY (`stud_id`) REFERENCES `studentas` (`stud_id`),
  ADD CONSTRAINT `grupes_studentai_ibfk_2` FOREIGN KEY (`pavadinimas`) REFERENCES `grupes` (`pavadinimas`);

--
-- Ограничения внешнего ключа таблицы `pazymiai`
--
ALTER TABLE `pazymiai`
  ADD CONSTRAINT `pazymiai_ibfk_1` FOREIGN KEY (`dalyko_pav`) REFERENCES `destomi_dalykai` (`dalyko_pav`),
  ADD CONSTRAINT `pazymiai_ibfk_2` FOREIGN KEY (`stud_id`) REFERENCES `studentas` (`stud_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
