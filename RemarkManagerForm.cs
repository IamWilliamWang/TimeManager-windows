using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 关机助手.Util;

namespace 关机助手
{
    public partial class RemarkManagerForm : Form
    {
        #region 窗体加载事件
        public RemarkManagerForm()
        {
            InitializeComponent();
        }

        private void RemarkManagerForm_Load(object sender, EventArgs e)
        {
            this.RefreshDatas();
            this.RefreshRemarks();
            DataTable data = (DataTable)this.dataGridViewDatas.DataSource;
            if (data == null)
                return;
            
            this.textBoxId.Text = data.Rows[data.Rows.Count - 1][0].ToString();
        }
        #endregion

        #region RemarkManager帮助函数
        /// <summary>
        /// 如果TextBox有空的，则提示报错，返回True。否则直接返回False
        /// </summary>
        /// <returns></returns>
        private bool AlertEmpty()
        {
            if (this.textBoxContent.Text == "" || this.textBoxId.Text == "")
            {
                MessageBox.Show("请先在上方输入内容！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            return false;
        }
        #endregion

        #region 数据表加载
        /// <summary>
        /// 将普通英文与UTF8混合字串转换成正常字串
        /// </summary>
        /// <param name="混合字符串">英文与UTF8混合字串</param>
        /// <returns></returns>
        private string Hex2ChiEngString(string 混合字符串)
        {
            int 处理结束所在Index = 0;
            int 中文字开始Index, 中文字结束Index;
            StringBuilder result = new StringBuilder(); // 真实字符串
            // 每次循环优先处理英文，然后在处理中文。以此类推
            while (处理结束所在Index < 混合字符串.Length)
            {
                中文字开始Index = 混合字符串.IndexOf("&#x", 处理结束所在Index);
                if (中文字开始Index == -1) // 如果接下来没有UTF8字符了
                    中文字开始Index = 混合字符串.Length; // 使开始Index出界
                if (中文字开始Index != 处理结束所在Index) // 有不用处理的英文字母直接放入原文
                {
                    result.Append(混合字符串.Substring(处理结束所在Index, 中文字开始Index - 处理结束所在Index));
                    处理结束所在Index = 中文字开始Index;
                }
                if (处理结束所在Index < 混合字符串.Length) // 还没处理完所有的字符串
                {
                    中文字结束Index = 混合字符串.IndexOf(";", 中文字开始Index) + 1; // 字符格式是&#x…;
                    // 循环查找连续的中文字符
                    while (true)
                    {
                        // 如果不再是中文字符，就跳出循环
                        if (混合字符串.IndexOf("&#x", 中文字结束Index) != 中文字结束Index)
                            break;
                        // 下个字还是中文就接着查找
                        中文字结束Index = 混合字符串.IndexOf(";", 中文字结束Index) + 1;
                    }
                    if (中文字结束Index == 0)
                        throw new Exception("中文Hex格式错误");
                    // 得到所有中文字符的utf8编码
                    string chinCharacterUtf8编码 = 混合字符串.Substring(中文字开始Index, 中文字结束Index - 中文字开始Index).Replace("&#x", "").Replace(";", "");
                    // 转化为汉字添加进去
                    result.Append(UnicodeSaverUtil.GetChsFromHex(chinCharacterUtf8编码));
                    处理结束所在Index = 中文字结束Index;
                }
            }
            return result.ToString();
        }

        private void RefreshRemarks()
        {
            DataTable result = SqlUtil.Select("[Remark],[Table]",
                "Id 序号,Remark 注释,开机时间 对应开机时间,RemarkTime 注释创建时间",
                "[Table].序号 = [Remark].Id");
            for (int rowIndex = 0; rowIndex < result.Rows.Count; rowIndex++)
            {
                DataRow r = result.Rows[rowIndex];
                for (int columnIndex = 0; columnIndex < r.ItemArray.Count(); columnIndex++)
                {
                    string itemString = r.ItemArray[columnIndex].ToString();
                    if (UnicodeSaverUtil.IsChineseString(itemString))
                    {
                        string transformResult = Hex2ChiEngString(itemString);
                        r[columnIndex] = transformResult;
                    }
                }
            }
            this.tabControl1.SelectedIndex = 0;
            this.dataGridViewRemarks.DataSource = result;
            this.dataGridViewRemarks.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        private void RefreshDatas()
        {
            this.dataGridViewDatas.DataSource = SqlUtil.Select("[Table]", "序号,开机时间,关机时间");
            this.dataGridViewDatas.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        #endregion

        #region 点击事件
        private string Text2Hex(string oldContent)
        {
            string formatedContent = ""; //只对oldContent中的中文部分进行utf8编码，其他部分不予处理
            foreach (char ch in oldContent.ToCharArray())
            {
                if (UnicodeSaverUtil.IsChineseChar(ch) || ch =='\'') //对所有中文或者'进行编码
                {
                    formatedContent += UnicodeSaverUtil.GetHexFromChs(ch);
                }
                else
                    formatedContent += ch;
            }
            return formatedContent;
        }

        private void button提交_Click(object sender, EventArgs e)
        {
            if (AlertEmpty())
                return;

            string formatedContent = Text2Hex(this.textBoxContent.Text);
            if (SqlUtil.Update("[Remark]", "Remark", "'" + formatedContent + "'", "id = " + this.textBoxId.Text)) //这里的update有时候不行，未知bug
            {
                System.Windows.MessageBox.Show("修改已提交到数据库。", "修改成功！", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                this.RefreshRemarks();
            }
        }

        private void button提交添加_Click(object sender, EventArgs e)
        {
            if (AlertEmpty())
                return;

            try {
                if (Util.SqlExecuter.GetMaxId() < int.Parse(this.textBoxId.Text))
                {
                    MessageBox.Show("输入序号不能超过原数据序号的最大值", "序号输入有误");
                    this.textBoxId.Text = "";
                    return;
                }
            }
            catch (FormatException) {
                MessageBox.Show("序号输入有误，请重新确认序号的正确性", "错误");
            }

            string formatedContent = Text2Hex(this.textBoxContent.Text);

            if (SqlUtil.Insert("[Remark]", this.textBoxId.Text + ", '" + formatedContent + "', getdate()", "Id,Remark,RemarkTime")) 
            {
                System.Windows.MessageBox.Show("已提交到数据库。", "修改成功！", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                this.RefreshRemarks();
                //this.dataGridViewRemarks.DataSource = Util.SqlServerConnection.ExecuteQuery("select Id 序号,Remark 注释,RemarkTime 注释创建时间 from [Remark]");
            }
        }
        #endregion

        #region 窗体大小改变事件
        private void RemarkManagerForm_Resize(object sender, EventArgs e)
        {
            this.RefreshRemarks();
        }
        #endregion

        #region HeaderCell修改事件
        private void dataGridViewRemarks_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //刷新界面后要把索引数字显示在HeaderCell（最左边那一列）上
            if (e.Row.Cells[0].Value != null)
            {
                e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
            }
        }
        #endregion
        
    }
}
