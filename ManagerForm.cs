﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using 关机助手.Util;

namespace 关机助手
{
    public partial class ManagerForm : Form
    {
        #region 常量定义
        public readonly static String TableName = "[Table]";
        private Semaphore semaphore = new Semaphore(initialCount: 1, maximumCount: 3);
        enum QueryMode { 显示所有数据, 显示后十五条数据, 显示后n条是数据, 精准查找, 统计结算填补 };
        #endregion

        #region 变量定义
        private DatabaseAgency database = new DatabaseAgency();
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
            if (database.ConnectionState == ConnectionState.Connecting)
            {
                MessageBox.Show("稍安勿躁，请在程序不忙时重试", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }
        #endregion

        #region 窗体加载事件
        public ManagerForm()
        {
            InitializeComponent();
        }

        private void DatabaseManagerForm_Load(object sender, EventArgs e)
        {
            //处理非UI线程异常，激活全局错误弹窗
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            if (MainForm.DatabaseOffline == true)
            {
                this.DatabaseOffline = true;
                return;
            }
            this.progressBar1.Value = 10;
            this.dataGridView1.RowHeadersWidth = GetRowHeaderWidth(1000);
            this.clearCacheBackgroundWorker.RunWorkerAsync();

            //显示后五条ToolStripMenuItem_Click(sender, e);
            this.toolStripStatusLabelTime.Text = "" + DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToShortTimeString();
            if (File.Exists(Properties.Resources.RecorderShellFullFilename) == false)
            {
                if (MessageBox.Show(needInitialized ? "是否自动完成初始化工作？" : "检测到开机记录已经失效，是否进行修复？", needInitialized ? "欢迎使用本软件" : "警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    安装ToolStripMenuItem_Click(sender, e);
            }

            this.TopMost = MainForm.窗口置顶;
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

        #region 数据显示
        /// <summary>
        /// 获得多少条查询结果对应的行号宽度
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        private int GetRowHeaderWidth(int rowsCount)
        {
            return 46 + 6 * Math.Max(0, ((int)Math.Log10(rowsCount) - 1));
        }

        /// <summary>
        /// 输出[Table]所有内容
        /// </summary>
        private void ShowTotalTable()
        {
            dataGridView1.DataSource = null;
            var queryResult = database.ExecuteQuery("select * from " + TableName);
            dataGridView1.DataSource = queryResult;
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            this.dataGridView1.RowHeadersWidth = GetRowHeaderWidth(queryResult.Rows.Count);
        }
        
        private void 展示所有数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;
            if (this.Width == 702)
                this.Width = 710;
            // 数据库已连接与未连接都可以处理
            if (database.ConnectionState == ConnectionState.Closed)
            {
                this.progressBar1.Value = 40;
                this.statusLabel.Text = "正在加载数据";
                backgroundQueryMode = QueryMode.显示所有数据;
                this.dbBackgroundWorker.RunWorkerAsync(null);
            }
            else
                ShowTotalTable();
        }
        
        private void 显示后20条ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;
            if (this.Width == 710)
                this.Width = 702;
            // 数据库已连接与未连接都可以处理
            if (!database.ConnectionOpenned())
            {
                this.progressBar1.Value = 40;
                this.statusLabel.Text = "正在加载数据";
                backgroundQueryMode = QueryMode.显示后十五条数据;
                this.dbBackgroundWorker.RunWorkerAsync(1);
            }
            else
            {
                dataGridView1.DataSource = null;
                var queryResult = database.ExecuteQuery(
                    "select * from " + 
                    TableName + 
                    " where 序号>((select max(序号) from " + TableName + ")-20)");
                dataGridView1.DataSource = queryResult;
                this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                this.dataGridView1.RowHeadersWidth = GetRowHeaderWidth(queryResult.Rows.Count);
            }
        }

        private void 显示后n条数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;
            if (this.Width == 710)
                this.Width = 702;
            string inputn = Interaction.InputBox("需要显示后几条数据？");
            if (inputn == "")
                return;
            int n = int.Parse(inputn);
            if (n < 1)
                return;
            // 数据库已连接与未连接都可以处理
            if (!database.ConnectionOpenned())
            {
                this.progressBar1.Value = 40;
                this.statusLabel.Text = "正在加载数据";
                backgroundQueryMode = QueryMode.显示后n条是数据;
                this.dbBackgroundWorker.RunWorkerAsync(n);
            }
            else
            {
                dataGridView1.DataSource = null;
                var queryResult = database.ExecuteQuery(
                    "select * from " +
                    TableName +
                    " where 序号>((select max(序号) from " + TableName + ")-"+n+")");
                dataGridView1.DataSource = queryResult;
                this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                this.dataGridView1.RowHeadersWidth = GetRowHeaderWidth(queryResult.Rows.Count);
            }
        }

        private string QueryCustomSQL { get; set; }

        private bool? InputCustomSQLString()
        {
            String input = Interaction.InputBox("请输入要查询的日期。支持年、月、日的任意组合", "精准查找", defaultText: "2018年1月1日").Trim();
            if (input == "")
                return null;
            int year = 0, month = 0, day = 0;
            int tmp = 0;
            foreach(char ch in input.ToCharArray())
            {
                if (ch == ' ')
                    continue;
                if (char.IsDigit(ch))
                    tmp = tmp * 10 + ch - '0';
                else if ("年月日".IndexOf(ch) != -1)
                {
                    if ('年' == ch)
                        year = tmp;
                    else if ('月' == ch)
                        month = tmp;
                    else if ('日' == ch)
                        day = tmp;
                    else
                        throw new Exception();
                    tmp = 0;
                }
                else
                    return false;
            }
            if (tmp != 0)
                return false;

            List<string> conditions = new List<string>();
            if (year != 0)
                conditions.Add("YEAR(开机时间)=" + year);
            if (month != 0)
                conditions.Add("MONTH(开机时间)=" + month);
            if (day != 0)
                conditions.Add("DAY(开机时间)=" + day);
            QueryCustomSQL = SqlUtil.Select_Sql("[Table]", "*", string.Join(" and ", conditions));
            return true;
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
            if (!database.ConnectionOpenned())
            {
                this.progressBar1.Value = 40;
                this.statusLabel.Text = "正在加载数据";
                backgroundQueryMode = QueryMode.精准查找;
                this.dbBackgroundWorker.RunWorkerAsync(null);
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = database.ExecuteQuery(QueryCustomSQL);
                this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
        }
        #endregion

        #region 数据库管理
        private void 填补空处ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;
            if (MessageBox.Show("该操作会将时长、当天使用次数、当月使用次数完全计算并填补，是否继续？", "一键填补", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            SqlExecuter.记录结算();
            this.Focus();
            this.progressBar1.Value = 40;
            this.statusLabel.Text = "正在填补空处";
            backgroundQueryMode = QueryMode.显示后十五条数据;
            this.dbBackgroundWorker.RunWorkerAsync(null);
        }
            #region 添加数据
        private void 插入开机记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;
            if (Cache.ExistCache()) 
            {
                MessageBox.Show("检测到缓存清理功能故障，无法继续进行添加操作。请暂时不要改变数据库内数据并与程序员联系！", "严重警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SqlExecuter.记录开机事件(TableName))
                MessageBox.Show("插入记录成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.显示后20条ToolStripMenuItem_Click(null, null);
        }

        private void 插入关机记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SqlExecuter.记录关机事件())
            {
                MessageBox.Show("添加关机记录成功！");
                显示后20条ToolStripMenuItem_Click(null, null);
            }
        }
            #endregion
            #region 删除数据
        private bool TruncateTable(string TableName)
        {
            if (MessageBox.Show("此操作将永久清空该表，执行后不可恢复！是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return false;

            if (database.ExecuteUpdate("delete from " + TableName) != 0)
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
            this.显示后20条ToolStripMenuItem_Click(null, null);
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
            string input = Interaction.InputBox("请输入要删除记录前方的序号", "删除指定一条记录", hint: "要删除的记录序号列对应的数字").Trim();
            if (input == "")
                return;
            string sql = "DELETE " +
                    "FROM[Table] " +
                    "WHERE 序号 = ";
            try
            {
                sql += int.Parse(input);
            }
            catch (FormatException)
            {
                MessageBox.Show("输入的不是正整数，删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                if (MessageBox.Show("序号为"+input+"将被永久删除。此操作不可恢复！是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;

                if (database.ExecuteUpdate(sql) > 0)
                {
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.显示后20条ToolStripMenuItem_Click(null, null);
                }
                else
                    MessageBox.Show("发生未知错误，删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("发生未知错误，删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 删除指定范围记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            string input = Interaction.InputBox("请输入要删除记录的序号范围", "删除指定范围记录", hint: "如，删除前十条记录可输入：1-10").Trim();
            if (input == "")
                return;
            List<int> inputSplit = new List<int>();
            int tmp = 0;
            foreach(char ch in input)
            {
                if (char.IsDigit(ch))
                    tmp = tmp * 10 + ch - '0';
                else if (ch == '-' && tmp != 0) 
                {
                    inputSplit.Add(tmp);
                    tmp = 0;
                }
            }
            if (tmp != 0)
                inputSplit.Add(tmp);
            if (inputSplit.Count < 2 || inputSplit[0] > inputSplit[1]) 
            {
                MessageBox.Show("输入格式错误，请确认无误后重试！", "错误提示");
                return;
            }

            string sql = "DELETE " +
                    "FROM[Table] " +
                    "WHERE 序号>=" + inputSplit[0] + " and 序号<=" + inputSplit[1];
            if (MessageBox.Show("序号范围为 " + inputSplit[0] + "至" + inputSplit[1] + " 的记录将被永久删除。此操作不可恢复！是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) 
                return;
            if (database.ExecuteUpdate(sql) > 0)
            {
                MessageBox.Show("序号范围为 " + inputSplit[0] + "至" + inputSplit[1] + " 的记录已被永久删除！", "删除成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.显示后20条ToolStripMenuItem_Click(null, null);
            }
            else
                MessageBox.Show("发生未知错误，删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void 删除最后一条记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            string connStr = Properties.Settings.Default.TimeDatabaseConnectionString;//连接字符串

            if (database.ExecuteUpdate(DeleteMaxIDSQL()) != 0)
            {
                MessageBox.Show("删除最后一条记录成功!", "删除成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.显示后20条ToolStripMenuItem_Click(null, null);
            }
        }

        private string DeleteMaxIDSQL() =>
            "DELETE " +
            "FROM[Table] " +
            "WHERE 开机时间 = ( " +
            "SELECT MAX(开机时间) " +
            "FROM[Table])";
            #endregion
            #region 修改数据
        private void 提交全部修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            if (database.UpdateDatabase((DataTable)dataGridView1.DataSource))
                System.Windows.MessageBox.Show("手动修改已提交到数据库。", "修改成功！", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            this.显示后20条ToolStripMenuItem_Click(sender, e);
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
                dataGridView1.DataSource = database.ExecuteQuery(executeSQL);
            else
            {
                int count = database.ExecuteUpdate(executeSQL);
                if (count == 0)
                    MessageBox.Show("执行失败，没有记录受到影响");
                else
                    MessageBox.Show("执行成功，" + count + "条记录受到影响");
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
            fileDialog.Title = "请选择脚本文件";
            fileDialog.ShowDialog();
            if (fileDialog.FileName == "")
                return;
            string filename = fileDialog.FileName.Trim().Replace("\"", "");

            int count = database.ExecuteUpdate(File.ReadAllText(filename).Replace("go", " "));
            if (count == 0)
                MessageBox.Show("执行失败，没有记录受到影响");
            else
                MessageBox.Show("执行成功，" + count + "条记录受到影响");
        }

        private void 释放数据库连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            database.ResetConnection();
            MessageBox.Show("释放成功");
        }

        private void 查看已连接数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (database.ConnectionOpenned() == true)
                MessageBox.Show("已连接数据库" + database.DbFullName, "数据库文件名");
            else if (database.ConnectionState == ConnectionState.Connecting)
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
            Util.FastModeExecutor.RunConsoleApplication(command.Split(' '));
        }

        private void 激活禁止系统休眠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new HibernateForm().ShowDialog();
        }
            #endregion
        #endregion

        #region 插件安装
        private void 安装ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainForm.WritePowerOnShellInStartUpFolderBat())
                MessageBox.Show("插件已经安装！", "安装成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("操作已取消！", "取消操作", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            
            File.Delete("write_bat.bat");
        }

        private void 卸载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(Properties.Resources.RecorderShellFullFilename);
                MessageBox.Show("插件已经卸载", "成功！");
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
            return !database.ConnectionOpenned();
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
                database.ResetConnection();
            }

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "备份文件 (*.backup)|*.backup|所有文件 (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                GZipUtil.Decompress(new FileInfo(fileDialog.FileName), Properties.Resources.MdfFilename);
                MessageBox.Show("无损备份文件加载成功！", "加载成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        private void 导入所有数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "还原文件 (*.rar)|*.rar|所有文件 (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                database.ResetConnection();
                String file = fileDialog.FileName;
                String rawRarTempFilename = file.Insert(file.Length - 4, "_rawfile");
                if (EncryptUtil.DecryptFile_HC128(file, rawRarTempFilename))
                {
                    if (WinRARUtil.DecompressFile(rawRarTempFilename, Directory.GetCurrentDirectory()))
                        MessageBox.Show("无损还原数据库成功！", "还原成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        MessageBox.Show("还原失败！该操作需要电脑上装有WinRAR软件。", "失败提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("还原失败！解密文件时发生未知错误。", "失败提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Delete(rawRarTempFilename);
                Application.Restart();
            }
        }
            #endregion
            #region 导出所有数据
        private void 导出所有数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            database.ResetConnection();

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
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "备份文件 (*.rar)|*.rar|所有文件 (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                database.ResetConnection();
                if (File.Exists(fileDialog.FileName))
                    File.Delete(fileDialog.FileName);
                if (WinRARUtil.CompressFile(
                    new String[] { new FileInfo("TimeDatabase.mdf").FullName,
                                   new FileInfo("TimeDatabase_log.ldf").FullName
                                 },
                    fileDialog.FileName))
                {
                    if (EncryptUtil.EncryptFile_HC128(fileDialog.FileName))
                        MessageBox.Show("无损备份数据库成功！", "备份成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        MessageBox.Show("备份失败！加密文件时发生未知错误。", "失败提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("备份失败！该操作需要电脑上装有WinRAR软件。", "失败提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void 导出下方表格ToolStripMenuItem_Click(object sender, EventArgs e)
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

        #region 其他管理器
        private void 日志管理器ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 备注管理器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AlertBusy())
                return;

            new RemarkManagerForm().Show();
        }
        
        private void 缓存管理器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CacheManagerForm().ShowDialog();
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
                database.OpenConnection();
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
            semaphore.WaitOne();
            if (backgroundQueryMode == QueryMode.显示所有数据)
                ShowTotalTable();
            else if (backgroundQueryMode == QueryMode.显示后十五条数据)
            {
                this.显示后20条ToolStripMenuItem_Click(sender, e);
            }
            else if (backgroundQueryMode == QueryMode.精准查找)
            {
                this.精准查找显示ToolStripMenuItem_Click(sender, e);
            }
            else
                this.显示后n条数据ToolStripMenuItem_Click(sender, e);
            //else if (backgroundQueryMode == QueryMode.统计结算填补)
            //{
            //    SqlExecuter.记录结算();
            //}
            this.progressBar1.Value = 100;
            this.statusLabel.Text = "完成";
            semaphore.Release();
        }
        #endregion
        
        #region 清除缓存后台线程
        private void clearCacheBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.statusLabel.Text = "正在清除缓存并完成同步操作";
            database.ClearCache();
        }

        private void clearCacheBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.statusLabel.Text = "完成";
            this.progressBar1.Value = 100;
            显示后20条ToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region 安全模式
        public bool DatabaseOffline { set { Enable安全模式(value); } }
        private void Enable安全模式(bool enable = true)
        {
            // 无用操作提前返回，提高代码效率
            if (this.时间记录显示ToolStripMenuItem.Enabled == !enable)
                return;

            this.时间记录显示ToolStripMenuItem.Enabled = !enable;
            this.插入开机记录_ToolStripMenuItem.Enabled = !enable;
            this.填补空处ToolStripMenuItem.Enabled = !enable;
            this.添加数据ToolStripMenuItem.Enabled = !enable;
            this.删除数据ToolStripMenuItem.Enabled = !enable;
            this.修改数据ToolStripMenuItem.Enabled = !enable;
            this.执行SQL语句ToolStripMenuItem.Enabled = !enable;
            this.导出下方表格ToolStripMenuItem.Enabled = !enable;
            this.日志管理器ToolStripMenuItem.Enabled = !enable;
            this.数据可视化ToolStripMenuItem.Enabled = !enable;
            this.备注管理器ToolStripMenuItem.Enabled = !enable;
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