using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Windows.Forms;
using static 关机小程序.SystemCommand;

namespace 关机小程序
{
    public partial class Form1 : Form
    {
        private static Form form1 = null;
        public static Form getForm()
        {
            return form1;
        }
        public Form1()
        {
            InitializeComponent();
            form1 = this;

            for(int i=0;i<=60;i+=5)
                this.comboBoxTime.Items.Add(i);
            comboBoxMode.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            cancelShutdownCommand();
            switch (this.comboBoxMode.Text)
            {
                case "关机":
                    float seconds = float.Parse(this.comboBoxTime.Text) * 60;
                    if (seconds == 0.0) seconds = (float)3;
                    runShutdownCommand(Mode.关机, seconds);
                    if (seconds == 3.0)
                    {
                        MessageBox.Show("如为误点，请按确定", "调整为3秒后关机。");
                        cancelShutdownCommand();
                    }
                    break;
                case "重启":
                    runShutdownCommand(Mode.重启, float.Parse(this.comboBoxTime.Text) * 60);
                    break;
            }

        }

        private void 确定button2_Click(object sender, EventArgs e)
        {
            int hoursRest;
            int minutesRest;
            int secondsRest;
            int restTime_seconds;
            hoursRest = this.dateTimePicker1.Value.Hour - DateTime.Now.Hour;
            minutesRest = this.dateTimePicker1.Value.Minute - DateTime.Now.Minute;
            secondsRest = this.dateTimePicker1.Value.Second - DateTime.Now.Second;

            restTime_seconds = hoursRest * 3600 + minutesRest * 60 + secondsRest;

            Boolean nextDay = false;
            if (restTime_seconds <= 0)
            {
                restTime_seconds += 24 * 3600;
                nextDay = true;
            }
            SystemCommand.runShutdownCommand(Mode.关机, restTime_seconds);
            MessageBox.Show("将在"+(nextDay? "明日" : "今日")+this.dateTimePicker1.Value.ToLongTimeString()+"关机","离关机还剩"+ restTime_seconds+"秒", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void 现在ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("现在要关机吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            runShutdownCommand(Mode.关机, 1);
            return;
        }

        private void HalfMinuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runShutdownCommand(Mode.关机, 30);
            return;
        }

        private void TenMinutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show("输入错误！");
            }
        }

        private void 现在ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("现在要重启电脑吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            runShutdownCommand(Mode.重启, 0);
            return;
        }

        private void HalfMinuteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            runShutdownCommand(Mode.重启, 30);
            return;
        }

        private void TenMinutesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            runShutdownCommand(Mode.重启, 600);
            return;
        }

        private void 自定义ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try {
                float second = float.Parse(Interaction.InputBox("几分钟：", "重启时间选择", "", -1, -1)) * 60;
                runShutdownCommand(Mode.重启, second);
            }
            catch
            {
                MessageBox.Show("输入错误！");
            }
        }

        private void 取消指令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cancelShutdownCommand();
            return;
        }

        private void 注册关机事件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float minutes = float.Parse(this.comboBoxTime.Text);
            if (minutes == 0)
            {
                MessageBox.Show("选择0分钟是危险行为！已阻止");
                return;
            }
            try {
                File.WriteAllText(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\autoshutdown.cmd", "shutdown -s -t " + minutes * 60);
            }
            catch {
                MessageBox.Show("失败！请使用管理员权限重启本程序");
                return;
            }
            MessageBox.Show("注册成功！开机"+minutes+"分钟后自动关机");
        }

        private void 销毁关机事件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            system("del " + "\"C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp\\autoshutdown.cmd\"");
            MessageBox.Show("销毁成功！");
        }

        private void toolStripComboBox透明度_TextChanged(object sender, EventArgs e)
        {
            this.Opacity = float.Parse(this.toolStripComboBox透明度.Text) / 60;
        }

        private void 显现ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
        }

        private void 打开启动文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\");
        }

        private void 应用AppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            system("copy /Y \"F:\\Visual Studio 2015\\关机小程序\\bin\\Debug\\关机小程序.exe\" \"C:\\Users\\william\\Desktop\\关机小程序(0).exe\"");
            MessageBox.Show("尝试完成");
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FormHelp().Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
