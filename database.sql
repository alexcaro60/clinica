CREATE DATABASE uniminuto-clinica

CREATE TABLE `citas` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `fecha` datetime DEFAULT NULL,
  `IdMedico` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `citas_IdMedico_IDX` (`IdMedico`) USING BTREE,
  CONSTRAINT `citas_FK` FOREIGN KEY (`IdMedico`) REFERENCES `medicos` (`id`)
);

CREATE TABLE `medicos` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `especialidad` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
);