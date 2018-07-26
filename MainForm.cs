using Microsoft.VisualBasic;
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
        private static Form form = null;
        //public static readonly String version="1.4.0";

        public MainForm()
        {
            InitializeComponent();
            comboBoxMode.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("关机助手");
            if (processes.Length > 1)
            {
                MessageBox.Show("后台已经启动本程序，不能重复启动！", "警告");
                Environment.Exit(0);
            }

            if (!File.Exists(Properties.Resources.MdfFilename))
            {
                BinaryWriterUtil.WriteFileToDisk(
                    GZipUtil.DecompressBytes(Properties.Resources.EmptyDB),
                    Properties.Resources.MdfFilename);

                MessageBox.Show("检测到您第一次使用本软件，请点击数据管理进行初始化操作。", "欢迎！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DatabaseManagerForm.needInitialized = true;
            }

            //this.connectSqlServerBackgroundWorker.RunWorkerAsync();
            SaveMainForm();

            AddSelectOptionsInComboBoxTime();

            this.MouseWheel += new MouseEventHandler(comboBoxTime_MouseWheel);

            AddNowTimeToFormTitle();

            FlushTitleInEverySecond();

            if (SqlServerConnection.ConnectionOpenned())
                this.文件ToolStripMenuItem.Enabled = false;
            //if(updateTitleBackgroundWorker.IsBusy == false)
            //    this.updateTitleBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 此方法已经废弃，建议使用Util.FastModeUtil.ShutdownWithSeconds方法代替
        /// </summary>
        /// <param name="countdownSeconds">关机倒计时（按秒计）</param>
        [Obsolete]
        public MainForm(int countdownSeconds) //Fast mode entry
        {
            FastModeUtil.ShutdownWithSeconds(countdownSeconds);
        }

        private void AddSelectOptionsInComboBoxTime()
        {
            for (int i = 0; i <= 60; i += 5)
                this.comboBoxTime.Items.Add(i);

        }

        private void AddNowTimeToFormTitle()
        {
            this.Text += " " + DateTime.Now.ToLongTimeString();

        }

        private void FlushTitleInEverySecond()
        {
            this.updateTitleTimer.Interval = 10000;
            this.updateTitleTimer.Start();
        }

        private void SaveMainForm()
        {
            form = this;
        }

        public static Form GetMainForm()
        {
            return form;
        }

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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.记录关机时间checkBox.Checked)
                    SqlExecuter.记录关机事件();

                if (this.comboBoxMode.Text != "休眠" && float.Parse(this.comboBoxTime.Text) < 0)
                    return;

                CancelShutdownCommand();
                try
                {
                    switch (this.comboBoxMode.Text)
                    {
                        case "关机":
                            float seconds = float.Parse(this.comboBoxTime.Text) * 60;
                            //if (seconds == 0.0) seconds = (float)3;
                            RunShutdownCommand(Mode.关机, seconds);
                            //if (seconds == 3.0)
                            //{
                            //    MessageBox.Show("如为误点，请按确定", "调整为3秒后关机。");
                            //    cancelShutdownCommand();
                            //}
                            break;
                        case "重启":
                            File.CreateText(@"C:\Users\william\DONOTWRITEDATA").Close();
                            RunShutdownCommand(Mode.重启, float.Parse(this.comboBoxTime.Text) * 60);
                            break;
                        case "休眠":
                            RunSuspendCommand(Mode.休眠);
                            休眠结束();
                            break;
                        case "延缓":
                            FastModeUtil.ShutdownWithMinutes_DelayMode(float.Parse(this.comboBoxTime.Text));
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

        private void 休眠结束()
        {

            new Thread(休眠结束后的工作).Start();
        }

        private void 休眠结束后的工作()
        {
            this.Visible = false;
            Thread.Sleep(10000);
            SqlExecuter.记录开机事件();
            退出ToolStripMenuItem_Click(null, null);
        }

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

        private void button2OK_Click(object sender, EventArgs e)
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

        private void 现在ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("现在要关机吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            CancelShutdownCommand();
            RunShutdownCommand(Mode.关机, 0);
            return;
        }

        private void HalfMinuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelShutdownCommand();
            RunShutdownCommand(Mode.关机, 30);
            return;
        }

        private void TenMinutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelShutdownCommand();
            RunShutdownCommand(Mode.关机, 600);
            return;
        }

        private void 自定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                float second = float.Parse(Interaction.InputBox("几分钟呢？", "关机时间选择", "", -1, -1)) * 60;
                RunShutdownCommand(Mode.关机, second);
            }
            catch
            {
                MessageBox.Show("输入错误！", "错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Obsolete Code
        //private void 现在ToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("现在要重启电脑吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        //        return;
        //    runShutdownCommand(Mode.重启, 0);
        //    return;
        //}

        //private void HalfMinuteToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    runShutdownCommand(Mode.重启, 30);
        //    return;
        //}

        //private void TenMinutesToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    runShutdownCommand(Mode.重启, 600);
        //    return;
        //}

        //private void 自定义ToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        float second = float.Parse(Interaction.InputBox("几分钟：", "重启时间选择", "", -1, -1)) * 60;
        //        runShutdownCommand(Mode.重启, second);
        //    }
        //    catch
        //    {
        //        MessageBox.Show("输入错误！", "错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        #endregion

        private void 取消指令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelShutdownCommand();
            return;
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
                    " -d " + minutes,
                    System.Text.Encoding.ASCII);
            }
            catch (UnauthorizedAccessException)
            {
                //MessageBox.Show("失败！请使用管理员权限重启本程序","失败警示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                restartWithAdminRight();

                return;
            }
            MessageBox.Show("注册成功！开机" + minutes + "分钟后自动关机", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 销毁关机事件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(Properties.Resources.RecorderShellFullFilename, "\"C:\\Users\\william\\sd.exe\" -k", System.Text.Encoding.ASCII);
            }
            catch (UnauthorizedAccessException)
            {
                //MessageBox.Show("失败！请使用管理员权限重启本程序","失败警示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                restartWithAdminRight();
                return;
            }
            MessageBox.Show("销毁成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void 显现ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
        }

        private void 打开启动文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", SystemStartupFolder());
        }

        private string SystemStartupFolder()
        {
            return @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\";
        }

        private void 应用AppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Thread(RunDevProc).Start();

            MessageBox.Show("尝试完成", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Environment.Exit(0);
        }

        private void RunDevProc()
        {
            //File.WriteAllText("update_exefile.cmd", CopyExeFileCommand());
            //Process.Start("cmd.exe", CopyExeFileCommand());
            //SystemCommandUtil.ExcuteCommand(@"F:\Visual Studio 2015\关机小程序\Resources\update_exefile.cmd.lnk");
            File.Copy(Properties.Resources.ExeDevelopFullFilename, "C:\\Users\\william\\Desktop\\关机助手0.exe");

        }

        private string CopyExeFileCommand() =>
            "/c taskkill /f /im 关机助手.exe\r\n"
            + "copy /Y \"" + Properties.Resources.ExeDevelopFullFilename + "\" \"C:\\Users\\william\\Desktop\\关机助手(0).exe\"\r\n"
            + "del \"C:\\Users\\william\\Desktop\\关机助手.exe\"\r\n"
            + "rename \"C:\\Users\\william\\Desktop\\关机助手(0).exe\" 关机助手.exe";


        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new HelpForm().Show();
        }

        bool sizeStrenthed = false;
        private void 更多ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sizeStrenthed == false)
            {
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 9);
                Thread.Sleep(25);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 5);
                Thread.Sleep(25);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 4);
                Thread.Sleep(25);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 3);
                Thread.Sleep(25);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 3);
                Thread.Sleep(25);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 3);
                Thread.Sleep(25);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 3);
                Thread.Sleep(25);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 3);
                Thread.Sleep(25);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 2);
                Thread.Sleep(25);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height + 1);
                this.退出ToolStripMenuItem.Text = "精简功能";
            }
            else
            {
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 9);
                Thread.Sleep(50);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 5);
                Thread.Sleep(50);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 4);
                Thread.Sleep(50);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 3);
                Thread.Sleep(50);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 3);
                Thread.Sleep(50);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 3);
                Thread.Sleep(50);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 3);
                Thread.Sleep(50);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 3);
                Thread.Sleep(50);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 2);
                Thread.Sleep(50);
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 1);
                this.退出ToolStripMenuItem.Text = "拓展功能";
            }
            sizeStrenthed = !sizeStrenthed;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.updateTitleTimer.Enabled = false;
            SqlServerConnection.CloseConnection();
            Application.Exit();
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            this.开发者模式contextMenuStrip.Items[0].Enabled = true;
            this.应用AppToolStripMenuItem_Click(new object(), new EventArgs());
        }

        private void comboBoxTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' || e.KeyChar == '\r')
            {
                buttonOK_Click(sender, e);
                Application.Exit();
            }
            else if (e.KeyChar == 'q')
                this.取消指令ToolStripMenuItem_Click(sender, e);
            else if (e.KeyChar == 27)
                Application.Exit();
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' || e.KeyChar == '\r')
                button2OK_Click(sender, e);
            else if (e.KeyChar == 'q')
                this.取消指令ToolStripMenuItem_Click(sender, e);
            else if (e.KeyChar == 27)
                Application.Exit();
        }

        private void 管理主窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form manager = new DatabaseManagerForm();
            manager.ShowDialog();
        }


        //private string contentTime;
        //private void updateTitleBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        //{
        //    String nowTime = "";
        //    Thread updateDataTimeThread = new Thread(new ThreadStart(() =>
        //    {
        //        for (int i = 1; i <= 100; i++)
        //        {
        //            nowTime = DateTime.Now.ToLongTimeString();
        //            contentTime = nowTime;

        //            //this.updateTitleBackgroundWorker.ReportProgress(i);
        //            Thread.Sleep(1000);
        //        }
        //    }));

        //    updateDataTimeThread.IsBackground = true;
        //    updateDataTimeThread.Start();
        //}

        //private void updateTitleBackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        //{
        //    this.Text = this.Text.Substring(0, this.Text.LastIndexOf(' ')) + " " + contentTime;
        //}

        private void updateTitleTimer_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(0, this.Text.LastIndexOf(' ')) + " " + DateTime.Now.ToLongTimeString();
            //Thread.Sleep(1000);
        }

        private void connectSqlServerBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                TimeUtil.Tik();
                SqlServerConnection.OpenConnection();
                TimeUtil.Tok(writeErrorLog: true);
            }
            catch (Exception e1)
            {
                ExceptionForm.ShowDialog(e1);
            }
            this.connectSqlServerBackgroundWorker.ReportProgress(100);
        }

        private void connectSqlServerBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            数据管理ToolStripMenuItem.Enabled = true;
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.帮助ToolStripMenuItem_Click(new Object(), new EventArgs());
        }

        private void checkSafeBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("关机助手");
            if (processes.Length > 1)
            {
                MessageBox.Show("后台已经启动本程序，不能重复启动！", "警告");
                Application.Exit();
            }

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SqlServerConnection.CloseConnection();
        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    SqlServerConnection.OpenConnection();
            //}
            //catch(Exception e2)
            //{
            //    ExceptionForm.ShowDialog(e2);
            //}
            //this.文件ToolStripMenuItem.Enabled = false;
            //if (SqlExecuter.记录开机事件(DatabaseManagerForm.TableName))
            //{
            //    MessageBox.Show("插入记录成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //SqlServerConnection.ResetConnection();
        }
        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxMode.Text == "休眠")
            {
                this.comboBoxTime.Text = "立即";
                this.comboBoxTime.Enabled = false;
            }
            else
            {
                this.comboBoxTime.Text = "0";
                this.comboBoxTime.Enabled = true;
            }
        }

        public static void restartWithAdminRight()
        {
            if (MessageBox.Show("请使用管理员权限重启本程序再进行此操作，是否程序允许获取权限？", "操作失败", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            else
            {
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.UseShellExecute = true;
                proc.WorkingDirectory = Environment.CurrentDirectory;
                proc.FileName = Application.ExecutablePath;
                proc.Verb = "runas";

                try
                {
                    Process.Start(proc);
                }
                catch
                {
                    // The user refused the elevation.
                    // Do nothing and return directly ...
                    MessageBox.Show("获得权限失败", "用户未允许授权", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Application.Exit();
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
                return;
            string filename = files[0];
            SqlServerConnection.OpenConnection(filename);
            MessageBox.Show("重定向数据库文件成功。","使用新的数据库");
        }

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
    }
}
