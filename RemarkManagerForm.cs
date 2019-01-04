using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 关机助手.Util;

namespace 关机助手
{
    public partial class RemarkManagerForm : Form
    {
        public RemarkManagerForm()
        {
            InitializeComponent();
        }

        private void RemarkManagerForm_Load(object sender, EventArgs e)
        {
            this.dataGridViewDatas.DataSource = SqlUtil.Select("[Table]", "序号,开机时间,关机时间");
            DataTable result = SqlUtil.Select("[Remark],[Table]", 
                "Id 序号,Remark 注释,开机时间 对应开机时间,RemarkTime 注释创建时间", 
                "[Table].序号 = [Remark].Id");
            for (int rowIndex = 0; rowIndex < result.Rows.Count; rowIndex++) 
            {
                DataRow r = result.Rows[rowIndex];
                for(int columnIndex=0;columnIndex<r.ItemArray.Count();columnIndex++)
                {
                    string itemString = r.ItemArray[columnIndex].ToString();
                    if (UnicodeUtil.IsChineseString(itemString))
                    {
                        string transformResult = Hex2ChiEngString(itemString);
                        r[columnIndex] = transformResult;
                    }                    
                    //char[] itemChars = itemString.ToCharArray();
                    //StringBuilder newString = new StringBuilder();
                    //for(int chIndex=0;chIndex< itemString.Length;chIndex++)
                    //{
                    //    if (Util.UnicodeUtil.IsUniChs(itemChars[chIndex]))
                    //        newString.Append(Util.UnicodeUtil.GetChsFromHex())
                    //}

                }
            }
            this.dataGridViewRemarks.DataSource = result;
            this.dataGridViewRemarks.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        /// <summary>
        /// 将英文与hex混合字串转换成正常字串
        /// </summary>
        /// <param name="itemString"></param>
        /// <returns></returns>
        private string Hex2ChiEngString(string itemString)
        {
            int finishedPointer = 0;
            int chiStartIndex, chiEndIndex;
            StringBuilder strResult = new StringBuilder();
            while(finishedPointer < itemString.Length)
            {
                chiStartIndex = itemString.IndexOf("&#x", finishedPointer);
                if (chiStartIndex == -1)
                    chiStartIndex = itemString.Length;
                if(chiStartIndex!=finishedPointer) //有不用处理的英文字母直接而放入
                {
                    strResult.Append(itemString.Substring(finishedPointer,chiStartIndex-finishedPointer));
                    finishedPointer = chiStartIndex;
                }
                if (finishedPointer < itemString.Length) {
                    chiEndIndex = itemString.IndexOf(";", chiStartIndex) + 1;
                    while (true)
                    {
                        if (itemString.IndexOf("&#x", chiEndIndex) != chiEndIndex)
                            break;
                        chiEndIndex = itemString.IndexOf(";", chiEndIndex) + 1;
                    }
                    if (chiEndIndex == 0)
                        throw new Exception("中文Hex格式错误");
                    string chinCharacterHex = itemString.Substring(chiStartIndex, chiEndIndex - chiStartIndex).Replace("&#x","").Replace(";","");
                    strResult.Append(UnicodeUtil.GetChsFromHex(chinCharacterHex));
                    finishedPointer = chiEndIndex;
                }
            }
            return strResult.ToString();
        }

        private void button提交_Click(object sender, EventArgs e)
        {
            string formatedContent = Text2Hex(this.textBoxContent.Text);

            //if (Util.SqlServerConnection.UpdateDatabase((DataTable) this.dataGridViewRemarks.DataSource))
            if (SqlUtil.Update("[Remark]", "Remark", "'" + formatedContent + "'", "id = " + this.textBoxId.Text)) //这里的update有时候不行，未知bug
            {
                System.Windows.MessageBox.Show("修改已提交到数据库。", "修改成功！", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                this.RemarkManagerForm_Load(sender, e);
                //this.dataGridViewRemarks.DataSource = Util.SqlServerConnection.ExecuteQuery("select Id 序号,Remark 注释,RemarkTime 注释创建时间 from [Remark]");
            }
        }

        private void button提交添加_Click(object sender, EventArgs e)
        {
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
                this.RemarkManagerForm_Load(sender, e);
                //this.dataGridViewRemarks.DataSource = Util.SqlServerConnection.ExecuteQuery("select Id 序号,Remark 注释,RemarkTime 注释创建时间 from [Remark]");
            }
        }

        private string Text2Hex(string oldContent)
        {
            string formatedContent = ""; //只对oldContent中的中文部分进行utf8编码，其他部分不予处理
            foreach (char ch in oldContent.ToCharArray())
            {
                if (Util.UnicodeUtil.IsChineseChar(ch))
                {
                    formatedContent += Util.UnicodeUtil.GetHexFromChs(ch);
                }
                else
                    formatedContent += ch;
            }
            return formatedContent;
        }

        private void dataGridViewRemarks_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //显示在HeaderCell上
            if (e.Row.Cells[0].Value != null)
            {
                e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
            }
        }
    }
}
