﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using 关机助手.Util;
using static 关机助手.Util.ShutdownUtil;

namespace 关机助手
{
    public partial class MainForm : Form
    {
        private DatabaseAgency database { get; set; } = new DatabaseAgency(); // 数据库连接代理
        private List<Thread> threadPool = new List<Thread>(); // 保存休眠/睡眠的线程列表
        private static MainForm mForm = null;

        #region 窗体加载关闭事件
        public MainForm()
        {
            InitializeComponent();
            // 保存this，MainForm只允许被实例化一次
            if (mForm != null)
                throw new Exception("Illegal initialization!");
            mForm = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (this.需要重复开启软件检查)
            {
                // 检测后台是否运行同一程序
                Process[] processes = Process.GetProcessesByName("关机助手");
                if (processes.Length == 0)
                    processes = Process.GetProcessesByName("TimeManager");
                if (processes.Length > 1)
                {
                    MessageBox.Show("检测到后台已经启动本程序，建议关闭其他窗口至只剩下本窗口。", "温馨提示");
                }
            }
            // 检测数据库文件是否存在，不存在则解压缩空数据库
            if (!File.Exists(Properties.Resources.MdfFilename))
            {
                BinaryWriterUtil.WriteFileToDisk(
                    GZipUtil.DecompressBytes(Properties.Resources.EmptyDB),
                    Properties.Resources.MdfFilename);

                MessageBox.Show("检测到您第一次使用本软件，请点击数据管理进行初始化操作。", "欢迎！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ManagerForm.needInitialized = true;
            }
            comboBoxMode.SelectedIndex = 0;
            // 加载配置文件，执行相应的操作（调用已有的事件函数，以免造成与显示不同步的问题）
            if (ConfigManager.MainFormConfigLoaded)
            {
                if (ConfigManager.SafeModeBoot)
                    MainForm.DatabaseOffline = true;
                if (ConfigManager.MainFormAutoDarkMode)
                    darkModeToolStripMenuItem_Click(null, null);
                if (ConfigManager.MainFormOpacity != -1)
                    this.toolStripComboBox不透明度.Text = ConfigManager.MainFormOpacity.ToString() + "%";
                if (ConfigManager.MainDefaultComboBoxIndex != -1)
                    this.comboBoxMode.SelectedIndex = ConfigManager.MainDefaultComboBoxIndex;
                if (ConfigManager.MainFormAutoShutdownSeconds != -1)
                {
                    if (this.label设置倒计时.Text.Contains("分钟"))
                        this.comboBoxTime.Text = ((double)ConfigManager.MainFormAutoShutdownSeconds / 60).ToString();
                    else
                        this.comboBoxTime.Text = ((double)ConfigManager.MainFormAutoShutdownSeconds / 3600).ToString();
                    button确定_Click(sender, e);
                }
                if (ConfigManager.MainFormHideInTaskbar)
                    任务栏隐匿ToolStripMenuItem_Click(null, null);
                if (ConfigManager.MainFormHideNotifyIcon)
                    隐藏右下角图标ToolStripMenuItem_Click(null, null);
            }
            // 获取版本号并替换标题
            this.Text = this.Text.Replace("{Version}", ProgramLauncher.Version());
            // 给ComboBox添加选项
            AddSelectOptionsInComboBoxTime();
            // 添加鼠标滚动事件
            this.MouseWheel += new MouseEventHandler(comboBoxTime_MouseWheel);
            // 添加时间并开启自动刷新时间线程
            AddNowTimeToFormTitle();
            FlushTitleInEvery10Second();
        }

        private void AddSelectOptionsInComboBoxTime()
        {
            for (int i = 0; i <= 60; i += 5)
                this.comboBoxTime.Items.Add(i);

        }

        private void AddNowTimeToFormTitle()
        {
            this.Text += " " + DateTime.Now.ToString("HH:mm:ss");

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Environment.Exit(0);
        }
        #endregion
        /******** 主窗体事件 *********/
        #region 鼠标滚动
        private void comboBoxTime_MouseWheel(object sender, MouseEventArgs e)
        {
            int nowNumber = -1;
            try { nowNumber = int.Parse(comboBoxTime.Text); }
            catch { return; }

            if (e.Delta > 0)
                this.comboBoxTime.Text = (nowNumber + 1).ToString();
            else if (e.Delta < 0)
            {
                if (nowNumber == 0)
                    nowNumber += 60;
                this.comboBoxTime.Text = (nowNumber - 1).ToString();
            }
        }
        #endregion

        #region 主窗体双击
        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            RunDevProc();
        }

        private void RunDevProc()
        {
            File.Copy(Properties.Resources.ExeDevelopFullFilename, "C:\\Users\\" + ProgramLauncher.SystemUserName + "\\Desktop\\关机助手0.exe");
        }
        #endregion

        #region 拖拽数据库文件事件
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
                return;
            string filename = files[0];
            if (filename.Contains(".mdf") == false)
            {
                MessageBox.Show("外链失败，请拖拽mdf文件。", "外链数据库", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            database.OpenConnection(filename);
            MessageBox.Show("外链数据库文件成功。", "已使用新的数据库");
        }
        #endregion
        /****** 窗体内控件事件 *******/
        #region 键盘输入事件
        private void comboBoxTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' || e.KeyChar == '\r')
            {
                button确定_Click(sender, e);
                if (this.comboBoxMode.Text != "休眠" && this.comboBoxMode.Text != "睡眠")
                    ApplicationExit();
            }
            else if (e.KeyChar == 'q')
                this.取消关机ToolStripMenuItem_Click(sender, e);
            else if (e.KeyChar == 27)
                ApplicationExit();
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' || e.KeyChar == '\r')
                button确认2_Click(sender, e);
            else if (e.KeyChar == 'q')
                this.取消关机ToolStripMenuItem_Click(sender, e);
            else if (e.KeyChar == 27)
                ApplicationExit();
        }
        #endregion

        #region 选项改变事件
        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxMode.Text == "重启")
                this.记录关机时间checkBox.Checked = false;
        }

        private void label设置倒计时_Click(object sender, EventArgs e)
        {
            if (this.label设置倒计时.Text.Contains("分钟"))
                this.label设置倒计时.Text = this.label设置倒计时.Text.Replace("分钟", "小时");
            else
                this.label设置倒计时.Text = this.label设置倒计时.Text.Replace("小时", "分钟");
        }
        #endregion
        /****************************/

        #region 菜单栏
            #region 插入
        private void 插入开机时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SqlExecuter.记录开机事件("[Table]"))
                MessageBox.Show("插入记录成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 插入关机时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SqlExecuter.记录关机事件())
                MessageBox.Show("添加关机记录成功！");
        }
            #endregion
            #region 管理器
        private void 管理器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!MainForm.DatabaseOffline)
                this.安全模式ToolStripMenuItem.Enabled = false;
            new ManagerForm().ShowDialog();
        }
            #endregion
            #region 取消关机
        private void 取消关机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelShutdownCommand();
        }

        private void 取消休眠睡眠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("关机助手");
            if (processes.Length == 0)
                processes = Process.GetProcessesByName("TimeManager");
            var nowId = Process.GetCurrentProcess().Id;
            List<int> killedIds = new List<int>();
            foreach (var process in processes)
            {
                if (process.Id == nowId)
                    continue;
                killedIds.Add(process.Id);
                process.Kill();
            }
            string killedIdsInfoString = killedIds.Count == 0 ? "" : ("同时检测到后台还有其它本程序正在运行，已结束这些进程（ID:" + String.Join(",", killedIds) + "）。");
            if (this.threadPool.Count == 0)
            {
                this.notifyIcon.ShowBalloonTip(2000, "无操作", "没有需要被取消的定时休眠/睡眠。" + killedIdsInfoString, ToolTipIcon.Info);
            }
            else
            {
                this.AbordSubThreads();
                this.notifyIcon.ShowBalloonTip(2000, "取消成功", "定时休眠/睡眠已经取消。" + killedIdsInfoString, ToolTipIcon.Info);
            }
        }

        private void 全部取消ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            取消关机ToolStripMenuItem_Click(null, null);
            取消休眠睡眠ToolStripMenuItem_Click(null, null);
        }
            #endregion
            #region 拓展功能
        private int 拓展功能ButtonClickedAnimatedTimes = 2; // 动画剩余显示次数。双数表示未展开
        private void 拓展功能ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (拓展功能ButtonClickedAnimatedTimes % 2 == 0)
            {
                if (拓展功能ButtonClickedAnimatedTimes > 0) // 动画被启用
                {
                    int waitMilisecond = 25;
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height + 10);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height + 5);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height + 4);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height + 4);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height + 4);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height + 3);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height + 2);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height + 2);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height + 1);
                    Thread.Sleep(waitMilisecond);
                }
                this.ClientSize = new Size(this.ClientSize.Width, 136);
                this.拓展功能ToolStripMenuItem.Text = "精简功能";
            }
            else
            {
                if (拓展功能ButtonClickedAnimatedTimes > 0) // 动画被启用
                {
                    int waitMilisecond = 50;
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height - 10);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height - 5);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height - 4);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height - 4);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height - 4);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height - 3);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height - 2);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height - 2);
                    Thread.Sleep(waitMilisecond);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height - 1);
                    Thread.Sleep(waitMilisecond);
                }
                this.ClientSize = new Size(this.ClientSize.Width, 98);
                this.拓展功能ToolStripMenuItem.Text = "拓展功能";
            }
            拓展功能ButtonClickedAnimatedTimes--;
        }
            #endregion
        #endregion

        #region 确定键
        private void Run休眠或睡眠(string type, float comboBoxTimeMinutes)
        {
            if (this.comboBoxMode.Text != "休眠" && this.comboBoxMode.Text != "睡眠")
                return;

            this.Hide();
            // 定时成功提示
            if (comboBoxTimeMinutes != 0)
                this.notifyIcon.ShowBalloonTip(2000, "即将进入" + type + "状态", "程序将等待" + comboBoxTimeMinutes + "分钟后执行" + type + "。如果需要撤销操作请右击图标，选择退出按钮。", ToolTipIcon.Info);
            // 提前10分钟提示
            if (comboBoxTimeMinutes > 10)
            {
                threadPool.Add(new Thread((typeStr) =>
                {
                    try
                    {
                        Thread.Sleep((int)((comboBoxTimeMinutes - 10) * 60000));
                    }
                    catch (Exception)
                    {
                    }
                    this.notifyIcon.ShowBalloonTip(2000, "即将进入" + type + "状态", "程序将在10分钟后执行" + type + "。如果需要撤销操作请右击图标，选择退出按钮。", ToolTipIcon.Info);
                }));
            }
            // 提前3分钟提示
            if (comboBoxTimeMinutes > 3)
            {
                threadPool.Add(new Thread((typeStr) =>
                {
                    try
                    {
                        Thread.Sleep((int)((comboBoxTimeMinutes - 3) * 60000));
                    }
                    catch (Exception)
                    {
                    }
                    this.notifyIcon.ShowBalloonTip(2000, "即将进入" + type + "状态", "程序将在3分钟后执行" + type + "。如果需要撤销操作请右击图标，选择退出按钮。", ToolTipIcon.Info);
                }));
            }
            // 提前1分钟提示
            if (comboBoxTimeMinutes > 1)
            {
                threadPool.Add(new Thread((typeStr) =>
                {
                    try
                    {
                        Thread.Sleep((int)((comboBoxTimeMinutes - 1) * 60000));
                    }
                    catch (Exception)
                    {
                    }
                    this.notifyIcon.ShowBalloonTip(2000, "即将进入" + type + "状态", "程序将在1分钟后执行" + type + "。如果需要撤销操作请右击图标，选择退出按钮。", ToolTipIcon.Info);
                }));
            }
            // 执行休眠/睡眠的线程
            threadPool.Add(new Thread((typeStr) =>
            {
                try
                {
                    Thread.Sleep((int)(comboBoxTimeMinutes * 60000));
                }
                catch (Exception)
                {
                    this.notifyIcon.ShowBalloonTip(2000, "取消成功", type + "已被取消", ToolTipIcon.Info);
                    return;
                }
                if (typeStr.ToString() == "休眠")
                {
                    RunSuspendCommand(Mode.休眠);
                    if (this.记录关机时间checkBox.Checked)
                        休眠后再次添加开机记录();
                }
                else if (typeStr.ToString() == "睡眠")
                {
                    RunSuspendCommand(Mode.睡眠);
                    if (this.记录关机时间checkBox.Checked)
                        休眠后再次添加开机记录();
                }
            }));
            // 启动所有线程
            foreach (var thread in threadPool)
                thread.Start(this.comboBoxMode.Text);
        }

        private void AbordSubThreads()
        {
            foreach (var thread in this.threadPool)
                thread.Abort();
            threadPool.Clear();
        }

        private void button确定_Click(object sender, EventArgs e)
        {
            try
            {
                float comboBoxTimeMinutes = float.Parse(this.comboBoxTime.Text);
                if (this.label设置倒计时.Text.Contains("小时"))
                    comboBoxTimeMinutes *= 60;

                if (this.记录关机时间checkBox.Checked && this.comboBoxMode.Text != "延缓")
                    SqlExecuter.记录关机事件();
                if (comboBoxTimeMinutes < 0) // 如果小于0，说明只是记录时间就可以推出
                    return;

                CancelShutdownCommand();
                try
                {
                    switch (this.comboBoxMode.Text)
                    {
                        case "关机":
                            float seconds = comboBoxTimeMinutes * 60;
                            RunShutdownCommand(Mode.关机, seconds);
                            break;
                        case "重启":
                            if (!this.记录关机时间checkBox.Checked)
                                File.CreateText(@"C:\Users\" + ProgramLauncher.SystemUserName + @"\DONOTWRITEDATA").Close();
                            RunShutdownCommand(Mode.重启, comboBoxTimeMinutes * 60);
                            break;
                        case "休眠":
                            Run休眠或睡眠("休眠", comboBoxTimeMinutes);
                            break;
                        case "延缓":
                            FastModeExecutor.ShutdownWithSeconds_DelayMode((int)(comboBoxTimeMinutes * 60));
                            break;
                        case "睡眠":
                            Run休眠或睡眠("睡眠", comboBoxTimeMinutes);
                            break;
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("请输入正确的数据！", "错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.comboBoxTime.Text = "0";
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionForm.ShowDialog(ex);
            }
        }
        #endregion

        #region 休眠后续工作
        private void 休眠后再次添加开机记录()
        {
            new Thread(() =>
            {
                SqlExecuter.记录开机事件();
                ApplicationExit();
            }).Start();
        }
        #endregion

        #region 拓展功能确定键
        private int GetRestTime_Seconds()
        {
            int hoursRest;
            int minutesRest;
            int secondsRest;
            int restTime_seconds;
            hoursRest = this.dateTimePicker1.Value.Hour - DateTime.Now.Hour;
            minutesRest = this.dateTimePicker1.Value.Minute - DateTime.Now.Minute;
            secondsRest = this.dateTimePicker1.Value.Second - DateTime.Now.Second;

            restTime_seconds = hoursRest * 3600 + minutesRest * 60 + secondsRest;
            return restTime_seconds;
        }

        private void button确认2_Click(object sender, EventArgs e)
        {
            /* 全面换成委托调用 */
            var saveTime = this.comboBoxTime.Text; // 保存原来的输入框
            var restTime_seconds = GetRestTime_Seconds(); // 获得剩余的时间秒数
            if (restTime_seconds < 0) // 如果小于0，加一天
                restTime_seconds += 24 * 3600;
            float comboBoxTime;  // 要放入的数字
            if (this.label设置倒计时.Text.Contains("小时"))
                comboBoxTime = (float)restTime_seconds / 3600;
            else
                comboBoxTime = (float)restTime_seconds / 60;
            this.comboBoxTime.Text = comboBoxTime.ToString(); // 放入后点确定
            this.button确定_Click(null, null);
            this.comboBoxTime.Text = saveTime; // 还原原来的输入框内容
            //try
            //{
            //    if (this.记录关机时间checkBox.Checked)
            //        SqlExecuter.记录关机事件();

            //    int restTime_seconds = GetRestTime_Seconds();

            //    Boolean nextDay = false;
            //    if (restTime_seconds <= 0)
            //    {
            //        restTime_seconds += 24 * 3600;
            //        nextDay = true;
            //    }
            //    ShutdownUtil.CancelShutdownCommand();
            //    ShutdownUtil.RunShutdownCommand(Mode.关机, restTime_seconds);
            //    MessageBox.Show("将在" + (nextDay ? "明日" : "今日") + this.dateTimePicker1.Value.ToLongTimeString() + "关机", "离关机还剩" + restTime_seconds + "秒", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (Exception ex)
            //{
            //    ExceptionForm.ShowDialog(ex);
            //}
        }
        #endregion

        #region 注册关机倒计时
        public static void WritePowerOnShellInStartUpFolder()
        {
            try
            {
                File.WriteAllText(Properties.Resources.RecorderShellFullFilename,
                        "chcp 65001\r\n" //先切换cmd的字符编码为UTF-8（注意一定要使用CRLF否则第二行会被吃字）
                        + Application.ExecutablePath + " -k",
                        new System.Text.UTF8Encoding(false)); //保存为无BOM的UTF-8文件
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
        }

        public static bool WritePowerOnShellInStartUpFolderBat()
        {
            string batContent = "chcp 65001\r\n@echo chcp 65001 > \"" + Properties.Resources.RecorderShellFullFilename + "\"\r\n";
            batContent += "@echo " + Application.ExecutablePath + " -k >> \"" + Properties.Resources.RecorderShellFullFilename + "\"";
            File.WriteAllText("write_bat.bat", batContent, new System.Text.UTF8Encoding(false));
            // 使用powershell命令进行runAs（C#方式尝试，宣告失败）
            Process process = new Process();
            process.StartInfo.FileName = "powershell.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = "Start-Process .\\write_bat.bat -Verb runAs";
            process.Start();
            process.WaitForExit(); // powershell用于启动管理员权限的cmd，此句无意义
            return process.StandardError.ReadToEnd() == "";
        }

        private void 注册关机事件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float? minutes = null;
            try
            {
                minutes = float.Parse(this.comboBoxTime.Text);
            }
            catch
            {
                MessageBox.Show("请输入正确的数据！", "错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (minutes == 0)
            {
                MessageBox.Show("选择0分钟是危险行为！已阻止", "错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                File.AppendAllText
                    (Properties.Resources.RecorderShellFullFilename,
                    " -d " + minutes + "m",
                    new System.Text.UTF8Encoding(false));
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("失败！即将获得管理员权限，请在“你要允许来自未知发布者的此应用对你的设备进行更改吗”点击“是”", "警示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                restartWithAdminRight();
                return;
            }
            MessageBox.Show("注册成功！开机" + minutes + "分钟后自动关机", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 销毁关机事件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                WritePowerOnShellInStartUpFolder();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("失败！即将获得管理员权限，请在“你要允许来自未知发布者的此应用对你的设备进行更改吗”点击“是”", "警示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                restartWithAdminRight();
                return;
            }
            MessageBox.Show("销毁成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string SystemStartupFolder() =>
            @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\";

        private void 打开启动文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", SystemStartupFolder());
        }
        #endregion

        #region 主界面附加右键功能
        private void 安全模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Text.IndexOf("启动安全模式") != -1)
            {
                if (DialogResult.Yes == MessageBox.Show("在数据库无法正常使用情况下，推荐使用此项功能。虽然可以保证软件的绝对稳定，但是会损失本系统绝大部分的功能。是否继续？", "即将启动安全模式", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    MainForm.DatabaseOffline = true;
            }
            else
            {
                MainForm.DatabaseOffline = false;
            }
        }

        private void 升级日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new HelpForm().Show();
        }

        private void 切断数据库连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            database.CloseConnection();
            database.DisposeConnection();
            Thread.Sleep(100);
            database.ResetConnection();
            MessageBox.Show("执行完毕");
        }

        private void 外链数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dbFile = new OpenFileDialog();
            dbFile.Filter = "mdf数据库文件|*.mdf";
            dbFile.Title = "选择需要外链的数据库文件";
            if (dbFile.ShowDialog() == DialogResult.Cancel)
                return;
            database.OpenConnection(dbFile.FileName);
            MessageBox.Show("外链数据库文件成功。", "已使用新的数据库");
        }

        private void 获得管理员权限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            restartWithAdminRight(true);
        }

        private void 缓存管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CacheManagerForm().ShowDialog();
        }

        private void 禁止一次开机记时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.CreateText(@"C:\Users\" + ProgramLauncher.SystemUserName + @"\DONOTWRITEDATA").Close();
            MessageBox.Show("下次开机不会记录开机时间", "成功");
        }

        /// <summary>
        /// 暗黑模式控制器
        /// </summary>
        public bool DarkMode
        {
            get
            {
                return this.暗黑模式ToolStripMenuItem.Text != "打开暗黑模式";
            }
            set
            {
                if (value) //打开暗黑模式
                {
                    this.BackColor = SystemColors.WindowFrame;
                    this.ForeColor = SystemColors.Window;
                    this.menuStrip.BackColor = SystemColors.WindowFrame;
                    this.插入ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.插入开机时间ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.插入开机时间ToolStripMenuItem.ForeColor = SystemColors.Menu;
                    this.插入关机时间ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.插入关机时间ToolStripMenuItem.ForeColor = SystemColors.Menu;
                    this.管理器ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.立刻取消ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.拓展功能ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.comboBoxTime.BackColor = SystemColors.WindowFrame;
                    this.comboBoxTime.ForeColor = SystemColors.Menu;
                    this.comboBoxMode.BackColor = SystemColors.WindowFrame;
                    this.comboBoxMode.ForeColor = SystemColors.Menu;
                    this.注册关机事件ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.注册关机事件ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.销毁关机事件ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.销毁关机事件ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.打开启动文件夹ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.打开启动文件夹ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.安全模式ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.安全模式ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.升级日志ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.升级日志ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.源头管理ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.源头管理ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.暗黑模式ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.暗黑模式ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.切断数据库连接ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.切断数据库连接ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.外链数据库ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.外链数据库ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.缓存管理ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.缓存管理ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.获得管理员权限ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.获得管理员权限ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.禁止一次开机记时间ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.禁止一次开机记时间ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.配置管理ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.配置管理ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.附加功能ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.附加功能ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.toolStripComboBox不透明度.BackColor = SystemColors.WindowFrame;
                    this.toolStripComboBox不透明度.ForeColor = SystemColors.Window;
                    this.任务栏隐匿ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.任务栏隐匿ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.退出ToolStripMenuItem1.BackColor = SystemColors.WindowFrame;
                    this.退出ToolStripMenuItem1.ForeColor = SystemColors.Window;
                    this.label指定时间.BackColor = SystemColors.WindowFrame;
                    this.dateTimePicker1.CalendarMonthBackground = SystemColors.WindowFrame;
                    this.buttonOK.BackColor = SystemColors.WindowFrame;
                    this.buttonOK.ForeColor = SystemColors.Window;
                    this.确定button2.BackColor = SystemColors.WindowFrame;
                    this.确定button2.ForeColor = SystemColors.Window;
                    this.记录关机时间checkBox.BackColor = SystemColors.WindowFrame;
                    this.记录关机时间checkBox.ForeColor = SystemColors.Window;
                    this.隐藏主窗口ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.隐藏主窗口ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.隐藏右下角图标ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.隐藏右下角图标ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.窗口置顶ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.窗口置顶ToolStripMenuItem.ForeColor = SystemColors.Window;
                    this.取消关机ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.取消关机ToolStripMenuItem.ForeColor = SystemColors.Menu;
                    this.取消休眠睡眠ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.取消休眠睡眠ToolStripMenuItem.ForeColor = SystemColors.Menu;
                    this.全部取消ToolStripMenuItem.BackColor = SystemColors.WindowFrame;
                    this.全部取消ToolStripMenuItem.ForeColor = SystemColors.Menu;
                    this.暗黑模式ToolStripMenuItem.Text = "关闭暗黑模式";
                }
                else //关闭暗黑模式
                {
                    this.BackColor = SystemColors.Control;
                    this.ForeColor = SystemColors.ControlText;
                    this.menuStrip.BackColor = SystemColors.Control;
                    this.插入ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.插入开机时间ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.插入开机时间ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.插入关机时间ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.插入关机时间ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.管理器ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.立刻取消ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.拓展功能ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.comboBoxTime.BackColor = SystemColors.Window;
                    this.comboBoxTime.ForeColor = SystemColors.WindowText;
                    this.comboBoxMode.BackColor = SystemColors.Window;
                    this.comboBoxMode.ForeColor = SystemColors.WindowText;
                    this.注册关机事件ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.注册关机事件ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.销毁关机事件ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.销毁关机事件ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.打开启动文件夹ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.打开启动文件夹ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.安全模式ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.安全模式ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.升级日志ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.升级日志ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.源头管理ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.源头管理ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.暗黑模式ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.暗黑模式ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.切断数据库连接ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.切断数据库连接ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.外链数据库ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.外链数据库ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.缓存管理ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.缓存管理ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.获得管理员权限ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.获得管理员权限ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.禁止一次开机记时间ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.禁止一次开机记时间ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.配置管理ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.配置管理ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.附加功能ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.附加功能ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.toolStripComboBox不透明度.BackColor = SystemColors.Window;
                    this.toolStripComboBox不透明度.ForeColor = SystemColors.WindowText;
                    this.任务栏隐匿ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.任务栏隐匿ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.退出ToolStripMenuItem1.BackColor = SystemColors.Control;
                    this.退出ToolStripMenuItem1.ForeColor = SystemColors.ControlText;
                    this.label指定时间.BackColor = SystemColors.Control;
                    this.dateTimePicker1.CalendarMonthBackground = SystemColors.Window;
                    this.buttonOK.BackColor = SystemColors.Control;
                    this.buttonOK.ForeColor = SystemColors.ControlText;
                    this.确定button2.BackColor = SystemColors.Control;
                    this.确定button2.ForeColor = SystemColors.ControlText;
                    this.记录关机时间checkBox.BackColor = SystemColors.Control;
                    this.记录关机时间checkBox.ForeColor = SystemColors.ControlText;
                    this.隐藏主窗口ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.隐藏主窗口ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.隐藏右下角图标ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.隐藏右下角图标ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.窗口置顶ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.窗口置顶ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.取消关机ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.取消关机ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.取消休眠睡眠ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.取消休眠睡眠ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.全部取消ToolStripMenuItem.BackColor = SystemColors.Control;
                    this.全部取消ToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    this.暗黑模式ToolStripMenuItem.Text = "打开暗黑模式";
                }
            }
        }

        private void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DarkMode = !DarkMode; //切换暗黑模式状态
        }

        private void 显示配置文件内容ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ConfigurationForm().ShowDialog();
        }

        private void 窗口置顶ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            窗口置顶 = !窗口置顶;
        }

        private void toolStripComboBox透明度_TextChanged(object sender, EventArgs e)
        {
            float opacity = 100;
            try
            {
                opacity = float.Parse(this.toolStripComboBox不透明度.Text.Replace("%", ""));
            }
            catch
            {
                this.toolStripComboBox不透明度.Text = "100";
            }
            finally
            {
                this.Opacity = opacity / 100;
            }
        }

        private void 隐藏主窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void 任务栏隐匿ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
        }

        private void 隐藏右下角图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.notifyIcon.Visible = false;
        }

        private void ApplicationExit()
        {
            this.updateTitleTimer.Enabled = false;
            this.notifyIcon.Visible = false;
            database.CloseConnection();
            Environment.Exit(0);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationExit();
        }
        #endregion

        #region 通知图标
        private void notifyIcon_Click(object sender, EventArgs e)
        {
            MainForm.mForm.Show();
            MainForm.mForm.Activate();
        }
        #endregion

        #region 更新时间后台线程
        private void FlushTitleInEvery10Second()
        {
            this.updateTitleTimer.Interval = 10000;
            this.updateTitleTimer.Start();
        }

        private void updateTitleTimer_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(0, this.Text.IndexOf(' ', 5)) + " " + DateTime.Now.ToString("HH:mm:ss");
        }
        #endregion

        #region 外部类调用模块
        /// <summary>
        /// 以管理管身份重新启动本程序
        /// </summary>
        /// <param name="muteMessage">禁止弹出提示</param>
        public static void restartWithAdminRight(bool muteMessage = false)
        {
            if (!muteMessage && MessageBox.Show("请使用管理员权限重启本程序再进行此操作，是否程序允许获取权限？", "操作失败", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            else
            {
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.UseShellExecute = true;
                proc.WorkingDirectory = Environment.CurrentDirectory;
                proc.FileName = Application.ExecutablePath;
                proc.Arguments = "MessageUnabled";
                proc.Verb = "runas";

                try
                {
                    Process.Start(proc);
                }
                catch
                {
                    // 用户点击不要授权
                    // 提示错误，什么都不要做
                    MessageBox.Show("获得权限失败", "用户未允许授权", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MainForm.mForm.ApplicationExit();
            }
        }

        public bool 需要重复开启软件检查 { get; set; } = true;

        public static string 当前选择模式 { get { return MainForm.mForm.comboBoxMode.Text; } }

        public static bool 窗口置顶 { get { return MainForm.mForm.TopMost; } set { MainForm.mForm.TopMost = value; mForm.窗口置顶ToolStripMenuItem.Text = (value ? "撤销" : "开启") + "窗口置顶"; } }

        /// <summary>
        /// 重新显示主窗口
        /// </summary>
        public static void Appear()
        {
            if (mForm == null)
                new MainForm().Show();
            else
                mForm.Show();
        }

        public static void CheckSaftyModeSanity()
        {
            if (mForm.database.ConnectionState != System.Data.ConnectionState.Closed)
                mForm.安全模式ToolStripMenuItem.Enabled = false;
        }

        #endregion

        #region 安全模式模块
        /// <summary>
        /// 是否脱离数据库连接（安全模式是否打开）
        /// </summary>
        public static bool DatabaseOffline
        {
            get => mForm.databaseOffline;
            set { mForm.Enable安全模式(value); }
        }
        /// <summary>
        /// 安全模式状态（请勿直接对此变量进行操作，请使用MainForm.DatabaseOffline）
        /// </summary>
        private bool databaseOffline { get; set; } = false;

        private void Enable安全模式(bool enable = true)
        {
            // 不必要的操作就提前返回，提高代码性能
            if (this.记录关机时间checkBox.Enabled == !enable)
                return;

            databaseOffline = enable;
            this.记录关机时间checkBox.Checked = !enable;
            this.记录关机时间checkBox.Enabled = !enable;
            this.安全模式ToolStripMenuItem.Text = enable ? "关闭安全模式" : "启动安全模式";
        }
        #endregion
    }
}