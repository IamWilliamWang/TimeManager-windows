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
        private String cacheName { get { return Cache.CacheFilename; } }
        private BackupCreater backup;
        private enum OutputState { ORIGINAL, TRADITIONAL, MODERN};
        private OutputState opState = OutputState.MODERN;

        #region 控制器
        /// <summary>
        /// 储存一条sql语句。是数据类型
        /// </summary>
        private class SqlItem
        {
            public readonly bool 记录开机;
            /// <summary>
            /// 整条sql语句
            /// </summary>
            public string SqlString { get; set; }
            /// <summary>
            /// sql语句中包含时间的部分
            /// </summary>
            public string SqlTime
            {
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
                this.记录开机 = this.SqlString.StartsWith("INSERT", true, System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// 将显示的一条转换成可以直接执行的sql字符串
        /// </summary>
        /// <param name="displayedItem">显示在屏幕上的单行文字</param>
        /// <returns></returns>
        private SqlItem GetSqlItemFromDisplayedString(string displayedItem)
        {
            ///直观模式的每条格式
            ///开机时间：2019-12-26 21:31:16.410
            ///关机时间： -- 2019-12-26 21:31:16.410
            if (opState == OutputState.MODERN)
            {
                if (displayedItem.Contains(" -- ") == false) //开机
                {
                    string time = displayedItem;
                    string sqlString = "INSERT INTO [Table](开机时间) VALUES ('@time')".Replace("@time", time);
                    SqlItem item = new SqlItem(sqlString);
                    return item;
                }
                else // 关机
                {
                    string time = displayedItem.Replace(" -- ", "");
                    string sqlString = "UPDATE [Table] SET 关机时间 = '@time', 时长 = '@time' - 开机时间 WHERE 序号 in (SELECT MAX(序号) FROM[Table]) "
                        .Replace("@time", time);
                    SqlItem item = new SqlItem(sqlString);
                    return item;
                }
            }
            ///经典模式的每条格式
            ///开机时间：2019-12-26 21:31:16.410
            ///关机时间：2019-12-26 21:31:16.410
            else if (opState == OutputState.TRADITIONAL)
            {
                if (displayedItem.Contains("开机"))
                {
                    int index = displayedItem.IndexOf('：');
                    if (index == -1)
                        throw new Exception("有条目不包含：");
                    string time = displayedItem.Substring(index + 1);
                    string sqlString = "INSERT INTO [Table](开机时间) VALUES ('@time')".Replace("@time", time);
                    SqlItem item = new SqlItem(sqlString);
                    return item;
                }
                else if (displayedItem.Contains("关机"))
                {
                    int index = displayedItem.IndexOf('：');
                    if (index == -1)
                        throw new Exception("有条目不包含：");
                    string time = displayedItem.Substring(index + 1);
                    string sqlString = "UPDATE [Table] SET 关机时间 = '@time', 时长 = '@time' - 开机时间 WHERE 序号 in (SELECT MAX(序号) FROM[Table]) "
                        .Replace("@time", time);
                    SqlItem item = new SqlItem(sqlString);
                    return item;
                }
                else
                    throw new ArgumentException("转换字符串无效！");
            }
            else if (opState == OutputState.ORIGINAL)
                return new SqlItem(displayedItem);
            return null;
        }

        /// <summary>
        /// 将直接执行的sql字符串转换成一条显示数据
        /// </summary>
        /// <param name="raw_sql">能直接执行的一句sql语句</param>
        /// <returns></returns>
        private string GetDisplayedStringFromSqlString(string raw_sql)
        {
            ///直观模式的每条格式
            ///开机时间：2019-12-26 21:31:16.410
            ///关机时间： -- 2019-12-26 21:31:16.410
            if (opState == OutputState.MODERN)
            {
                SqlItem sqlItem = new SqlItem(raw_sql);
                if (sqlItem.记录开机)
                    return sqlItem.SqlTime;
                else
                    return " -- " + sqlItem.SqlTime;
            }
            ///经典模式的每条格式
            ///开机时间：2019-12-26 21:31:16.410
            ///关机时间：2019-12-26 21:31:16.410
            else if (opState == OutputState.TRADITIONAL)
            {
                SqlItem sqlItem = new SqlItem(raw_sql);
                return (sqlItem.记录开机 ? "开机时间：" : "关机时间：") + sqlItem.SqlTime;
            }
            else if (opState == OutputState.ORIGINAL)
                return raw_sql;
            return null;
        }

        /// <summary>
        /// 批量转换sql语句到输出显示条目
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        private List<String> ConvertSqls2Strings(String[] sqls)
        {
            List<String> result = new List<String>();
            foreach (var sqlStr in sqls)
                result.Add(GetDisplayedStringFromSqlString(sqlStr));
            return result;
        }

        /// <summary>
        /// 批量转换输出显示条目到sql语句
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        private List<String> ConvertStrings2Sqls(String[] strings)
        {
            List<String> result = new List<String>();
            foreach (var dispStr in strings)
            {
                if (dispStr == "")
                    continue;
                SqlItem sqlItem = GetSqlItemFromDisplayedString(dispStr);
                result.Add(sqlItem.SqlString);
            }
            return result;
        }

        /// <summary>
        /// 获取显示的所有Sql语句，设置需要显示的Sql语句。（控制器）
        /// </summary>
        public string CacheText { get {
                // 获取CacheTextLines并连接成Text格式
                StringBuilder originalSqls = new StringBuilder();
                foreach (var sql in CacheTextLines)
                    originalSqls.AppendLine(sql);
                return originalSqls.ToString();
            } set {
                // 分割成string[]格式并传递给CacheTextLines
                CacheTextLines = value.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            } }

        /// <summary>
        /// 获取显示的所有Sql语句，设置需要显示的Sql语句。（控制器）
        /// </summary>
        public string[] CacheTextLines { get { // 获取，转换，返回
                string text = this.textBoxCache.Text;
                if (opState == OutputState.MODERN) 
                    text = text.Replace(" -- ", "\r\n -- "); // 关机记录移到开机记录下面。使得一行只包含一条数据
                string[] displayedContentLines = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                List<String> originalSqlLines = ConvertStrings2Sqls(displayedContentLines);
                return originalSqlLines.ToArray();
            } set { // 显示内容
                // 转换，显示
                string[] sqlStrings = value;
                List<String> displayedList = ConvertSqls2Strings(sqlStrings); // 最后要显示的内容
                if (opState == OutputState.MODERN)
                {
                    StringBuilder displayStr = new StringBuilder();
                    foreach (String str in displayedList)
                        displayStr.AppendLine(str);
                    displayStr.Replace("\r\n -- ", " -- "); // 关机记录移到开机记录后面。使得一行包含多条数据
                    this.textBoxCache.Text = displayStr.ToString();
                    return;
                }
                this.textBoxCache.Lines = displayedList.ToArray();
            } }
        #endregion

        #region 加载与关闭窗口
        public CacheManagerForm()
        {
            InitializeComponent();
            backup = new BackupCreater(cacheName, interval: 30000, 备份后缀名: ".autobackup", hideBackup: true);
        }

        /// <summary>
        /// 显示在屏幕上有多少行字
        /// </summary>
        private int ShowedTextLines
        {
            get
            {
                double 每行文字实际高度 = 16.65;
                return (int)(this.textBoxCache.ClientSize.Height / 每行文字实际高度);
            }
        }

        private void LoadData()
        {
            string[] allLines = Cache.GetAllLines();
            if (allLines == null)
            {
                this.Text = "缓存管理器（未找到缓存文件）";
                return;
            }
            CacheTextLines = allLines;
        }

        private void AutoScrollBar()
        {
            int lineCount = this.textBoxCache.GetLineFromCharIndex(this.CacheText.Length) + 1;
            // 当总行数大于显示，显示ScrollBar
            if (this.textBoxCache.ScrollBars == ScrollBars.None && lineCount > ShowedTextLines) // 提高执行效率
                this.textBoxCache.ScrollBars = ScrollBars.Vertical;
            // 当总行数小于显示，隐藏ScrollBar
            if (this.textBoxCache.ScrollBars == ScrollBars.Vertical && lineCount < ShowedTextLines) // 提高执行效率
                this.textBoxCache.ScrollBars = ScrollBars.None;
        }

        private void CacheManagerForm_Load(object sender, EventArgs e)
        {
            if (MainForm.DatabaseOffline)
                this.buttonClearCache.Enabled = false;
            else
                backup.Start();
            if (File.Exists(Cache.Backup.Backup文件名))
            {
                if (DialogResult.Yes == MessageBox.Show("检测到上次执行缓存时程序崩溃，是否恢复原来的缓存文件？", "程序崩溃后的自动恢复", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    Cache.Backup.RestoreFile(deleteBackupFile: true);
            }
            LoadData();
            AutoScrollBar();
            if (ConfigManager.CacheManagerConfigLoaded)
            {
                this.textBox源.Text = ConfigManager.CacheManagerFromPath;
                this.textBox目标.Text = ConfigManager.CacheManagerToPath;
                if (ConfigManager.CacheManagerAutoMerge)
                    this.button合并_Click(sender, e);
            }
            this.储存的缓存内容 = this.CacheText;
        }

        private string 储存的缓存内容 { get; set; }
        private void CacheManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.CacheText != this.储存的缓存内容) 
            {
                var result = MessageBox.Show("检测到有未保存的内容，是否对缓存内容进行保存？", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (DialogResult.Yes == result)
                    UpdateCache();
                else if(DialogResult.Cancel == result)
                    e.Cancel = true;
            }
        }

        private void CacheManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            backup.Stop();
            backup.DeleteBackup();
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
            dragFilename = dragFilename.Substring(0, dragFilename.LastIndexOf('\\') + 1) + cacheName;
            this.textBox源.Text = dragFilename;
        }

        private void textBox目标_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
                return;
            string dragFilename = files[0];
            dragFilename = dragFilename.Substring(0, dragFilename.LastIndexOf('\\') + 1) + cacheName;
            this.textBox目标.Text = dragFilename;
        }
        #endregion

        #region 菜单栏
        private void 删除缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("删除文件操作不可恢复，是否继续？", "删除警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;
            File.Delete(cacheName);
            MessageBox.Show("已删除缓存文件！");
            this.Close();
        }

        private void 移动缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Cache.ExistCache() == false)
            {
                MessageBox.Show("不存在缓存文件，移动文件失败");
                return;
            }
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                DefaultExt = ".cache",
                FileName = cacheName,
                Filter = "缓存文件|TimeDatabase.cache",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Title = "移动文件",
                CheckFileExists = false
            };
            fileDialog.ShowDialog();
            if (fileDialog.FileName == cacheName)
                return;
            File.Delete(fileDialog.FileName);
            File.Move(cacheName, fileDialog.FileName);
        }

        private void 另存为缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Cache.ExistCache() == false)
            {
                MessageBox.Show("不存在缓存文件，另存为失败");
                return;
            }
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                DefaultExt = ".cache",
                FileName = cacheName,
                Filter = "缓存文件|" + cacheName,
                InitialDirectory = Directory.GetCurrentDirectory(),
                Title = "另存为",
                CheckFileExists = false
            };
            fileDialog.ShowDialog();
            if (fileDialog.FileName == cacheName)
                return;
            using (StreamWriter writer = new StreamWriter(fileDialog.FileName, false))
            using (StreamReader reader = new StreamReader(cacheName))
                writer.Write(reader.ReadToEnd());
        }

        private void 插入开机缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cache.AppendCache("INSERT INTO [Table](开机时间) VALUES (GETDATE())"
                .Replace("GETDATE()", "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'"), false);
            this.Close();
        }

        private void 插入关机缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cache.AppendCache("UPDATE [Table] SET 关机时间 = GETDATE(), 时长 = GETDATE() - 开机时间 WHERE 序号 in (SELECT MAX(序号) FROM[Table]) "
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
            CacheTextLines = Cache.GetAllLines();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            UpdateCache();
            MessageBox.Show("保存成功！");
        }

        private void UpdateCache()
        {
            if (this.CacheText == "")
                File.Delete(cacheName);
            Cache.SetAllLines(CacheTextLines);
            this.储存的缓存内容 = this.CacheText;
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

        /// <summary>
        /// 两个string[]相加，null参数容错
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
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
                源内容 = Cache.GetAllLines(this.textBox源.Text);
                目标内容 = Cache.GetAllLines(this.textBox目标.Text);
            }
            catch
            {
                if (Cache.ExistCache(this.textBox目标.Text) == false
                    && Cache.ExistCache(this.textBox源.Text))
                    目标内容 = null;
                else
                {
                    MessageBox.Show("源文件名有误，无法进行合并");
                    return;
                }
            }
            string[] resultLines = SortStringsByTime(ListAdd(源内容, 目标内容));
            File.Delete(this.textBox目标.Text);
            Cache.SetAllLines(resultLines, this.textBox目标.Text);
            if (File.Exists(this.textBox源.Text))
                File.Delete(this.textBox源.Text);
            this.LoadData();
            MessageBox.Show("合并成功！");
        }
        #endregion

        #region 模式切换
        private void 直观模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (opState != OutputState.MODERN)
                opState = OutputState.MODERN;
            LoadData();
        }

        private void 经典模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (opState != OutputState.TRADITIONAL)
                opState = OutputState.TRADITIONAL;
            LoadData();
        }

        private void 原始模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (opState != OutputState.ORIGINAL)
                opState = OutputState.ORIGINAL;
            LoadData();
        }
        #endregion
        
        private void CacheManagerForm_Resize(object sender, EventArgs e)
        {
            AutoScrollBar();
        }
    }
}
