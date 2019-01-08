CREATE TABLE [dbo].[Table] (
    [序号]     INT      NOT NULL,
    [开机时间]   DATETIME NOT NULL,
    [关机时间]   DATETIME NULL,
    [时长]     TIME      NULL,
	[当天使用次数] INT      NULL,
    PRIMARY KEY CLUSTERED ([开机时间] ASC)
);

