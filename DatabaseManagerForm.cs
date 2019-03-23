using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using 关机助手.Util;

namespace 关机助手
{
    public partial class DatabaseManagerForm : Form
    {
        #region 常量定义
        public readonly static String TableName = "[Table]";
        enum QueryMode { 显示所有数据, 显示后五行数据, 精准查找 };
        #endregion

        #region 变量定义
        private DatabaseAgency db = new DatabaseAgency();
        private QueryMode backgroundQueryMode = new QueryMode();
        /// <summary>
        /// 是否需要初始化操作（只有第一次点开时才为True）
        /// </summary>
        public static bool needInitialized { get; set; } = false;
        #endregion

        #region DatabaseManager帮助函数
        /// <summary>
        /// 检查数据库是否处于正在连接状态，如果是则提示并返回True，否则直接返回False
        /// </summary>
        /// <returns></returns>
        private bool AlertBusy()
        {
            if (db.ConnectionState == ConnectionState.Connecting)
            {
                MessageBox.Show("稍安勿躁，请在程序不忙时重试", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }
        #endregion

        #region 窗体加载事件
        public DatabaseManagerForm()
        {
            InitializeComponent();
        }
        
        private void SqlServerResult_Load(object sender, EventArgs e)
        {
            //处理非UI线程异常，激活全局错误弹窗
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            if (MainForm.DatabaseOffline == true)
            {
                this.DatabaseOffline = true;
                return;
            }
            this.progressBar1.Value = 10;
            this.clearCacheBackgroundWorker.RunWorkerAsync();

            //显示后五条ToolStripMenuItem_Click(sender, e);
            this.toolStripStatusLabelTime.Text = "" + DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToShortTimeString();
            if (File.Exists(Properties.Resources.RecorderShellFullFilename) == false)
            {
                if (MessageBox.Show(needInitialized ? "是否自动完成初始化工作？" : "检测到开机记录已经失效，是否进行修复？", needInitialized ? "欢迎使用本软件" : "警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    安装写入开机记录时间插件ToolStripMenuItem_Click(sender, e);
            }
        }

        private void DatabaseManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.CheckSaftyModeSanity();
        }
        #endregion

        #region 异常处理函数
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionForm.ShowDialog((Exception)e.ExceptionObject);
        }
        #endregion

        #region HeaderCell修改事件
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //显示在HeaderCell上
            if (e.Row.Cells[0].Value != null)
            {
                e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
            }
        }
        #endregion

        #region 数据显示模块
        /// <summary>
        /// 输出[Table]所有内容
        /// </summary>
        private void ShowTotalTable()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = db.ExecuteQuery("select * from " + TableName);
            this.dataGridView1.RowHeadersWidth = 53;
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }
        
        private void 展示所有数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;
            if(this.Width==702)
                this.Width = 710;
            // 数据库已连接与未连接都可以处理
            if (db.ConnectionState == ConnectionState.Closed)
            {
                this.progressBar1.Value = 40;
                this.statusLabel.Text = "正在加载数据";
                backgroundQueryMode = QueryMode.显示所有数据;
                this.dbBackgroundWorker.RunWorkerAsync(null);
            }
            else
                ShowTotalTable();
        }

        private void 显示后15条ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;
            if(this.Width==710)
                this.Width = 702;
            // 数据库已连接与未连接都可以处理
            if (!db.ConnectionOpenned())
            {
                this.progressBar1.Value = 40;
                this.statusLabel.Text = "正在加载数据";
                backgroundQueryMode = QueryMode.显示后五行数据;
                this.dbBackgroundWorker.RunWorkerAsync(1);
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = db.ExecuteQuery(
                    "select * from " + 
                    TableName + 
                    " where 序号>((select max(序号) from " + TableName + ")-14)");
                this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
        }

        private string QueryCustomSQL { get; set; }

        private bool? InputCustomSQLString()
        {
            String input = Interaction.InputBox("请输入要查询的日期。支持年、年月、年月日", "精准查找", hint: "如“2018年1月1日”");
            if (input == "")
                return null;
            String[] conditions = input.Split(new char[] { '年', '月', '日' }, StringSplitOptions.RemoveEmptyEntries);
            // Check sanity
            foreach (String condition in conditions)
                foreach (Char ch in condition.ToCharArray())
                    if (char.IsDigit(ch) == false)
                        return false;

            if (conditions.Length == 1)
            {
                QueryCustomSQL = SqlUtil.Select_Sql("[Table]", "*", "YEAR(开机时间)=" + conditions[0]);
                return true;
            }
            else if (conditions.Length == 2)
            {
                QueryCustomSQL = SqlUtil.Select_Sql("[Table]", "*"
                    , "YEAR(开机时间)=" + conditions[0] + " and " +
                    "MONTH(开机时间)=" + conditions[1]);
                return true;
            }
            else if (conditions.Length == 3)
            {
                QueryCustomSQL = SqlUtil.Select_Sql("[Table]", "*"
                    , "YEAR(开机时间)=" + conditions[0] + " and " +
                    "MONTH(开机时间)=" + conditions[1] + " and " +
                    "DAY(开机时间)=" + conditions[2]);
                return true;
            }
            return false;
        }

        private void 精准查找显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;
            var success = InputCustomSQLString();
            if (success == null)
                return;
            if (success == false)
            {
                this.展示所有数据ToolStripMenuItem_Click(sender, e);
                MessageBox.Show("输入格式错误！已为您显示所有数据","错误提示");
                return;
            }
            if (!db.ConnectionOpenned())
            {
                this.progressBar1.Value = 40;
                this.statusLabel.Text = "正在加载数据";
                backgroundQueryMode = QueryMode.精准查找;
                this.dbBackgroundWorker.RunWorkerAsync(null);
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = db.ExecuteQuery(QueryCustomSQL);
                this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
        }
        #endregion

        #region 数据库管理
        private void 填补空处ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            SqlExecuter.记录结算();
            this.显示后15条ToolStripMenuItem_Click(sender, e);
        }
            #region 添加数据
        private void 插入开机记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;
            if (File.Exists("TimeDatabase.cache")) 
            {
                MessageBox.Show("检测到缓存清理功能故障，无法继续进行添加操作。请暂时不要改变数据库内数据并与程序员联系！", "严重警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SqlExecuter.记录开机事件(TableName))
                MessageBox.Show("插入记录成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.显示后15条ToolStripMenuItem_Click(null, null);
        }

        private void 插入关机记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SqlExecuter.记录关机事件())
            {
                MessageBox.Show("添加关机记录成功！");
                显示后15条ToolStripMenuItem_Click(null, null);
            }
        }
            #endregion
            #region 删除数据
        private bool TruncateTable(string TableName)
        {
            if (MessageBox.Show("此操作不可恢复！是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return false;

            if (db.ExecuteUpdate("delete from " + TableName) != 0)
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

        private void 清除主表数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            TruncateTable(TableName);
            this.显示后15条ToolStripMenuItem_Click(null, null);
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
        private void 删除指定一条记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;
            string input = Interaction.InputBox("请输入要删除条目前的序号", "删除指定条记录",hint:"要删除的记录序号列对应的数字");
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

                if (db.ExecuteUpdate(sql) > 0)
                {
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.显示后15条ToolStripMenuItem_Click(null, null);
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

            if (db.ExecuteUpdate(DeleteMaxIDSQL()) != 0)
            {
                MessageBox.Show("删除最后一条记录成功!", "删除成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.显示后15条ToolStripMenuItem_Click(null, null);
            }
        }

        private string DeleteMaxIDSQL() =>
            "DELETE " +
            "FROM[Table] " +
            "WHERE 序号 = ( " +
            "SELECT MAX(序号) " +
            "FROM[Table])";
        #endregion
            #region 修改数据
        private void 提交修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            if (db.UpdateDatabase((DataTable)dataGridView1.DataSource))
                System.Windows.MessageBox.Show("手动修改已提交到数据库。", "修改成功！", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            this.显示后15条ToolStripMenuItem_Click(sender, e);
        }
        #endregion
            #region 高级选项
        private void 执行SQL语句ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            if (MessageBox.Show("确定要使用该操作吗？使用该操作可能会导致不可逆的错误发生，您必须自己确信SQL语句完全正确，程序不会提供错误提醒。如果您是管理人员，且知道此举的后果，请点击确认。如果是正常用户请点击取消！", "严重使用警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;

            String executeSQL = Interaction.InputBox("请输入要执行的SQL语句(表名[Table]):", "SQL输入");
            if (executeSQL.Equals(""))
                return;
            executeSQL = executeSQL.ToUpper();
            dataGridView1.DataSource = null;

            if (executeSQL.IndexOf("SELECT") == 0)
                dataGridView1.DataSource = db.ExecuteQuery(executeSQL);
            else
            {
                int count = db.ExecuteUpdate(executeSQL);
                if (count == 0)
                    MessageBox.Show("执行失败，没有条目受到影响");
                else
                    MessageBox.Show("执行成功，" + count + "条条目受到影响");
            }
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

            int count = db.ExecuteUpdate(File.ReadAllText(filename).Replace("go", " "));
            if (count == 0)
                MessageBox.Show("执行失败，没有条目受到影响");
            else
                MessageBox.Show("执行成功，" + count + "条条目受到影响");
        }

        private void 释放数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            db.ResetConnection();
            MessageBox.Show("释放成功");
        }

        private void 查看已连接数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (db.ConnectionOpenned() == true)
                MessageBox.Show("已连接数据库" + SqliteConnection.DbFullName, "数据库文件名");
            else if (db.ConnectionState == ConnectionState.Connecting)
                MessageBox.Show("正在连接数据库，请稍后重试. . .");
            else
                MessageBox.Show("未连接数据库");
        }

        private void 报错窗口预览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new Exception("我是报错框。");
        }

        private void 命令行选项使用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            string command = Interaction.InputBox("输入命令行指令：", "命令行选项使用");
            if (command == "")
                return;
            Util.FastModeUtil.RunConsoleApplication(command.Split(' '));
        }

        private void 激活禁止系统休眠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new HibernateForm().ShowDialog();
        }
            #endregion
        #endregion

        #region 必要插件安装
        private void 安装写入开机记录时间插件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(Properties.Resources.RecorderShellFullFilename, @"C:\Users\" + ProgramLauncher.SystemUserName + @"\sd.exe" + " -k " + Directory.GetCurrentDirectory() + "\\" + Properties.Resources.MdfFilename, System.Text.Encoding.ASCII);
                MessageBox.Show("已经安装！", "成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (UnauthorizedAccessException)
            {
                MainForm.restartWithAdminRight();
            }
        }

        private void 卸载写入开机记录时间插件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

        #region 备份与恢复
            #region 导入所有数据
        private Boolean CanReadMdfFile()
        {
            return !db.ConnectionOpenned();
        }
        /// <summary>
        /// 选择并加载.backup备份数据库文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 无损加载数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanReadMdfFile() == false)
            {
                db.ResetConnection();
            }

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "备份文件 (*.backup)|*.backup|所有文件 (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                GZipUtil.Decompress(new FileInfo(fileDialog.FileName), Properties.Resources.MdfFilename);
                MessageBox.Show("无损备份文件加载成功！", "加载成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void 还原数据库_RarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            db.ResetConnection();
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
            #region 导出所有数据
        private void 无损导出数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            db.ResetConnection();

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

        private void 备份数据库_RarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            db.ResetConnection();
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
        private void 保存下方数据ToolStripMenuItem_Click(object sender, EventArgs e)
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
        #endregion
        #endregion

        #region 高级功能
        private void 日志管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            new LogManagerForm().Show();
        }

        private void 数据可视化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            new AnalysingForm().Show();
        }

        private void 注释管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            new RemarkManagerForm().Show();
        }
        
        #endregion

        #region 返回主界面
        private void 返回主界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        
        #region 数据库后台线程
        /// <summary>
        /// 后台线程执行函数。只负责打开数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dbBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                db.OpenConnection();
                this.dbBackgroundWorker.ReportProgress(100); //报告执行完毕，执行RunWorkerCompleted
            }
            catch (Exception ex)
            {
                ExceptionForm.ShowDialog(ex);
            }
        }

        /// <summary>
        /// 后台线程执行完毕函数。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openDBBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (backgroundQueryMode == QueryMode.显示所有数据)
                ShowTotalTable();
            else if (backgroundQueryMode == QueryMode.显示后五行数据)
            {
                this.显示后15条ToolStripMenuItem_Click(sender, e);
            }
            else
            {
                this.精准查找显示ToolStripMenuItem_Click(sender, e);
            }
            this.progressBar1.Value = 100;
            this.statusLabel.Text = "完成";
        }
        #endregion
        
        #region 清除缓存后台线程
        private void clearCacheBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.statusLabel.Text = "正在清除缓存并完成同步操作";
            SqlServerConnection.ClearCache();
        }

        private void clearCacheBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.statusLabel.Text = "完成";
            this.progressBar1.Value = 100;
            显示后15条ToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region 安全模式模块
        public bool DatabaseOffline { set { Enable安全模式(value); } }
        private void Enable安全模式(bool enable = true)
        {
            // 无用操作提前返回，提高代码效率
            if (this.查询所有记录ToolStripMenuItem.Enabled == !enable)
                return;

            this.查询所有记录ToolStripMenuItem.Enabled = !enable;
            this.插入一条开机记录ToolStripMenuItem.Enabled = !enable;
            this.填补空处ToolStripMenuItem.Enabled = !enable;
            this.添加数据ToolStripMenuItem.Enabled = !enable;
            this.删除数据ToolStripMenuItem.Enabled = !enable;
            this.修改数据ToolStripMenuItem.Enabled = !enable;
            this.执行SQL语句ToolStripMenuItem.Enabled = !enable;
            this.保存下方数据ToolStripMenuItem.Enabled = !enable;
            this.日志管理ToolStripMenuItem.Enabled = !enable;
            this.数据可视化ToolStripMenuItem.Enabled = !enable;
            this.注释管理ToolStripMenuItem.Enabled = !enable;
            this.命令行选项使用ToolStripMenuItem.Enabled = !enable;
            this.运行SQL脚本ToolStripMenuItem.Enabled = !enable;

            if (enable)
            {
                //为高级选项添加停用安全模式按钮
                var 安全模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                高级选项ToolStripMenuItem.DropDownItems.Add(安全模式ToolStripMenuItem);
                安全模式ToolStripMenuItem.Name = "停用安全模式ToolStripMenuItem";
                安全模式ToolStripMenuItem.Text = "停用安全模式";
                安全模式ToolStripMenuItem.Click += new System.EventHandler(this.停用安全模式ToolStripMenuItem_Click);
            }
        }

        private void 停用安全模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DatabaseOffline = false; //取消安全模式状态
            MainForm.DatabaseOffline = false; //取消主页安全模式状态
            高级选项ToolStripMenuItem.DropDownItems.RemoveByKey("停用安全模式ToolStripMenuItem"); //删除停用安全模式按钮
        }
        #endregion

    }
}