print('===========================================================================')
GO
print('Start Creating Script for REGISTER Resource')
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
print('Started Creating [dbo].[usp_User_SELECT_BY_Id]')
GO
print('---------------------------------------------------------------------------')
GO

IF OBJECT_ID(N'[dbo].[usp_User_SELECT_BY_Id]') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[usp_User_SELECT_BY_Id] AS BEGIN SET NOCOUNT ON; END')
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[usp_User_SELECT_BY_Id]
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
print('Finished Creating [dbo].[usp_User_SELECT_BY_Id]')
GO
print('===========================================================================')
GO



print('===========================================================================')
GO
print('Started Creating [dbo].[usp_User_REGISTER]')
GO
print('---------------------------------------------------------------------------')
GO

IF OBJECT_ID(N'[dbo].[usp_User_REGISTER]') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[usp_User_REGISTER] AS BEGIN SET NOCOUNT ON; END')
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[usp_User_REGISTER]
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

print('---------------------------------------------------------------------------')
GO
print('Finished Creating [dbo].[usp_User_REGISTER]')
GO
print('===========================================================================')
GO




print('===========================================================================')
GO
print('Started Creating [dbo].[usp_User_VERIFY]')
GO
print('---------------------------------------------------------------------------')
GO

IF OBJECT_ID(N'[dbo].[usp_User_VERIFY]') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[usp_User_VERIFY] AS BEGIN SET NOCOUNT ON; END')
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[usp_User_VERIFY]
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

print('---------------------------------------------------------------------------')
GO
print('Finished Creating [dbo].[usp_User_VERIFY]')
GO
print('===========================================================================')
GO


print('---------------------------------------------------------------------------')
GO
print('Finished Creating Script for REGISTER Resource')
GO
print('===========================================================================')
GO
