using System;
using System.Windows.Forms;

namespace 关机助手
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void button返回_Click(object sender, EventArgs e)
        {
            MainForm.Appear();
            this.Close();
        }

        private void FormHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Appear();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            labelVersions.Text = labelVersions.Text.Replace("{Version}", ProgramLauncher.Version(2));
            this.textBoxUpdateLog.Text = this.textBoxUpdateLog.Text.Replace("{Version}", ProgramLauncher.Version(2));
            this.labelCopyright.Text = this.labelCopyright.Text.Replace("{Year}", DateTime.Now.Year.ToString());
        }
    }
}
