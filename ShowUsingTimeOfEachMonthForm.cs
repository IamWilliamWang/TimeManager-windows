using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using 关机小程序.Util;

namespace 关机小程序
{
    public partial class ShowUsingTimeOfEachMonthForm : Form
    {
        DataTable result;

        public ShowUsingTimeOfEachMonthForm()
        {
            InitializeComponent();
        }

        private void ShowUsingTimeOfEachMonth_Load(object sender, EventArgs e)
        {
            this.fillDataGridView();
            this.fillChart();
        }
        #region DataGridView初始化
        private void fillDataGridView()
        {
            this.queryAndCalculate();
            DataTable resultTable = this.resultTable();
            this.dataGridView1.DataSource = resultTable;
        }

        private void queryAndCalculate()
        {
            SqlServerStatement.getStatement().executeUpdate(deleteTempVarsSQL());
            SqlServerStatement.getStatement().executeUpdate(createSqlFunctionSQL());
            result = SqlServerStatement.getStatement().executeQuery(countSumDateTimeOfEachMonthSQL());
        }

        private DataTable resultTable()
        {
            return result;
        }

        private string createSqlFunctionSQL()
        {
            return "Create function SecToDateTime(@sec int) /*将second转换成 天,时:分:秒*/\n" +
                "Returns varchar(50) /*返回类型为varchar*/\n" +
                "As\n" +
                "Begin\n" +
                "   Declare @day int\n" +
                "   Declare @hour int\n" +
                "   Declare @min int\n" +
                "   Declare @dt varchar(50)\n" +
                "   set @day = 0\n" +
                "   set @hour = 0\n" +
                "   set @min = 0\n" +
                "   while @sec > 60\n" +
                "   begin\n" +
                "      set @sec = @sec - 60\n" +
                "      set @min = @min + 1\n" +
                "   end\n" +
                "\n" +
                "   while @min > 60\n" +
                "   begin\n" +
                "      set @min = @min - 60\n" +
                "      set @hour = @hour + 1\n" +
                "   end\n" +
                "               \n " +
                "   while @hour > 24\n" +
                "   begin\n" +
                "      set @hour = @hour - 24\n" +
                "      set @day = @day + 1\n" +
                "   end\n" +
                "               \n " +
                "   set @dt = convert(varchar(50), @day) + ' days ' + convert(varchar(50), @hour) + ':' + convert(varchar(50), @min) + ':' + convert(varchar(50), @sec)\n" +
                "Return @dt\n" +
                "End\n";
        }

        private string deleteTempVarsSQL()
        {
            return "drop function SecToDateTime " +
                    "select * from 每月累计时长表 " +
                    "drop table 每月累计时长表";
        }

        private string countSumDateTimeOfEachMonthSQL()
        {
            return "/*创建该函数对象*/\n" +
                "/*重复创建需要先 drop function SecToDateTime */\n" +
                "               \n " +
                "select YEAR(开机时间) 年份, MONTH(开机时间) 月份, dbo.SecToDateTime(sum(datediff(second, '00:00:00', 时长))) 当月累计时长 /*into 每月累计时长表*/\n" +
                "from[Table]\n" +
                "group by YEAR(开机时间), MONTH(开机时间);\n" +
                "/*where YEAR(开机时间) == parentTable.YEAR(开机时间) and MONTH(开机时间) == parentTable.MONTH(开机时间)\n" +
                "            \n" +
                "update [Table]\n" +
                "set 当月时长累计=每月累计时长表.当月累计时长\n" +
                "where [Table].开机时间=每月累计时长表.年 and [Table].开机时间=每月累计时长表.月\n" +
                "*/ ";
        }
        #endregion
        #region Chart初始化
        private void fillChart()
        {
            //Series series = new Series();
            //series.ChartType = SeriesChartType.Column;//直方图
            //series.BorderWidth = 2;
            //series.Color = graphColor;
            //series.LegendText = LegendText;
            //series.IsValueShownAsLabel = this.IsValueShownAsLabel;
            List<string> xData = new List<string>();
            List<double> yData = new List<double>();
            
            foreach(DataRow row in resultTable().Rows)
            {
                xData.Add(row[0] + "年" + row[1] + "月");
                yData.Add(transfer2Hour(row[2].ToString()));
            }

            //chart1.Series[0]["PieLabelStyle"] = "Outside";
            chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            chart1.Series[0].Points.DataBindXY(xData, yData);

        }

        private double transfer2Hour(String original)
        {
            String[] dayHourMinSec = original.Replace(" ", "").Replace("days", ":").Split(':');
            double day = double.Parse(dayHourMinSec[0]);
            double hour = double.Parse(dayHourMinSec[1]);
            double min = double.Parse(dayHourMinSec[2]);
            double sec = double.Parse(dayHourMinSec[3]);
            
            return cutDownSmallNumber(day*24 + hour + min/60 + sec/3600, 2);
        }

        private double cutDownSmallNumber(double longNumber, int smallNumberLength)
        {
            int tmp = (int)Math.Floor(longNumber * Math.Pow(10, smallNumberLength));
            return 1.0 * tmp / Math.Pow(10, smallNumberLength);
        }
        #endregion

        #region 图形切换选项卡
        private void 饼图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].ChartType = SeriesChartType.Pie;
            this.chart1.Series[0].IsVisibleInLegend = true;
        }

        private void 空心饼图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].ChartType = SeriesChartType.Doughnut;
            this.chart1.Series[0].IsVisibleInLegend = true;
        }

        private void 柱状图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].ChartType = SeriesChartType.Column;
            this.chart1.Series[0].IsVisibleInLegend = false;
        }

        private void 折线图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].ChartType = SeriesChartType.Line;
            this.chart1.Series[0].IsVisibleInLegend = false;
        }

        private void 曲线图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].ChartType = SeriesChartType.Spline;
            this.chart1.Series[0].IsVisibleInLegend = false;
        }

        
        #endregion
        #region 更换颜色选项卡
        private void 红色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Color = Color.Red;
        }

        private void 蓝色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Color = Color.Blue;
        }

        private void 黄色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Color = Color.Yellow;
        }

        private void 紫色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Color = Color.BlueViolet;
        }

        private void 灰色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Color = Color.Gray;
        }

        private void 浅蓝色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Color = Color.LightSkyBlue;
        }
        #endregion
        private void 切换3D效果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chart1.ChartAreas[0].Area3DStyle.Enable3D = !this.chart1.ChartAreas[0].Area3DStyle.Enable3D;
        }

        private void 关闭本窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 保存图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "png文件 (*.png)|所有文件 (*.*)";
            fileDialog.RestoreDirectory = true;

            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                String fullFileName = fileDialog.FileName;
                if (fullFileName.Contains("png") == false)
                    fullFileName += ".png";
                chart1.SaveImage(fullFileName, ChartImageFormat.Png);
                MessageBox.Show("已经成功保存图片！", "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }
    }
}
