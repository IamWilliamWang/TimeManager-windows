UPDATE [Table]
SET 关机时间 = GETDATE(), 时长 = GETDATE() - 开机时间
WHERE 序号 in
(SELECT MAX(序号)
FROM[Table])