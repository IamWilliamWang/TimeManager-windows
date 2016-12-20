using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 关机小程序
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            for(int i=0;i<=60;i+=5)
                this.comboBoxTime.Items.Add(i);
            comboBoxMode.SelectedIndex = 0;
            
        }

        static String system(String command)
        {
            Process process = new Process();

            process.StartInfo.FileName = "cmd";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            process.StandardInput.WriteLine(command);
            process.StandardInput.WriteLine("exit");

            return process.StandardOutput.ReadToEnd();
        }

        private void runShutdownCommand(Mode mode)
        {
            if (mode == Mode.取消)
                runCommand(Mode.取消, -1);
        }

        private void runCommand(Mode mode, float seconds)
        {
            runCommand(mode, (int)seconds);
        }

        private void runCommand(Mode mode, int seconds)
        {
            String command = "shutdown ";
            switch (mode)
            {
                case Mode.关机:
                    command += "-s -t " + seconds;
                    break;
                case Mode.重启:
                    command += "-g -t " + seconds;
                    break;
                case Mode.取消:
                    command += "-a";
                    break;
                default:
                    MessageBox.Show("Error!");
                    return;
            }
            system(command);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            switch (this.comboBoxMode.Text)
            {
                case "关机":
                    runCommand(Mode.关机, float.Parse(this.comboBoxTime.Text) * 60);
                    break;
                case "重启":
                    runCommand(Mode.重启, float.Parse(this.comboBoxTime.Text) * 60);
                    break;
            }

        }
       
        private void 现在ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("现在要关机吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            runCommand(Mode.关机, 0);
            return;
        }

        private void HalfMinuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runCommand(Mode.关机, 30);
            return;
        }

        private void TenMinutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runCommand(Mode.关机, 600);
            return;
        }

        private void 自定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                float second = float.Parse(Interaction.InputBox("几分钟呢？", "关机时间选择", "", -1, -1)) * 60;
                runCommand(Mode.关机, second);
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
            runCommand(Mode.重启, 0);
            return;
        }

        private void HalfMinuteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            runCommand(Mode.重启, 30);
            return;
        }

        private void TenMinutesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            runCommand(Mode.重启, 600);
            return;
        }

        private void 自定义ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try {
                float second = float.Parse(Interaction.InputBox("几分钟：", "重启时间选择", "", -1, -1)) * 60;
                runCommand(Mode.重启, second);
            }
            catch
            {
                MessageBox.Show("输入错误！");
            }
        }

        private void 取消指令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runShutdownCommand(Mode.取消);
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
            MessageBox.Show("注册成功！");
        }

        private void 销毁关机事件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\autoshutdown.cmd", "");
            }
            catch
            {
                MessageBox.Show("失败！请使用管理员权限重启本程序");
                return;
            }
            system("del " + "\"C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp\\autoshutdown.cmd\"");
            MessageBox.Show("销毁成功！");
        }

        private void toolStripComboBox透明度_TextChanged(object sender, EventArgs e)
        {
            this.Opacity = float.Parse(this.toolStripComboBox透明度.Text) / 60;
        }

        private void 显现ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
        }

        private void 打开启动文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\");
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        enum Mode { 取消, 关机, 重启 };
    }
}
