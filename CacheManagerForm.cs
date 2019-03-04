using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using 关机助手.Util;

namespace 关机助手
{
    public partial class CacheManagerForm : Form
    {
        public CacheManagerForm()
        {
            InitializeComponent();
        }

        private void CacheManagerForm_Load(object sender, EventArgs e)
        {
            if (MainForm.databaseOffline)
                this.buttonClearCache.Enabled = false;
            if (File.Exists(CacheUtil.CacheFilename) == false)
            {
                MessageBox.Show("缓存文件不存在。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.textBox.Lines = CacheUtil.GetAllLines();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Process notepadProcess = new Process();
            notepadProcess.StartInfo.FileName = "notepad.exe";
            notepadProcess.StartInfo.Arguments = "\"" + new FileInfo("TimeDatabase.cache").FullName + "\"";
            notepadProcess.Start();
            notepadProcess.WaitForExit();
            this.textBox.Lines = CacheUtil.GetAllLines();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            CacheUtil.SetAllLines(this.textBox.Lines);
            MessageBox.Show("保存成功！");
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (CacheUtil.CleanDbAndExecuteTasks() == 0) 
                MessageBox.Show("操作失败。可能原因：缓存文件异常，或者缓存为空。", "提示");
            else
                MessageBox.Show("操作成功，已经清除缓存并提交到数据库中。", "提示");

            this.Close();
        }

        private void CacheManagerForm_DoubleClick(object sender, EventArgs e)
        {
            this.buttonEdit_Click(sender, e);
        }
    }
}
