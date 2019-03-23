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
        // 缓存文件名
        private String cache { get { return CacheUtil.CacheFilename; } }
        #region 加载窗口事件
        public CacheManagerForm()
        {
            InitializeComponent();
        }

        private void CacheManagerForm_Load(object sender, EventArgs e)
        {
            if (MainForm.DatabaseOffline)
                this.buttonClearCache.Enabled = false;
            if (File.Exists(CacheUtil.CacheFilename) == false)
            {
                this.Text += "（未找到缓存文件）";
                return;
            }
            this.textBox.Lines = CacheUtil.GetAllLines();
        }
        #endregion

        #region 窗体双击事件
        private void CacheManagerForm_DoubleClick(object sender, EventArgs e)
        {
            this.buttonEdit_Click(sender, e);
        }
        #endregion

        #region 拖拽操作
        private void textBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void textBox源_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
                return;
            string dragFilename = files[0];
            dragFilename = dragFilename.Substring(0, dragFilename.LastIndexOf('\\') + 1) + cache;
            this.textBox源.Text = dragFilename;
        }

        private void textBox目标_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
                return;
            string dragFilename = files[0];
            dragFilename = dragFilename.Substring(0, dragFilename.LastIndexOf('\\') + 1) + cache;
            this.textBox目标.Text = dragFilename;
        }
        #endregion

        #region 菜单栏
        private void 删除缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("删除文件操作不可恢复，是否继续？", "删除警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;
            File.Delete(cache);
            MessageBox.Show("已删除缓存文件！");
        }

        private void 移动缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(cache) == false)
            {
                MessageBox.Show("不存在缓存文件，移动文件失败");
                return;
            }
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                DefaultExt = ".cache",
                FileName = cache,
                Filter = "缓存文件|TimeDatabase.cache",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Title = "移动文件",
                CheckFileExists = false
            };
            fileDialog.ShowDialog();
            if (fileDialog.FileName == cache)
                return;
            File.Delete(fileDialog.FileName);
            File.Move(cache, fileDialog.FileName);
        }

        private void 另存为缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(cache) == false)
            {
                MessageBox.Show("不存在缓存文件，另存为失败");
                return;
            }
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                DefaultExt = ".cache",
                FileName = cache,
                Filter = "缓存文件|" + cache,
                InitialDirectory = Directory.GetCurrentDirectory(),
                Title = "另存为",
                CheckFileExists = false
            };
            fileDialog.ShowDialog();
            if (fileDialog.FileName == cache)
                return;
            using (StreamWriter writer = new StreamWriter(fileDialog.FileName, false))
            using (StreamReader reader = new StreamReader(cache))
                writer.Write(reader.ReadToEnd());
        }

        private void 插入开机缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CacheUtil.AppendCache("INSERT INTO [Table](开机时间) VALUES (GETDATE())"
                .Replace("GETDATE()", "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'"), false);
            this.Close();
        }

        private void 插入关机缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CacheUtil.AppendCache("UPDATE [Table] SET 关机时间 = GETDATE(), 时长 = GETDATE() - 开机时间 WHERE 序号 in (SELECT MAX(序号) FROM[Table]) "
                .Replace("GETDATE()", "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'"), false);
            this.Close();
        }
        #endregion

        #region 缓存编辑
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
            var allLines = CacheUtil.GetAllLines(cache);
            if (allLines == null || allLines.Length == 0)
            {
                MessageBox.Show("成功！但是缓存文件为空或者没有缓存文件");
                File.Delete(cache);
                this.Close();
            }
            if (CacheUtil.CleanDbAndExecuteTasks() == 0)
                MessageBox.Show("操作失败。可能原因：缓存文件异常，或者缓存为空。", "提示");
            else
                MessageBox.Show("操作成功，已经清除缓存并提交到数据库中。", "提示");
            
            this.Close();
        }
        #endregion
        
        #region 缓存合并
        private void button合并_Click(object sender, EventArgs e)
        {
            String 源内容 = null;
            String 目标内容 = null;
            try
            {
                源内容 = File.ReadAllText(this.textBox源.Text);
                目标内容 = File.ReadAllText(this.textBox目标.Text);
            }
            catch
            {
                if (File.Exists(this.textBox目标.Text) == false
                    && File.Exists(this.textBox源.Text))
                    目标内容 = "";
                else
                {
                    MessageBox.Show("源文件名有误，无法进行合并");
                    return;
                }
            }
            int 插入index = 目标内容.LastIndexOf(CacheUtil.CacheSpliter, 目标内容.Length - 2) + 1;
            StringBuilder stringBuilder = new StringBuilder(目标内容);
            stringBuilder.Insert(插入index, 源内容);
            //注释掉的方法会报未授权Exception，原因是文件是隐藏的
            //using (FileStream file = new FileStream(this.textBox目标.Text, FileMode.Create))
            //using (StreamWriter writer = new StreamWriter(file))
            //    writer.Write(stringBuilder);
            File.Delete(this.textBox目标.Text);
            File.WriteAllText(this.textBox目标.Text, stringBuilder.ToString());
            File.SetAttributes(this.textBox目标.Text, FileAttributes.Hidden);
            File.Delete(this.textBox源.Text);
            this.CacheManagerForm_Load(sender, e);
            MessageBox.Show("合并成功！");
        }
        #endregion
    }
}
