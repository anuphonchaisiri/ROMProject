
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.COM_AUTH_USER
	(
	userid varchar(100) NOT NULL,
	username varchar(100) NULL,
	description varchar(200) NULL,
	created_on varchar(14) NULL,
	created_by varchar(20) NULL,
	updated_on varchar(14) NULL,
	updated_by varchar(20) NULL,
	password varchar(45) NOT NULL,
	secret varchar(45) NOT NULL,
	active bit NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.COM_AUTH_USER ADD CONSTRAINT
	PK_COM_AUTH_USER PRIMARY KEY CLUSTERED 
	(
	userid
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.COM_AUTH_USER SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.COM_AUTH_USER', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.COM_AUTH_USER', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.COM_AUTH_USER', 'Object', 'CONTROL') as Contr_Per 