using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 关机小程序
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();
        }

        private void button返回_Click(object sender, EventArgs e)
        {
            Form1.getForm().Show();
            this.Close();
        }

        private void FormHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.getForm().Show();
        }
    }
}
