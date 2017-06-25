/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


/*-----------------------------------------------
	[MainPreferenceSet] Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[MainPreferenceSet])
BEGIN
	BEGIN TRANSACTION;
	INSERT INTO [dbo].[MainPreferenceSet]([MainPreferenceSetID], [MainPreferenceSetName])
	SELECT N'dc0981bd-0164-4042-a313-5d79cff5211c', N'Set Store' UNION ALL
	SELECT N'72f5566e-3bf5-46e6-9406-b13e80f83bcc', N'My Sets' UNION ALL
	SELECT N'78ac5cf7-a5ab-4377-b9f9-d105f462c26e', N'Organization Sets'
	COMMIT;
	RAISERROR (N'[dbo].[MainPreferenceSet]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;

END
GO


/*-----------------------------------------------
	[IssueType] Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[IssueType])

BEGIN

	BEGIN TRANSACTION;
	INSERT INTO [dbo].[IssueType]([IssueTypeID], [IssueTypeName])
	SELECT N'6225fbd2-c4eb-474d-834f-4818bde8e4eb', N'Later Rated' UNION ALL
	SELECT N'6125fbd2-c4eb-474d-834f-4818bde8e4eb', N'Options' UNION ALL
	SELECT N'6025fbd2-c4eb-474d-834f-4818bde8e4eb', N'Numeric' UNION ALL
	SELECT N'6325fbd2-c4eb-474d-834f-4818bde8e4eb', N'Not Rated'UNION ALL
	SELECT N'00000000-0000-0000-0000-000000000000', N'Select One'
	COMMIT;
	RAISERROR (N'[dbo].[IssueType]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;

END

GO


IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'PrefAppUser')
DROP USER [PrefAppUser]
GO

/****** Object:  Login [PrefAppUser]    Script Date: 08/25/2010 10:31:45 ******/
IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'PrefAppUser')
DROP LOGIN [PrefAppUser]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [PrefAppUser]    Script Date: 08/25/2010 10:31:45 ******/
CREATE LOGIN [PrefAppUser] WITH PASSWORD='PrefAppUserPassword', DEFAULT_DATABASE=[PrefApp], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

 
CREATE USER [PrefAppUser] FOR LOGIN [PrefAppUser] 
GO


EXEC sp_addrolemember N'db_owner', N'PrefAppUser'
go
