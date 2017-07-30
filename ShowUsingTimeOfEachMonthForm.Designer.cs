namespace 关机小程序
{
    partial class ShowUsingTimeOfEachMonthForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.关闭本窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(757, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(341, 405);
            this.dataGridView1.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea3.Area3DStyle.Enable3D = true;
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(13, 28);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series3.Label = "#VALX #VAL";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(738, 405);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关闭本窗口ToolStripMenuItem,
            this.更换颜色ToolStripMenuItem,
            this.图形切换ToolStripMenuItem,
            this.切换3D效果ToolStripMenuItem,
            this.保存图片ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1110, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 关闭本窗口ToolStripMenuItem
            // 
            this.关闭本窗口ToolStripMenuItem.Name = "关闭本窗口ToolStripMenuItem";
            this.关闭本窗口ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.关闭本窗口ToolStripMenuItem.Text = "关闭本窗口";
            this.关闭本窗口ToolStripMenuItem.Click += new System.EventHandler(this.关闭本窗口ToolStripMenuItem_Click);
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
            // ShowUsingTimeOfEachMonthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 445);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "ShowUsingTimeOfEachMonthForm";
            this.Text = "统计结果显示";
            this.Load += new System.EventHandler(this.ShowUsingTimeOfEachMonth_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 关闭本窗口ToolStripMenuItem;
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
    }
}