using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using 关机小程序.Util;
using static 关机小程序.ShutdownUtil;

namespace 关机小程序
{
    public partial class MainForm : Form
    {
        private static Form form1 = null;
        //public static readonly String version="1.4.0";

        public MainForm()
        {
            InitializeComponent();
            saveForm1();

            addSelectOptionsInComboBoxTime();

            this.MouseWheel += new MouseEventHandler(comboBoxTime_MouseWheel);

            addNowTimeToFormTitle();

            flushTitleInEverySecond();
            //if(updateTitleBackgroundWorker.IsBusy == false)
            //    this.updateTitleBackgroundWorker.RunWorkerAsync();
        }

        public MainForm(string fastMode)
        {
            InitializeComponent();
            saveForm1();
            addSelectOptionsInComboBoxTime();
            
            buttonOK_Click(new object(), new EventArgs());//Click the OK button
        }

        private void addSelectOptionsInComboBoxTime()
        {
            for (int i = 0; i <= 60; i += 5)
                this.comboBoxTime.Items.Add(i);
            comboBoxMode.SelectedIndex = 0;
        }

        private void addNowTimeToFormTitle()
        {
            this.Text += " " + DateTime.Now.ToLongTimeString();

        }

        private void flushTitleInEverySecond()
        {
            this.updateTitleTimer.Interval = 10000;
            this.updateTitleTimer.Start();
        }

        private void saveForm1()
        {
            form1 = this;
        }

        public static Form getForm()
        {
            return form1;
        }

        private void comboBoxTime_MouseWheel(object sender, MouseEventArgs e)
        {
            int nowNumber = int.Parse(comboBoxTime.Text);
            if (e.Delta > 0)
                this.comboBoxTime.Text = (nowNumber + 1).ToString();
            else if (e.Delta < 0)
            {
                if (nowNumber == 0)
                    nowNumber += 60;
                this.comboBoxTime.Text = (nowNumber - 1).ToString();
            }
        }        

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.记录关机时间checkBox.Checked)
                SqlExecuter.记录关机事件();

            cancelShutdownCommand();
            try
            {
                switch (this.comboBoxMode.Text)
                {
                    case "关机":
                        float seconds = float.Parse(this.comboBoxTime.Text) * 60;
                        //if (seconds == 0.0) seconds = (float)3;
                        runShutdownCommand(Mode.关机, seconds);
                        //if (seconds == 3.0)
                        //{
                        //    MessageBox.Show("如为误点，请按确定", "调整为3秒后关机。");
                        //    cancelShutdownCommand();
                        //}
                        break;
                    case "重启":
                        runShutdownCommand(Mode.重启, float.Parse(this.comboBoxTime.Text) * 60);
                        break;
                }
            }
            catch (FormatException e1)
            {
                MessageBox.Show("请输入正确的数据！", "错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.comboBoxTime.Text = "0";
                return;
            }
        }

        private int getRestTime_Seconds()
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
            if (this.记录关机时间checkBox.Checked)
                SqlExecuter.记录关机事件();

            int restTime_seconds = getRestTime_Seconds();

            Boolean nextDay = false;
            if (restTime_seconds <= 0)
            {
                restTime_seconds += 24 * 3600;
                nextDay = true;
            }
            ShutdownUtil.cancelShutdownCommand();
            ShutdownUtil.runShutdownCommand(Mode.关机, restTime_seconds);
            MessageBox.Show("将在" + (nextDay ? "明日" : "今日") + this.dateTimePicker1.Value.ToLongTimeString() + "关机", "离关机还剩" + restTime_seconds + "秒", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 现在ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("现在要关机吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            cancelShutdownCommand();
            runShutdownCommand(Mode.关机, 0);
            return;
        }

        private void HalfMinuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cancelShutdownCommand();
            runShutdownCommand(Mode.关机, 30);
            return;
        }

        private void TenMinutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cancelShutdownCommand();
            runShutdownCommand(Mode.关机, 600);
            return;
        }

        private void 自定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                float second = float.Parse(Interaction.InputBox("几分钟呢？", "关机时间选择", "", -1, -1)) * 60;
                runShutdownCommand(Mode.关机, second);
            }
            catch
            {
                MessageBox.Show("输入错误！","错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            cancelShutdownCommand();
            return;
        }

        private void 注册关机事件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            float? minutes = null;
            try {
                minutes = float.Parse(this.comboBoxTime.Text);
            }
            catch
            {
                MessageBox.Show("请输入正确的数据！","错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (minutes == 0)
            {
                MessageBox.Show("选择0分钟是危险行为！已阻止","错误警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                File.WriteAllText(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\autoshutdown.cmd", "shutdown -s -t " + minutes * 60);
            }
            catch
            {
                MessageBox.Show("失败！请使用管理员权限重启本程序","失败警示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("注册成功！开机"+minutes+"分钟后自动关机","成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 销毁关机事件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemCommandUtil.ExcuteCommand("del " + "\"C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp\\autoshutdown.cmd\"");
            MessageBox.Show("销毁成功！","成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripComboBox透明度_TextChanged(object sender, EventArgs e)
        {
            float opacity = 100;
            try {
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
            System.Diagnostics.Process.Start("Explorer.exe", systemStartupFolder());
        }

        private string systemStartupFolder()
        {
            return @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\";
        }

        private void 应用AppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemCommandUtil.ExcuteCommand(copyEXECommand());
            MessageBox.Show("尝试完成","", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private string copyEXECommand()
        {
            return "copy /Y \"F:\\Visual Studio 2015\\关机小程序\\bin\\Debug\\关机小程序.exe\" \"C:\\Users\\william\\Desktop\\关机小程序(0).exe\"";
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new HelpForm().Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.updateTitleTimer.Enabled = false;
            Application.Exit();
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            this.开发者模式contextMenuStrip.Items[0].Enabled = true;
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


        private string contentTime;
        private void updateTitleBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            String nowTime = "";
            Thread updateDataTimeThread = new Thread(new ThreadStart(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    nowTime = DateTime.Now.ToLongTimeString();
                    contentTime = nowTime;

                    //this.updateTitleBackgroundWorker.ReportProgress(i);
                    Thread.Sleep(1000);
                }
            }));

            updateDataTimeThread.IsBackground = true;
            updateDataTimeThread.Start();
        }

        private void updateTitleBackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.Text = this.Text.Substring(0, this.Text.LastIndexOf(' ')) + " " + contentTime;
        }

        private void updateTitleTimer_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(0, this.Text.LastIndexOf(' ')) + " " + DateTime.Now.ToLongTimeString();
            Thread.Sleep(1000);
        }
    }
}
