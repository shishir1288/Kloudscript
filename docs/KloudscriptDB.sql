USE [master]
GO
/****** Object:  Database [KloudscriptDb]    Script Date: 12-09-2022 09:04:26 ******/
CREATE DATABASE [KloudscriptDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'KloudscriptDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\KloudscriptDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'KloudscriptDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\KloudscriptDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [KloudscriptDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [KloudscriptDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [KloudscriptDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [KloudscriptDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [KloudscriptDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [KloudscriptDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [KloudscriptDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [KloudscriptDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [KloudscriptDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [KloudscriptDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [KloudscriptDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [KloudscriptDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [KloudscriptDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [KloudscriptDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [KloudscriptDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [KloudscriptDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [KloudscriptDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [KloudscriptDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [KloudscriptDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [KloudscriptDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [KloudscriptDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [KloudscriptDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [KloudscriptDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [KloudscriptDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [KloudscriptDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [KloudscriptDb] SET  MULTI_USER 
GO
ALTER DATABASE [KloudscriptDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [KloudscriptDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [KloudscriptDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [KloudscriptDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [KloudscriptDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [KloudscriptDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [KloudscriptDb] SET QUERY_STORE = OFF
GO
USE [KloudscriptDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12-09-2022 09:04:26 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShortUrls]    Script Date: 12-09-2022 09:04:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShortUrls](
	[Id] [uniqueidentifier] NOT NULL,
	[OriginalUrl] [nvarchar](max) NULL,
	[ShortCode] [nvarchar](max) NULL,
	[TinyUrl] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ShortUrls] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [KloudscriptDb] SET  READ_WRITE 
GO
