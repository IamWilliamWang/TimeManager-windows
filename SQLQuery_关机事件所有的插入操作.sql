UPDATE [Table]
SET 关机时间=GETDATE(),时长=GETDATE()-开机时间
WHERE 序号 in (
SELECT MAX(序号)
FROM [Table])

UPDATE [Table]
SET 当天使用次数=(
SELECT COUNT(*)
FROM [Table] AS FatherTable
WHERE EXISTS(
SELECT *
FROM [Table]
WHERE 序号=(SELECT MAX(序号) FROM [Table])
AND YEAR([Table].开机时间)=YEAR([FatherTable].开机时间)
AND MONTH([Table].开机时间)=MONTH([FatherTable].开机时间)
AND DAY([Table].开机时间)=DAY([FatherTable].开机时间)))
WHERE 序号=(
SELECT MAX(序号)
FROM [Table])