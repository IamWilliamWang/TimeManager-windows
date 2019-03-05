using System;
using System.Data;
using System.Windows.Forms;
using 关机助手.Util;

namespace 关机助手
{
    public partial class LogManagerForm : Form
    {
        SqlConnectionAgency database = new SqlConnectionAgency();
        public LogManagerForm()
        {
            InitializeComponent();
        }

        private void LogManagerForm_Load(object sender, EventArgs e)
        {
            FillDataGridView();
            FillTextBox();
        }

        private void FillDataGridView()
        {
            dataGridView1.DataSource = SqlUtil.Select("[DBLog]", "Id 序号,EventTime 发生时间,EventType 事件类型,EventDuration 持续时间");
        }
        
        private void FillTextBox()
        {
            DataTable analyseData = database.ExecuteQuery(AverageDurationSQL());
            foreach (DataRow row in analyseData.Rows)
            {
                textBox1.Text = "数据库平均登录时间为: ";
                try
                {
                    textBox1.Text += row[0].ToString();
                    if(float.Parse(row[0].ToString()) > 5)
                        CreateTipButton(float.Parse(row[0].ToString()));
                }
                catch (FormatException)
                {
                    textBox1.Text += "0";
                }
                textBox1.Text += "s";
            }

        }

        private void CreateTipButton(float seconds)
        {
            ToolStripMenuItem tipButton = new ToolStripMenuItem();
            tipButton.Name = "tipToolStripMenuItem";
            //tipButton.Size = new System.Drawing.Size(68, 21);
            tipButton.Text = "*提速建议*";
            tipButton.Click += TipButton_Click;
            this.menuStrip1.Items.Add(tipButton);
        }

        private void TipButton_Click(object sender, EventArgs e)
        {
            string content = "系统检测到您的数据库平均登录时间较长，原因是由于硬盘的读写速度较慢，"
                + "操作数据库所需要的系统资源较多，而且记录开始时间的时候系统非常繁忙，导致了等待"
                + "时间过长。建议在主界面中使用休眠功能来代替关机功能，使用休眠功能可以大幅度提高速度。";
            MessageBox.Show(content, "提速建议", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private String AverageDurationSQL()
        {
            return @"
SELECT 
avg( 
    cast( 
        datediff(mcs, '00:00:00', EventDuration) * 1e-6 as Decimal(14, 6) 
    ) 
) 
FROM[DBLog] 
WHERE EventType = 'Log in.'";
        }

        private void 提交修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (database.UpdateDatabase((DataTable) dataGridView1.DataSource))
                System.Windows.MessageBox.Show("手动修改已提交到数据库。", "修改成功！", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

       
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //显示在HeaderCell上
            if (e.Row.Cells[0].Value != null)
            {
                e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
            }
        }
    }
}
