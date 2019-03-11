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

namespace 关机助手补丁
{
    public partial class PatchForm : Form
    {
        private readonly String cache = "TimeDatabase.cache";
        public PatchForm()
        {
            InitializeComponent();
        }

        private void button开机_Click(object sender, EventArgs e)
        {
            File.AppendAllText(cache,
                "INSERT INTO [Table](开机时间) VALUES (" + GETDATE() + ")鋝");
            Application.Exit();
        }

        private void button关机_Click(object sender, EventArgs e)
        {
            String nowTime = GETDATE();
            File.AppendAllText(cache,
                "UPDATE [Table] SET 关机时间 = " + nowTime + ", 时长 = "+ nowTime + " - 开机时间 WHERE 序号 in (SELECT MAX(序号) FROM[Table]) 鋝");
            Application.Exit();
        }

        private String GETDATE()
        {
            return "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "notepad.exe";
            process.StartInfo.Arguments = cache;
            process.Start();
        }

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
            string filename = files[0];
            filename = filename.Substring(0, filename.LastIndexOf('\\')+1) + cache;
            this.textBox源.Text = filename;
        }

        private void textBox目标_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
                return;
            string filename = files[0];
            filename = filename.Substring(0, filename.LastIndexOf('\\') + 1) + cache;
            this.textBox目标.Text = filename;
        }

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
                MessageBox.Show("文件名有误，无法进行合并");
                return;
            }
            int 插入index = 目标内容.LastIndexOf('鋝', 目标内容.Length - 2) + 1;
            StringBuilder stringBuilder = new StringBuilder(目标内容);
            stringBuilder.Insert(插入index, 源内容);
            File.Delete(this.textBox目标.Text);
            File.WriteAllText(this.textBox目标.Text, stringBuilder.ToString());
            File.SetAttributes(this.textBox目标.Text, FileAttributes.Hidden);
            File.Delete(this.textBox源.Text);
            MessageBox.Show("成功！");
        }

        private void 删除文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("删除文件操作不可恢复，是否继续？", "删除警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;
            File.Delete(cache);
            MessageBox.Show("已删除缓存文件！");
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 移动文件ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void FormMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
                this.Size = new Size(436, 201);
            else if (e.Delta > 0)
                this.Size = new Size(436, 142);
        }
    }
}
