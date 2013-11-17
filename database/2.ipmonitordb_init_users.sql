-- --------------------------------------------------------
-- 主机:                           113.207.127.15
-- 服务器版本:                        5.1.68-community - MySQL Community Server (GPL)
-- 服务器操作系统:                      Win64
-- HeidiSQL 版本:                  8.0.0.4396
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
-- 导出 ipmonitordb 的数据库结构
CREATE DATABASE IF NOT EXISTS `ipmonitordb` /*!40100 DEFAULT CHARACTER SET gb2312 */;
USE `ipmonitordb`;

-- 正在导出表  ipmonitordb.users 的数据：~20 rows (大约)
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT IGNORE INTO `users` (`ID`, `UserType`, `UserName`, `Password`, `FullName`, `Email`, `Phone`, `Province`, `City`, `Address`, `Description`, `CreatedBy`, `CreatedDate`, `LastUpdatedBy`, `LastUpdatedDate`) VALUES
	(1, 0, 'administrator', 'ce0bfd15059b68d67688884d7a3d3e8c', '超级管理员', NULL, '18699999999', '重庆', '重庆', NULL, '初始化超级管理员用户，不能删除', '初始化', '2013-05-30 13:17:32', '初始\n\n化', '2013-05-30 13:17:32');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
