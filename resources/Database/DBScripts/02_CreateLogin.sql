USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [firetracker_app]    Script Date: 2/20/2025 7:16:54 PM ******/
CREATE LOGIN [firetracker_app] WITH PASSWORD=N'Pwd123#fta_pwd#', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON

GO

--ALTER LOGIN [firetracker_app] DISABLE
--GO



