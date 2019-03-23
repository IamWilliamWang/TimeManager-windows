using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using 关机助手.Util;
using static 关机助手.Util.ShutdownUtil;

namespace 关机助手
{
    public partial class MainForm : Form
    {
        private static MainForm mForm = null;
        // SqlServer连接代理
        private DatabaseAgency database { get; set; } = new DatabaseAgency();
        
        #region 窗体加载关闭事件
        public MainForm()
        {
            InitializeComponent();
            comboBoxMode.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (this.需要重复开启软件检查)
            {
                // 检测后台是否运行同一程序
                Process[] processes = Process.GetProcessesByName("关机助手"); 
                if (processes.Length > 1)
                {
                    MessageBox.Show("检测到后台已经启动本程序，强烈建议只开启一个本程序，否则可能会导致意外后果。", "警告");
                }
            }

            //检测数据库文件是否存在，不存在则解压缩空数据库
            if (!File.Exists(Properties.Resources.MdfFilename)) 
            {
                BinaryWriterUtil.WriteFileToDisk(
                    GZipUtil.DecompressBytes(Properties.Resources.EmptyDB),
                    Properties.Resources.MdfFilename);

                MessageBox.Show("检测到您第一次使用本软件，请点击数据管理进行初始化操作。", "欢迎！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DatabaseManagerForm.needInitialized = true;
            }

            // 获取版本号并替换标题
            this.Text = this.Text.Replace("{Version}", ProgramLauncher.Version(2));
            // 保存Form
            SaveMainForm();
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


        private void SaveMainForm()
        {
            mForm = this;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            database.CloseConnection();
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
                this.comboBoxTime.Text = ( nowNumber + 1 ).ToString();
            else if (e.Delta < 0)
            {
                if (nowNumber == 0)
                    nowNumber += 60;
                this.comboBoxTime.Text = ( nowNumber - 1 ).ToString();
            }
        }
        #endregion

        #region 主窗体双击
        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            this.应用AppToolStripMenuItem_Click(new object(), new EventArgs());
        }

        private void 应用AppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Thread(RunDevProc).Start();

            MessageBox.Show("尝试完成", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Environment.Exit(0);
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
                Application.Exit();
            }
            else if (e.KeyChar == 'q')
                this.取消关机ToolStripMenuItem_Click(sender, e);
            else if (e.KeyChar == 27)
                Application.Exit();
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' || e.KeyChar == '\r')
                button确认2_Click(sender, e);
            else if (e.KeyChar == 'q')
                this.取消关机ToolStripMenuItem_Click(sender, e);
            else if (e.KeyChar == 27)
                Application.Exit();
        }
        #endregion

        #region 选项改变事件
        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxMode.Text == "重启")
                this.记录关机时间checkBox.Checked = false;
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
            #region 数据管理
        private void 数据管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!MainForm.DatabaseOffline)
                this.安全模式ToolStripMenuItem.Enabled = false;
            new DatabaseManagerForm().ShowDialog();
        }
        #endregion
            #region 取消关机
        private void 取消关机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelShutdownCommand();
            return;
        }
        #endregion
            #region 拓展功能
        bool 拓展功能ButtonClicked { get; set; } = false;
        private void 拓展功能ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (拓展功能ButtonClicked == false)
            {
                int waitMilisecond = 25;
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 10);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 5);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 4);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 4);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 4);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 3);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 2);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 2);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 1);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 1);
                this.拓展功能ToolStripMenuItem.Text = "精简功能";
            }
            else
            {
                int waitMilisecond = 50;
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 10);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 5);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 4);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 4);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 4);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 3);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 2);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 2);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 1);
                Thread.Sleep(waitMilisecond);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 1);
                this.拓展功能ToolStripMenuItem.Text = "拓展功能";
            }
            拓展功能ButtonClicked = !拓展功能ButtonClicked;
        }
        #endregion
        #endregion

        #region 确定键
        private void button确定_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.记录关机时间checkBox.Checked)
                    SqlExecuter.记录关机事件();

                if (float.Parse(this.comboBoxTime.Text) < 0)
                    return;

                if (this.comboBoxMode.Text == "休眠" || this.comboBoxMode.Text == "睡眠") 
                {
                    this.Hide();
                    try
                    {
                        Thread.Sleep((int)(float.Parse(this.comboBoxTime.Text) * 60000));
                    }
                    catch(ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("输入的数字有误，请重新输入！");
                        this.Show();
                        return;
                    }
                }

                CancelShutdownCommand();
                try
                {
                    switch (this.comboBoxMode.Text)
                    {
                        case "关机":
                            float seconds = float.Parse(this.comboBoxTime.Text) * 60;
                            RunShutdownCommand(Mode.关机, seconds);
                            break;
                        case "重启":
	                        if (!this.记录关机时间checkBox.Checked)
	                            File.CreateText(@"C:\Users\"+ProgramLauncher.SystemUserName+@"\DONOTWRITEDATA").Close();
                            RunShutdownCommand(Mode.重启, float.Parse(this.comboBoxTime.Text) * 60);
                            break;
                        case "休眠":
                            RunSuspendCommand(Mode.休眠);
                            再次添加开机记录();
                            break;
                        case "延缓":
                            FastModeUtil.ShutdownWithMinutes_DelayMode(float.Parse(this.comboBoxTime.Text));
                            break;
                        case "睡眠":
                            RunSuspendCommand(Mode.睡眠);
                            再次添加开机记录();
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
        private void 再次添加开机记录()
        {
            new Thread(休眠结束后的工作).Start();
        }

        private void 休眠结束后的工作()
        {
            SqlExecuter.记录开机事件();
            退出ToolStripMenuItem_Click(null, null);
        }
        #endregion

        #region 确定键2
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
            try
            {
                if (this.记录关机时间checkBox.Checked)
                    SqlExecuter.记录关机事件();

                int restTime_seconds = GetRestTime_Seconds();

                Boolean nextDay = false;
                if (restTime_seconds <= 0)
                {
                    restTime_seconds += 24 * 3600;
                    nextDay = true;
                }
                ShutdownUtil.CancelShutdownCommand();
                ShutdownUtil.RunShutdownCommand(Mode.关机, restTime_seconds);
                MessageBox.Show("将在" + ( nextDay ? "明日" : "今日" ) + this.dateTimePicker1.Value.ToLongTimeString() + "关机", "离关机还剩" + restTime_seconds + "秒", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ExceptionForm.ShowDialog(ex);
            }
        }
        #endregion
        
        #region 注册关机倒计时
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
                    " -d " + minutes,
                    System.Text.Encoding.ASCII);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("失败！即将获得管理员权限，请在“你要允许来自未知发布者的此应用对你的设备进行更改吗”点击“是”","警示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                restartWithAdminRight();
                return;
            }
            MessageBox.Show("注册成功！开机" + minutes + "分钟后自动关机", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 销毁关机事件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(Properties.Resources.RecorderShellFullFilename, "\"C:\\Users\\"+ProgramLauncher.SystemUserName+"\\sd.exe\" -k", System.Text.Encoding.ASCII);
            }
            catch (UnauthorizedAccessException)
            {
            	MessageBox.Show("失败！即将获得管理员权限，请在“你要允许来自未知发布者的此应用对你的设备进行更改吗”点击“是”","警示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (DialogResult.Yes == MessageBox.Show("在数据库无法正常使用情况下，推荐使用此项功能。虽然可以保证软件的绝对稳定，但是会损失本系统绝大部分的功能。是否继续？", "离线使用警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
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
        
        private void toolStripComboBox透明度_TextChanged(object sender, EventArgs e)
        {
            float opacity = 100;
            try
            {
                opacity = float.Parse(this.toolStripComboBox透明度.Text);
            }
            catch
            {
                this.toolStripComboBox透明度.Text = "100";
            }
            finally
            {
                this.Opacity = opacity / 100;
            }
        }

        private void 任务栏隐匿ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.updateTitleTimer.Enabled = false;
            database.CloseConnection();
            Application.Exit();
        }
        #endregion

        #region 更新时间线程
        private void FlushTitleInEvery10Second()
        {
            this.updateTitleTimer.Interval = 10000;
            this.updateTitleTimer.Start();
        }

        private void updateTitleTimer_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(0, this.Text.IndexOf(' ',5)) + " " + DateTime.Now.ToString("HH:mm:ss");
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

                Application.Exit();
            }
        }
        
        public bool 需要重复开启软件检查 { get; set; } = true;

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
