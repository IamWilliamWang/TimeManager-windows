using 关机小程序.SqlServerDatabase;

namespace 关机小程序
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
            this.timeDatabaseDataSet = new 关机小程序.SqlServerDatabase.TimeDatabaseDataSet();
            this.timeDatabaseDataSetBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.查询所有记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开机记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.允许开机记录时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.禁止开机记录时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入一条开机记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除所有记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除最后一条记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.提交手动修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入与导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入所有数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出所有数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.储存表格至excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.无损导出数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.一键填补空处ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全面总结汇报ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭此窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.无损加载数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSetBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
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
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(662, 366);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "查询窗口：";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查询所有记录ToolStripMenuItem,
            this.开机记录ToolStripMenuItem,
            this.编辑数据库ToolStripMenuItem,
            this.导入与导出ToolStripMenuItem,
            this.一键填补空处ToolStripMenuItem,
            this.全面总结汇报ToolStripMenuItem,
            this.关闭此窗口ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(686, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 查询所有记录ToolStripMenuItem
            // 
            this.查询所有记录ToolStripMenuItem.Name = "查询所有记录ToolStripMenuItem";
            this.查询所有记录ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.查询所有记录ToolStripMenuItem.Text = "全刷新";
            this.查询所有记录ToolStripMenuItem.Click += new System.EventHandler(this.全刷新ToolStripMenuItem_Click);
            // 
            // 开机记录ToolStripMenuItem
            // 
            this.开机记录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.允许开机记录时间ToolStripMenuItem,
            this.禁止开机记录时间ToolStripMenuItem});
            this.开机记录ToolStripMenuItem.Name = "开机记录ToolStripMenuItem";
            this.开机记录ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.开机记录ToolStripMenuItem.Text = "开机记录状态";
            // 
            // 允许开机记录时间ToolStripMenuItem
            // 
            this.允许开机记录时间ToolStripMenuItem.Name = "允许开机记录时间ToolStripMenuItem";
            this.允许开机记录时间ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.允许开机记录时间ToolStripMenuItem.Text = "允许开机记录时间";
            this.允许开机记录时间ToolStripMenuItem.Click += new System.EventHandler(this.允许开机记录时间ToolStripMenuItem_Click);
            // 
            // 禁止开机记录时间ToolStripMenuItem
            // 
            this.禁止开机记录时间ToolStripMenuItem.Name = "禁止开机记录时间ToolStripMenuItem";
            this.禁止开机记录时间ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.禁止开机记录时间ToolStripMenuItem.Text = "禁止开机记录时间";
            this.禁止开机记录时间ToolStripMenuItem.Click += new System.EventHandler(this.禁止开机记录时间ToolStripMenuItem_Click);
            // 
            // 编辑数据库ToolStripMenuItem
            // 
            this.编辑数据库ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插入一条开机记录ToolStripMenuItem,
            this.删除所有记录ToolStripMenuItem,
            this.删除最后一条记录ToolStripMenuItem,
            this.提交手动修改ToolStripMenuItem});
            this.编辑数据库ToolStripMenuItem.Name = "编辑数据库ToolStripMenuItem";
            this.编辑数据库ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.编辑数据库ToolStripMenuItem.Text = "编辑数据库";
            // 
            // 插入一条开机记录ToolStripMenuItem
            // 
            this.插入一条开机记录ToolStripMenuItem.Name = "插入一条开机记录ToolStripMenuItem";
            this.插入一条开机记录ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.插入一条开机记录ToolStripMenuItem.Text = "插入一次开机记录";
            this.插入一条开机记录ToolStripMenuItem.Click += new System.EventHandler(this.插入一条开机记录ToolStripMenuItem_Click);
            // 
            // 删除所有记录ToolStripMenuItem
            // 
            this.删除所有记录ToolStripMenuItem.Name = "删除所有记录ToolStripMenuItem";
            this.删除所有记录ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.删除所有记录ToolStripMenuItem.Text = "删除所有记录";
            this.删除所有记录ToolStripMenuItem.Click += new System.EventHandler(this.删除所有记录ToolStripMenuItem_Click);
            // 
            // 删除最后一条记录ToolStripMenuItem
            // 
            this.删除最后一条记录ToolStripMenuItem.Name = "删除最后一条记录ToolStripMenuItem";
            this.删除最后一条记录ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.删除最后一条记录ToolStripMenuItem.Text = "删除最后一条记录";
            this.删除最后一条记录ToolStripMenuItem.Click += new System.EventHandler(this.删除最后一条记录ToolStripMenuItem_Click);
            // 
            // 提交手动修改ToolStripMenuItem
            // 
            this.提交手动修改ToolStripMenuItem.Name = "提交手动修改ToolStripMenuItem";
            this.提交手动修改ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.提交手动修改ToolStripMenuItem.Text = "提交手动修改";
            this.提交手动修改ToolStripMenuItem.Click += new System.EventHandler(this.提交手动修改ToolStripMenuItem_Click);
            // 
            // 导入与导出ToolStripMenuItem
            // 
            this.导入与导出ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入所有数据ToolStripMenuItem,
            this.导出所有数据ToolStripMenuItem});
            this.导入与导出ToolStripMenuItem.Name = "导入与导出ToolStripMenuItem";
            this.导入与导出ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.导入与导出ToolStripMenuItem.Text = "导入与导出";
            // 
            // 导入所有数据ToolStripMenuItem
            // 
            this.导入所有数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.无损加载数据库ToolStripMenuItem});
            this.导入所有数据ToolStripMenuItem.Name = "导入所有数据ToolStripMenuItem";
            this.导入所有数据ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.导入所有数据ToolStripMenuItem.Text = "导入所有数据";
            // 
            // 导出所有数据ToolStripMenuItem
            // 
            this.导出所有数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.储存表格至excelToolStripMenuItem,
            this.无损导出数据库ToolStripMenuItem});
            this.导出所有数据ToolStripMenuItem.Name = "导出所有数据ToolStripMenuItem";
            this.导出所有数据ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.导出所有数据ToolStripMenuItem.Text = "导出所有数据";
            // 
            // 储存表格至excelToolStripMenuItem
            // 
            this.储存表格至excelToolStripMenuItem.Name = "储存表格至excelToolStripMenuItem";
            this.储存表格至excelToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.储存表格至excelToolStripMenuItem.Text = "生成Excel文档";
            this.储存表格至excelToolStripMenuItem.Click += new System.EventHandler(this.储存表格至excelToolStripMenuItem_Click);
            // 
            // 无损导出数据库ToolStripMenuItem
            // 
            this.无损导出数据库ToolStripMenuItem.Name = "无损导出数据库ToolStripMenuItem";
            this.无损导出数据库ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.无损导出数据库ToolStripMenuItem.Text = "无损导出数据库";
            this.无损导出数据库ToolStripMenuItem.Click += new System.EventHandler(this.生成备份文档ToolStripMenuItem_Click);
            // 
            // 一键填补空处ToolStripMenuItem
            // 
            this.一键填补空处ToolStripMenuItem.Name = "一键填补空处ToolStripMenuItem";
            this.一键填补空处ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.一键填补空处ToolStripMenuItem.Text = "一键填补空处";
            this.一键填补空处ToolStripMenuItem.Click += new System.EventHandler(this.开始统计结算ToolStripMenuItem_Click);
            // 
            // 全面总结汇报ToolStripMenuItem
            // 
            this.全面总结汇报ToolStripMenuItem.Name = "全面总结汇报ToolStripMenuItem";
            this.全面总结汇报ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.全面总结汇报ToolStripMenuItem.Text = "全面总结汇报";
            this.全面总结汇报ToolStripMenuItem.Click += new System.EventHandler(this.统计每月上机时间ToolStripMenuItem_Click);
            // 
            // 关闭此窗口ToolStripMenuItem
            // 
            this.关闭此窗口ToolStripMenuItem.Name = "关闭此窗口ToolStripMenuItem";
            this.关闭此窗口ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.关闭此窗口ToolStripMenuItem.Text = "关闭此窗口";
            this.关闭此窗口ToolStripMenuItem.Click += new System.EventHandler(this.关闭此窗口ToolStripMenuItem_Click);
            // 
            // 无损加载数据库ToolStripMenuItem
            // 
            this.无损加载数据库ToolStripMenuItem.Name = "无损加载数据库ToolStripMenuItem";
            this.无损加载数据库ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.无损加载数据库ToolStripMenuItem.Text = "无损加载数据库";
            this.无损加载数据库ToolStripMenuItem.Click += new System.EventHandler(this.加载无损备份文档ToolStripMenuItem_Click);
            // 
            // DatabaseManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 418);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "DatabaseManagerForm";
            this.Text = "开关机时间记录——管理主窗口";
            this.Load += new System.EventHandler(this.SqlServerResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSetBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource timeDatabaseDataSetBindingSource;
        private TimeDatabaseDataSet timeDatabaseDataSet;
        private System.Windows.Forms.BindingSource timeDatabaseDataSetBindingSource1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查询所有记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开机记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 允许开机记录时间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 禁止开机记录时间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入一条开机记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除所有记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭此窗口ToolStripMenuItem;
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
    }
}