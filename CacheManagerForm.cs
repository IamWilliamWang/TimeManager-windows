using System;
using System.Collections.Generic;
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
        private int CacheTextLength { get; set; } = 0;
        #region 加载窗口事件
        public CacheManagerForm()
        {
            InitializeComponent();
        }

        private void CacheManagerForm_Load(object sender, EventArgs e)
        {
            if (MainForm.DatabaseOffline)
                this.buttonClearCache.Enabled = false;
            string[] allLines = CacheUtil.GetAllLines();
            if (allLines == null)
            {
                this.Text += "（未找到缓存文件）";
                return;
            }
            this.textBox.Lines = allLines;
            this.CacheTextLength = this.textBox.Text.Replace("\r", "").Replace("\n", "").Length;
        }

        private bool CacheChanged { get { return this.CacheTextLength != this.textBox.Text.Replace("\r", "").Replace("\n", "").Length; } }

        private void CacheManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CacheChanged)
                if (DialogResult.Yes == MessageBox.Show("检测到有未保存的内容，是否对缓存内容进行保存？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information)) 
                    UpdateCache();
        }
        #endregion

        #region 窗体双击事件
        private void CacheManagerForm_DoubleClick(object sender, EventArgs e)
        {
            this.buttonOpenFile_Click(sender, e);
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
            if (CacheUtil.ExistCache() == false)
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
            if (CacheUtil.ExistCache() == false)
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
        private void buttonOpenFile_Click(object sender, EventArgs e)
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
            UpdateCache();
            MessageBox.Show("保存成功！");
        }

        private void UpdateCache()
        {
            if (this.textBox.Text == "")
                File.Delete(cache);
            CacheUtil.SetAllLines(this.textBox.Lines);
            this.CacheTextLength = this.textBox.Text.Replace("\r", "").Replace("\n", "").Length;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            UpdateCache();
            int effectRows = new DatabaseAgency().ClearCache();
            if (effectRows == -1)
                MessageBox.Show("无需提交，因为没有缓存文件。");
            else if (effectRows == 0)
                MessageBox.Show("无需提交，因为缓存为空。", "提示");
            else
                MessageBox.Show("操作成功，已经清除缓存并提交到数据库中。", "提示");
            this.Close();
        }
        #endregion

        #region 缓存合并
        private class SqlItem
        {
            public string SqlString { get; set; }
            public string SqlTime {
                get
                {
                    int from = SqlString.IndexOf('\'') + 1;
                    int to = SqlString.IndexOf('\'', from);
                    return SqlString.Substring(from, to - from);
                }
            }
            public SqlItem(string sqlString)
            {
                this.SqlString = sqlString;
            }
        }
        private string[] SortStringsByTime(string[] cacheLines)
        {
            List<SqlItem> list = new List<SqlItem>();
            foreach (string cache in cacheLines)
                list.Add(new SqlItem(cache));
            list.Sort((item1, item2) => item1.SqlTime.CompareTo(item2.SqlTime));
            List<string> result = new List<string>();
            foreach (SqlItem item in list)
                result.Add(item.SqlString);
            return result.ToArray();
        }

        private string[] ListAdd(string[] list1, string[] list2)
        {
            // 防错处理
            if (list1 == null && list2 == null)
                return new string[0];
            else if (list2 == null)
                return list1;
            else if (list1 == null)
                return list2;

            var result = new List<string>(list1);
            result.AddRange(list2);
            return result.ToArray();
        }

        private void button合并_Click(object sender, EventArgs e)
        {
            String[] 源内容 = null;
            String[] 目标内容 = null;
            try
            {
                源内容 = CacheUtil.GetAllLines(this.textBox源.Text);
                目标内容 = CacheUtil.GetAllLines(this.textBox目标.Text);
            }
            catch
            {
                if (CacheUtil.ExistCache(this.textBox目标.Text) == false
                    && CacheUtil.ExistCache(this.textBox源.Text))
                    目标内容 = null;
                else
                {
                    MessageBox.Show("源文件名有误，无法进行合并");
                    return;
                }
            }
            string[] resultLines = SortStringsByTime(ListAdd(源内容, 目标内容));
            File.Delete(this.textBox目标.Text);
            CacheUtil.SetAllLines(resultLines, this.textBox目标.Text);
            File.Delete(this.textBox源.Text);
            this.CacheManagerForm_Load(sender, e);
            MessageBox.Show("合并成功！");
        }
        #endregion

    }
}
