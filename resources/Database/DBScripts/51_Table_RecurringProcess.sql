USE [FireTrackerDb]
GO

CREATE TABLE [dbo].[RecurringProcess](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[MessageTemplate] [nvarchar](max) NULL,
	[Level] [nvarchar](max) NULL,
	[TimeStamp] [datetime2](7) NULL,
	[Exception] [nvarchar](max) NULL,
	[Properties] [nvarchar](max) NULL,
	[TaskManagerProcessId] [nvarchar](50) NULL,
	[RecurringJobName] [nvarchar](256) NULL,
	[LogCleanupDate] [date] NULL,
 CONSTRAINT [PK_RecurringProcess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)

) 

GO


