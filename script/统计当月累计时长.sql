Drop function SecToDateTime
Go

Create function SecToDateTime(@total int) /*将second转换成 天,时:分:秒*/
Returns varchar(50) /*返回类型为varchar*/
As
Begin
   Declare @day int
   Declare @hour int
   Declare @min int
   Declare @sec int
   Declare @dt varchar(50)
   set @sec = @total%60 /*秒数*/
   set @total = floor(@total/60) /*total变为多少分钟*/
   set @min = @total%60 /*分数*/
   set @total = floor(@total/60) /*total变为多少小时*/
   set @hour = @total%24 /*天数*/
   set @total = floor(@total/24) /*total变为多少天*/
   set @day = @total
   
   set @dt = convert(varchar(50),@day)+' days '+convert(varchar(50),@hour)+':'+convert(varchar(50),@min)+':'+convert(varchar(50),@sec)
Return @dt
End
Go
/*创建该函数对象*/
/*重复创建需要先 drop function SecToDateTime */

select YEAR(开机时间) 年,MONTH(开机时间) 月,dbo.SecToDateTime(sum(datediff(second,'00:00:00',时长))) 当月累计时长 /*into 每月累计时长表*/
from [Table]
group by YEAR(开机时间),MONTH(开机时间)
order by YEAR(开机时间),MONTH(开机时间);
/*where YEAR(开机时间) == parentTable.YEAR(开机时间) and MONTH(开机时间) == parentTable.MONTH(开机时间)

update [Table]
set 当月时长累计=每月累计时长表.当月累计时长
where [Table].开机时间=每月累计时长表.年 and [Table].开机时间=每月累计时长表.月
*/
