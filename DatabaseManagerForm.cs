using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using 关机助手.Util;

namespace 关机助手
{
    public partial class DatabaseManagerForm : Form
    {
        //private SqlDataAdapter adapter;
        //private DataTable table;
        public readonly static String TableName = "[Table]";
        enum QueryOperate { 显示所有数据, 显示后五行数据 };
        private QueryOperate backgroundQueryOperate = new QueryOperate();
        public static bool? needInitialized { get; set; }

        public DatabaseManagerForm()
        {
            InitializeComponent();
        }

        private void SqlServerResult_Load(object sender, EventArgs e)
        {
            //处理非UI线程异常，激活全局错误弹窗
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            显示后五条ToolStripMenuItem_Click(sender, e);
            
            if (File.Exists(Properties.Resources.RecorderShellFullFilename) == false)
            {
                if (MessageBox.Show(needInitialized ?? false ? "是否自动完成初始化工作？" : "检测到开机记录已经失效，是否进行修复？", needInitialized ?? false ? "欢迎使用本软件" : "警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    允许开机记录时间ToolStripMenuItem_Click(sender, e);
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionForm.ShowDialog((Exception) e.ExceptionObject);
        }

        #region 全刷新
        private void ShowDatabase()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = SqlServerConnection.ExecuteQuery("select * from " + TableName);
            if (dataGridView1.DataSource == null)
                MessageBox.Show("稍安勿躁，请在程序不忙时重试", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private Boolean CanReadMdfFile()
        {
            return !SqlServerConnection.ConnectionOpenned();
        }

        private void 全刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            if (SqlServerConnection.GetConnectionState() == ConnectionState.Closed)
            {
                this.progressBar1.Value = 40;
                backgroundQueryOperate = QueryOperate.显示所有数据;
                this.openDBBackgroundWorker.RunWorkerAsync(null);
            }
            else
                ShowDatabase();
        }

        private void 显示后五条ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            if (!SqlServerConnection.ConnectionOpenned())
            {
                this.progressBar1.Value = 20;
                backgroundQueryOperate = QueryOperate.显示后五行数据;
                this.openDBBackgroundWorker.RunWorkerAsync(1);
            }
            else
            {
                dataGridView1.DataSource = null;

                dataGridView1.DataSource = SqlServerConnection.ExecuteQuery(QueryLastFiveSQL());
                //if (dataGridView1.DataSource == null)
                //    MessageBox.Show("稍安勿躁，请在程序不忙时重试", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }

        }

        private String QueryLastFiveSQL() =>
            "select * from " + TableName + " where 序号>((select max(序号) from " + TableName + ")-14)";
        #endregion

        #region 插件安装
        private void 允许开机记录时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if(ResourceFileUtil.WriteFileToDisk(Properties.Resources.开机小程序, Properties.Resources.RecorderFullFilename))
            try
            {
                File.WriteAllText(Properties.Resources.RecorderShellFullFilename, @"C:\Users\william\sd.exe" + " -k " + Directory.GetCurrentDirectory() + "\\" + Properties.Resources.MdfFilename, System.Text.Encoding.ASCII);
                MessageBox.Show("已经安装！", "成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (UnauthorizedAccessException)
            {
                //MessageBox.Show("需要管理员权限！请以管理员身份启动本程序", "失败警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainForm.restartWithAdminRight();
            }
        }

        private void 禁止开机记录时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SystemCommandUtil.ExcuteCommand("del \"C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp\\TimeSaver.exe\"");
            try
            {
                File.Delete(Properties.Resources.RecorderShellFullFilename);
                MessageBox.Show("已经卸载", "成功！");
            }
            catch (Exception e1)
            {
                ExceptionForm.ShowDialog(e1);
            }
        }
        #endregion

        #region 查看日志
        private void 查看日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            new LogManagerForm().Show();
        }
        #endregion

        #region 编辑数据库
        private void 插入一条开机记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            if (SqlExecuter.记录开机事件(TableName))
                MessageBox.Show("插入记录成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 插入关机记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(SqlExecuter.记录关机事件())
                MessageBox.Show("添加关机记录成功！");
        }

        private void 删除所有主表记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            TruncateTable(TableName);
        }

        private void 清除日志数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            TruncateTable("[DBLog]");
        }

        private void 清除注释数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            TruncateTable("[Remark]");
        }

        private bool TruncateTable(string TableName)
        {
            if (MessageBox.Show("此操作不可恢复！是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return false;

            if (SqlServerConnection.ExecuteUpdate("delete from " + TableName) != 0)
            {
                MessageBox.Show("清空数据库" + TableName + "成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }
            else
            {
            	MessageBox.Show("数据库" + TableName + "是空的！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            	return false;
            }
            
        }

        private void 删除指定一条记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;
            string input = Microsoft.VisualBasic.Interaction.InputBox("请输入要删除条目前的序号", "删除任意条");
            if (input == "")
                return;
            string sql = "DELETE " +
                    "FROM[Table] " +
                    "WHERE 序号 = ";
            try
            {
                sql += int.Parse(input);
            }
            catch (Exception)
            {
                MessageBox.Show("输入的不是正整数，删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                if (MessageBox.Show("此操作不可恢复！是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;

                if (SqlServerConnection.ExecuteUpdate(sql) > 0)
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("发生未知错误，删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("发生未知错误，删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 删除最后一条记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            string connStr = Properties.Settings.Default.TimeDatabaseConnectionString;//连接字符串

            if (SqlServerConnection.ExecuteUpdate(DeleteMaxIDSQL()) != 0)
                MessageBox.Show("删除最后一条记录成功!", "删除成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string DeleteMaxIDSQL() =>
            "DELETE " +
            "FROM[Table] " +
            "WHERE 序号 = ( " +
            "SELECT MAX(序号) " +
            "FROM[Table])";

        private void 提交手动修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            if (SqlServerConnection.UpdateDatabase((DataTable) dataGridView1.DataSource))
                System.Windows.MessageBox.Show("手动修改已提交到数据库。", "修改成功！", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            this.显示后五条ToolStripMenuItem_Click(sender, e);
        }

        private void 执行SQL语句ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            if (MessageBox.Show("确定要使用该操作吗？使用该操作可能会导致不可逆的错误发生，您必须自己确信SQL语句完全正确，程序不会提供错误提醒。如果您是管理人员，且知道此举的后果，请点击确认。如果是正常用户请点击取消！", "严重使用警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;

            String executeSQL = Microsoft.VisualBasic.Interaction.InputBox("请输入要执行的SQL语句(表名[Table]):", "SQL输入");
            if (executeSQL.Equals(""))
                return;
            executeSQL = executeSQL.ToUpper();
            dataGridView1.DataSource = null;

            if (executeSQL.IndexOf("SELECT") == 0)
                dataGridView1.DataSource = SqlServerConnection.ExecuteQuery(executeSQL);
            else
            {
                int count = SqlServerConnection.ExecuteUpdate(executeSQL);
                if (count == 0)
                    MessageBox.Show("执行失败，没有条目受到影响");
                else
                    MessageBox.Show("执行成功，" + count + "条条目受到影响");
            }
        }

        private void 添加编辑注释ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            new RemarkManagerForm().Show();
        }

        private string updateLastRemarkSql(string remark) =>
            "UPDATE [Table] " + 
            "SET remark=\'"+remark+"\' "+
            "WHERE 序号 = ( " +
            "SELECT MAX(序号) " +
            "FROM[Table])";

        private void 释放数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlServerConnection.ResetConnection();
            MessageBox.Show("释放成功");
        }

        private void 查看已连接数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SqlServerConnection.ConnectionOpenned() == true)
                MessageBox.Show("已连接数据库"+SqlServerConnection.GetConnection().Database, "数据库文件名");
            else if (SqlServerConnection.GetConnectionState() == ConnectionState.Connecting)
                MessageBox.Show("正在连接数据库，请稍后重试. . .");
            else
                MessageBox.Show("未连接数据库");
        }

        private void 强制性报错ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new Exception("我是报错框。");
        }
        #endregion

        //private void SqlServerResult_DoubleClick(object sender, EventArgs e)
        //{
        //    this.编辑数据库ToolStripMenuItem.Enabled = true;
        //}

        #region 导入与导出
        private void 储存表格至excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("即将输出所有内容到EXCEL表格中，期间可能要等待数十秒。是否继续？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("未找到需要保存的数据");
                return;
            }
            if (!this.DataSetToExcel(true))
                MessageBox.Show("输出失败！");
            else
            	MessageBox.Show("输出完成，正在弹出Excel表格。如有部分内容不正确请手动调整对应的单元格格式。","成功"
            		, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool DataSetToExcel(bool isShowExcle)
        {
            DataTable dataTable = (DataTable) dataGridView1.DataSource;
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
            ( (Microsoft.Office.Interop.Excel.Range) excel.Cells[1, 1] ).Select();
            ( (Microsoft.Office.Interop.Excel.Worksheet) excel.ActiveWorkbook.ActiveSheet ).Paste(Missing.Value, Missing.Value);
            Clipboard.Clear();

            return true;
        }

        private void 生成备份文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (true/*CanReadMdfFile() == false*/)
            //{
            //MessageBox.Show("功能仅限在该窗口内未点击其他任何非导入导出按钮时使用！", "请重启程序后优先使用此功能", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //return;
            SqlServerConnection.ResetConnection();
            //}

            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "备份文件 (*.backup)|*.backup|所有文件 (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Util.GZipUtil.Compress(new FileInfo(Properties.Resources.MdfFilename), new FileInfo(fileDialog.FileName), ".backup", true);
                MessageBox.Show("无损备份数据库成功！", "备份成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rar压缩数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlServerConnection.ResetConnection();
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "备份文件 (*.rar)|*.rar|所有文件 (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (WinRARUtil.CompressFile(
                    new String[] { new FileInfo("TimeDatabase.mdf").FullName,
                                   new FileInfo("TimeDatabase_log.ldf").FullName
                                 },
                    fileDialog.FileName))
                    MessageBox.Show("无损备份数据库成功！", "备份成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("备份失败！该操作需要电脑上装有WinRAR软件。", "失败提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void 加载无损备份文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanReadMdfFile() == false)
            {
                //MessageBox.Show("功能仅限在该窗口内未点击其他任何非导入导出按钮时使用！", "请重启程序后优先使用此功能", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                SqlServerConnection.ResetConnection();
            }

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "备份文件 (*.backup)|*.backup|所有文件 (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                GZipUtil.Decompress(new FileInfo(fileDialog.FileName), Properties.Resources.MdfFilename);
                MessageBox.Show("无损备份文件加载成功！", "加载成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

        #region 一键填补
        private void 开始统计结算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            SqlExecuter.记录结算();
            this.显示后五条ToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region 总结汇报
        private void 统计每月上机时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            new AnalysingForm().Show();
        }
        #endregion

        #region 关闭本窗口
        private void 关闭此窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        #endregion

        #region 打开数据库连接BackgroundWorker
        private void openDBBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                SqlServerConnection.OpenConnection();
                this.openDBBackgroundWorker.ReportProgress(100);
            }
            catch (Exception ex)
            {
                ExceptionForm.ShowDialog(ex);
            }
        }

        private void openDBBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.progressBar1.Value = 100;
            if (backgroundQueryOperate == QueryOperate.显示所有数据)
                ShowDatabase();
            else if (backgroundQueryOperate == QueryOperate.显示后五行数据)
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = SqlServerConnection.ExecuteQuery(QueryLastFiveSQL());
            }

        }


        #endregion

        private bool AlertBusy()
        {
            if (SqlServerConnection.GetConnectionState() == ConnectionState.Connecting)
            {
                MessageBox.Show("稍安勿躁，请在程序不忙时重试", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }
        
    }
}