using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace 关机助手.Util
{
    class SqlServerConnection 
    {
        private static readonly String connString = Properties.Settings.Default.TimeDatabaseConnectionString;
        
        public static string DbFullName { get { return GetConnection().Database; } }
        public static SqlServerConnection Instance { get; } = new SqlServerConnection();
        public static ConnectionState ConnectionState { get { return GetConnection().State; } }      
        private SqlConnection connection { get; set; }
        private SqlDataAdapter adapter { get; set; }

        /// <summary>
        /// 内部使用的构造函数
        /// </summary>
        private SqlServerConnection()
        {
            connection = new SqlConnection(connString);//建立到数据库的连接
        }

        public static void OpenConnection()
        {

            if (ConnectionOpenned())
                return;
            if (GetConnection().State == ConnectionState.Connecting)
            {
                throw new Exception("无法建立数据库连接，数据库状态为正在连接");
            }
            try
            {
                TimeUtil.Tik();
                SqlServerConnection.GetConnection().Open();
                TimeUtil.Tok();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static void OpenConnection(String dbFullFilename)
        {
            Instance.connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + dbFullFilename + ";Integrated Security=True;Connect Timeout=30");
            OpenConnection();
        }

        public static void CloseConnection()
        {
            if (!ConnectionOpenned())
                return;

            try
            {
                SqlServerConnection.GetConnection().Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 释放由Component使用的所有资源
        /// </summary>
        public static void DisposeConnection()
        {
            GetConnection().Dispose();
        }

        /// <summary>
        /// 强制释放连接以及所占用的文件
        /// </summary>
        public static void ResetConnection()
        {
            if (GetConnection().State != ConnectionState.Closed)
                SystemCommandUtil.ExecuteCommand("taskkill /fi \"imagename eq sqlservr.exe\" /f");
        }

        public static Boolean ConnectionOpenned()
        {
            return GetConnection().State == ConnectionState.Open;
        }

        public static DataTable ExecuteQuery(string selectCommandText)
        {
            try
            {
                if (GetConnection().State == ConnectionState.Closed)
                    OpenConnection();
                else if (GetConnection().State == ConnectionState.Connecting)
                    return null;

                Instance.adapter = new SqlDataAdapter(selectCommandText, GetConnection());
                SqlCommandBuilder builder = new SqlCommandBuilder(Instance.adapter);
                DataTable table = new DataTable();
                Instance.adapter.Fill(table);
                return table;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int ExecuteUpdate(string commandText)
        {
            try
            {
                if (GetConnection().State == ConnectionState.Closed)
                    OpenConnection();

                SqlCommand cmd = new SqlCommand(commandText, GetConnection());
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void ExecuteQuery_delay(string selectCommandText)
        {
            throw new NotImplementedException();

        }

        public static void ExecuteUpdateUsingCache(string commandText)
        {
            CacheUtil.AppendCache(commandText);
        }

        public static int ClearCache()
        {
            return CacheUtil.CleanDbAndExecuteTasks();
        }

        public static Boolean UpdateDatabase(DataTable dataTable)
        {
            try
            {
                if (GetConnection().State == ConnectionState.Closed)
                    OpenConnection();

                //创建命令重建对象
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(Instance.adapter);
                try
                {
                    if (Instance.adapter.Update(dataTable) == 0)
                        return false;
                }
                catch (System.InvalidOperationException)
                {
                    MessageBox.Show("序号不可变！", "更新失败");
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 不用事务直接执行sql脚本(beta)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static int ExecuteSqlWithGo(String sql)
        {
            int effectedRows = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = SqlServerConnection.GetConnection();
            try
            {
                //注： 此处以 换行_后面带0到多个空格_再后面是go 来分割字符串
                String[] sqlArr = Regex.Split(sql.Trim(), "\r\n\\s*go", RegexOptions.IgnoreCase);
                foreach (string strsql in sqlArr)
                {
                    if (strsql.Trim().Length > 1 && strsql.Trim() != "\r\n")
                    {
                        cmd.CommandText = strsql;
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0)
                            effectedRows += r;
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                CloseConnection();
            }
            return effectedRows;
        }

        /// <summary>
        /// 使用事务直接执行sql脚本(beta)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static int ExecuteSqlWithGoUseTran(String sql)
        {
            int effectedRows = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = GetConnection();
            SqlTransaction tx = GetConnection().BeginTransaction();
            cmd.Transaction = tx;
            try
            {
                //注： 此处以 换行_后面带0到多个空格_再后面是go 来分割字符串
                String[] sqlArr = Regex.Split(sql.Trim(), "\r\n\\s*go", RegexOptions.IgnoreCase);
                foreach (string strsql in sqlArr)
                {
                    if (strsql.Trim().Length > 1 && strsql.Trim() != "\r\n")
                    {
                        cmd.CommandText = strsql;
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0)
                            effectedRows += r;
                    }
                }
                tx.Commit();
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                tx.Rollback();
                throw new Exception(E.Message);
            }
            finally
            {
                CloseConnection();
            }
            return effectedRows;
        }

        /// <summary>
        /// 在程序执行中禁止使用该类的所有函数
        /// </summary>
        public static void Disable()
        {
            CloseConnection();
            Instance.connection = null;
        }

        private static SqlConnection GetConnection()
        {
            return Instance.connection;
        }

        #region Sql错误处理类
        //private static class Error
        //{
        //    public static void SqlExceptionOccur(System.Data.SqlClient.SqlException sqlExp)
        //    {
        //        if (sqlExp.ErrorCode == 50)
        //        {
        //            LogUtil.Log("发生了 Local Database Runtime 错误。在 LocalDB 实例启动期间出错: 无法启动 SQL Server 进程。", sqlExp);
        //            MessageBox.Show("无法启动数据库实例，详情请看日志文件。", "错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //        }
        //        else if (sqlExp.ToString().Contains("执行超时已过期"))
        //        {
        //            LogUtil.Log("服务器响应超时。完成操作之前已超时或服务器未响应", sqlExp);
        //            MessageBox.Show("服务器响应超时，详情请看日志文件。", "错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        else
        //        {
        //            LogUtil.Log("未知错误发生", sqlExp);
        //            MessageBox.Show("未知错误发生，详情请看日志文件。", "错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //        }
        //    }
        //}
        #endregion
    }
}
