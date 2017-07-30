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
    public partial class DatabaseManagerForm : Form
    {
        private SqlDataAdapter adapter;
        private DataTable table;
        private readonly static String TableName = "[Table]";

        public DatabaseManagerForm()
        {
            InitializeComponent();
        }

        private void SqlServerResult_Load(object sender, EventArgs e)
        {
            showDatabase();
        }

        #region 全刷新
        private void showDatabase()
        {
            table = SqlServerStatement.getStatement().executeQuery("select * from " + TableName);
            dataGridView1.DataSource = table;
        }

        private void 全刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDatabase();
        }
        #endregion

        #region 开机记录状态
        private void 允许开机记录时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\TimeSaver.exe", FileMode.Create);
                fs.Write(Properties.Resources.开机小程序, 0, Properties.Resources.开机小程序.Length);
                fs.Close();
                MessageBox.Show("已经允许开机记录时间！", "成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                MessageBox.Show("需要管理员权限！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 禁止开机记录时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemCommandUtil.ExcuteCommand("del \"C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp\\TimeSaver.exe\"");
            MessageBox.Show("已经禁止开机记录时间", "成功！");
        }
        #endregion

        #region 编辑数据库
        private void 插入一条开机记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (SqlServerStatement.getStatement().executeUpdate(insertPoweronTimeSQL()) != 0)
                MessageBox.Show("插入记录成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string insertPoweronTimeSQL()
        {
            return "INSERT "
                    + "INTO " + TableName + "(开机时间) "
                    + "VALUES (" + "\'" + DateTime.Now + "\')";
        }

        private void 删除所有记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("此操作不可恢复！是否继续？","警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (SqlServerStatement.getStatement().executeUpdate("delete from " + TableName) != 0) 
                MessageBox.Show("清空数据库成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void 删除最后一条记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connStr = Properties.Settings.Default.TimeDatabaseConnectionString;//连接字符串

            if (SqlServerStatement.getStatement().executeUpdate(deleteMaxIDSQL()) != 0)
                MessageBox.Show("删除最后一条记录成功!", "删除成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string deleteMaxIDSQL()
        {
            return "DELETE " +
                    "FROM[Table] " +
                    "WHERE 序号 = ( " +
                    "SELECT MAX(序号) " +
                    "FROM[Table])";
        }

        private void 提交手动修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlServerStatement.getStatement().updateDatabase(table);

        }
        #endregion

        private void SqlServerResult_DoubleClick(object sender, EventArgs e)
        {
            this.编辑数据库ToolStripMenuItem.Enabled = true;
        }

        #region 导入与导出
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
        #endregion

        #region 一键填补
        private void 开始统计结算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlExecuter.记录结算();
            showDatabase();
        }
        #endregion

        #region 总结汇报
        private void 统计每月上机时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ShowUsingTimeOfEachMonthForm().Show();
        }
        #endregion

        #region 关闭本窗口
        private void 关闭此窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}