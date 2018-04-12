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
    public partial class LogManagerForm : Form
    {
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
            dataGridView1.DataSource = Util.SqlServerConnection.ExecuteQuery(ShowLogDBSQL());
        }

        private String ShowLogDBSQL()
        {
            return "select Id 序号,EventTime 发生时间,EventType 事件类型,EventDuration 持续时间 from [DBLog]";
        }

        private void FillTextBox()
        {
            DataTable analyseData = Util.SqlServerConnection.ExecuteQuery(AverageDurationSQL());
            foreach (DataRow row in analyseData.Rows)
            {
                textBox1.Text = "平均登录时间为: ";
                try
                {
                    textBox1.Text += row[0].ToString();
                }
                catch (FormatException)
                {
                    textBox1.Text += "0";
                }
                textBox1.Text += "s";
            }
            
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
            if(SqlServerConnection.UpdateDatabase((DataTable)dataGridView1.DataSource))
                System.Windows.MessageBox.Show("手动修改已提交到数据库。", "修改成功！", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

    }
}
