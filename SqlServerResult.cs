using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 关机小程序
{
    public partial class SqlServerResult : Form
    {
        private SqlDataAdapter adapter;
        private DataTable table;
        private readonly static String TableName = "[Table]";

        public SqlServerResult()
        {
            InitializeComponent();
        }

        private void SqlServerResult_Load(object sender, EventArgs e)
        {
            showDatabase();
        }

        private void showDatabase()
        {
            string connStr = Properties.Settings.Default.TimeDatabaseConnectionString;//连接字符串
            SqlConnection conn = new SqlConnection(connStr);//建立到数据库的连接
            adapter = new SqlDataAdapter("select * from " + TableName, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.DeleteCommand = builder.GetDeleteCommand();
            adapter.UpdateCommand = builder.GetUpdateCommand();
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void 查询所有ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDatabase();
        }

        public static void 记录关机事件()
        {
            //DateTime now = DateTime.Now;
            //String sql = "UPDATE " + SqlServerResult.TableName + " "
            //            + "SET 关机时间=\'" + DateTime.Now + "\' "
            //            + "WHERE 序号=" + get最大序号() + " ";
            String sql = 
                "UPDATE [Table] "+
                "SET 关机时间 = GETDATE(), 时长 = GETDATE() - 开机时间 "+
                "WHERE 序号 in "+
                "(SELECT MAX(序号) "+
                "FROM[Table]) "+
                " "+
                "UPDATE[Table] " +
                "SET 当天使用次数 = ("+
                "SELECT COUNT(*) "+
                "FROM[Table] AS FatherTable "+
                "WHERE EXISTS("+
                "SELECT * "+
                "FROM[Table] "+
                "WHERE 序号 = (SELECT MAX(序号) FROM[Table]) "+
                "AND YEAR([Table].开机时间) = YEAR([FatherTable].开机时间) "+
                "AND MONTH([Table].开机时间) = MONTH([FatherTable].开机时间) "+
                "AND DAY([Table].开机时间) = DAY([FatherTable].开机时间))) "+
                "WHERE 序号 = ("+
                "SELECT MAX(序号) "+
                "FROM[Table])";

            String connString = Properties.Settings.Default.TimeDatabaseConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void 删除所有记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("此操作不可恢复！是否继续？","警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            string connStr = Properties.Settings.Default.TimeDatabaseConnectionString;//连接字符串
            SqlConnection conn = new SqlConnection(connStr);//建立到数据库的连接
            adapter = new SqlDataAdapter("delete from " + TableName, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            MessageBox.Show("删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void SqlServerResult_DoubleClick(object sender, EventArgs e)
        {
            this.管理员选项ToolStripMenuItem.Enabled = true;
        }

        /*为了避免在SQL查询中查找空数据库而产生错误，但会一定程度降低程序效率*/
        static int get最大序号()
        {
            int 序号 = 0;
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.TimeDatabaseConnectionString))
            {
                conn.Open();//打开数据库  
                            //Console.WriteLine("数据库打开成功!");  
                            //创建数据库命令  
                SqlCommand cmd = conn.CreateCommand();
                //创建查询语句  
                cmd.CommandText = "SELECT MAX(序号) FROM " + TableName;
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

        private void 插入一条开机记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            String sql = "INSERT "
                        + "INTO " + TableName + "(序号,开机时间) "
                        + "VALUES (" + (get最大序号() + 1) + ",\'" + DateTime.Now + "\')";

            String connString = Properties.Settings.Default.TimeDatabaseConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Success!");
        }

        private void 允许开机记录时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\TimeSaver.exe", FileMode.Create);
                fs.Write(Resource.开机小程序, 0, Resource.开机小程序.Length);
                fs.Close();
                MessageBox.Show("成功！！");
            }
            catch(System.UnauthorizedAccessException ex)
            {
                MessageBox.Show("需要管理员权限！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 禁止开机记录时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemCommand.system("del \"C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp\\TimeSaver.exe\"");
            MessageBox.Show("成功！！");
        }

        private void 关闭此窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }   
}