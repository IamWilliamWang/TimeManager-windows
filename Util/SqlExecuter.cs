using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 关机小程序.Util
{
    class SqlExecuter
    {
        private static SqlExecuter mySqlExecuter = new SqlExecuter();
        public readonly string connString = Properties.Settings.Default.TimeDatabaseConnectionString;
        SqlConnection connection;

        public SqlExecuter()
        {
            connection = new SqlConnection(connString);//建立到数据库的连接
        }

        public bool executeUpdate(string sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public DataTable executeQuery(string sql)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        //public void 记录关机事件()//Doesn't work yet
        //{
        //    记录关机事件("'00:00:00'");
        //}

        public void 记录关机事件()
        {
            //if (关机倒计时字段.Contains("'") == false)
            //{
            //    关机倒计时字段 = "'" + 关机倒计时字段 + "'";
            //}
            //DateTime now = DateTime.Now;
            //String sql = "UPDATE " + SqlServerResult.TableName + " "
            //            + "SET 关机时间=\'" + DateTime.Now + "\' "
            //            + "WHERE 序号=" + get最大序号() + " ";
            String sql =
                "UPDATE [Table] " +
                "SET 关机时间 = GETDATE(), 时长 = GETDATE() - 开机时间 + " + " " +
                "WHERE 序号 in " +
                "(SELECT MAX(序号) " +
                "FROM[Table]) ";

            executeUpdate(sql);
        }

        public void 记录结算()
        {
            if (MessageBox.Show("结算可能会持续数十秒，是否继续？", "结算使用记录", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }
            String sql =
                "declare @i int " +
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
                "end ";

            executeUpdate(sql);
        }
     
        private int get最大序号()/*为了避免在SQL查询中查找空数据库而产生错误，但会一定程度降低程序效率*/
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

        public static SqlExecuter getInstance()
        {
            return mySqlExecuter;
        }
    }
}
