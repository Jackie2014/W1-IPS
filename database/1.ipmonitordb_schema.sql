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


-- 导出  表 ipmonitordb.cidrsettings 结构
CREATE TABLE IF NOT EXISTS `cidrsettings` (
  `ID` varchar(36) NOT NULL,
  `IPStart` varchar(50) NOT NULL,
  `IPEnd` varchar(50) NOT NULL,
  `IPStartNum` int(11) NOT NULL,
  `IPEndNum` int(11) NOT NULL,
  `TCPThreshold` int(11) NOT NULL,
  `TCPPort` int(11) NOT NULL,
  `TTLThreshold` int(11) NOT NULL,
  `TTLExceptionKeys` varchar(200) DEFAULT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `LastUpdatedBy` varchar(50) NOT NULL,
  `LastUpdatedDate` datetime NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- 数据导出被取消选择。


-- 导出  表 ipmonitordb.clientips 结构
CREATE TABLE IF NOT EXISTS `clientips` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ClientPublicIP` varchar(45) DEFAULT NULL,
  `ClientPrivateIP` varchar(45) DEFAULT NULL,
  `SubmitFromServerIP` varchar(45) DEFAULT NULL,
  `ClientProvince` varchar(45) DEFAULT NULL,
  `ClientCity` varchar(45) DEFAULT NULL,
  `ClientDistinct` varchar(10) DEFAULT NULL,
  `ClientDetailAddr` varchar(100) DEFAULT NULL,
  `ExpectedOperatorProvince` varchar(45) DEFAULT NULL,
  `ExpectedOperatorCity` varchar(45) DEFAULT NULL,
  `ExpectedOperator` varchar(45) DEFAULT NULL,
  `RealOperatorProvince` varchar(45) DEFAULT NULL,
  `RealOperatorCity` varchar(45) DEFAULT NULL,
  `RealOperator` varchar(45) DEFAULT NULL,
  `Status` int(1) unsigned zerofill DEFAULT '0',
  `UserName` varchar(100) DEFAULT NULL,
  `ClientRecordor` varchar(45) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `LastUpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- 数据导出被取消选择。


-- 导出  表 ipmonitordb.ipdatabase 结构
CREATE TABLE IF NOT EXISTS `ipdatabase` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `IPStart` varchar(45) NOT NULL,
  `IPEnd` varchar(45) NOT NULL,
  `Country` varchar(45) DEFAULT NULL,
  `Province` varchar(45) DEFAULT NULL,
  `City` varchar(45) DEFAULT NULL,
  `IPBelongTo` varchar(100) DEFAULT NULL,
  `IPStartNum` bigint(20) DEFAULT '0',
  `IPEndNum` bigint(20) DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `Idx_IP` (`IPStart`,`IPEnd`)
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- 数据导出被取消选择。


-- 导出  表 ipmonitordb.ipscanresults 结构
CREATE TABLE IF NOT EXISTS `ipscanresults` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `IP` varchar(50) NOT NULL,
  `TCPTime` int(11) NOT NULL,
  `TCPValidation` varchar(50) NOT NULL,
  `TTLValidation` varchar(50) NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  `LastUpdatedDate` datetime NOT NULL,
  `LastUpdatedBy` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_IPSCANRESULT_DATE` (`CreatedDate`)
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- 数据导出被取消选择。


-- 导出  表 ipmonitordb.loginrecords 结构
CREATE TABLE IF NOT EXISTS `loginrecords` (
  `ID` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `UserName` varchar(45) DEFAULT NULL,
  `LoginDate` datetime DEFAULT NULL,
  `LoginIP` varchar(30) DEFAULT NULL,
  `IsLoginFromClient` int(1) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `FK_UserName_idx` (`UserName`),
  CONSTRAINT `FK_UserName2` FOREIGN KEY (`UserName`) REFERENCES `users` (`UserName`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- 数据导出被取消选择。


-- 导出  表 ipmonitordb.routedatas 结构
CREATE TABLE IF NOT EXISTS `routedatas` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `GroupID` int(11) NOT NULL,
  `SeqNoInGroup` int(11) DEFAULT NULL,
  `T1` varchar(45) DEFAULT NULL,
  `T2` varchar(45) DEFAULT NULL,
  `T3` varchar(45) DEFAULT NULL,
  `RouteIP` varchar(45) DEFAULT NULL,
  `IPBelongTo` varchar(100) DEFAULT NULL,
  `IPBelongToProvince` varchar(45) DEFAULT NULL,
  `IPBelongToCity` varchar(45) DEFAULT NULL,
  `RouteDate` datetime DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(45) DEFAULT NULL,
  `LastUpdatedDate` datetime DEFAULT NULL,
  `LastUpdatedBy` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- 数据导出被取消选择。


-- 导出  表 ipmonitordb.serversettings 结构
CREATE TABLE IF NOT EXISTS `serversettings` (
  `ID` int(11) NOT NULL,
  `ServerName` varchar(100) NOT NULL,
  `ServerType` int(1) NOT NULL DEFAULT '0',
  `DNS` varchar(100) DEFAULT NULL,
  `IP` varchar(45) NOT NULL,
  `HostLocation` varchar(200) DEFAULT NULL,
  `IsDeleted` int(1) DEFAULT '0',
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(100) DEFAULT NULL,
  `LastUpdatedDate` datetime DEFAULT NULL,
  `LastUpdatedBy` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- 数据导出被取消选择。


-- 导出  表 ipmonitordb.tokens 结构
CREATE TABLE IF NOT EXISTS `tokens` (
  `Token` varchar(36) NOT NULL,
  `UserName` varchar(100) DEFAULT NULL,
  `IP` varchar(20) DEFAULT NULL,
  `MacAddr` varchar(45) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `ExpiredDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Token`),
  KEY `FK_UserName_idx` (`UserName`),
  CONSTRAINT `FK_UserName` FOREIGN KEY (`UserName`) REFERENCES `users` (`UserName`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- 数据导出被取消选择。


-- 导出  表 ipmonitordb.users 结构
CREATE TABLE IF NOT EXISTS `users` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserType` int(1) NOT NULL DEFAULT '2',
  `UserName` varchar(100) NOT NULL,
  `Password` varchar(100) NOT NULL,
  `FullName` varchar(20) NOT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Phone` varchar(100) DEFAULT NULL,
  `Province` varchar(20) DEFAULT NULL,
  `City` varchar(20) DEFAULT NULL,
  `Address` varchar(200) DEFAULT NULL,
  `Description` varchar(1000) DEFAULT NULL,
  `CreatedBy` varchar(100) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `LastUpdatedBy` varchar(100) DEFAULT NULL,
  `LastUpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `UDX_USERNAME` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- 数据导出被取消选择。
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
