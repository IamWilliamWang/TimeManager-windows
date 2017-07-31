using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关机小程序.Util
{
    class SqlServerStatement
    {
        private static SqlServerStatement myStatement = new SqlServerStatement();
        private readonly string connString = Properties.Settings.Default.TimeDatabaseConnectionString;
        public SqlConnection connection;

        SqlDataAdapter adapter;

        private SqlServerStatement()
        {
            connection = new SqlConnection(connString);//建立到数据库的连接
        }

        public int executeUpdate(string sql)
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //throw e;
                return 0;
            }
        }

        public DataTable executeQuery(string sql)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            adapter = new SqlDataAdapter(sql, connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public Boolean updateDatabase(DataTable dataTable)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            //创建命令重建对象
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

            try
            {
                adapter.Update(dataTable);
            }
            catch (SqlException)
            {
                System.Windows.MessageBox.Show("可能原因：连接不到数据库或者第一列置为了空。", "修改失败！", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
            System.Windows.MessageBox.Show("手动修改已提交到数据库。", "修改成功！", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            return true;
        }

        public void openConnection()
        {
            if (connection.State == ConnectionState.Open)
                return;

            this.connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == ConnectionState.Closed)
                return;

            this.connection.Close();
        }

        public static SqlServerStatement getStatement()
        {
            return myStatement;
        }
    }
}
