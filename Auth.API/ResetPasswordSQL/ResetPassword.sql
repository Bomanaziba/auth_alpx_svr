print('===========================================================================')
GO
print('Start Creating Script for Reset Password Resource')
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
print('Started Creating [dbo].[usp_User_UPDATE_PASSWORD_BY_ID]')
GO
print('---------------------------------------------------------------------------')
GO

IF OBJECT_ID(N'[dbo].[usp_User_UPDATE_PASSWORD_BY_ID]') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[usp_User_UPDATE_PASSWORD_BY_ID] AS BEGIN SET NOCOUNT ON; END')
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[usp_User_UPDATE_PASSWORD_BY_ID]
    @Id bigint,
    @Password varchar(250),
    @Salt varchar(250)

AS

UPDATE [dbo].[Users] WITH (NOLOCK)
SET
[Password] = @Password,
[Salt] = @Salt

WHERE
	[Id] = @Id

--endregion
GO

print('---------------------------------------------------------------------------')
GO
print('Finished Creating [dbo].[usp_User_UPDATE_PASSWORD_BY_ID]')
GO
print('===========================================================================')
GO


print('---------------------------------------------------------------------------')
GO
print('Finished Creating Script for Reset Password Resource')
GO
print('===========================================================================')
GO

