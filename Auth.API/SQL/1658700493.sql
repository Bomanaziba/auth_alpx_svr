/****** Object:  Table [dbo].[ApplicationLog]    Script Date: 7/24/2022 3:40:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentId] [bigint] NOT NULL,
	[MachineName] [nvarchar](255) NULL,
	[UserName] [nvarchar](100) NULL,
	[UserAgent] [nvarchar](255) NULL,
	[LogDate] [datetime] NULL,
	[LogType] [nvarchar](255) NULL,
	[LogMessage] [text] NULL,
	[Source] [nvarchar](255) NULL,
	[StackTrace] [text] NULL,
	[QueryStringData] [text] NULL,
	[FormData] [text] NULL,
	[ChainId] [uniqueidentifier] NULL,
	[ExtraInfo] [text] NULL,
 CONSTRAINT [PK__tbl_application_log__id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 7/24/2022 3:40:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](500) NULL,
	[DbConnectionString] [nvarchar](max) NULL,
	[DbType] [int] NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[ClientId] [nvarchar](max) NULL,
	[ClientSecret] [nvarchar](max) NULL,
	[BaseUrl] [nvarchar](max) NULL,
	[JwtToken] [nvarchar](500) NULL,
	[Salt] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientJwtTokenKey]    Script Date: 7/24/2022 3:40:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientJwtTokenKey](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[JwtTokenKey] [nvarchar](max) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientResource]    Script Date: 7/24/2022 3:40:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientResource](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ResourceId] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 7/24/2022 3:40:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ResourceId] [bigint] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](100) NULL,
	[ServiceId] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/24/2022 3:40:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 7/24/2022 3:40:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](100) NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemSettings]    Script Date: 7/24/2022 3:40:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemSettings](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[Description] [varchar](500) NULL,
	[Group] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClient]    Script Date: 7/24/2022 3:40:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClient](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[IsEnabled] [bit] NULL,
	[IsActive] [bit] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
	[LastSeen] [datetime2](7) NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[GenderId] [int] NULL,
	[DateOfBirth] [datetime2](7) NULL,
	[Nationality] [int] NULL,
	[Race] [int] NULL,
	[Password] [nvarchar](max) NULL,
	[Salt] [nvarchar](max) NULL,
	[IsVerified] [bit] NULL,
	[DateVerified] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserVerifcationCode]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserVerifcationCode](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[Code] [varchar](max) NULL,
	[TimeSent] [datetime2](7) NOT NULL,
	[TimeElasped] [datetime2](7) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_tbl_application_log_ON_chain_id]    Script Date: 7/24/2022 3:40:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_tbl_application_log_ON_chain_id] ON [dbo].[ApplicationLog]
(
	[ChainId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApplicationLog] ADD  CONSTRAINT [DF__tbl_tqf_a__paren__361203C5]  DEFAULT ((0)) FOR [ParentId]
GO
ALTER TABLE [dbo].[ApplicationLog] ADD  CONSTRAINT [DF__tbl_tqf_a__chain__370627FE]  DEFAULT (newid()) FOR [ChainId]
GO
/****** Object:  StoredProcedure [dbo].[usp_ApplicationLog_INSERTUPDATE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ApplicationLog_INSERTUPDATE]
	@Id bigint,
	@ParentId bigint,
	@MachineName nvarchar(255),
	@UserName nvarchar(100),
	@UserAgent nvarchar(255),
	@LogDate datetime,
	@LogType nvarchar(255),
	@LogMessage text,
	@Source nvarchar(255),
	@StackTrace text,
	@QueryStringData text,
	@FormData text,
	@ChainId uniqueidentifier,
	@ExtraInfo text
AS


IF EXISTS(SELECT [Id] FROM [dbo].[ApplicationLog] WHERE [Id] = @Id)
BEGIN
	UPDATE [dbo].[ApplicationLog] SET
		[ParentId] = @ParentId,
		[MachineName] = @MachineName,
		[UserName] = @UserName,
		[UserAgent] = @UserAgent,
		[LogDate] = @LogDate,
		[LogType] = @LogType,
		[LogMessage] = @LogMessage,
		[Source] = @Source,
		[StackTrace] = @StackTrace,
		[QueryStringData] = @QueryStringData,
		[FormData] = @FormData,
		[ChainId] = @ChainId,
		[ExtraInfo] = @ExtraInfo
	WHERE
		[Id] = @Id
END
ELSE
BEGIN

INSERT INTO [dbo].[ApplicationLog] (
	[ParentId],
	[MachineName],
	[UserName],
	[UserAgent],
	[LogDate],
	[LogType],
	[LogMessage],
	[Source],
	[StackTrace],
	[QueryStringData],
	[FormData],
	[ChainId],
	[ExtraInfo]
) VALUES (
	@ParentId,
	@MachineName,
	@UserName,
	@UserAgent,
	@LogDate,
	@LogType,
	@LogMessage,
	@Source,
	@StackTrace,
	@QueryStringData,
	@FormData,
	@ChainId,
	@ExtraInfo
)

SET @Id = SCOPE_IDENTITY()
SELECT @Id

END

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_ApplicationLog_UPDATE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ApplicationLog_UPDATE]
	@Id bigint,
	@ParentId bigint,
	@MachineName nvarchar(255),
	@UserName nvarchar(100),
	@UserAgent nvarchar(255),
	@LogDate datetime,
	@LogType nvarchar(255),
	@LogMessage text,
	@Source nvarchar(255),
	@StackTrace text,
	@QueryStringData text,
	@FormData text,
	@ChainId uniqueidentifier,
	@ExtraInfo text
AS


UPDATE [dbo].[ApplicationLog] SET
	[ParentId] = @ParentId,
	[MachineName] = @MachineName,
	[UserName] = @UserName,
	[UserAgent] = @UserAgent,
	[LogDate] = @LogDate,
	[LogType] = @LogType,
	[LogMessage] = @LogMessage,
	[Source] = @Source,
	[StackTrace] = @StackTrace,
	[QueryStringData] = @QueryStringData,
	[FormData] = @FormData,
	[ChainId] = @ChainId,
	[ExtraInfo] = @ExtraInfo
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Client_DEACTIVE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Client_DEACTIVE]
	@Id bigint
AS


UPDATE [dbo].[Client] SET
	[IsActive] = 0
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Client_DELETE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Client_DELETE]
	@Id bigint
AS


DELETE FROM [dbo].[Client]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Client_INSERT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Client_INSERT]
	@Name varchar(100),
    @Description varchar(max),
    @DbConnectionString nvarchar(max) NULL,
    @DbType int,
	@IsActive bit,
	@IsEnabled bit,
	@DateCreated datetime,
	@Id bigint OUTPUT
AS


INSERT INTO [dbo].[Client]
	(
	    [Name],
        [Description],
        [DbConnectionString],
        [DbType],
		[IsEnabled],
		[IsActive],
		[DateCreated]
	)
VALUES
	(
	    @Name,
        @Description,
        @DbConnectionString,
        @DbType,
	    @IsEnabled,
	    @IsActive,
	    @DateCreated
)

SET @Id = SCOPE_IDENTITY()
SELECT @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Client_INSERT_Credentials]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Client_INSERT_Credentials]
	@ClientId nvarchar(max),
    @ClientSecret nvarchar(max),
    @Salt nvarchar(max),
	@Id bigint
AS


UPDATE [dbo].[Client]
    SET 
    [ClientId] = @ClientId ,
    [ClientSecret] = @ClientSecret,
    [Salt] = @Salt
WHERE Id = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Client_INSERTUPDATE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Client_INSERTUPDATE]
	@Id bigint,
    @IdOut bigint OUTPUT,
    @ClientId nvarchar(100),
    @ClientSecret nvarchar(100),
	@Name varchar(100),
    @Description varchar(max),
    @DbConnectionString nvarchar(max) NULL,
    @DbType int,
	@IsActive bit,
	@IsEnabled bit,
	@DateCreated datetime,
	@DateModified datetime

AS


IF EXISTS(SELECT [Id]
FROM [dbo].[Client]
WHERE [Id] = @Id)
BEGIN
	
UPDATE [dbo].[Client] SET
	[Name] = @Name,
	[Description] = @Description,
    [DbConnectionString] = @DbConnectionString,
    [DbType] = @DbType,
	[IsEnabled] = @IsEnabled,
	[DateModified] = @DateModified
WHERE
	[Id] = @Id

    SET @IdOut = @Id

    SELECT @IdOut

END
ELSE
BEGIN

INSERT INTO [dbo].[Client]
	(
	    [Name],
        [Description],
		[ClientId],
        [ClientSecret],
        [DbConnectionString],
        [DbType],
		[IsEnabled],
		[IsActive],
		[DateCreated]
	)
VALUES
	(
	    @Name,
        @Description,
		@ClientId,
        @ClientSecret,
        @DbConnectionString,
        @DbType,
	    @IsEnabled,
	    @IsActive,
	    @DateCreated
)

SET @IdOut = SCOPE_IDENTITY()
SELECT @IdOut

	

END

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Client_SELECT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Client_SELECT]
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED


SELECT * FROM
	[dbo].[Client]

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Client_SELECT_BY_ClientId]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Client_SELECT_BY_ClientId]
	@ClientId VARCHAR(100)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT * FROM
	[dbo].[Client]
WHERE
	[ClientId] = @ClientId

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Client_SELECT_BY_Id]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Client_SELECT_BY_Id]
	@Id bigint
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT * FROM
	[dbo].[Client]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Client_SELECT_BY_Name]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Client_SELECT_BY_Name]
	@Name VARCHAR(100)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT * FROM
	[dbo].[Client]
WHERE
	[Name] = @Name

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Client_UPDATE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Client_UPDATE]
	@Id bigint,
    @Name varchar(100),
    @Description varchar(max),
    @DbConnectionString nvarchar(max) NULL,
    @DbType int,
	@BaseUrl nvarchar(max),
	@IsEnabled bit,
	@DateModified datetime
AS

UPDATE [dbo].[Client] SET
	[Name] = @Name,
	[Description] = @Description,
    [DbConnectionString] = @DbConnectionString,
    [DbType] = @DbType,
	[BaseUrl] = @BaseUrl,
	[IsEnabled] = @IsEnabled,
	[DateModified] = @DateModified
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_ClientJwtTokenKey_DELETE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ClientJwtTokenKey_DELETE]
	@Id bigint
AS


DELETE FROM [dbo].[ClientJwtTokenKey]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_ClientJwtTokenKey_INSERT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ClientJwtTokenKey_INSERT]
	@ClientId int,
	@JwtTokenKey nvarchar(max),
	@Id bigint OUTPUT
AS


INSERT INTO [dbo].[ClientJwtTokenKey]
	(
	    [ClientId],
        [JwtTokenKey],
        [DateCreated]
	)
VALUES
	(
	    @ClientId,
        @JwtTokenKey,
        GETDATE()

)

SET @Id = SCOPE_IDENTITY()
SELECT @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_ClientJwtTokenKey_SELECT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ClientJwtTokenKey_SELECT]
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED


SELECT * FROM
	[dbo].[ClientJwtTokenKey]

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_ClientJwtTokenKey_SELECT_BY_ID]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ClientJwtTokenKey_SELECT_BY_ID]
(
    @ClientId INT
)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT * FROM
	[dbo].[ClientJwtTokenKey]
WHERE ClientId = @ClientId

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_ClientJwtTokenKey_UPDATE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ClientJwtTokenKey_UPDATE]
	@Id bigint,
    @ClientId int,
    @JwtTokenKey nvarchar(max)
AS

UPDATE [dbo].[ClientJwtTokenKey] SET

        [ClientId] = @ClientId,
        [JwtTokenKey] = @JwtTokenKey
	
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_ClientResource_DELETE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ClientResource_DELETE]
	@Id bigint
AS


DELETE FROM [dbo].[ClientResource]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_ClientResource_DELETE_BY_ResourceId_ClientId]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ClientResource_DELETE_BY_ResourceId_ClientId]
	@ClientId int,
    @ResourceId int
AS


DELETE FROM [dbo].[ClientResource]
WHERE
	[ClientId] = @ClientId AND [ResourceId] = @ResourceId

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_ClientResource_INSERT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ClientResource_INSERT]
	@ClientId int,
	@ResourceId int,
	@Id bigint OUTPUT
AS


INSERT INTO [dbo].[ClientResource]
	(
	    [ClientId],
        [ResourceId]
	)
VALUES
	(
	    @ClientId,
        @ResourceId
)

SET @Id = SCOPE_IDENTITY()
SELECT @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_ClientResource_SELECT_BY_ClientId]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_ClientResource_SELECT_BY_ClientId]
(
    @ClientId int
)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED


SELECT * FROM
	[dbo].[ClientResource]
WHERE [ClientId] = @ClientId

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_ClientResource_SELECT_BY_ClientId_ResourceId]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ClientResource_SELECT_BY_ClientId_ResourceId]
(
    @ClientId int,
    @ResourceId int
)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED


SELECT ResourceId FROM
	[dbo].[ClientResource]
WHERE [ClientId] = @ClientId AND [ResourceId] = @ResourceId

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_ClientResourceDetails_SELECT_BY_ClientId_ResourceId]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ClientResourceDetails_SELECT_BY_ClientId_ResourceId]
(
    @ClientId int,
    @ResourceId int
)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED


SELECT * FROM
	[dbo].[ClientResource]
WHERE [ClientId] = @ClientId AND [ResourceId] = @ResourceId

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Code_DELECT_BY_USERNAME_CODE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Code_DELECT_BY_USERNAME_CODE]
	@Username varchar(250),
    @Code varchar(250)

AS

DELETE FROM [dbo].[UserCode]
WHERE
	[Username] = @Username AND [Code] = @Code
GO
/****** Object:  StoredProcedure [dbo].[usp_Code_DELETE_BY_USERNAME]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Code_DELETE_BY_USERNAME]
	@Username varchar(250)
AS

DELETE FROM [dbo].[UserCode]
WHERE
	[Username] = @Username AND [Email] = @Username
GO

/****** Object:  StoredProcedure [dbo].[usp_Log_DELETE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Log_DELETE]
	@Id bigint
AS


DELETE FROM [dbo].[ApplicationLog]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Log_INSERT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Log_INSERT]
	@ParentId bigint,
	@MachineName nvarchar(255),
	@UserName nvarchar(100),
	@UserAgent nvarchar(255),
	@LogDate datetime,
	@LogType nvarchar(255),
	@LogMessage text,
	@Source nvarchar(255),
	@StackTrace text,
	@QueryStringData text,
	@FormData text,
	@ChainId uniqueidentifier,
	@ExtraInfo text,
	@Id bigint OUTPUT
AS


INSERT INTO [dbo].[ApplicationLog] (
	[ParentId],
	[MachineName],
	[UserName],
	[UserAgent],
	[LogDate],
	[LogType],
	[LogMessage],
	[Source],
	[StackTrace],
	[QueryStringData],
	[FormData],
	[ChainId],
	[ExtraInfo]
) VALUES (
	@ParentId,
	@MachineName,
	@UserName,
	@UserAgent,
	@LogDate,
	@LogType,
	@LogMessage,
	@Source,
	@StackTrace,
	@QueryStringData,
	@FormData,
	@ChainId,
	@ExtraInfo
)

SET @Id = SCOPE_IDENTITY()
SELECT @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Log_SELECT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Log_SELECT]
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT *
FROM
	[dbo].[ApplicationLog]

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Log_SELECT_BY_Id]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Log_SELECT_BY_Id]
	@Id bigint
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT *
FROM
	[dbo].[ApplicationLog]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Resource_DEACTIVE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Resource_DEACTIVE]
	@Id bigint
AS


UPDATE [dbo].[Resource] SET
	[IsActive] = 0
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Resource_DELETE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Resource_DELETE]
	@Id bigint
AS


DELETE FROM [dbo].[Resource]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Resource_INSERT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Resource_INSERT]
	@ResourceId int,
	@Name varchar(100),
    @Description varchar(max),
    @ServiceId int,
	@IsActive bit,
	@IsEnabled bit,
	@DateCreated datetime,
	@Id bigint OUTPUT
AS


INSERT INTO [dbo].[Resource]
	(
		[ResourceId],
	    [Name],
        [Description],
        [ServiceId],
		[IsEnabled],
		[IsActive],
		[DateCreated]
	)
VALUES
	(
		@ResourceId,
	    @Name,
        @Description,
        @ServiceId,
	    @IsEnabled,
	    @IsActive,
	    @DateCreated
)

SET @Id = SCOPE_IDENTITY()
SELECT @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Resource_INSERTUPDATE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Resource_INSERTUPDATE]
	@Id bigint,
    @IdOut bigint OUTPUT,
	@ResourceId int,
	@Name varchar(100),
    @Description varchar(max),
    @ServiceId int,
	@IsActive bit,
	@IsEnabled bit,
	@DateCreated datetime,
	@DateModified datetime

AS


IF EXISTS(SELECT [Id]
FROM [dbo].[Resource]
WHERE [Id] = @Id)
BEGIN
	
UPDATE [dbo].[Resource] SET
	[ResourceId] = @ResourceId,
	[Name] = @Name,
	[Description] = @Description,
    [ServiceId] = @ServiceId,
	[IsEnabled] = @IsEnabled,
	[DateModified] = @DateModified
WHERE
	[Id] = @Id

    SET @IdOut = @Id

    SELECT @IdOut

END
ELSE
BEGIN

INSERT INTO [dbo].[Resource]
	(
		[ResourceId],
	    [Name],
        [Description],
        [ServiceId],
		[IsEnabled],
		[IsActive],
		[DateCreated]
	)
VALUES
	(
		@ResourceId,
	    @Name,
        @Description,
        @ServiceId,
	    @IsEnabled,
	    @IsActive,
	    @DateCreated
)

SET @IdOut = SCOPE_IDENTITY()
SELECT @IdOut

	

END

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Resource_SELECT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Resource_SELECT]
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED


SELECT * FROM
	[dbo].[Resource]
    

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Resource_SELECT_BY_Id]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Resource_SELECT_BY_Id]
	@Id bigint
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT * FROM
	[dbo].[Resource]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Resource_SELECT_BY_Name]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Resource_SELECT_BY_Name]
	@Name VARCHAR(50)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT * FROM
	[dbo].[Resource]
WHERE
	[Name] = @Name

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Resource_SELECT_BY_ResourceId]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Resource_SELECT_BY_ResourceId]
	@ResourceId INT
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT * FROM
	[dbo].[Resource]
WHERE
	[ResourceId] = @ResourceId

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Resource_SELECT_BY_ServiceId]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Resource_SELECT_BY_ServiceId]
	@ServiceId INT
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT * FROM
	[dbo].[Resource]
WHERE
	[ServiceId] = @ServiceId

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Resource_UPDATE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Resource_UPDATE]
	@Id bigint,
	@ResourceId int,
    @Name varchar(100),
    @ServiceId int,
    @Description varchar(max),
	@IsEnabled bit,
	@DateModified datetime
AS

UPDATE [dbo].[Resource] SET
	[ResourceId] = @ResourceId,
	[Name] = @Name,
	[Description] = @Description,
    [ServiceId] = @ServiceId,
	[IsEnabled] = @IsEnabled,
	[DateModified] = @DateModified
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Service_DEACTIVE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Service_DEACTIVE]
	@Id bigint
AS


UPDATE [dbo].[Service] SET
	[IsActive] = 0
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Service_DELETE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Service_DELETE]
	@Id bigint
AS


DELETE FROM [dbo].[Service]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Service_INSERT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Service_INSERT]
	@Name varchar(100),
    @Description varchar(max),
	@IsActive bit,
	@IsEnabled bit,
	@DateCreated datetime,
	@Id bigint OUTPUT
AS


INSERT INTO [dbo].[Service]
	(
	    [Name],
        [Description],
		[IsEnabled],
		[IsActive],
		[DateCreated]
	)
VALUES
	(
	    @Name,
        @Description,
	    @IsEnabled,
	    @IsActive,
	    @DateCreated
)

SET @Id = SCOPE_IDENTITY()
SELECT @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Service_INSERTUPDATE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Service_INSERTUPDATE]
	@Id bigint,
    @IdOut bigint OUTPUT,
	@Name varchar(100),
    @Description varchar(max),
	@IsActive bit,
	@IsEnabled bit,
	@DateCreated datetime,
	@DateModified datetime

AS


IF EXISTS(SELECT [Id]
FROM [dbo].[Service]
WHERE [Id] = @Id)
BEGIN
	
UPDATE [dbo].[Service] SET
	[Name] = @Name,
	[Description] = @Description,
	[IsEnabled] = @IsEnabled,
	[DateModified] = @DateModified
WHERE
	[Id] = @Id

    SET @IdOut = @Id

    SELECT @IdOut

END
ELSE
BEGIN

INSERT INTO [dbo].[Service]
	(
	    [Name],
        [Description],
		[IsEnabled],
		[IsActive],
		[DateCreated]
	)
VALUES
	(
	    @Name,
        @Description,
	    @IsEnabled,
	    @IsActive,
	    @DateCreated
)

SET @IdOut = SCOPE_IDENTITY()
SELECT @IdOut

	

END

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Service_SELECT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Service_SELECT]
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED


SELECT * FROM
	[dbo].[Service]

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Service_SELECT_BY_Id]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Service_SELECT_BY_Id]
	@Id bigint
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT * FROM
	[dbo].[Service]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Service_SELECT_BY_Name]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Service_SELECT_BY_Name]
	@Name VARCHAR(50)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT * FROM
	[dbo].[Service]
WHERE
	[Name] = @Name

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_Service_UPDATE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Service_UPDATE]
	@Id bigint,
    @Name varchar(100),
    @Description varchar(max),
	@IsEnabled bit,
	@DateModified datetime
AS

UPDATE [dbo].[Service] SET
	[Name] = @Name,
	[Description] = @Description,
	[IsEnabled] = @IsEnabled,
	[DateModified] = @DateModified
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_SystemSettings_DELETE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_SystemSettings_DELETE]
	@Id bigint
AS


DELETE FROM [dbo].[SystemSettings]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_SystemSettings_INSERT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_SystemSettings_INSERT]
	@Key nvarchar(max),
    @Value nvarchar(max),
	@Group nvarchar(max),
	@Description varchar(450),
	@Id bigint OUTPUT
AS


INSERT INTO [dbo].[SystemSettings]
	(
	    [Key],
        [Value],
		[Group],
		[Description]
	)
VALUES
	(
	    @Key,
        @Value,
	    @Group,
		@Description
)

SET @Id = SCOPE_IDENTITY()
SELECT @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_SystemSettings_INSERTUPDATE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_SystemSettings_INSERTUPDATE]
	@Key nvarchar(max),
    @Value nvarchar(max),
	@Group nvarchar(max),
	@Description varchar(450),
	@Id bigint,
    @IdOut bigint OUTPUT

AS


IF EXISTS(SELECT [Id]
FROM [dbo].[SystemSettings]
WHERE [Id] = @Id)
BEGIN
	
UPDATE [dbo].[SystemSettings] SET
	[Key] = @Key,
	[Description] = @Description,
	[Value] = @Value,
	[Group] = @Group
WHERE
	[Id] = @Id

    SET @IdOut = @Id

    SELECT @IdOut

END
ELSE
BEGIN

INSERT INTO [dbo].[SystemSettings]
	(
	    [Key],
        [Value],
		[Group],
		[Description]
	)
VALUES
	(
	    @Key,
        @Value,
	    @Group,
		@Description
)

SET @Id = SCOPE_IDENTITY()
SELECT @Id

END

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_SystemSettings_SELECT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_SystemSettings_SELECT]
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED


SELECT * FROM
	[dbo].[SystemSettings]

--endregion



PRINT N'Creating [dbo].[usp_SystemSettings_SELECT_BY_Group]'
GO
/****** Object:  StoredProcedure [dbo].[usp_SystemSettings_SELECT_BY_Group]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_SystemSettings_SELECT_BY_Group]
(
	@Group nvarchar(max)
)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED


SELECT * FROM
	[dbo].[SystemSettings]
WHERE
	[Group] = @Group
--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_SystemSettings_SELECT_BY_Id]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_SystemSettings_SELECT_BY_Id]
	@Id bigint
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT * FROM
	[dbo].[SystemSettings]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_SystemSettings_SELECT_BY_Key]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_SystemSettings_SELECT_BY_Key]
	@Key VARCHAR(50)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT * FROM
	[dbo].[SystemSettings]
WHERE
	[Key] = @Key

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_SystemSettings_UPDATE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_SystemSettings_UPDATE]
	@Key nvarchar(max),
    @Value nvarchar(max),
	@Group nvarchar(max),
	@Description varchar(450),
	@Id bigint
AS

UPDATE [dbo].[SystemSettings] SET
	[Key] = @Key,
	[Description] = @Description,
	[Value] = @Value,
	[Group] = @Group
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_User_DEACTIVE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_DEACTIVE]
	@Id bigint
AS


UPDATE [dbo].[Users] SET
	[IsActive] = 0
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_User_DELETE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_DELETE]
	@Id bigint
AS


DELETE FROM [dbo].[Users]
WHERE
	[Id] = @Id
SELECT @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_User_INSERT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_INSERT]
	@FirstName nvarchar(100),
    @LastName varchar(100),
    @GenderId int,
    @DateOfBirth datetime2,
    @Nationality int,
    @Race int,
    @Username nvarchar(100),
	@Email nvarchar(100),
	@IsActive bit,
	@IsEnabled bit,
	@DateCreated datetime,
	@Id bigint OUTPUT
AS


INSERT INTO [dbo].[Users]
(
    [FirstName],
    [LastName],
    [GenderId],
    [DateOfBirth],
    [Nationality],
    [Race],
	[Username],
	[Email],
	[IsActive],
	[IsEnabled],
	[DateCreated]
)
VALUES
(
	@FirstName,
    @LastName,
    @GenderId,
    @DateOfBirth,
    @Nationality,
    @Race,
    @Username,
	@Email,
	@IsActive,
	@IsEnabled,
	@DateCreated
)

SET @Id = SCOPE_IDENTITY()
SELECT @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_User_INSERTUPDATE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_INSERTUPDATE]
	@Id bigint,
	@Email [varchar](100),
	@Username [varchar](100),
    @Password [varchar](max),
	@IsEnabled [bit],
	@DateModified [datetime],
	@IsActive [bit],
	@DateCreated [datetime],
	@LastSeen [datetime]
AS


IF EXISTS(SELECT [Id]
FROM [dbo].[Users]
WHERE [Id] = @Id)
BEGIN
	UPDATE [dbo].[Users] SET
		[Email] = @Email,
		[Username] = @Username,
    	[Password] = @Password,
		[IsEnabled] = @IsEnabled,
		[DateModified] = @DateModified,
		[LastSeen] = @LastSeen
	WHERE
		[Id] = @Id
END
ELSE
BEGIN

	INSERT INTO [dbo].[Users]
		(
			[Username],
			[Email],
			[Password],
			[IsActive],
			[DateCreated]
		)
	VALUES
		(
			@Username,
			@Email,
			@Password,
			@IsActive,
			@DateCreated
		)

	SET @Id = SCOPE_IDENTITY()
	SELECT @Id

END

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_User_REGISTER]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_REGISTER]
		@Username nvarchar(100),
		@Email nvarchar(100),
    	@Password nvarchar(max),
        @Salt nvarchar(max),
		@IsActive bit,
		@IsEnabled bit,
		@DateCreated datetime,
		@Id bigint OUTPUT
	AS

	INSERT INTO [dbo].[Users]
	(
		[Username],
		[Email],
		[Password],
        [Salt],
		[IsActive],
		[IsEnabled],
		[DateCreated]
	)

	VALUES
	(
		@Username,
		@Email,
		@Password,
        @Salt,
		@IsActive,
		@IsEnabled,
		@DateCreated
	)

	SET @Id = SCOPE_IDENTITY()
	SELECT @Id

	--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_User_SELECT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_User_SELECT]
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED


SELECT	* FROM [dbo].[Users]

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_User_SELECT_BY_Email]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_SELECT_BY_Email]
	@Email VARCHAR(100)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT
	[Id],
	[Email],
	[Username],
    [Password],
	[IsEnabled],
	[IsActive],
	[DateCreated],
	[DateModified],
	[LastSeen]
FROM
	[dbo].[Users]
WHERE
	[Email] = @Email

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_User_SELECT_BY_Id]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_SELECT_BY_Id]
	@Id bigint
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT
	[Id],
	[Email],
	[Username],
    [Password],
	[IsEnabled],
	[IsActive],
	[DateCreated],
	[DateModified],
	[LastSeen]
FROM
	[dbo].[Users]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_User_SELECT_BY_Username]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_SELECT_BY_Username]
	@Username varchar(100)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT * FROM
	[dbo].[Users]
WHERE
	[Username] = @Username

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_User_SELECT_BY_Username_and_Email]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_SELECT_BY_Username_and_Email]
	@Username varchar(100)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT 
	[Id],
	[Email],
	[Username],
	[IsEnabled],
	[IsActive],
	[DateCreated],
	[DateModified],
	[LastSeen],
	[FirstName],
	[LastName],
	[GenderId],
	[DateOfBirth],
	[Nationality],
	[Race],
	[Password],
	[Salt],
	[IsVerified],
	[DateVerified] FROM
	[dbo].[Users]
WHERE
	[Username] = @Username OR [Email] = @Username

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_User_UPDATE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_UPDATE]
	@Id bigint,
	@Email [varchar](100),
    @FirstName [varchar] (100),
    @LastName [varchar] (100),
    @GenderId [int],
    @RaceId [int],
    @NationalityId [int],
    @DateOfBirth [datetime2],
	@Username [varchar](100),
	@DateModified [datetime]
AS

UPDATE [dbo].[Users] SET
	[Email] = @Email,
    [FirstName] = @FirstName,
    [LastName] = @LastName,
    [GenderId] = @GenderId,
    [Race] = @RaceId,
    [Nationality] = @NationalityId,
    [DateOfBirth] = @DateOfBirth,
	[Username] = @Username,
	[DateModified] = @DateModified
WHERE
	[Id] = @Id

SELECT @Id
--endregion
GO

/****** Object:  StoredProcedure [dbo].[usp_User_UPDATE_PASSWORD_BY_ID]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_UPDATE_PASSWORD_BY_ID]
	@Id bigint,
    @Password varchar(250),
    @Salt varchar(250)

AS

UPDATE [dbo].[Users]
SET
[Password] = @Password,
[Salt] = @Salt

WHERE
	[Id] = @Id
GO

/****** Object:  StoredProcedure [dbo].[usp_User_VERIFY]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_VERIFY]
	@Username [varchar](100),
    @IsVerified [bit],
    @DateVerified [datetime2],
	@DateModified [datetime2]
AS

UPDATE [dbo].[Users] SET
	[IsVerified] = @IsVerified,
    [DateVerified] = @DateVerified,
	[DateModified] = @DateModified
WHERE
	[Username] = @Username

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_UserRole_DELETE]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UserRole_DELETE]
	@Id bigint
AS


DELETE FROM [dbo].[UserRole]
WHERE
	[Id] = @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_UserRole_DELETE_BY_RoleId_UserId]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UserRole_DELETE_BY_RoleId_UserId]
	@UserId int,
    @RoleId int
AS


DELETE FROM [dbo].[UserRole]
WHERE
	[UserId] = @UserId AND [RoleId] = @RoleId

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_UserRole_INSERT]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UserRole_INSERT]
	@RoleId int,
	@UserId int,
	@Id bigint OUTPUT
AS


INSERT INTO [dbo].[UserRole]
	(
	    [RoleId],
        [UserId]
	)
VALUES
	(
	    @RoleId,
        @UserId
)

SET @Id = SCOPE_IDENTITY()
SELECT @Id

--endregion
GO
/****** Object:  StoredProcedure [dbo].[usp_UserRole_SELECT_BY_UserId]    Script Date: 7/24/2022 3:40:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UserRole_SELECT_BY_UserId]
(
    @UserId int
)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED


SELECT * FROM
	[dbo].[UserRole]
WHERE [UserId] = @UserId

--endregion
GO
USE [master]
GO
ALTER DATABASE [AccountService] SET  READ_WRITE 
GO
