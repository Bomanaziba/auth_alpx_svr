print('===========================================================================')
GO
print('Start Dropping Script for Login Resource')
GO
print('---------------------------------------------------------------------------')
GO

IF NOT EXISTS (SELECT * FROM [dbo].[MigrationsJournal] WHERE [ScriptName] = 'Register' OR [ScriptName] = 'ForgetPassword' OR [ScriptName] = 'ResetPassword')
BEGIN
	IF OBJECT_ID('[dbo].[Users]') IS NOT NULL
	BEGIN
		DROP TABLE [dbo].[Users]
	END
	IF OBJECT_ID('[dbo].[usp_User_SELECT_BY_Username_and_Email]') IS NOT NULL
	BEGIN
		DROP PROCEDURE [dbo].[usp_User_SELECT_BY_Username_and_Email]
	END
END
GO

IF  OBJECT_ID(N'[dbo].[ClientJwtTokenKey]') IS NOT NULL
BEGIN
	DROP TABLE [dbo].[ClientJwtTokenKey]
END
GO

IF OBJECT_ID('[dbo].[usp_ClientJwtTokenKey_SELECT_BY_ID]') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[usp_ClientJwtTokenKey_SELECT_BY_ID]
END
GO

DELETE FROM [dbo].[MigrationsJournal]
WHERE [Name] = 'Login' OR [Name] = 'LoginDrop'
GO

IF NOT EXISTS (SELECT * FROM [dbo].[MigrationsJournal])
BEGIN
	DROP TABLE [dbo].[MigrationsJournal]
END
GO

print('---------------------------------------------------------------------------')
GO
print('Finished Dropping Script for Login Resource')
GO
print('===========================================================================')
GO