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
            MainForm.GetMainForm().Show();
            this.Close();

        }

        private void FormHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.GetMainForm().Show();
        }
    }
}
