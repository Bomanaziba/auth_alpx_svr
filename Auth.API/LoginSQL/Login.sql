print('===========================================================================')
GO
print('Start Creating Script for LOGIN Resource')
GO
print('---------------------------------------------------------------------------')
GO

print('===========================================================================')
GO
print('Start Creating [dbo].[Users]')
GO
print('---------------------------------------------------------------------------')
GO

IF OBJECT_ID(N'[dbo].[Users]') IS NULL
BEGIN

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

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
	
END
GO

print('---------------------------------------------------------------------------')
GO
print('Finished Creating [dbo].[Users]')
GO
print('===========================================================================')
GO



print('===========================================================================')
GO
print('Start Creating [dbo].[ClientJwtTokenKey]')
GO
print('---------------------------------------------------------------------------')
GO

IF OBJECT_ID(N'[dbo].[ClientJwtTokenKey]') IS NULL
BEGIN

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

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
END
GO

print('---------------------------------------------------------------------------')
GO
print('Finished Creating [dbo].[ClientJwtTokenKey]')
GO
print('===========================================================================')
GO


print('===========================================================================')
GO
print('Started Creating [dbo].[usp_User_SELECT_BY_Username_and_Email]')
GO
print('---------------------------------------------------------------------------')
GO

IF OBJECT_ID(N'[dbo].[usp_User_SELECT_BY_Username_and_Email]') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[usp_User_SELECT_BY_Username_and_Email] AS BEGIN SET NOCOUNT ON; END')
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[usp_User_SELECT_BY_Username_and_Email]
	@Username varchar(100)
AS

SET NOCOUNT ON

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
	[DateVerified]
FROM
	[dbo].[Users] WITH (NOLOCK)
WHERE
	[Id] = @Id
 
--endregion
GO

print('---------------------------------------------------------------------------')
GO
print('Finished Creating [dbo].[usp_User_SELECT_BY_Username_and_Email]')
GO
print('===========================================================================')
GO


print('===========================================================================')
GO
print('Started Creating [dbo].[usp_User_SELECT_BY_Username_and_Email]')
GO
print('---------------------------------------------------------------------------')
GO

IF OBJECT_ID(N'[dbo].[usp_ClientJwtTokenKey_SELECT_BY_ID]') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[usp_ClientJwtTokenKey_SELECT_BY_ID] AS BEGIN SET NOCOUNT ON; END')
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[usp_ClientJwtTokenKey_SELECT_BY_ID]
(
    @ClientId INT
)
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT 
	[Id],
	[ClientId],
	[JwtTokenKey],
	[DateCreated]
 FROM
	[dbo].[ClientJwtTokenKey]
WHERE ClientId = @ClientId
 
--endregion
GO

print('---------------------------------------------------------------------------')
GO
print('Finished Creating [dbo].[usp_ClientJwtTokenKey_SELECT_BY_ID]')
GO
print('===========================================================================')
GO


print('===========================================================================')
GO
print('Started Creating [dbo].[usp_ClientJwtTokenKey_INSERT]')
GO
print('---------------------------------------------------------------------------')
GO

IF OBJECT_ID(N'[dbo].[usp_ClientJwtTokenKey_INSERT]') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[usp_ClientJwtTokenKey_INSERT] AS BEGIN SET NOCOUNT ON; END')
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[usp_ClientJwtTokenKey_INSERT]
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

print('---------------------------------------------------------------------------')
GO
print('Finished Creating [dbo].[usp_ClientJwtTokenKey_INSERT]')
GO
print('===========================================================================')
GO

print('---------------------------------------------------------------------------')
GO
print('Finishing Creating Script for LOGIN Resource')
GO
print('===========================================================================')
GO

