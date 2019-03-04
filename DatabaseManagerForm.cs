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
        private SqlConnectionAgency dbAgency = new SqlConnectionAgency();

        public readonly static String TableName = "[Table]";
        enum QueryOperate { 显示所有数据, 显示后五行数据, 精准查找 };
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

            if (MainForm.databaseOffline)
            {
                this.查询所有记录ToolStripMenuItem.Enabled = false;
                this.插入一条开机记录ToolStripMenuItem.Enabled = false;
                this.一键填补空处ToolStripMenuItem.Enabled = false;
                this.添加数据ToolStripMenuItem.Enabled = false;
                this.删除数据ToolStripMenuItem.Enabled = false;
                this.修改数据ToolStripMenuItem.Enabled = false;
                this.执行SQL语句ToolStripMenuItem.Enabled = false;
                this.储存表格至excelToolStripMenuItem.Enabled = false;
                this.日志管理ToolStripMenuItem.Enabled = false;
                this.全面总结汇报ToolStripMenuItem.Enabled = false;
                this.注释管理ToolStripMenuItem.Enabled = false;
                this.终端功能使用ToolStripMenuItem.Enabled = false;
                this.运行SQL脚本ToolStripMenuItem.Enabled = false;
                return;
            }
            this.progressBar1.Value = 10;
            this.clearCacheBackgroundWorker.RunWorkerAsync();

            //显示后五条ToolStripMenuItem_Click(sender, e);
            this.toolStripStatusLabelTime.Text = "" + DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToShortTimeString();
            if (File.Exists(Properties.Resources.RecorderShellFullFilename) == false)
            {
                if (MessageBox.Show(needInitialized ?? false ? "是否自动完成初始化工作？" : "检测到开机记录已经失效，是否进行修复？", needInitialized ?? false ? "欢迎使用本软件" : "警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    允许开机记录时间ToolStripMenuItem_Click(sender, e);
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionForm.ShowDialog((Exception)e.ExceptionObject);
        }

        #region 全刷新
        private void ShowDatabase()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dbAgency.ExecuteQuery("select * from " + TableName);
            //if (dataGridView1.DataSource == null)
            //    MessageBox.Show("稍安勿躁，请在程序不忙时重试", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.dataGridView1.RowHeadersWidth = 53;
            this.Width += 7;
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        private Boolean CanReadMdfFile()
        {
            return !dbAgency.ConnectionOpenned();
        }

        private void 全刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            if (dbAgency.ConnectionState == ConnectionState.Closed)
            {
                this.progressBar1.Value = 40;
                this.statusLabel.Text = "正在加载数据";
                backgroundQueryOperate = QueryOperate.显示所有数据;
                this.headerWidthSizeNeedChanged = true;
                this.openDBBackgroundWorker.RunWorkerAsync(null);
            }
            else
                ShowDatabase();
        }

        private void 显示后五条ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            if (!dbAgency.ConnectionOpenned())
            {
                this.progressBar1.Value = 40;
                this.statusLabel.Text = "正在加载数据";
                backgroundQueryOperate = QueryOperate.显示后五行数据;
                this.headerWidthSizeNeedChanged = true;
                this.openDBBackgroundWorker.RunWorkerAsync(1);
            }
            else
            {
                dataGridView1.DataSource = null;

                dataGridView1.DataSource = dbAgency.ExecuteQuery(QueryLastFiveSQL());
                //if (dataGridView1.DataSource == null)
                //    MessageBox.Show("稍安勿躁，请在程序不忙时重试", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

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
                File.WriteAllText(Properties.Resources.RecorderShellFullFilename, @"C:\Users\" + ProgramLauncher.SystemUserName + @"\sd.exe" + " -k " + Directory.GetCurrentDirectory() + "\\" + Properties.Resources.MdfFilename, System.Text.Encoding.ASCII);
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
            if (CacheExist()) 
            {
                MessageBox.Show("检测到缓存清理功能故障，无法继续进行添加操作。请暂时不要改变数据库内数据并与程序员联系！", "严重警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SqlExecuter.记录开机事件(TableName))
                MessageBox.Show("插入记录成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.显示后五条ToolStripMenuItem_Click(null, null);
        }

        private bool CacheExist()
        {
            return File.Exists("TimeDatabase.cache");
        }

        private void 插入关机记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SqlExecuter.记录关机事件())
            {
                MessageBox.Show("添加关机记录成功！");
                显示后五条ToolStripMenuItem_Click(null, null);
            }
        }

        private void 删除所有主表记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            TruncateTable(TableName);
            this.显示后五条ToolStripMenuItem_Click(null, null);
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

            if (dbAgency.ExecuteUpdate("delete from " + TableName) != 0)
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

                if (dbAgency.ExecuteUpdate(sql) > 0)
                {
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.显示后五条ToolStripMenuItem_Click(null, null);
                }
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

            if (dbAgency.ExecuteUpdate(DeleteMaxIDSQL()) != 0)
            {
                MessageBox.Show("删除最后一条记录成功!", "删除成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.显示后五条ToolStripMenuItem_Click(null, null);
            }
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

            if (dbAgency.UpdateDatabase((DataTable)dataGridView1.DataSource))
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
                dataGridView1.DataSource = dbAgency.ExecuteQuery(executeSQL);
            else
            {
                int count = dbAgency.ExecuteUpdate(executeSQL);
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
            "SET remark=\'" + remark + "\' " +
            "WHERE 序号 = ( " +
            "SELECT MAX(序号) " +
            "FROM[Table])";

        private void 释放数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dbAgency.ResetConnection();
            MessageBox.Show("释放成功");
        }

        private void 查看已连接数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dbAgency.ConnectionOpenned() == true)
                MessageBox.Show("已连接数据库" + SqliteConnection.DbFullName, "数据库文件名");
            else if (dbAgency.ConnectionState == ConnectionState.Connecting)
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
                MessageBox.Show("输出完成，正在弹出Excel表格。如有部分内容不正确请手动调整对应的单元格格式。", "成功"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool DataSetToExcel(bool isShowExcle)
        {
            DataTable dataTable = (DataTable)dataGridView1.DataSource;
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

        private void 生成备份文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (true/*CanReadMdfFile() == false*/)
            //{
            //MessageBox.Show("功能仅限在该窗口内未点击其他任何非导入导出按钮时使用！", "请重启程序后优先使用此功能", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //return;
            dbAgency.ResetConnection();
            //}

            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "备份文件 (*.backup)|*.backup|所有文件 (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(fileDialog.FileName))
                    File.Delete(fileDialog.FileName);
                Util.GZipUtil.Compress(new FileInfo(Properties.Resources.MdfFilename), new FileInfo(fileDialog.FileName), ".backup", true);
                MessageBox.Show("无损备份数据库成功！", "备份成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rar压缩数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dbAgency.ResetConnection();
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "备份文件 (*.rar)|*.rar|所有文件 (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(fileDialog.FileName))
                    File.Delete(fileDialog.FileName);
                if (WinRARUtil.CompressFile(
                    new String[] { new FileInfo("TimeDatabase.mdf").FullName,
                                   new FileInfo("TimeDatabase_log.ldf").FullName
                                 },
                    fileDialog.FileName))
                {
                    if (EncryptUtil.DecryptFile(fileDialog.FileName))
                        MessageBox.Show("无损备份数据库成功！", "备份成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        MessageBox.Show("备份失败！加密文件时发生未知错误。", "失败提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("备份失败！该操作需要电脑上装有WinRAR软件。", "失败提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 加载无损备份文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanReadMdfFile() == false)
            {
                //MessageBox.Show("功能仅限在该窗口内未点击其他任何非导入导出按钮时使用！", "请重启程序后优先使用此功能", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                dbAgency.ResetConnection();
            }

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "备份文件 (*.backup)|*.backup|所有文件 (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                GZipUtil.Decompress(new FileInfo(fileDialog.FileName), Properties.Resources.MdfFilename);
                MessageBox.Show("无损备份文件加载成功！", "加载成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rar还原数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dbAgency.ResetConnection();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "还原文件 (*.rar)|*.rar|所有文件 (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (WinRARUtil.DecompressFile(fileDialog.FileName, Directory.GetCurrentDirectory()))
                {
                    if (EncryptUtil.DecryptFile(fileDialog.FileName))
                        MessageBox.Show("无损还原数据库成功！", "还原成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        MessageBox.Show("还原失败！解密文件时发生未知错误。", "失败提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("还原失败！该操作需要电脑上装有WinRAR软件。", "失败提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.Restart();
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
                dbAgency.OpenConnection();
                this.openDBBackgroundWorker.ReportProgress(100);
            }
            catch (Exception ex)
            {
                ExceptionForm.ShowDialog(ex);
            }
        }

        private void openDBBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (backgroundQueryOperate == QueryOperate.显示所有数据)
                ShowDatabase();
            else if (backgroundQueryOperate == QueryOperate.显示后五行数据)
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dbAgency.ExecuteQuery(QueryLastFiveSQL());
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dbAgency.ExecuteQuery(QueryCustomSQL);
            }
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            this.progressBar1.Value = 100;
            this.statusLabel.Text = "完成";
        }


        #endregion

        private bool AlertBusy()
        {
            if (dbAgency.ConnectionState == ConnectionState.Connecting)
            {
                MessageBox.Show("稍安勿躁，请在程序不忙时重试", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }

        private void 终端功能使用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            string command = Microsoft.VisualBasic.Interaction.InputBox("输入终端指令：", "终端操作");
            if (command == "")
                return;
            Util.FastModeUtil.RunConsoleApplication(command.Split(' '));
        }

        private void 激活禁止系统休眠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new HibernateForm().ShowDialog();
            //if (index1 < index2)
            //{
            //    if(DialogResult.OK == MessageBox.Show("系统休眠功能已经开启。是否需要关闭？", "停用休眠功能", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            //    {
            //        //if (SystemCommandUtil.ExcuteCommand("powercfg /HIBERNATE off").IndexOf("无法执行操作") == -1)
            //        //    MessageBox.Show("已尝试完成。", "尝试停用休眠功能");
            //        //else 
            //        if (DialogResult.OK == MessageBox.Show("尝试失败，需要获得管理员权限，是否允许并重启本程序？", "失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
            //        {
            //            MainForm.restartWithAdminRight(true);
            //        }
            //        else
            //            MessageBox.Show("关闭休眠失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //else
            //{
            //    if (DialogResult.OK == MessageBox.Show("系统休眠功能已经关闭。是否需要开启？", "激活休眠功能", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            //    {
            //        //if (SystemCommandUtil.ExcuteCommand("powercfg /HIBERNATE on").IndexOf("无法执行操作") != -1)
            //        //    MessageBox.Show("已尝试完成。", "尝试激活休眠功能");
            //        //else 
            //        if (DialogResult.OK == MessageBox.Show("尝试失败，需要获得管理员权限，是否允许并重启本程序？", "失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
            //        {
            //            MainForm.restartWithAdminRight(true);
            //        }
            //        else
            //            MessageBox.Show("激活休眠失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void 运行SQL脚本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            if (MessageBox.Show("确定要使用该操作吗？使用该操作可能会导致不可逆的错误发生，您必须自己确信SQL语句完全正确，程序不会提供错误提醒。如果您是管理人员，且知道此举的后果，请点击确认。如果是正常用户请点击取消！", "严重使用警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Sql文件|*.sql";
            fileDialog.Title = "请输入脚本带完整路径文件名";
            fileDialog.ShowDialog();
            if (fileDialog.FileName == "")
                return;
            string filename = fileDialog.FileName.Trim().Replace("\"", "");

            int count = dbAgency.ExecuteUpdate(File.ReadAllText(filename).Replace("go", " "));
            if (count == 0)
                MessageBox.Show("执行失败，没有条目受到影响");
            else
                MessageBox.Show("执行成功，" + count + "条条目受到影响");
        }

        bool headerWidthSizeNeedChanged = false;
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //显示在HeaderCell上
            if (e.Row.Cells[0].Value != null)
            {
                e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();

                //if (headerWidthSizeNeedChanged)
                //{
                //    this.dataGridView1.RowHeadersWidth = (int)Math.Log10(this.dataGridView1.Rows.Count) * 6 + 40;
                //    this.headerWidthSizeNeedChanged = false;
                //}
            }
        }

        private void clearCacheBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.statusLabel.Text = "正在清除缓存并完成同步操作";
            SqlServerConnection.ClearCache();
        }

        private void clearCacheBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.statusLabel.Text = "完成";
            this.progressBar1.Value = 100;
            显示后五条ToolStripMenuItem_Click(sender, e);
        }

        //private void 浏览缓存文件ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (File.Exists(SqlServerConnection.CacheFilename) == false)
        //    {
        //        MessageBox.Show("当前没有缓存，无需查看。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }
        //    MessageBox.Show(File.ReadAllText(SqlServerConnection.CacheFilename).Replace(SqlServerConnection.CacheSpliter, '\n'), "查看缓存文件");
        //}

        //private void 编辑缓存文件ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (File.Exists(SqlServerConnection.CacheFilename) == false)
        //    {
        //        MessageBox.Show("当前没有缓存，无法编辑。", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //    Util.SystemCommandUtil.ExcuteCommand("notepad \"" + new FileInfo("TimeDatabase.cache").FullName + "\"");
        //}

        private void 缓存管理器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CacheManagerForm().ShowDialog();
        }

        private string QueryCustomSQL { get; set; }

        private bool GetCustomSQLString()
        {
            String input = Microsoft.VisualBasic.Interaction.InputBox("请输入要查询的日期，如“2018年1月1日”。支持年、年月、年月日","精准查找");
            if (input == "")
                return false;
            String[] condition = input.Split(new char[] { '年', '月', '日' }, StringSplitOptions.RemoveEmptyEntries);
            if (condition.Length == 1)
            {
                QueryCustomSQL = SqlUtil.Select_Sql("[Table]", "*", "YEAR(开机时间)=" + condition[0]);
                return true;
            }
            else if (condition.Length == 2)
            {
                QueryCustomSQL = SqlUtil.Select_Sql("[Table]", "*"
                    , "YEAR(开机时间)=" + condition[0] + " and " +
                    "MONTH(开机时间)=" + condition[1]);
                return true;
            }
            else if (condition.Length == 3)
            {
                QueryCustomSQL = SqlUtil.Select_Sql("[Table]", "*"
                    , "YEAR(开机时间)=" + condition[0] + " and " +
                    "MONTH(开机时间)=" + condition[1] + " and " +
                    "DAY(开机时间)=" + condition[2]);
                return true;
            }
            return false;
        }

        private void 精准查找显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;
            if (GetCustomSQLString() == false)
            {
                this.全刷新ToolStripMenuItem_Click(sender, e);
                return;
            }
            if (!dbAgency.ConnectionOpenned())
            {
                this.progressBar1.Value = 40;
                this.statusLabel.Text = "正在加载数据";
                backgroundQueryOperate = QueryOperate.精准查找;
                this.headerWidthSizeNeedChanged = true;
                this.openDBBackgroundWorker.RunWorkerAsync(null);
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dbAgency.ExecuteQuery(QueryCustomSQL);
                this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
        }
    }
}