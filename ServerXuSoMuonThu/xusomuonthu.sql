-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 16, 2022 at 08:45 AM
-- Server version: 10.4.22-MariaDB
-- PHP Version: 8.1.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `xusomuonthu`
--

-- --------------------------------------------------------

--
-- Table structure for table `banbe`
--

CREATE TABLE `banbe` (
  `banbe_ID` int(11) NOT NULL,
  `banbe_IDnguoichoi1` int(11) NOT NULL,
  `banbe_IDnguoichoi2` int(11) NOT NULL,
  `banbe_Tennguoichoi1` varchar(52) COLLATE utf8_unicode_ci NOT NULL,
  `banbe_Tennguoichoi2` varchar(52) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `banbe`
--

INSERT INTO `banbe` (`banbe_ID`, `banbe_IDnguoichoi1`, `banbe_IDnguoichoi2`, `banbe_Tennguoichoi1`, `banbe_Tennguoichoi2`) VALUES
(9, 5, 4, 'Valentines', 'Dương Trường Vũ'),
(10, 4, 5, 'Dương Trường Vũ', 'Valentines');

-- --------------------------------------------------------

--
-- Table structure for table `banbe_khoa`
--

CREATE TABLE `banbe_khoa` (
  `bbkhoa_ID` int(11) DEFAULT NULL,
  `bbkhoa_IDnguoichoi1` int(11) NOT NULL,
  `bbkhoa_IDnguoichoi2` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `banbe_loimoi`
--

CREATE TABLE `banbe_loimoi` (
  `bbloimoi_ID` int(11) NOT NULL,
  `bbloimoi_IDNguoiChoi1` int(11) NOT NULL,
  `bbloimoi_IDNguoiChoi2` int(11) NOT NULL,
  `bbloimoi_TenNguoiChoi1` varchar(52) COLLATE utf8_unicode_ci NOT NULL,
  `bbloimoi_TenNguoiChoi2` varchar(52) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `banbe_loimoi`
--

INSERT INTO `banbe_loimoi` (`bbloimoi_ID`, `bbloimoi_IDNguoiChoi1`, `bbloimoi_IDNguoiChoi2`, `bbloimoi_TenNguoiChoi1`, `bbloimoi_TenNguoiChoi2`) VALUES
(1, -100, 4, 'aaa', 'bbbb'),
(2, -101, 4, 'nbv', 'mnhu');

-- --------------------------------------------------------

--
-- Table structure for table `dongiaodich`
--

CREATE TABLE `dongiaodich` (
  `ID_GiaoDich` int(11) NOT NULL,
  `ID_nguoichoi1` int(11) NOT NULL,
  `ID_nguoichoi2` int(11) NOT NULL,
  `ThoiGianGiaoDich` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `inventoryitem`
--

CREATE TABLE `inventoryitem` (
  `IDtaikhoan` int(11) NOT NULL,
  `itemID` int(11) NOT NULL,
  `SoLuong` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `inventoryitem`
--

INSERT INTO `inventoryitem` (`IDtaikhoan`, `itemID`, `SoLuong`) VALUES
(4, 1, 123),
(4, 2, 48),
(4, 3, 23),
(4, 4, 19),
(4, 5, 9),
(4, 6, 7),
(4, 7, 86),
(5, 1, 161),
(5, 2, 98),
(5, 3, 43),
(5, 4, 42),
(5, 5, 11),
(5, 6, 11),
(5, 7, 87),
(7, 1, 3),
(7, 2, 2),
(7, 3, 1),
(7, 4, 1),
(14, 1, 3),
(14, 2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `nhanvat`
--

CREATE TABLE `nhanvat` (
  `IDtaikhoan` int(11) NOT NULL,
  `TenNhanVat` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `STT` int(11) NOT NULL,
  `GioiTinh` tinyint(1) NOT NULL,
  `TrangThai` int(11) NOT NULL DEFAULT 0,
  `NgayTaoNhanVat` datetime NOT NULL DEFAULT current_timestamp(),
  `TrangPhuc` int(11) NOT NULL DEFAULT 0,
  `ThuCung` int(11) DEFAULT 0,
  `Ngoc` int(11) NOT NULL DEFAULT 0,
  `Vang` int(11) NOT NULL DEFAULT 0,
  `HoatDongGanNhat` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `nhanvat`
--

INSERT INTO `nhanvat` (`IDtaikhoan`, `TenNhanVat`, `STT`, `GioiTinh`, `TrangThai`, `NgayTaoNhanVat`, `TrangPhuc`, `ThuCung`, `Ngoc`, `Vang`, `HoatDongGanNhat`) VALUES
(4, 'Dương Trường Vũ', 1, 0, 0, '2022-02-13 12:26:45', 1, 3, 0, 0, '2022-03-11 19:49:10'),
(5, 'Valentines', 2, 1, 0, '2022-02-17 07:28:22', 1, 4, 0, 0, '2022-03-11 19:48:25'),
(6, 'Valentines', 1, 0, 0, '2022-02-17 07:33:57', 1, 3, 0, 0, '2022-02-27 00:00:00'),
(7, 'chào cậu', 1, 1, 0, '2022-03-06 23:21:19', 0, -1, 0, 0, '2022-03-06 23:22:10'),
(8, 'Valentines', 3, 1, 0, '2022-03-12 09:32:24', 0, -1, 0, 0, '2022-03-12 09:32:24'),
(9, 'a6', 1, 0, 0, '2022-03-12 11:23:49', 0, -1, 0, 0, '2022-03-12 11:23:49'),
(12, 'a8', 1, 0, 0, '2022-03-12 11:24:36', 0, -1, 0, 0, '2022-03-12 11:24:36'),
(13, 'a5', 1, 0, 0, '2022-04-15 22:32:27', 0, 0, 0, 0, '2022-04-15 22:32:27'),
(14, 'a9', 1, 0, 0, '2022-04-15 22:38:51', 0, 0, 0, 0, '2022-04-15 22:38:51');

-- --------------------------------------------------------

--
-- Table structure for table `taikhoan`
--

CREATE TABLE `taikhoan` (
  `IDtaikhoan` int(11) NOT NULL,
  `TaiKhoan` varchar(52) COLLATE utf8_unicode_ci NOT NULL,
  `MatKhau` varchar(72) COLLATE utf8_unicode_ci NOT NULL,
  `Gmail` varchar(64) COLLATE utf8_unicode_ci NOT NULL,
  `ThoiGianDangKy` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `taikhoan`
--

INSERT INTO `taikhoan` (`IDtaikhoan`, `TaiKhoan`, `MatKhau`, `Gmail`, `ThoiGianDangKy`) VALUES
(4, 'a1', 'danaTECH.G.XuSoMuonThu8RI5xwkLxndB8iyMqU4maU0uaDecjTy', 'ahaha@gmail.com', '2022-02-12 18:53:38'),
(5, 'a2', 'danaTECH.G.XuSoMuonThucvOC3iCnzbB1qnuk/wFhexuhK0BJvEi', 'a2@gmail.com', '2022-02-17 07:27:26'),
(6, 'a3', 'danaTECH.G.XuSoMuonThukAnimyh6wMXRTr2tHXp2X9GrDkj9Udu', 'dtv@gmail.com', '2022-02-17 07:33:29'),
(7, 'abc', 'danaTECH.G.XuSoMuonThuFKnuWdwJKYbOb0NqGBWiJS2dUsFELrS', 'hihihi@gmail.com', '2022-03-06 23:20:35'),
(8, 'a4', 'danaTECH.G.XuSoMuonThukAnimyh6wMXRTr2tHXp2X9GrDkj9Udu', 'ahaha@gmail.com', '2022-03-12 09:31:54'),
(9, 'a6', 'danaTECH.G.XuSoMuonThufOeE9fjLaUWiZxv6NT4.xawPKOCn50y', 'a6@gmail.com', '2022-03-12 11:01:24'),
(10, 'a7', 'danaTECH.G.XuSoMuonThuJyIEYdyoK7KC.Cc8BQeEjXs.USHKXbS', 'abc@gmail.com', '2022-03-12 11:07:51'),
(11, 'abv', 'danaTECH.G.XuSoMuonThu8RI5xwkLxndB8iyMqU4maU0uaDecjTy', 'abcc@gmail.com', '2022-03-12 11:21:53'),
(12, 'a8', 'danaTECH.G.XuSoMuonThu.1B63KQchL0YAb0QLK1BIklgx.slugu', 'a8@gmail.com', '2022-03-12 11:24:27'),
(13, 'a5', 'danaTECH.G.XuSoMuonThuc8HcFX6dQsSLxE2manpFfSRXEN6lOju', 'a5@gmail.com', '2022-04-15 22:32:15'),
(14, 'a9', 'danaTECH.G.XuSoMuonThuiP.HofAeQuVGXwEMz3NktW7hvBIN75m', 'a9@gmail.com', '2022-04-15 22:38:44');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `banbe`
--
ALTER TABLE `banbe`
  ADD PRIMARY KEY (`banbe_ID`);

--
-- Indexes for table `banbe_loimoi`
--
ALTER TABLE `banbe_loimoi`
  ADD PRIMARY KEY (`bbloimoi_ID`);

--
-- Indexes for table `dongiaodich`
--
ALTER TABLE `dongiaodich`
  ADD PRIMARY KEY (`ID_GiaoDich`);

--
-- Indexes for table `inventoryitem`
--
ALTER TABLE `inventoryitem`
  ADD PRIMARY KEY (`IDtaikhoan`,`itemID`);

--
-- Indexes for table `nhanvat`
--
ALTER TABLE `nhanvat`
  ADD PRIMARY KEY (`IDtaikhoan`);

--
-- Indexes for table `taikhoan`
--
ALTER TABLE `taikhoan`
  ADD PRIMARY KEY (`IDtaikhoan`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `banbe`
--
ALTER TABLE `banbe`
  MODIFY `banbe_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `banbe_loimoi`
--
ALTER TABLE `banbe_loimoi`
  MODIFY `bbloimoi_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=33;

--
-- AUTO_INCREMENT for table `dongiaodich`
--
ALTER TABLE `dongiaodich`
  MODIFY `ID_GiaoDich` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `taikhoan`
--
ALTER TABLE `taikhoan`
  MODIFY `IDtaikhoan` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `inventoryitem`
--
ALTER TABLE `inventoryitem`
  ADD CONSTRAINT `IDtaikhoan` FOREIGN KEY (`IDtaikhoan`) REFERENCES `nhanvat` (`IDtaikhoan`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
