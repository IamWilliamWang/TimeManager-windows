using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace 关机助手.Util
{
    class SqlExecuter
    {
        static SqlConnectionAgency dbAgency = new SqlConnectionAgency();

        //public void 记录关机事件()//Doesn't work yet
        //{
        //    记录关机事件("'00:00:00'");
        //}

        /// <summary>
        /// 推荐使用 "记录开机事件(TableName)" 代替本方法
        /// </summary>
        
        public static void 记录开机事件()
        {
            //String sql = "INSERT INTO[Table](开机时间) " +
            //           "VALUES (GETDATE())";

            //sqlite.ExecuteUpdate(sql);
            记录开机事件("[Table]");
        }

        public static bool 记录开机事件(String TableName)
        {
            if (!SqlServerConnection.ConnectionOpenned())
            {
                dbAgency.ExecuteUpdate_delay(InsertPowerOnTimeSQL(TableName));
                return true;
            }
            else
                return dbAgency.ExecuteUpdate(InsertPowerOnTimeSQL(TableName)) != 0;
        }

        private static string InsertPowerOnTimeSQL(String TableName)
        {
            if(SqlConnectionAgency.DBType == SqlConnectionAgency.DatabaseType.MSSqlServer)
                return "INSERT "
            + "INTO " + TableName + "(开机时间) "
            + "VALUES (GETDATE())";
            else return
                "INSERT "
           + "INTO " + TableName + "(开机时间) "
           + "VALUES (\'"+DateTime.Now.ToString("s")+"\')";
        }
            

        public static bool 记录关机事件()
        {
            if (!SqlServerConnection.ConnectionOpenned())
            {
                dbAgency.ExecuteUpdate_delay(UpdateShutdownTimeSQL());
                return true;
            }
            else
                return dbAgency.ExecuteUpdate(UpdateShutdownTimeSQL()) != 0;
            
        }

        public static bool 记录延迟关机事件(String 延迟时间)
        {
            if (延迟时间[0] != '\'' && 延迟时间[延迟时间.Length - 1] != '\'')
            {
                return dbAgency.ExecuteUpdate(UpdateShutdownTimeSQL("'"+延迟时间+"'")) != 0;
            }
            else
                return dbAgency.ExecuteUpdate(UpdateShutdownTimeSQL(延迟时间)) != 0;
        }

        private static string UpdateShutdownTimeSQL()
        {
            return UpdateShutdownTimeSQL(SqlConnectionAgency.DBType);
        }

        private static string UpdateShutdownTimeSQL(SqlConnectionAgency.DatabaseType databaseType)
        {
            if(databaseType == SqlConnectionAgency.DatabaseType.SqLite)
                return "UPDATE [Table] " +
            "SET 关机时间 = \'"+DateTime.Now.ToString("s")+"\', 时长 = \'" + DateTime.Now.ToString("s") + "\' - 开机时间  " +
            "WHERE 序号 in " +
            "(SELECT MAX(序号) " +
            "FROM[Table]) ";
            else return "UPDATE [Table] " +
            "SET 关机时间 = GETDATE(), 时长 = GETDATE() - 开机时间  " +
            "WHERE 序号 in " +
            "(SELECT MAX(序号) " +
            "FROM[Table]) ";
        }
            

        private static string UpdateShutdownTimeSQL(String 延迟时间) =>
            "UPDATE [Table] " +
            "SET 关机时间 = GETDATE()+" + 延迟时间 +
            ", 时长 = GETDATE()+" + 延迟时间 + " - 开机时间  " +
            "WHERE 序号 in " +
            "(SELECT MAX(序号) " +
            "FROM[Table]) ";

        public static void 记录结算()
        {
            if (MessageBox.Show("填补可能会持续数十秒，是否继续？", "一键填补", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            dbAgency.ExecuteUpdate(CalculateEverydayTimesAndUsedTimesSQL());
        }
     
        private static string CalculateEverydayTimesAndUsedTimesSQL() =>
            "declare @i int " + //填补当天使用次数
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


            "    UPDATE[Table] " + //填补所有的当月使用次数栏
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
            "end " +


            "update[Table] " + //手动插入关机时间后运行此代码，将该填写的时长填补完成
            "set 时长 = 关机时间 - 开机时间 "+
            "where 关机时间 is not null and 时长 is null";

        /// <summary>
        /// 请使用GetMaxId()代替
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public static int 获取最大序号()/*为了避免在SQL查询中查找空数据库而产生错误，但会一定程度降低程序效率*/
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
                catch (System.Data.SqlTypes.SqlNullValueException) //空数据库异常
                {
                    序号 = 0;
                }
            }
            return 序号;
        }

        public static int GetMaxId()
        {
            DataTable table = dbAgency.ExecuteQuery("SELECT MAX(序号) FROM [Table]");
            int maxId = int.Parse(table.Rows[0][0].ToString());
            return maxId >= 0 ? maxId : 0;
        }

        /// <summary>
        /// Please use SqlServerStatement.getStatement() instead
        /// </summary>
        /// <returns></returns>
        //[Obsolete]
        //public static sqlite GetInstance()
        //{
        //    return sqlite.GetStatement();
        //}
    }
}
