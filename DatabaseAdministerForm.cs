using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 关机小程序.Util;

namespace 关机小程序
{
    public partial class DatabaseAdministerForm : Form
    {
        private SqlDataAdapter adapter;
        private DataTable table;
        private readonly static String TableName = "[Table]";

        public DatabaseAdministerForm()
        {
            InitializeComponent();
        }

        private void SqlServerResult_Load(object sender, EventArgs e)
        {
            showDatabase();
        }

        private void showDatabase()
        {
            table = SqlExecuter.getInstance().executeQuery("select * from " + TableName);
            dataGridView1.DataSource = table;
        }

        private void 查询所有ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDatabase();
        }

        private void 删除所有记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("此操作不可恢复！是否继续？","警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (SqlExecuter.getInstance().executeUpdate("delete from " + TableName) == true) 
                MessageBox.Show("删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void SqlServerResult_DoubleClick(object sender, EventArgs e)
        {
            this.管理员选项ToolStripMenuItem.Enabled = true;
        }
   
        private void 插入一条开机记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            String sql = "INSERT "
                        + "INTO " + TableName + "(开机时间) "
                        + "VALUES (" + "\'" + DateTime.Now + "\')";

            if(SqlExecuter.getInstance().executeUpdate(sql))
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
            SystemCommandUtil.ExcuteCommand("del \"C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp\\TimeSaver.exe\"");
            MessageBox.Show("成功！！");
        }

        private void 关闭此窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 删除最后一条记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connStr = Properties.Settings.Default.TimeDatabaseConnectionString;//连接字符串
            String sql= "DELETE "+
                        "FROM[Table] "+
                        "WHERE 序号 = ( "+
                        "SELECT MAX(序号) "+
                        "FROM[Table])";
            
            if(SqlExecuter.getInstance().executeUpdate(sql))
                MessageBox.Show("Success!");
        }

        private void 储存表格至excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("即将输出所有内容到EXCEL表格中，期间可能要等待数十秒。是否继续？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            if (!this.DataSetToExcel(true))
                MessageBox.Show("输出失败！");
        }

        public bool DataSetToExcel(bool isShowExcle)
        {
            DataTable dataTable = table;
            int rowNumber = dataTable.Rows.Count;
            int columnNumber = dataTable.Columns.Count;
            String stringBuffer = "";

            if (rowNumber == 0)
            {
                MessageBox.Show("没有任何数据可以导入到Excel文件！");
                return false;
            }

            //建立Excel对象 
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Visible = isShowExcle;//是否打开该Excel文件 

            //填充数据 
            for (int i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < columnNumber; j++)
                {
                    stringBuffer += dataTable.Rows[i].ItemArray[j].ToString();
                    if (j < columnNumber - 1)
                    {
                        stringBuffer += "\t";
                    }
                }
                stringBuffer += "\n";
            }
            Clipboard.Clear();
            Clipboard.SetDataObject(stringBuffer);
            ((Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 1]).Select();
            ((Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveWorkbook.ActiveSheet).Paste(Missing.Value, Missing.Value);
            Clipboard.Clear();

            return true;
        }

        private void 开始统计结算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlExecuter.getInstance().记录结算();
        }
    }
}