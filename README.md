Prerequisitos del proyecto para Microsoft Visual Studio 2022

MicrosoftAspNetCore.Api (7.0.5)
MicrosoftEntityFrameworkCore.Design(7.0.20)
MicrosoftEntityFrameworkCore.SqlServer(7.0.20)
MicrosoftEntityFrameworkCore.Tools(7.0.20)

la cadena de conexion de la base de datos se llama DefaultConnection y esta en el fichero appsettings.json. este es un ejemplo. en caso de necesitar modificar, solo buscar este ejemplo y modificarlo a partir de los :

"DefaultConnection": "Data Source=localhost;Initial Catalog=Tareas;User ID=sa;Password=sasa;TrustServerCertificate=True"


se debe ejecutar esta consulta en SQL server. Este script creara la Base de datos y la tabla Tareas 

USE [master]
GO
/****** Object:  Database [Tareas]    Script Date: 20/11/2026 10:05:55 a. m. ******/
CREATE DATABASE [Tareas] ON  PRIMARY 
( NAME = N'Tareas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Tareas.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Tareas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Tareas_log.LDF' , SIZE = 576KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Tareas] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Tareas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Tareas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Tareas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Tareas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Tareas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Tareas] SET ARITHABORT OFF 
GO
ALTER DATABASE [Tareas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Tareas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Tareas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Tareas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Tareas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Tareas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Tareas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Tareas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Tareas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Tareas] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Tareas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Tareas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Tareas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Tareas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Tareas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Tareas] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Tareas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Tareas] SET RECOVERY FULL 
GO
ALTER DATABASE [Tareas] SET  MULTI_USER 
GO
ALTER DATABASE [Tareas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Tareas] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Tareas', N'ON'
GO
USE [Tareas]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 20/11/2026 10:05:55 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tareas]    Script Date: 20/11/2026 10:05:55 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tareas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](200) NOT NULL,
	[Descripcion] [nvarchar](1000) NULL,
	[Estado] [int] NOT NULL,
	[Responsable] [nvarchar](100) NOT NULL,
	[FechaLimite] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Tareas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261120140101_InitialCreate', N'7.0.20')
GO
USE [master]
GO
ALTER DATABASE [Tareas] SET  READ_WRITE 
GO


ejemplo de como ejecutar el metodo paginado donde se puede filtrar por diferentes valores como estado, responsable, etc..

GetTareasPaginado?page=1&pageSize=10&estado=EnProgreso&responsable=ana&search=informe




