USE [master]

IF EXISTS (SELECT * FROM sysdatabases WHERE name='CampervibeBooking') 
BEGIN 
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'CampervibeBooking'
	ALTER DATABASE [CampervibeBooking] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [CampervibeBooking]
END
GO

CREATE DATABASE [CampervibeBooking] 
GO

USE [CampervibeBooking]

IF NOT EXISTS(SELECT name FROM [master].[dbo].syslogins WHERE name = 'CampervibeUser')
BEGIN
	CREATE LOGIN [CampervibeUser] WITH PASSWORD = 'CampervibeUser123' 
END
GO

IF NOT EXISTS (SELECT * FROM sys.sysusers WHERE name = N'CampervibeUser')
BEGIN
	CREATE USER [CampervibeUser] FOR LOGIN [CampervibeUser] WITH DEFAULT_SCHEMA=[dbo]	
END
GO

IF DATABASE_PRINCIPAL_ID('AllowSelectInsertUpdate') IS NULL
BEGIN
	CREATE ROLE [AllowSelectInsertUpdate] 	
END
GO

EXEC sp_addrolemember 'AllowSelectInsertUpdate', 'CampervibeUser'
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Booking')
BEGIN
	DROP TABLE [dbo].[Booking]
END 
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Booking')
BEGIN
CREATE TABLE [dbo].[Booking](
		[Id] [uniqueidentifier] NOT NULL,
		[BookingNumber] [nvarchar](50) NULL,
		[StartDate] [datetime] NOT NULL,
		[EndDate] [datetime] NOT NULL,
		[StartMileage] [decimal](8,2) NULL,
		[EndMileage] [decimal](8,2) NULL,
		[VehicleId] [uniqueidentifier] NULL,
		[CustomerId] [uniqueidentifier] NULL,
		[CollectedOn] [datetime] NULL,
		[ReturnedOn] [datetime] NULL,
		[Total] [decimal](5,2) NULL,
		[CreatedById] [uniqueidentifier] NULL,
		[CreatedOn] [datetime] NULL,
		[LastModifiedById] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GRANT SELECT, INSERT, UPDATE ON [Booking] TO [AllowSelectInsertUpdate]
END 
GO
