Create function SecToDateTime(@sec int) /*将second转换成 天,时:分:秒*/
Returns varchar(50) /*返回类型为varchar*/
As
Begin
   Declare @day int
   Declare @hour int
   Declare @min int
   Declare @dt varchar(50)
   set @day = 0
   set @hour = 0
   set @min = 0
   while @sec > 60
   begin
      set @sec = @sec-60
      set @min = @min+1
   end

   while @min > 60
   begin
      set @min = @min-60
      set @hour = @hour+1
   end

   while @hour > 24
   begin
      set @hour = @hour-24
      set @day = @day+1
   end
   
   set @dt = convert(varchar(50),@day)+','+convert(varchar(50),@hour)+':'+convert(varchar(50),@min)+':'+convert(varchar(50),@sec)
Return @dt
End
Go
/*创建该函数对象*/
/*重复创建需要先 drop function SecToDateTime */

select YEAR(开机时间) 年,MONTH(开机时间) 月,dbo.SecToDateTime(sum(datediff(second,'00:00:00',时长))) 当月累计时长 /*into 每月累计时长表*/
from [Table]
group by YEAR(开机时间),MONTH(开机时间);
/*where YEAR(开机时间) == parentTable.YEAR(开机时间) and MONTH(开机时间) == parentTable.MONTH(开机时间)

update [Table]
set 当月时长累计=每月累计时长表.当月累计时长
where [Table].开机时间=每月累计时长表.年 and [Table].开机时间=每月累计时长表.月
*/
