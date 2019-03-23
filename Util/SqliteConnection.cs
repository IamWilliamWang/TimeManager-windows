using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关机助手.Util
{
    class SqliteConnection 
    {
        private static string connStr = "Data Source=TimeDatabase.db;";

        public static string DbFullName { get { return Instance.connection.Database; } }
        public static SqliteConnection Instance { get; } = new SqliteConnection();

        public ConnectionState ConnectionState { get { return Instance.connection.State; } }

        private SQLiteConnection connection { get; set; } = new SQLiteConnection(connStr);

        public void OpenConnection()
        {
            if (!ConnectionOpenned())
                connection.Open();
        }

        public void OpenConnection(String dbFullFilename)
        {
            if (!ConnectionOpenned())
            {
                connection = new SQLiteConnection("Data Source=" + dbFullFilename);
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (ConnectionState != ConnectionState.Closed)
                connection.Close();
        }

        public void DisposeConnection()
        {
            connection.Dispose();
        }

        public void ResetConnection()
        {
            throw new NotImplementedException();
        }

        public Boolean ConnectionOpenned()
        {
            return ConnectionState == ConnectionState.Open;
        }

        public DataTable ExecuteQuery(string selectCommandText)
        {
            //SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectCommandText, SqliteConnection.connStr);
            //DataTable dataTable = new DataTable();
            //SQLiteCommand command = new SQLiteCommand(connection);
            //adapter.Fill(dataTable);
            return ExecuteDataTable(selectCommandText);
        }

        public int ExecuteUpdate(string commandText)
        {
            return ExecuteNonQuery(commandText);
        }

        public Boolean UpdateDatabase(DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 执行增删改的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        private int ExecuteNonQuery(string sql, params SQLiteParameter[] pms)
        {
            using (SQLiteConnection con = new SQLiteConnection(connStr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                {
                    if (pms.Length != 0)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// 执行返回单个值的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        private object ExecuteScalar(string sql, params SQLiteParameter[] pms)
        {
            using (SQLiteConnection con = new SQLiteConnection(connStr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                {
                    if (pms.Length != 0)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 执行返回reader的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        private SQLiteDataReader ExecuteReader(string sql, params SQLiteParameter[] pms)
        {
            SQLiteConnection con = new SQLiteConnection(connStr);
            using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
            {
                if (pms.Length != 0)
                {
                    cmd.Parameters.AddRange(pms);
                }
                try
                {
                    con.Open();
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    //System.Data.CommandBehavior.CloseConnection,表示DataReader对象调用结束后，释放conn
                }
                catch
                {
                    con.Close();
                    con.Dispose();
                    throw;
                }
            }
        }

        /// <summary>
        /// 执行返回一个DataTable的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        private DataTable ExecuteDataTable(string sql, params SQLiteParameter[] pms)
        {
            DataTable dt = new DataTable();
            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connStr))
            {
                if (pms.Length != 0)
                {
                    adapter.SelectCommand.Parameters.AddRange(pms);
                }
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}
