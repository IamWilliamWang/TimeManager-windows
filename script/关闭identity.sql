/*
C:\USERS\WILLIAM\DESKTOP\TIMEDATABASE.MDF 的部署脚本

此代码由工具生成。
如果重新生成此代码，则对此文件的更改可能导致
不正确的行为并将丢失。
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "C:\USERS\WILLIAM\DESKTOP\TIMEDATABASE.MDF"
:setvar DefaultFilePrefix "C_\USERS\WILLIAM\DESKTOP\TIMEDATABASE.MDF_"
:setvar DefaultDataPath "C:\Users\william\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\william\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
请检测 SQLCMD 模式，如果不支持 SQLCMD 模式，请禁用脚本执行。
要在启用 SQLCMD 模式后重新启用脚本，请执行:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'要成功执行此脚本，必须启用 SQLCMD 模式。';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'正在开始重新生成表 [dbo].[Table]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Table] (
    [序号]     INT      NOT NULL,
    [开机时间]   DATETIME NOT NULL,
    [关机时间]   DATETIME NULL,
    [时长]     TIME (7) NULL,
    [当天使用次数] INT      NULL,
    [当月使用次数] INT      NULL,
    PRIMARY KEY CLUSTERED ([开机时间] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Table])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Table] ([开机时间], [序号], [关机时间], [时长], [当天使用次数], [当月使用次数])
        SELECT   [开机时间],
                 [序号],
                 [关机时间],
                 [时长],
                 [当天使用次数],
                 [当月使用次数]
        FROM     [dbo].[Table]
        ORDER BY [开机时间] ASC;
    END

DROP TABLE [dbo].[Table];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Table]', N'Table';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'更新完成。';


GO
