namespace 关机小程序
{
    partial class SqlServerResult
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
            this.timeDatabaseDataSet = new 关机小程序.TimeDatabaseDataSet();
            this.timeDatabaseDataSetBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.查询所有记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开机记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.允许开机记录时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.禁止开机记录时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.管理员选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入一条开机记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除所有记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除最后一条记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.储存表格至excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭此窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始统计结算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.dataGridView1.Size = new System.Drawing.Size(552, 229);
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
            this.管理员选项ToolStripMenuItem,
            this.关闭此窗口ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(577, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 查询所有记录ToolStripMenuItem
            // 
            this.查询所有记录ToolStripMenuItem.Name = "查询所有记录ToolStripMenuItem";
            this.查询所有记录ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.查询所有记录ToolStripMenuItem.Text = "查询所有记录";
            this.查询所有记录ToolStripMenuItem.Click += new System.EventHandler(this.查询所有ToolStripMenuItem_Click);
            // 
            // 开机记录ToolStripMenuItem
            // 
            this.开机记录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.允许开机记录时间ToolStripMenuItem,
            this.禁止开机记录时间ToolStripMenuItem});
            this.开机记录ToolStripMenuItem.Name = "开机记录ToolStripMenuItem";
            this.开机记录ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.开机记录ToolStripMenuItem.Text = "开机记录";
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
            // 管理员选项ToolStripMenuItem
            // 
            this.管理员选项ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插入一条开机记录ToolStripMenuItem,
            this.删除所有记录ToolStripMenuItem,
            this.删除最后一条记录ToolStripMenuItem,
            this.开始统计结算ToolStripMenuItem,
            this.储存表格至excelToolStripMenuItem});
            this.管理员选项ToolStripMenuItem.Name = "管理员选项ToolStripMenuItem";
            this.管理员选项ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.管理员选项ToolStripMenuItem.Text = "管理员选项";
            // 
            // 插入一条开机记录ToolStripMenuItem
            // 
            this.插入一条开机记录ToolStripMenuItem.Name = "插入一条开机记录ToolStripMenuItem";
            this.插入一条开机记录ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.插入一条开机记录ToolStripMenuItem.Text = "插入一条开机记录";
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
            // 储存表格至excelToolStripMenuItem
            // 
            this.储存表格至excelToolStripMenuItem.Name = "储存表格至excelToolStripMenuItem";
            this.储存表格至excelToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.储存表格至excelToolStripMenuItem.Text = "储存表格至excel";
            this.储存表格至excelToolStripMenuItem.Click += new System.EventHandler(this.储存表格至excelToolStripMenuItem_Click);
            // 
            // 关闭此窗口ToolStripMenuItem
            // 
            this.关闭此窗口ToolStripMenuItem.Name = "关闭此窗口ToolStripMenuItem";
            this.关闭此窗口ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.关闭此窗口ToolStripMenuItem.Text = "关闭此窗口";
            this.关闭此窗口ToolStripMenuItem.Click += new System.EventHandler(this.关闭此窗口ToolStripMenuItem_Click);
            // 
            // 开始统计结算ToolStripMenuItem
            // 
            this.开始统计结算ToolStripMenuItem.Name = "开始统计结算ToolStripMenuItem";
            this.开始统计结算ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.开始统计结算ToolStripMenuItem.Text = "开始统计结算";
            this.开始统计结算ToolStripMenuItem.Click += new System.EventHandler(this.开始统计结算ToolStripMenuItem_Click);
            // 
            // SqlServerResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 281);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "SqlServerResult";
            this.Text = "开关机时间记录——管理主窗口";
            this.Load += new System.EventHandler(this.SqlServerResult_Load);
            this.DoubleClick += new System.EventHandler(this.SqlServerResult_DoubleClick);
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
        private System.Windows.Forms.ToolStripMenuItem 管理员选项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入一条开机记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除所有记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭此窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除最后一条记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 储存表格至excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开始统计结算ToolStripMenuItem;
    }
}