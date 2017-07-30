Create function SecToDateTime(@sec int)
Returns varchar(20)
As
Begin
   Declare @day int
   Declare @hour int
   Declare @min int
   Declare @datetime varchar(20)
   set @day = 0
   set @hour = 0
   set @min = 0
   while @sec > 60
   begin
      set @sec = @sec-60
      set @min = @min+1
   end

   while @min > 60
      set @min = @min-60
      set @hour = @hour+1
   end

   /*while @hour > 24
      set @hour = @hour-24
      set @day = @day+1
   end*/
   
   set @datetime = ''+@hour
Return @datetime

End

select YEAR(开机时间),MONTH(开机时间),dbo.SecToDateTime(sum(datediff(second,'00:00:00',时长)))
from [Table]
group by YEAR(开机时间),MONTH(开机时间)