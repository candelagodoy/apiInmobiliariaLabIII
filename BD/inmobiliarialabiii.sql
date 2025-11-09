-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 09-11-2025 a las 16:18:18
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliarialabiii`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `idContrato` int(11) NOT NULL,
  `idInquilino` int(11) NOT NULL,
  `idInmueble` int(11) NOT NULL,
  `montoAlquiler` decimal(11,0) NOT NULL,
  `fechaInicio` date NOT NULL,
  `fechaFinalizacion` date NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`idContrato`, `idInquilino`, `idInmueble`, `montoAlquiler`, `fechaInicio`, `fechaFinalizacion`, `estado`) VALUES
(12, 19, 23, 450000, '2024-11-10', '2026-11-02', 0),
(13, 20, 23, 450000, '2025-11-17', '2026-11-27', 0),
(14, 20, 24, 850000, '2025-11-03', '2027-11-16', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `idInmueble` int(11) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `uso` varchar(50) NOT NULL,
  `tipo` varchar(11) NOT NULL,
  `ambientes` int(11) NOT NULL,
  `superficie` int(50) NOT NULL,
  `valor` decimal(10,0) NOT NULL,
  `latitud` decimal(1,0) NOT NULL,
  `imagen` varchar(100) NOT NULL,
  `disponible` tinyint(1) NOT NULL,
  `longitud` decimal(10,0) NOT NULL,
  `idPropietario` int(11) NOT NULL,
  `tieneContratoVigente` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`idInmueble`, `direccion`, `uso`, `tipo`, `ambientes`, `superficie`, `valor`, `latitud`, `imagen`, `disponible`, `longitud`, `idPropietario`, `tieneContratoVigente`) VALUES
(23, 'constitucion 78', 'comercial', 'casa', 8, 45274, 745278, 9, '35510fa1-cde7-4ece-a812-cf1b9fc1e3b3.png', 0, 54165, 12, 0),
(24, 'ilia 8', 'comercial', 'casa', 8, 45274, 745278, 9, 'e58ddfff-b2fd-4f83-831d-87b95946a66e.png', 0, 54165, 12, 0),
(26, 'Mitre 56', 'Residencial', 'Deposito', 4, 452, 850000, 9, 'f3387685-8e94-49bb-acf5-bc01946f4c1b.jpg', 0, 485, 12, 1),
(27, 'Ayacucho 234', 'Residencial', 'Casa', 4, 452, 850000, 9, '7a678739-0024-4f9b-91b2-14e2ed4f8519.jpg', 0, 485, 12, 1),
(28, 'Chacabuco 567', 'Residencial', 'Casa', 4, 452, 850000, 9, 'f98627e7-c0b7-432a-bfd5-76aee6b388e5.jpg', 0, 485, 12, 1),
(29, 'Chacabuco 567', 'Residencial', 'Casa', 4, 452, 850000, 9, '14b8e716-a47a-4434-9aed-662a8c4f7b51.jpg', 0, 485, 12, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `dni` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `telefono` varchar(50) NOT NULL,
  `idInquilino` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`nombre`, `apellido`, `dni`, `email`, `telefono`, `idInquilino`) VALUES
('Pepito', 'Perez', '20456789', 'pepito@gmail.com', '2664899944', 19),
('Maria', 'Lopez', '18758751', 'maria@gmail.com', '2665895623', 20);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `idPago` int(11) NOT NULL,
  `detalle` varchar(100) NOT NULL,
  `idContrato` int(11) NOT NULL,
  `fechaPago` date NOT NULL,
  `monto` decimal(10,0) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`idPago`, `detalle`, `idContrato`, `fechaPago`, `monto`, `estado`) VALUES
(25, 'mes abril', 12, '2025-09-25', 450000, 1),
(26, 'noviembre', 13, '2025-11-03', 600000, 0),
(27, 'mes octubre', 12, '2025-11-26', 450000, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `dni` varchar(50) NOT NULL,
  `telefono` varchar(50) NOT NULL,
  `idPropietario` int(11) NOT NULL,
  `email` varchar(100) NOT NULL,
  `clave` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`nombre`, `apellido`, `dni`, `telefono`, `idPropietario`, `email`, `clave`) VALUES
('Candela', 'Godoy', '39393939', '2664898989', 12, 'candegg@gmail.com', '3A0G2+zJ3luLnlC44+Xe5HGw/9RWJNoyF2XZACvev20='),
('Pepito', 'Perez', '25896896', '2665868686', 13, 'pepito@gmail.com', '3A0G2+zJ3luLnlC44+Xe5HGw/9RWJNoyF2XZACvev20=');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipoinmueble`
--

CREATE TABLE `tipoinmueble` (
  `idTipoInmueble` int(11) NOT NULL,
  `nombre` varchar(200) NOT NULL,
  `descripción` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `idUsuario` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `apellido` varchar(250) NOT NULL,
  `email` varchar(250) NOT NULL,
  `clave` varchar(250) NOT NULL,
  `avatar` varchar(255) NOT NULL,
  `rol` tinyint(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`idContrato`),
  ADD KEY `idInmueble` (`idInmueble`),
  ADD KEY `idInquilino` (`idInquilino`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`idInmueble`),
  ADD KEY `idPropietario` (`idPropietario`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`idInquilino`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`idPago`),
  ADD KEY `idContrato` (`idContrato`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`idPropietario`);

--
-- Indices de la tabla `tipoinmueble`
--
ALTER TABLE `tipoinmueble`
  ADD PRIMARY KEY (`idTipoInmueble`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`idUsuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT de la tabla `tipoinmueble`
--
ALTER TABLE `tipoinmueble`
  MODIFY `idTipoInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`idInmueble`) REFERENCES `inmueble` (`idInmueble`),
  ADD CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`idInquilino`) REFERENCES `inquilino` (`idInquilino`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`idPropietario`) REFERENCES `propietario` (`idPropietario`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`idContrato`) REFERENCES `contrato` (`idContrato`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
