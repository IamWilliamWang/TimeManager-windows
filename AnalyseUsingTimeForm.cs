using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using 关机助手.Util;

namespace 关机助手
{
    public partial class AnalyseUsingTimeForm : Form
    {
        DataTable resultTable { get; set; }

        public AnalyseUsingTimeForm()
        {
            InitializeComponent();
        }

        private void ShowUsingTimeOfEachMonth_Load(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            this.FillDataGridView();
            this.FillChart();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionForm.ShowDialog((Exception)e.ExceptionObject);
        }

        #region DataGridView初始化
        private void FillDataGridView()
        {
            this.QueryAndCalculate();
            DataTable resultTable = this.resultTable;
            this.dataGridView.DataSource = resultTable;
        }

        private void QueryAndCalculate()
        {
            try
            {
                SqlServerConnection.ExecuteUpdate(DeleteTempVarsSQL());
            }
            catch (Exception)
            {

            }
            SqlServerConnection.ExecuteUpdate(CreateSqlFunctionSQL());
            resultTable = SqlServerConnection.ExecuteQuery(CountSumDateTimeOfEachMonthSQL());
        }

        private string CreateSqlFunctionSQL()
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

        private string DeleteTempVarsSQL()
        {
            return "drop function SecToDateTime " +
                    //"select * from 每月累计时长表 " +
                    "drop table 每月累计时长表";
        }

        private string CountSumDateTimeOfEachMonthSQL()
        {
            return "/*创建该函数对象*/\n" +
                "/*重复创建需要先 drop function SecToDateTime */\n" +
                "               \n " +
                "select YEAR(开机时间) 年份, MONTH(开机时间) 月份, dbo.SecToDateTime(sum(datediff(second, '00:00:00', 时长))) 当月累计时长 /*into 每月累计时长表*/\n" +
                "from[Table]\n" +
                "group by  MONTH(开机时间),YEAR(开机时间);\n" +
                "/*where YEAR(开机时间) == parentTable.YEAR(开机时间) and MONTH(开机时间) == parentTable.MONTH(开机时间)\n" +
                "            \n" +
                "update [Table]\n" +
                "set 当月时长累计=每月累计时长表.当月累计时长\n" +
                "where [Table].开机时间=每月累计时长表.年 and [Table].开机时间=每月累计时长表.月\n" +
                "*/ ";
        }
        #endregion

        #region Chart初始化
        private void FillChart()
        {
            //Series series = new Series();
            //series.ChartType = SeriesChartType.Column;//直方图
            //series.BorderWidth = 2;
            //series.Color = graphColor;
            //series.LegendText = LegendText;
            //series.IsValueShownAsLabel = this.IsValueShownAsLabel;
            List<string> xData = new List<string>();
            List<double> yData = new List<double>();

            foreach (DataRow row in resultTable.Rows)
            {
                xData.Add(row[0] + "年" + row[1] + "月");
                yData.Add(Transfer2Hour(row[2].ToString()));
            }


            //chart1.Series[0]["PieLabelStyle"] = "Outside";
            chart1.Series[0].Points.Clear();
            chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            chart1.Series[0].Points.DataBindXY(xData, yData);

        }

        private double Transfer2Hour(String original)
        {
            String[] dayHourMinSec = original.Replace(" ", "").Replace("days", ":").Split(':');
            if (dayHourMinSec.Count() != 4)
                return 0;

            double day = double.Parse(dayHourMinSec[0]);
            double hour = double.Parse(dayHourMinSec[1]);
            double min = double.Parse(dayHourMinSec[2]);
            double sec = double.Parse(dayHourMinSec[3]);

            return CutDownSmallNumber(day * 24 + hour + min / 60 + sec / 3600, 2);
        }

        private double CutDownSmallNumber(double longNumber, int smallNumberLength)
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

        #region 切换3D效果选项卡
        private void 切换3D效果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chart1.ChartAreas[0].Area3DStyle.Enable3D = !this.chart1.ChartAreas[0].Area3DStyle.Enable3D;
        }
        #endregion

        #region 关闭选项卡
        private void 关闭本窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 关闭程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region 保存图片选项卡
        private void 保存图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "png文件 (*.png)|所有文件 (*.*)";
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                String fullFileName = fileDialog.FileName;
                if (fullFileName.Contains("png") == false)
                    fullFileName += ".png";
                chart1.SaveImage(fullFileName, ChartImageFormat.Png);
                MessageBox.Show("已经成功保存图片！", "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        #endregion

        #region 分析时间分布
        private void 开机时间分布ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> hourNums = new List<int>();
            List<int> 频度 = new List<int>();
            for (int i = 0; i < 24; i++)
            {
                hourNums.Add(i);
                频度.Add(0);
            }
            AnalyseByTimes(hourNums, 频度,"开机");

            this.chart1.Series[0].Points.Clear();
            this.chart1.Series[0].Points.DataBindXY(hourNums, 频度);
            this.柱状图ToolStripMenuItem_Click(sender, e);

            this.返回总图ToolStripMenuItem.Enabled = true;
        }

        private void AnalyseByTimes(List<int> hourNums, List<int> 频度, string keyWord)
        {
            if (keyWord != "开机" && keyWord != "关机")
                return;

            string sql = "select datename(hour, "+keyWord+"时间) from[Table]";
            DataTable queryResult = SqlServerConnection.ExecuteQuery(sql);
            foreach(DataRow row in queryResult.Rows)
            {
                if (row[0].ToString() == "")
                    continue;
                频度[int.Parse(row[0].ToString())]++;
            }
        }

        private void 关机时间分布ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> hourNums = new List<int>();
            List<int> 频度 = new List<int>();
            for (int i = 0; i < 24; i++)
            {
                hourNums.Add(i);
                频度.Add(0);
            }
            AnalyseByTimes(hourNums, 频度, "关机");

            this.chart1.Series[0].Points.Clear();
            this.chart1.Series[0].Points.DataBindXY(hourNums, 频度);
            
            this.柱状图ToolStripMenuItem_Click(sender, e);

            this.返回总图ToolStripMenuItem.Enabled = true;
        }

        private void 返回总图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FillChart();
            this.饼图ToolStripMenuItem_Click(sender, e);
            this.返回总图ToolStripMenuItem.Enabled = false;
        }
        #endregion


    }
}
