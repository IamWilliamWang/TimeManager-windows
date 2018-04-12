using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 关机助手
{
    public partial class ExceptionForm : Form
    {
        Exception mException = null;
        public ExceptionForm(Exception exception)
        {
            InitializeComponent();
            mException = exception;
        }

        private void ExceptionForm_Load(object sender, EventArgs e)
        {
            this.labelInformation.Text = (CutContent(mException.Message));
            this.textBox1.Lines = TextBoxContent();
        }

        private string ForamtInformation(string label)
        {
            String newLabel = "";
            for(int i = 0; i < label.Count(); i++)
            {
                if (i != 0 && i % 50 == 0)
                    newLabel += "\n";
                newLabel += label[i];
            }
            return newLabel;
        }

        private string CutContent(String message)
        {
            int toIndex = message.IndexOf(". ");
            if (toIndex == -1)
                toIndex = message.IndexOf('。');
            if (toIndex != -1) 
                message = message.Substring(0, toIndex + 1);
            return message;
        }

        private String[] TextBoxContent()
        {
            return new String[]
            {
                "有关调用实时(JIT)调试而不是此对话框的详细信息，",
                "请参见此消息的结尾。",
                "",
                "************** 异常文本 **************",
                mException.ToString()
            };
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            Util.LogUtil.Log(this.labelInformation.Text, mException);
            MessageBox.Show("日志写入成功！请将日志发送给开发人员", "发送报告", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.buttonSend.Enabled = false;
            String current = System.IO.Directory.GetCurrentDirectory();
            OpenFolderAndSelectFile(current + "\\"+Util.LogUtil.LogFileName);
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static void Show(Exception exception)
        {
            new ExceptionForm(exception).Show();
        }

        public static void ShowDialog(Exception exception)
        {
            new ExceptionForm(exception).ShowDialog();
        }

        private void OpenFolderAndSelectFile(String fileFullName)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/select," + fileFullName;
            System.Diagnostics.Process.Start(psi);
        }
    }
}
