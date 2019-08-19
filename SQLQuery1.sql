create table [dbo].[Staff] (
	[Id] int Identity(1,1) Not null,
	[StaffId] nvarchar(max) not null,
	[StaffName] nvarchar(max) not null,
	primary key (Id)
);

CREATE TABLE [dbo].[ActionLog] (
    [LogId]        BIGINT        IDENTITY (0, 1) NOT NULL,
    [Operator]     VARCHAR (10)  NOT NULL,
    [Refer]        VARCHAR (300) NULL,
    [Destination]  VARCHAR (300) NOT NULL,
    [Method]       VARCHAR (5)   NULL,
    [MobleDevices] BIT           NOT NULL,
    [IPAddress]    VARCHAR (40)  NULL,
    [RequestTime]  DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_ActionLog_1] PRIMARY KEY CLUSTERED ([LogId] ASC)
);

CREATE TABLE [dbo].[Files] (
    [FileId]       INT            IDENTITY (1, 1) NOT NULL,
    [FileName]     NVARCHAR (MAX) NOT NULL,
    [ModifiedTime] DATETIME       NOT NULL,
    [FilePath]     NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([FileId] ASC)
);