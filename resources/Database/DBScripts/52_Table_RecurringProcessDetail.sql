USE [FireTrackerDb]
GO

CREATE TABLE [dbo].[RecurringProcessDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[MessageTemplate] [nvarchar](max) NULL,
	[Level] [nvarchar](max) NULL,
	[TimeStamp] [datetime2](7) NULL,
	[Exception] [nvarchar](max) NULL,
	[Properties] [nvarchar](max) NULL,
	[TaskManagerProcessId] [nvarchar](50) NULL,
	[MessageType] [nvarchar](20) NULL,
	[FireTrackerMsg] [nvarchar](max) NULL,
 CONSTRAINT [PK_RecurringProcessDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)

)
GO


