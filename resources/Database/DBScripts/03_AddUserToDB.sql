/*
USE [master]
GO

CREATE USER [firetracker_app] FOR LOGIN [firetracker_app] WITH DEFAULT_SCHEMA=[dbo]
GO
*/

USE [FireTrackerDb]
GO
CREATE USER [firetracker_app] FOR LOGIN [firetracker_app]
GO


USE [FireTrackerDb]
GO
ALTER ROLE [db_owner] ADD MEMBER [firetracker_app]
GO


