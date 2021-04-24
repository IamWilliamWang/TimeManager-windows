namespace 关机助手
{
    partial class AnalysingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.分析时间分布ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开机时间分布ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关机时间分布ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.返回总图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更换颜色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.红色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.蓝色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.黄色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.紫色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.灰色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.浅蓝色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更换颜色提示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图形切换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.饼图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空心饼图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.柱状图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.折线图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.曲线图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.切换3D效果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据标签ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.结束程序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(838, 28);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(343, 452);
            this.dataGridView.TabIndex = 0;
            // 
            // chart1
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.AxisX.Title = "时间（月）";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.Title = "总计时长（小时）";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(13, 28);
            this.chart.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Label = "#VALX #VAL";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(819, 452);
            this.chart.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.分析时间分布ToolStripMenuItem,
            this.更换颜色ToolStripMenuItem,
            this.图形切换ToolStripMenuItem,
            this.切换3D效果ToolStripMenuItem,
            this.保存图片ToolStripMenuItem,
            this.数据标签ToolStripMenuItem,
            this.结束程序ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1193, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 分析时间分布ToolStripMenuItem
            // 
            this.分析时间分布ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开机时间分布ToolStripMenuItem,
            this.关机时间分布ToolStripMenuItem,
            this.返回总图ToolStripMenuItem});
            this.分析时间分布ToolStripMenuItem.Name = "分析时间分布ToolStripMenuItem";
            this.分析时间分布ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.分析时间分布ToolStripMenuItem.Text = "分析时间分布";
            // 
            // 开机时间分布ToolStripMenuItem
            // 
            this.开机时间分布ToolStripMenuItem.Name = "开机时间分布ToolStripMenuItem";
            this.开机时间分布ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.开机时间分布ToolStripMenuItem.Text = "开机时间分布";
            this.开机时间分布ToolStripMenuItem.Click += new System.EventHandler(this.开机时间分布ToolStripMenuItem_Click);
            // 
            // 关机时间分布ToolStripMenuItem
            // 
            this.关机时间分布ToolStripMenuItem.Name = "关机时间分布ToolStripMenuItem";
            this.关机时间分布ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.关机时间分布ToolStripMenuItem.Text = "关机时间分布";
            this.关机时间分布ToolStripMenuItem.Click += new System.EventHandler(this.关机时间分布ToolStripMenuItem_Click);
            // 
            // 返回总图ToolStripMenuItem
            // 
            this.返回总图ToolStripMenuItem.Name = "返回总图ToolStripMenuItem";
            this.返回总图ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.返回总图ToolStripMenuItem.Text = "返回总图";
            this.返回总图ToolStripMenuItem.Click += new System.EventHandler(this.返回总图ToolStripMenuItem_Click);
            // 
            // 更换颜色ToolStripMenuItem
            // 
            this.更换颜色ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.红色ToolStripMenuItem,
            this.蓝色ToolStripMenuItem,
            this.黄色ToolStripMenuItem,
            this.紫色ToolStripMenuItem,
            this.灰色ToolStripMenuItem,
            this.浅蓝色ToolStripMenuItem,
            this.更换颜色提示ToolStripMenuItem});
            this.更换颜色ToolStripMenuItem.Name = "更换颜色ToolStripMenuItem";
            this.更换颜色ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.更换颜色ToolStripMenuItem.Text = "更换颜色";
            // 
            // 红色ToolStripMenuItem
            // 
            this.红色ToolStripMenuItem.Name = "红色ToolStripMenuItem";
            this.红色ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.红色ToolStripMenuItem.Text = "红色";
            this.红色ToolStripMenuItem.Click += new System.EventHandler(this.红色ToolStripMenuItem_Click);
            // 
            // 蓝色ToolStripMenuItem
            // 
            this.蓝色ToolStripMenuItem.Name = "蓝色ToolStripMenuItem";
            this.蓝色ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.蓝色ToolStripMenuItem.Text = "蓝色";
            this.蓝色ToolStripMenuItem.Click += new System.EventHandler(this.蓝色ToolStripMenuItem_Click);
            // 
            // 黄色ToolStripMenuItem
            // 
            this.黄色ToolStripMenuItem.Name = "黄色ToolStripMenuItem";
            this.黄色ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.黄色ToolStripMenuItem.Text = "黄色";
            this.黄色ToolStripMenuItem.Click += new System.EventHandler(this.黄色ToolStripMenuItem_Click);
            // 
            // 紫色ToolStripMenuItem
            // 
            this.紫色ToolStripMenuItem.Name = "紫色ToolStripMenuItem";
            this.紫色ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.紫色ToolStripMenuItem.Text = "紫色";
            this.紫色ToolStripMenuItem.Click += new System.EventHandler(this.紫色ToolStripMenuItem_Click);
            // 
            // 灰色ToolStripMenuItem
            // 
            this.灰色ToolStripMenuItem.Name = "灰色ToolStripMenuItem";
            this.灰色ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.灰色ToolStripMenuItem.Text = "灰色";
            this.灰色ToolStripMenuItem.Click += new System.EventHandler(this.灰色ToolStripMenuItem_Click);
            // 
            // 浅蓝色ToolStripMenuItem
            // 
            this.浅蓝色ToolStripMenuItem.Name = "浅蓝色ToolStripMenuItem";
            this.浅蓝色ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.浅蓝色ToolStripMenuItem.Text = "浅蓝色";
            this.浅蓝色ToolStripMenuItem.Click += new System.EventHandler(this.浅蓝色ToolStripMenuItem_Click);
            // 
            // 更换颜色提示ToolStripMenuItem
            // 
            this.更换颜色提示ToolStripMenuItem.Enabled = false;
            this.更换颜色提示ToolStripMenuItem.Name = "更换颜色提示ToolStripMenuItem";
            this.更换颜色提示ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.更换颜色提示ToolStripMenuItem.Text = "tip饼图不可变";
            // 
            // 图形切换ToolStripMenuItem
            // 
            this.图形切换ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.饼图ToolStripMenuItem,
            this.空心饼图ToolStripMenuItem,
            this.柱状图ToolStripMenuItem,
            this.折线图ToolStripMenuItem,
            this.曲线图ToolStripMenuItem});
            this.图形切换ToolStripMenuItem.Name = "图形切换ToolStripMenuItem";
            this.图形切换ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.图形切换ToolStripMenuItem.Text = "图形切换";
            // 
            // 饼图ToolStripMenuItem
            // 
            this.饼图ToolStripMenuItem.Name = "饼图ToolStripMenuItem";
            this.饼图ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.饼图ToolStripMenuItem.Text = "饼图";
            this.饼图ToolStripMenuItem.Click += new System.EventHandler(this.饼图ToolStripMenuItem_Click);
            // 
            // 空心饼图ToolStripMenuItem
            // 
            this.空心饼图ToolStripMenuItem.Name = "空心饼图ToolStripMenuItem";
            this.空心饼图ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.空心饼图ToolStripMenuItem.Text = "空心饼图";
            this.空心饼图ToolStripMenuItem.Click += new System.EventHandler(this.空心饼图ToolStripMenuItem_Click);
            // 
            // 柱状图ToolStripMenuItem
            // 
            this.柱状图ToolStripMenuItem.Name = "柱状图ToolStripMenuItem";
            this.柱状图ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.柱状图ToolStripMenuItem.Text = "柱状图";
            this.柱状图ToolStripMenuItem.Click += new System.EventHandler(this.柱状图ToolStripMenuItem_Click);
            // 
            // 折线图ToolStripMenuItem
            // 
            this.折线图ToolStripMenuItem.Name = "折线图ToolStripMenuItem";
            this.折线图ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.折线图ToolStripMenuItem.Text = "折线图";
            this.折线图ToolStripMenuItem.Click += new System.EventHandler(this.折线图ToolStripMenuItem_Click);
            // 
            // 曲线图ToolStripMenuItem
            // 
            this.曲线图ToolStripMenuItem.Name = "曲线图ToolStripMenuItem";
            this.曲线图ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.曲线图ToolStripMenuItem.Text = "曲线图";
            this.曲线图ToolStripMenuItem.Click += new System.EventHandler(this.曲线图ToolStripMenuItem_Click);
            // 
            // 切换3D效果ToolStripMenuItem
            // 
            this.切换3D效果ToolStripMenuItem.Name = "切换3D效果ToolStripMenuItem";
            this.切换3D效果ToolStripMenuItem.Size = new System.Drawing.Size(84, 21);
            this.切换3D效果ToolStripMenuItem.Text = "切换3D效果";
            this.切换3D效果ToolStripMenuItem.Click += new System.EventHandler(this.切换3D效果ToolStripMenuItem_Click);
            // 
            // 保存图片ToolStripMenuItem
            // 
            this.保存图片ToolStripMenuItem.Name = "保存图片ToolStripMenuItem";
            this.保存图片ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.保存图片ToolStripMenuItem.Text = "保存图片";
            this.保存图片ToolStripMenuItem.Click += new System.EventHandler(this.保存图片ToolStripMenuItem_Click);
            // 
            // 数据标签ToolStripMenuItem
            // 
            this.数据标签ToolStripMenuItem.Name = "数据标签ToolStripMenuItem";
            this.数据标签ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.数据标签ToolStripMenuItem.Text = "关闭数据标签";
            this.数据标签ToolStripMenuItem.Click += new System.EventHandler(this.数据标签ToolStripMenuItem_Click);
            // 
            // 结束程序ToolStripMenuItem
            // 
            this.结束程序ToolStripMenuItem.Name = "结束程序ToolStripMenuItem";
            this.结束程序ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.结束程序ToolStripMenuItem.Text = "结束程序";
            this.结束程序ToolStripMenuItem.Click += new System.EventHandler(this.关闭程序ToolStripMenuItem_Click);
            // 
            // AnalysingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 492);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AnalysingForm";
            this.Text = "数据可视化";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 数据标签ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 切换3D效果ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图形切换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 饼图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 柱状图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 折线图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更换颜色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 红色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 蓝色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 黄色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 曲线图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更换颜色提示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 紫色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 灰色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 浅蓝色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空心饼图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分析时间分布ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开机时间分布ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关机时间分布ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 结束程序ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 返回总图ToolStripMenuItem;
    }
}