using 关机助手.SqlServerDatabase;

namespace 关机助手
{
    partial class DatabaseManagerForm
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
            this.components = new System.ComponentModel.Container();
            this.timeDatabaseDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timeDatabaseDataSet = new 关机助手.SqlServerDatabase.TimeDatabaseDataSet();
            this.timeDatabaseDataSetBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.查询所有记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示后五条ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.日志管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入一条开机记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除所有记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除指定一条记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除最后一条记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.提交手动修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.执行SQL语句ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.释放数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开机记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.允许开机记录时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.禁止开机记录时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入与导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入所有数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.无损加载数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出所有数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.无损导出数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.储存表格至excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.一键填补空处ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全面总结汇报ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.openDBBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSetBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timeDatabaseDataSetBindingSource
            // 
            this.timeDatabaseDataSetBindingSource.DataSource = this.timeDatabaseDataSet;
            this.timeDatabaseDataSetBindingSource.Position = 0;
            // 
            // timeDatabaseDataSet
            // 
            this.timeDatabaseDataSet.DataSetName = "TimeDatabaseDataSet";
            this.timeDatabaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // timeDatabaseDataSetBindingSource1
            // 
            this.timeDatabaseDataSetBindingSource1.DataSource = this.timeDatabaseDataSet;
            this.timeDatabaseDataSetBindingSource1.Position = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 29);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(662, 377);
            this.dataGridView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查询所有记录ToolStripMenuItem,
            this.日志管理ToolStripMenuItem,
            this.编辑数据库ToolStripMenuItem,
            this.开机记录ToolStripMenuItem,
            this.导入与导出ToolStripMenuItem,
            this.一键填补空处ToolStripMenuItem,
            this.全面总结汇报ToolStripMenuItem,
            this.关ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(686, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 查询所有记录ToolStripMenuItem
            // 
            this.查询所有记录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全部显示ToolStripMenuItem,
            this.显示后五条ToolStripMenuItem1});
            this.查询所有记录ToolStripMenuItem.Name = "查询所有记录ToolStripMenuItem";
            this.查询所有记录ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.查询所有记录ToolStripMenuItem.Text = "数据显示";
            // 
            // 全部显示ToolStripMenuItem
            // 
            this.全部显示ToolStripMenuItem.Name = "全部显示ToolStripMenuItem";
            this.全部显示ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.全部显示ToolStripMenuItem.Text = "全部显示";
            this.全部显示ToolStripMenuItem.Click += new System.EventHandler(this.全刷新ToolStripMenuItem_Click);
            // 
            // 显示后五条ToolStripMenuItem1
            // 
            this.显示后五条ToolStripMenuItem1.Name = "显示后五条ToolStripMenuItem1";
            this.显示后五条ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.显示后五条ToolStripMenuItem1.Text = "显示后15条";
            this.显示后五条ToolStripMenuItem1.Click += new System.EventHandler(this.显示后五条ToolStripMenuItem_Click);
            // 
            // 日志管理ToolStripMenuItem
            // 
            this.日志管理ToolStripMenuItem.Name = "日志管理ToolStripMenuItem";
            this.日志管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.日志管理ToolStripMenuItem.Text = "日志管理";
            this.日志管理ToolStripMenuItem.Click += new System.EventHandler(this.查看日志ToolStripMenuItem_Click);
            // 
            // 编辑数据库ToolStripMenuItem
            // 
            this.编辑数据库ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插入一条开机记录ToolStripMenuItem,
            this.删除所有记录ToolStripMenuItem,
            this.删除指定一条记录ToolStripMenuItem,
            this.删除最后一条记录ToolStripMenuItem,
            this.提交手动修改ToolStripMenuItem,
            this.执行SQL语句ToolStripMenuItem,
            this.释放数据库ToolStripMenuItem});
            this.编辑数据库ToolStripMenuItem.Name = "编辑数据库ToolStripMenuItem";
            this.编辑数据库ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.编辑数据库ToolStripMenuItem.Text = "数据库管理";
            // 
            // 插入一条开机记录ToolStripMenuItem
            // 
            this.插入一条开机记录ToolStripMenuItem.Name = "插入一条开机记录ToolStripMenuItem";
            this.插入一条开机记录ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.插入一条开机记录ToolStripMenuItem.Text = "插入一次开机记录";
            this.插入一条开机记录ToolStripMenuItem.Click += new System.EventHandler(this.插入一条开机记录ToolStripMenuItem_Click);
            // 
            // 删除所有记录ToolStripMenuItem
            // 
            this.删除所有记录ToolStripMenuItem.Name = "删除所有记录ToolStripMenuItem";
            this.删除所有记录ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.删除所有记录ToolStripMenuItem.Text = "删除所有记录";
            this.删除所有记录ToolStripMenuItem.Click += new System.EventHandler(this.删除所有记录ToolStripMenuItem_Click);
            // 
            // 删除指定一条记录ToolStripMenuItem
            // 
            this.删除指定一条记录ToolStripMenuItem.Name = "删除指定一条记录ToolStripMenuItem";
            this.删除指定一条记录ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.删除指定一条记录ToolStripMenuItem.Text = "删除指定一条记录";
            this.删除指定一条记录ToolStripMenuItem.Click += new System.EventHandler(this.删除指定一条记录ToolStripMenuItem_Click);
            // 
            // 删除最后一条记录ToolStripMenuItem
            // 
            this.删除最后一条记录ToolStripMenuItem.Name = "删除最后一条记录ToolStripMenuItem";
            this.删除最后一条记录ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.删除最后一条记录ToolStripMenuItem.Text = "删除最后一条记录";
            this.删除最后一条记录ToolStripMenuItem.Click += new System.EventHandler(this.删除最后一条记录ToolStripMenuItem_Click);
            // 
            // 提交手动修改ToolStripMenuItem
            // 
            this.提交手动修改ToolStripMenuItem.Name = "提交手动修改ToolStripMenuItem";
            this.提交手动修改ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.提交手动修改ToolStripMenuItem.Text = "手动修改提交";
            this.提交手动修改ToolStripMenuItem.Click += new System.EventHandler(this.提交手动修改ToolStripMenuItem_Click);
            // 
            // 执行SQL语句ToolStripMenuItem
            // 
            this.执行SQL语句ToolStripMenuItem.Name = "执行SQL语句ToolStripMenuItem";
            this.执行SQL语句ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.执行SQL语句ToolStripMenuItem.Text = "执行SQL语句(慎重)";
            this.执行SQL语句ToolStripMenuItem.Click += new System.EventHandler(this.执行SQL语句ToolStripMenuItem_Click);
            // 
            // 释放数据库ToolStripMenuItem
            // 
            this.释放数据库ToolStripMenuItem.Name = "释放数据库ToolStripMenuItem";
            this.释放数据库ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.释放数据库ToolStripMenuItem.Text = "释放数据库连接";
            this.释放数据库ToolStripMenuItem.Click += new System.EventHandler(this.释放数据库ToolStripMenuItem_Click);
            // 
            // 开机记录ToolStripMenuItem
            // 
            this.开机记录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.允许开机记录时间ToolStripMenuItem,
            this.禁止开机记录时间ToolStripMenuItem});
            this.开机记录ToolStripMenuItem.Name = "开机记录ToolStripMenuItem";
            this.开机记录ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.开机记录ToolStripMenuItem.Text = "插件安装";
            // 
            // 允许开机记录时间ToolStripMenuItem
            // 
            this.允许开机记录时间ToolStripMenuItem.Name = "允许开机记录时间ToolStripMenuItem";
            this.允许开机记录时间ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.允许开机记录时间ToolStripMenuItem.Text = "安装写入开机时间插件";
            this.允许开机记录时间ToolStripMenuItem.Click += new System.EventHandler(this.允许开机记录时间ToolStripMenuItem_Click);
            // 
            // 禁止开机记录时间ToolStripMenuItem
            // 
            this.禁止开机记录时间ToolStripMenuItem.Name = "禁止开机记录时间ToolStripMenuItem";
            this.禁止开机记录时间ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.禁止开机记录时间ToolStripMenuItem.Text = "卸载写入开机时间插件";
            this.禁止开机记录时间ToolStripMenuItem.Click += new System.EventHandler(this.禁止开机记录时间ToolStripMenuItem_Click);
            // 
            // 导入与导出ToolStripMenuItem
            // 
            this.导入与导出ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入所有数据ToolStripMenuItem,
            this.导出所有数据ToolStripMenuItem});
            this.导入与导出ToolStripMenuItem.Name = "导入与导出ToolStripMenuItem";
            this.导入与导出ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.导入与导出ToolStripMenuItem.Text = "备份与恢复";
            // 
            // 导入所有数据ToolStripMenuItem
            // 
            this.导入所有数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.无损加载数据库ToolStripMenuItem});
            this.导入所有数据ToolStripMenuItem.Name = "导入所有数据ToolStripMenuItem";
            this.导入所有数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.导入所有数据ToolStripMenuItem.Text = "导入所有数据";
            // 
            // 无损加载数据库ToolStripMenuItem
            // 
            this.无损加载数据库ToolStripMenuItem.Name = "无损加载数据库ToolStripMenuItem";
            this.无损加载数据库ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.无损加载数据库ToolStripMenuItem.Text = "无损加载数据库";
            this.无损加载数据库ToolStripMenuItem.Click += new System.EventHandler(this.加载无损备份文档ToolStripMenuItem_Click);
            // 
            // 导出所有数据ToolStripMenuItem
            // 
            this.导出所有数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.无损导出数据库ToolStripMenuItem,
            this.储存表格至excelToolStripMenuItem});
            this.导出所有数据ToolStripMenuItem.Name = "导出所有数据ToolStripMenuItem";
            this.导出所有数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.导出所有数据ToolStripMenuItem.Text = "导出所有数据";
            // 
            // 无损导出数据库ToolStripMenuItem
            // 
            this.无损导出数据库ToolStripMenuItem.Name = "无损导出数据库ToolStripMenuItem";
            this.无损导出数据库ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.无损导出数据库ToolStripMenuItem.Text = "无损导出数据库";
            this.无损导出数据库ToolStripMenuItem.Click += new System.EventHandler(this.生成备份文档ToolStripMenuItem_Click);
            // 
            // 储存表格至excelToolStripMenuItem
            // 
            this.储存表格至excelToolStripMenuItem.Name = "储存表格至excelToolStripMenuItem";
            this.储存表格至excelToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.储存表格至excelToolStripMenuItem.Text = "保存下方数据(Excel)";
            this.储存表格至excelToolStripMenuItem.Click += new System.EventHandler(this.储存表格至excelToolStripMenuItem_Click);
            // 
            // 一键填补空处ToolStripMenuItem
            // 
            this.一键填补空处ToolStripMenuItem.Name = "一键填补空处ToolStripMenuItem";
            this.一键填补空处ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.一键填补空处ToolStripMenuItem.Text = "填补空处";
            this.一键填补空处ToolStripMenuItem.Click += new System.EventHandler(this.开始统计结算ToolStripMenuItem_Click);
            // 
            // 全面总结汇报ToolStripMenuItem
            // 
            this.全面总结汇报ToolStripMenuItem.Name = "全面总结汇报ToolStripMenuItem";
            this.全面总结汇报ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.全面总结汇报ToolStripMenuItem.Text = "数据可视化";
            this.全面总结汇报ToolStripMenuItem.Click += new System.EventHandler(this.统计每月上机时间ToolStripMenuItem_Click);
            // 
            // 关ToolStripMenuItem
            // 
            this.关ToolStripMenuItem.Name = "关ToolStripMenuItem";
            this.关ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.关ToolStripMenuItem.Text = "回主页";
            this.关ToolStripMenuItem.Click += new System.EventHandler(this.关闭此窗口ToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(574, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Step = 20;
            this.progressBar1.TabIndex = 3;
            // 
            // openDBBackgroundWorker
            // 
            this.openDBBackgroundWorker.WorkerReportsProgress = true;
            this.openDBBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.openDBBackgroundWorker_DoWork);
            this.openDBBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.openDBBackgroundWorker_RunWorkerCompleted);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(150, 150);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(150, 175);
            this.toolStripContainer1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(100, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // DatabaseManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 417);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DatabaseManagerForm";
            this.Text = "开关机时间记录——管理主窗口";
            this.Load += new System.EventHandler(this.SqlServerResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSetBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource timeDatabaseDataSetBindingSource;
        private TimeDatabaseDataSet timeDatabaseDataSet;
        private System.Windows.Forms.BindingSource timeDatabaseDataSetBindingSource1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查询所有记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开机记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 允许开机记录时间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 禁止开机记录时间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入一条开机记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除所有记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除最后一条记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入与导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入所有数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出所有数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 储存表格至excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 提交手动修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 一键填补空处ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全面总结汇报ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 无损导出数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 无损加载数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 执行SQL语句ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示后五条ToolStripMenuItem1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker openDBBackgroundWorker;
        private System.Windows.Forms.ToolStripMenuItem 释放数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除指定一条记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 日志管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关ToolStripMenuItem;
    }
}