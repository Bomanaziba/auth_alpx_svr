print('===========================================================================')
GO
print('Start Dropping Script for Register Resource')
GO
print('---------------------------------------------------------------------------')
GO

IF NOT EXISTS (SELECT * FROM [dbo].[MigrationsJournal] WHERE [ScriptName] = 'Login' OR [ScriptName] = 'ForgetPassword' OR [ScriptName] = 'ResetPassword')
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

IF OBJECT_ID('[dbo].[usp_User_REGISTER]') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[usp_User_REGISTER]
END
GO

DELETE FROM [dbo].[MigrationsJournal]
WHERE [Name] = 'Register' OR [Name] = 'RegisterDrop'
GO

IF NOT EXISTS (SELECT * FROM [dbo].[MigrationsJournal])
BEGIN
	DROP TABLE [dbo].[MigrationsJournal]
END

print('---------------------------------------------------------------------------')
GO
print('Finished Dropping Script for Register Resource')
GO
print('===========================================================================')
GO