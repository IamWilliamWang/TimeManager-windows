using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace 关机助手.Util
{
    class SqlServerConnection
    {
        private static readonly String connString = Properties.Settings.Default.TimeDatabaseConnectionString;
        private static SqlServerConnection myStatement = new SqlServerConnection();

        //public static string connString
        //{ get { return connString; }
        //}

        public SqlConnection connection{ get; set; }

        private SqlDataAdapter adapter;

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
                SqlServerConnection.GetInstance().connection.Open();
                TimeUtil.Tok();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public static void OpenConnection(String dbFullFilename)
        {
            GetInstance().connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+dbFullFilename+";Integrated Security=True;Connect Timeout=30");
            OpenConnection();
        }

        public static void CloseConnection()
        {
            if (!ConnectionOpenned())
                return;

            try
            {
                SqlServerConnection.GetInstance().connection.Close();
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
            GetInstance().connection.Dispose();
        }

        /// <summary>
        /// 强制释放连接以及所占用的文件
        /// </summary>
        public static void ResetConnection()
        {
            SystemCommandUtil.ExcuteCommand("taskkill /fi \"imagename eq sqlservr.exe\" /f");
        }

        public static Boolean ConnectionOpenned()
        {
            return GetInstance().connection.State == ConnectionState.Open;
        }

        public static ConnectionState GetConnectionState()
        {
            return GetInstance().connection.State;
        }

        public static DataTable ExecuteQuery(string selectCommandText)
        {
            try
            {
                if (GetInstance().connection.State == ConnectionState.Closed)
                    OpenConnection();
                else if (GetInstance().connection.State == ConnectionState.Connecting)
                    return null;

                GetInstance().adapter = new SqlDataAdapter(selectCommandText, GetInstance().connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(GetInstance().adapter);
                DataTable table = new DataTable();
                GetInstance().adapter.Fill(table);
                return table;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public static int ExecuteUpdate(string commandText)
        {
            try {
                if (GetInstance().connection.State == ConnectionState.Closed)
                    OpenConnection();

                SqlCommand cmd = new SqlCommand(commandText, GetInstance().connection);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Boolean UpdateDatabase(DataTable dataTable)
        {
            try
            {
                if (GetInstance().connection.State == ConnectionState.Closed)
                    OpenConnection();

                //创建命令重建对象
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(GetInstance().adapter);
                try
                {
                    if (GetInstance().adapter.Update(dataTable) == 0)
                        return false;
                }
                catch (System.InvalidOperationException)
                {
                    MessageBox.Show("序号不可变！", "更新失败");
                    return false;
                }

                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public static SqlConnection GetConnection()
        {
            return GetInstance().connection;
        }

        private static SqlServerConnection GetInstance()
        {
            return myStatement;
        }


        private static class Error
        {
            public static void SqlExceptionOccur(System.Data.SqlClient.SqlException sqlExp)
            {
                if(sqlExp.ErrorCode == 50)
                {
                    LogUtil.Log("发生了 Local Database Runtime 错误。在 LocalDB 实例启动期间出错: 无法启动 SQL Server 进程。", sqlExp);
                    MessageBox.Show("无法启动数据库实例，详情请看日志文件。", "错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
                else if(sqlExp.ToString().Contains("执行超时已过期"))
                {
                	LogUtil.Log("服务器响应超时。完成操作之前已超时或服务器未响应", sqlExp);
                	MessageBox.Show("服务器响应超时，详情请看日志文件。", "错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    LogUtil.Log("未知错误发生", sqlExp);
                    MessageBox.Show("未知错误发生，详情请看日志文件。", "错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}
