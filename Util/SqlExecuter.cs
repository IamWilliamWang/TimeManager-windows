using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace 关机助手.Util
{
    class SqlExecuter
    {


        //public void 记录关机事件()//Doesn't work yet
        //{
        //    记录关机事件("'00:00:00'");
        //}

        /// <summary>
        /// 请使用 "记录开机事件(TableName)" 代替本方法
        /// </summary>
        [Obsolete]
        public static void 记录开机事件()
        {
            String sql = "INSERT INTO[Table](开机时间) " +
                       "VALUES (GETDATE())";

            SqlServerConnection.ExecuteUpdate(sql);
        }

        public static bool 记录开机事件(String TableName)
        {
            if (SqlServerConnection.ExecuteUpdate(InsertPowerOnTimeSQL(TableName)) != 0)
                return true;
            return false;
        }

        private static string InsertPowerOnTimeSQL(String TableName)
        {
            return "INSERT "
                    + "INTO " + TableName + "(开机时间) "
                    + "VALUES (" + "\'" + DateTime.Now + "\')";
        }

        public static void 记录关机事件()
        {
            SqlServerConnection.ExecuteUpdate(UpdateShutdownTimeSQL());
        }

        public static void 记录关机事件(String 延迟时间)
        {
            if (延迟时间[0] != '\'' && 延迟时间[延迟时间.Length - 1] != '\'')
            {
                SqlServerConnection.ExecuteUpdate(UpdateShutdownTimeSQL("'"+延迟时间+"'"));
            }
            else
                SqlServerConnection.ExecuteUpdate(UpdateShutdownTimeSQL(延迟时间));
        }

        private static string UpdateShutdownTimeSQL()
        {
            return "UPDATE [Table] " +
                "SET 关机时间 = GETDATE(), 时长 = GETDATE() - 开机时间  " +
                "WHERE 序号 in " +
                "(SELECT MAX(序号) " +
                "FROM[Table]) ";
        }

        private static string UpdateShutdownTimeSQL(String 延迟时间)
        {
            return "UPDATE [Table] " +
                "SET 关机时间 = GETDATE()+" + 延迟时间 +
                ", 时长 = GETDATE()+" + 延迟时间 + " - 开机时间  " +
                "WHERE 序号 in " +
                "(SELECT MAX(序号) " +
                "FROM[Table]) ";
        }

        public static void 记录结算()
        {
            if (MessageBox.Show("填补可能会持续数十秒，是否继续？", "一键填补", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            SqlServerConnection.ExecuteUpdate(CalculateEverydayTimesAndUsedTimesSQL());
        }
     
        private static string CalculateEverydayTimesAndUsedTimesSQL()
        {
            return "declare @i int " +
                "set @i = (SELECT MAX(序号) FROM[Table]) " +
                "while @i > 0 " +
                "begin " +
                "    UPDATE[Table] " +
                "    SET 当天使用次数 = ( " +
                "        SELECT COUNT(*) " +
                "        FROM[Table] AS FatherTable " +
                "        WHERE EXISTS( " +
                "            SELECT * " +
                "            FROM[Table] " +
                "            WHERE 序号 = (@i) " +
                "                AND YEAR([Table].开机时间) = YEAR([FatherTable].开机时间) " +
                "                AND MONTH([Table].开机时间) = MONTH([FatherTable].开机时间) " +
                "                AND DAY([Table].开机时间) = DAY([FatherTable].开机时间) " +
                "            ) " +
                "        ) " +
                "    WHERE 序号 = (@i) " +
                "    set @i = @i - 1 " +
                "end " +
                "set @i = (SELECT MAX(序号) FROM[Table]) " +
                "while @i > 0 " +
                "begin " +
                "    UPDATE[Table] " +
                "    SET 当月使用次数 = ( " +
                "        SELECT COUNT(*) " +
                "        FROM[Table] AS FatherTable " +
                "        WHERE EXISTS( " +
                "            SELECT * " +
                "            FROM[Table] " +
                "            WHERE 序号 = (@i) " +
                "                AND YEAR([Table].开机时间) = YEAR([FatherTable].开机时间) " +
                "                AND MONTH([Table].开机时间) = MONTH([FatherTable].开机时间) " +
                "            ) " +
                "        ) " +
                "    WHERE 序号 = (@i) " +
                "    set @i = @i - 1 " +
                "end ";
        }
        private int 最大序号()/*为了避免在SQL查询中查找空数据库而产生错误，但会一定程度降低程序效率*/
        {
            int 序号 = 0;
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.TimeDatabaseConnectionString))
            {
                conn.Open();//打开数据库  
                            //Console.WriteLine("数据库打开成功!");  
                            //创建数据库命令  
                SqlCommand cmd = conn.CreateCommand();
                //创建查询语句  
                cmd.CommandText = "SELECT MAX(序号) FROM [Table]";
                //从数据库中读取数据流存入reader中  
                SqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    //从reader中读取下一行数据,如果没有数据,reader.Read()返回flase  
                    while (reader.Read())
                    {
                        //for(int i=0;i<reader.FieldCount;i++)
                        //    Console.WriteLine()
                        序号 = reader.GetInt32(0);
                    }
                }
                catch (System.Data.SqlTypes.SqlNullValueException 空数据库异常)
                {
                    序号 = 0;
                }
            }
            return 序号;
        }

        /// <summary>
        /// Please use SqlServerStatement.getStatement() instead
        /// </summary>
        /// <returns></returns>
        //[Obsolete]
        //public static SqlServerConnection GetInstance()
        //{
        //    return SqlServerConnection.GetStatement();
        //}
    }
}
