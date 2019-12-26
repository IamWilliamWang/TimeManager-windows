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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timeDatabaseDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timeDatabaseDataSet = new 关机助手.SqlServerDatabase.TimeDatabaseDataSet();
            this.timeDatabaseDataSetBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.查询所有记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.展示所有数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示后15条ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.精准查找显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入一条开机记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.填补空处ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入开机记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入关机记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除最后一条记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除指定一条记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除所有记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除主表数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除日志数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除注释数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.提交手动修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.直接修改下方数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.高级选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.执行SQL语句ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行SQL脚本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.释放数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看已连接数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报错窗口预览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.命令行选项使用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.激活禁止系统休眠ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开机记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开机记录时间插件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.安装ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.卸载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入与导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入所有数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出所有数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存下方表格ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.高级功能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.日志管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据可视化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.注释管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.返回主界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.clearCacheBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.显示后n条数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSetBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 29);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersWidth = 46;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(662, 372);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridView1_RowStateChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查询所有记录ToolStripMenuItem,
            this.插入一条开机记录ToolStripMenuItem,
            this.编辑数据库ToolStripMenuItem,
            this.开机记录ToolStripMenuItem,
            this.导入与导出ToolStripMenuItem,
            this.高级功能ToolStripMenuItem,
            this.返回主界面ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(686, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 查询所有记录ToolStripMenuItem
            // 
            this.查询所有记录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.展示所有数据ToolStripMenuItem,
            this.显示后15条ToolStripMenuItem1,
            this.显示后n条数据ToolStripMenuItem,
            this.精准查找显示ToolStripMenuItem});
            this.查询所有记录ToolStripMenuItem.Name = "查询所有记录ToolStripMenuItem";
            this.查询所有记录ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.查询所有记录ToolStripMenuItem.Text = "时间记录显示";
            // 
            // 展示所有数据ToolStripMenuItem
            // 
            this.展示所有数据ToolStripMenuItem.Name = "展示所有数据ToolStripMenuItem";
            this.展示所有数据ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.展示所有数据ToolStripMenuItem.Text = "展示所有数据";
            this.展示所有数据ToolStripMenuItem.Click += new System.EventHandler(this.展示所有数据ToolStripMenuItem_Click);
            // 
            // 显示后15条ToolStripMenuItem1
            // 
            this.显示后15条ToolStripMenuItem1.Name = "显示后15条ToolStripMenuItem1";
            this.显示后15条ToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.显示后15条ToolStripMenuItem1.Text = "显示后15条数据";
            this.显示后15条ToolStripMenuItem1.Click += new System.EventHandler(this.显示后15条ToolStripMenuItem_Click);
            // 
            // 精准查找显示ToolStripMenuItem
            // 
            this.精准查找显示ToolStripMenuItem.Name = "精准查找显示ToolStripMenuItem";
            this.精准查找显示ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.精准查找显示ToolStripMenuItem.Text = "精准查找显示";
            this.精准查找显示ToolStripMenuItem.Click += new System.EventHandler(this.精准查找显示ToolStripMenuItem_Click);
            // 
            // 插入一条开机记录ToolStripMenuItem
            // 
            this.插入一条开机记录ToolStripMenuItem.Name = "插入一条开机记录ToolStripMenuItem";
            this.插入一条开机记录ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.插入一条开机记录ToolStripMenuItem.Text = "插入开机记录";
            this.插入一条开机记录ToolStripMenuItem.Click += new System.EventHandler(this.插入开机记录ToolStripMenuItem_Click);
            // 
            // 编辑数据库ToolStripMenuItem
            // 
            this.编辑数据库ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.填补空处ToolStripMenuItem,
            this.添加数据ToolStripMenuItem,
            this.删除数据ToolStripMenuItem,
            this.修改数据ToolStripMenuItem,
            this.高级选项ToolStripMenuItem});
            this.编辑数据库ToolStripMenuItem.Name = "编辑数据库ToolStripMenuItem";
            this.编辑数据库ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.编辑数据库ToolStripMenuItem.Text = "数据库管理";
            // 
            // 填补空处ToolStripMenuItem
            // 
            this.填补空处ToolStripMenuItem.Name = "填补空处ToolStripMenuItem";
            this.填补空处ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.填补空处ToolStripMenuItem.Text = "填补空处";
            this.填补空处ToolStripMenuItem.Click += new System.EventHandler(this.填补空处ToolStripMenuItem_Click);
            // 
            // 添加数据ToolStripMenuItem
            // 
            this.添加数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插入开机记录ToolStripMenuItem,
            this.插入关机记录ToolStripMenuItem});
            this.添加数据ToolStripMenuItem.Name = "添加数据ToolStripMenuItem";
            this.添加数据ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.添加数据ToolStripMenuItem.Text = "添加数据";
            // 
            // 插入开机记录ToolStripMenuItem
            // 
            this.插入开机记录ToolStripMenuItem.Name = "插入开机记录ToolStripMenuItem";
            this.插入开机记录ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.插入开机记录ToolStripMenuItem.Text = "插入开机记录";
            this.插入开机记录ToolStripMenuItem.Click += new System.EventHandler(this.插入开机记录ToolStripMenuItem_Click);
            // 
            // 插入关机记录ToolStripMenuItem
            // 
            this.插入关机记录ToolStripMenuItem.Name = "插入关机记录ToolStripMenuItem";
            this.插入关机记录ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.插入关机记录ToolStripMenuItem.Text = "插入关机记录";
            this.插入关机记录ToolStripMenuItem.Click += new System.EventHandler(this.插入关机记录ToolStripMenuItem_Click);
            // 
            // 删除数据ToolStripMenuItem
            // 
            this.删除数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除最后一条记录ToolStripMenuItem,
            this.删除指定一条记录ToolStripMenuItem,
            this.删除所有记录ToolStripMenuItem});
            this.删除数据ToolStripMenuItem.Name = "删除数据ToolStripMenuItem";
            this.删除数据ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除数据ToolStripMenuItem.Text = "删除数据";
            // 
            // 删除最后一条记录ToolStripMenuItem
            // 
            this.删除最后一条记录ToolStripMenuItem.Name = "删除最后一条记录ToolStripMenuItem";
            this.删除最后一条记录ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.删除最后一条记录ToolStripMenuItem.Text = "删除最后一条记录";
            this.删除最后一条记录ToolStripMenuItem.Click += new System.EventHandler(this.删除最后一条记录ToolStripMenuItem_Click);
            // 
            // 删除指定一条记录ToolStripMenuItem
            // 
            this.删除指定一条记录ToolStripMenuItem.Name = "删除指定一条记录ToolStripMenuItem";
            this.删除指定一条记录ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.删除指定一条记录ToolStripMenuItem.Text = "删除指定一条记录";
            this.删除指定一条记录ToolStripMenuItem.Click += new System.EventHandler(this.删除指定一条记录ToolStripMenuItem_Click);
            // 
            // 删除所有记录ToolStripMenuItem
            // 
            this.删除所有记录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清除主表数据ToolStripMenuItem,
            this.清除日志数据ToolStripMenuItem,
            this.清除注释数据ToolStripMenuItem});
            this.删除所有记录ToolStripMenuItem.Name = "删除所有记录ToolStripMenuItem";
            this.删除所有记录ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.删除所有记录ToolStripMenuItem.Text = "删除所有记录";
            // 
            // 清除主表数据ToolStripMenuItem
            // 
            this.清除主表数据ToolStripMenuItem.Name = "清除主表数据ToolStripMenuItem";
            this.清除主表数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.清除主表数据ToolStripMenuItem.Text = "清除主表数据";
            this.清除主表数据ToolStripMenuItem.Click += new System.EventHandler(this.清除主表数据ToolStripMenuItem_Click);
            // 
            // 清除日志数据ToolStripMenuItem
            // 
            this.清除日志数据ToolStripMenuItem.Name = "清除日志数据ToolStripMenuItem";
            this.清除日志数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.清除日志数据ToolStripMenuItem.Text = "清除日志数据";
            this.清除日志数据ToolStripMenuItem.Click += new System.EventHandler(this.清除日志数据ToolStripMenuItem_Click);
            // 
            // 清除注释数据ToolStripMenuItem
            // 
            this.清除注释数据ToolStripMenuItem.Name = "清除注释数据ToolStripMenuItem";
            this.清除注释数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.清除注释数据ToolStripMenuItem.Text = "清除注释数据";
            this.清除注释数据ToolStripMenuItem.Click += new System.EventHandler(this.清除注释数据ToolStripMenuItem_Click);
            // 
            // 修改数据ToolStripMenuItem
            // 
            this.修改数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.提交手动修改ToolStripMenuItem,
            this.直接修改下方数据ToolStripMenuItem});
            this.修改数据ToolStripMenuItem.Name = "修改数据ToolStripMenuItem";
            this.修改数据ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.修改数据ToolStripMenuItem.Text = "修改数据";
            // 
            // 提交手动修改ToolStripMenuItem
            // 
            this.提交手动修改ToolStripMenuItem.Name = "提交手动修改ToolStripMenuItem";
            this.提交手动修改ToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.提交手动修改ToolStripMenuItem.Text = "提交下方的全部修改";
            this.提交手动修改ToolStripMenuItem.Click += new System.EventHandler(this.提交修改ToolStripMenuItem_Click);
            // 
            // 直接修改下方数据ToolStripMenuItem
            // 
            this.直接修改下方数据ToolStripMenuItem.Enabled = false;
            this.直接修改下方数据ToolStripMenuItem.Name = "直接修改下方数据ToolStripMenuItem";
            this.直接修改下方数据ToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.直接修改下方数据ToolStripMenuItem.Text = "(请直接修改下方数据)";
            // 
            // 高级选项ToolStripMenuItem
            // 
            this.高级选项ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.执行SQL语句ToolStripMenuItem,
            this.运行SQL脚本ToolStripMenuItem,
            this.释放数据库ToolStripMenuItem,
            this.查看已连接数据库ToolStripMenuItem,
            this.报错窗口预览ToolStripMenuItem,
            this.命令行选项使用ToolStripMenuItem,
            this.激活禁止系统休眠ToolStripMenuItem});
            this.高级选项ToolStripMenuItem.Name = "高级选项ToolStripMenuItem";
            this.高级选项ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.高级选项ToolStripMenuItem.Text = "高级选项";
            // 
            // 执行SQL语句ToolStripMenuItem
            // 
            this.执行SQL语句ToolStripMenuItem.Name = "执行SQL语句ToolStripMenuItem";
            this.执行SQL语句ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.执行SQL语句ToolStripMenuItem.Text = "执行sql语句(不推荐)";
            this.执行SQL语句ToolStripMenuItem.Click += new System.EventHandler(this.执行SQL语句ToolStripMenuItem_Click);
            // 
            // 运行SQL脚本ToolStripMenuItem
            // 
            this.运行SQL脚本ToolStripMenuItem.Name = "运行SQL脚本ToolStripMenuItem";
            this.运行SQL脚本ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.运行SQL脚本ToolStripMenuItem.Text = "运行sql脚本(不推荐)";
            this.运行SQL脚本ToolStripMenuItem.Click += new System.EventHandler(this.运行SQL脚本ToolStripMenuItem_Click);
            // 
            // 释放数据库ToolStripMenuItem
            // 
            this.释放数据库ToolStripMenuItem.Name = "释放数据库ToolStripMenuItem";
            this.释放数据库ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.释放数据库ToolStripMenuItem.Text = "释放数据库连接";
            this.释放数据库ToolStripMenuItem.Click += new System.EventHandler(this.释放数据库ToolStripMenuItem_Click);
            // 
            // 查看已连接数据库ToolStripMenuItem
            // 
            this.查看已连接数据库ToolStripMenuItem.Name = "查看已连接数据库ToolStripMenuItem";
            this.查看已连接数据库ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.查看已连接数据库ToolStripMenuItem.Text = "查看已连接数据库";
            this.查看已连接数据库ToolStripMenuItem.Click += new System.EventHandler(this.查看已连接数据库ToolStripMenuItem_Click);
            // 
            // 报错窗口预览ToolStripMenuItem
            // 
            this.报错窗口预览ToolStripMenuItem.Name = "报错窗口预览ToolStripMenuItem";
            this.报错窗口预览ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.报错窗口预览ToolStripMenuItem.Text = "报错窗口预览";
            this.报错窗口预览ToolStripMenuItem.Click += new System.EventHandler(this.报错窗口预览ToolStripMenuItem_Click);
            // 
            // 命令行选项使用ToolStripMenuItem
            // 
            this.命令行选项使用ToolStripMenuItem.Name = "命令行选项使用ToolStripMenuItem";
            this.命令行选项使用ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.命令行选项使用ToolStripMenuItem.Text = "命令行选项使用";
            this.命令行选项使用ToolStripMenuItem.Click += new System.EventHandler(this.命令行选项使用ToolStripMenuItem_Click);
            // 
            // 激活禁止系统休眠ToolStripMenuItem
            // 
            this.激活禁止系统休眠ToolStripMenuItem.Name = "激活禁止系统休眠ToolStripMenuItem";
            this.激活禁止系统休眠ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.激活禁止系统休眠ToolStripMenuItem.Text = "激活/停用系统休眠";
            this.激活禁止系统休眠ToolStripMenuItem.Click += new System.EventHandler(this.激活禁止系统休眠ToolStripMenuItem_Click);
            // 
            // 开机记录ToolStripMenuItem
            // 
            this.开机记录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开机记录时间插件ToolStripMenuItem});
            this.开机记录ToolStripMenuItem.Name = "开机记录ToolStripMenuItem";
            this.开机记录ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.开机记录ToolStripMenuItem.Text = "插件安装";
            // 
            // 开机记录时间插件ToolStripMenuItem
            // 
            this.开机记录时间插件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.安装ToolStripMenuItem,
            this.卸载ToolStripMenuItem});
            this.开机记录时间插件ToolStripMenuItem.Name = "开机记录时间插件ToolStripMenuItem";
            this.开机记录时间插件ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.开机记录时间插件ToolStripMenuItem.Text = "开机记录时间插件";
            // 
            // 安装ToolStripMenuItem
            // 
            this.安装ToolStripMenuItem.Name = "安装ToolStripMenuItem";
            this.安装ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.安装ToolStripMenuItem.Text = "安装";
            this.安装ToolStripMenuItem.Click += new System.EventHandler(this.安装写入开机记录时间插件ToolStripMenuItem_Click);
            // 
            // 卸载ToolStripMenuItem
            // 
            this.卸载ToolStripMenuItem.Name = "卸载ToolStripMenuItem";
            this.卸载ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.卸载ToolStripMenuItem.Text = "卸载";
            this.卸载ToolStripMenuItem.Click += new System.EventHandler(this.卸载写入开机记录时间插件ToolStripMenuItem_Click);
            // 
            // 导入与导出ToolStripMenuItem
            // 
            this.导入与导出ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入所有数据ToolStripMenuItem,
            this.导出所有数据ToolStripMenuItem,
            this.保存下方表格ToolStripMenuItem});
            this.导入与导出ToolStripMenuItem.Name = "导入与导出ToolStripMenuItem";
            this.导入与导出ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.导入与导出ToolStripMenuItem.Text = "备份与恢复";
            // 
            // 导入所有数据ToolStripMenuItem
            // 
            this.导入所有数据ToolStripMenuItem.Name = "导入所有数据ToolStripMenuItem";
            this.导入所有数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.导入所有数据ToolStripMenuItem.Text = "导入所有数据";
            this.导入所有数据ToolStripMenuItem.Click += new System.EventHandler(this.还原数据库_RarToolStripMenuItem_Click);
            // 
            // 导出所有数据ToolStripMenuItem
            // 
            this.导出所有数据ToolStripMenuItem.Name = "导出所有数据ToolStripMenuItem";
            this.导出所有数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.导出所有数据ToolStripMenuItem.Text = "导出所有数据";
            this.导出所有数据ToolStripMenuItem.Click += new System.EventHandler(this.备份数据库_RarToolStripMenuItem_Click);
            // 
            // 保存下方表格ToolStripMenuItem
            // 
            this.保存下方表格ToolStripMenuItem.Name = "保存下方表格ToolStripMenuItem";
            this.保存下方表格ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.保存下方表格ToolStripMenuItem.Text = "保存下方表格";
            this.保存下方表格ToolStripMenuItem.Click += new System.EventHandler(this.保存下方数据ToolStripMenuItem_Click);
            // 
            // 高级功能ToolStripMenuItem
            // 
            this.高级功能ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.日志管理ToolStripMenuItem,
            this.数据可视化ToolStripMenuItem,
            this.注释管理ToolStripMenuItem});
            this.高级功能ToolStripMenuItem.Name = "高级功能ToolStripMenuItem";
            this.高级功能ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.高级功能ToolStripMenuItem.Text = "高级功能";
            // 
            // 日志管理ToolStripMenuItem
            // 
            this.日志管理ToolStripMenuItem.Name = "日志管理ToolStripMenuItem";
            this.日志管理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.日志管理ToolStripMenuItem.Text = "日志管理";
            this.日志管理ToolStripMenuItem.Click += new System.EventHandler(this.日志管理ToolStripMenuItem_Click);
            // 
            // 数据可视化ToolStripMenuItem
            // 
            this.数据可视化ToolStripMenuItem.Name = "数据可视化ToolStripMenuItem";
            this.数据可视化ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.数据可视化ToolStripMenuItem.Text = "数据可视化";
            this.数据可视化ToolStripMenuItem.Click += new System.EventHandler(this.数据可视化ToolStripMenuItem_Click);
            // 
            // 注释管理ToolStripMenuItem
            // 
            this.注释管理ToolStripMenuItem.Name = "注释管理ToolStripMenuItem";
            this.注释管理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.注释管理ToolStripMenuItem.Text = "注释管理";
            this.注释管理ToolStripMenuItem.Click += new System.EventHandler(this.注释管理ToolStripMenuItem_Click);
            // 
            // 返回主界面ToolStripMenuItem
            // 
            this.返回主界面ToolStripMenuItem.Name = "返回主界面ToolStripMenuItem";
            this.返回主界面ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.返回主界面ToolStripMenuItem.Text = "返回主界面";
            this.返回主界面ToolStripMenuItem.Click += new System.EventHandler(this.返回主界面ToolStripMenuItem_Click);
            // 
            // dbBackgroundWorker
            // 
            this.dbBackgroundWorker.WorkerReportsProgress = true;
            this.dbBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dbBackgroundWorker_DoWork);
            this.dbBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.openDBBackgroundWorker_RunWorkerCompleted);
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
            // clearCacheBackgroundWorker
            // 
            this.clearCacheBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.clearCacheBackgroundWorker_DoWork);
            this.clearCacheBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.clearCacheBackgroundWorker_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelTime,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 401);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(686, 25);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelTime
            // 
            this.toolStripStatusLabelTime.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.toolStripStatusLabelTime.Name = "toolStripStatusLabelTime";
            this.toolStripStatusLabelTime.Size = new System.Drawing.Size(579, 20);
            this.toolStripStatusLabelTime.Spring = true;
            this.toolStripStatusLabelTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusLabel
            // 
            this.statusLabel.Margin = new System.Windows.Forms.Padding(0, 3, 20, 2);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.statusLabel.Size = new System.Drawing.Size(52, 20);
            this.statusLabel.Text = "完成";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(574, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Step = 20;
            this.progressBar1.TabIndex = 6;
            // 
            // 显示后n条数据ToolStripMenuItem
            // 
            this.显示后n条数据ToolStripMenuItem.Name = "显示后n条数据ToolStripMenuItem";
            this.显示后n条数据ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.显示后n条数据ToolStripMenuItem.Text = "显示后n条数据";
            this.显示后n条数据ToolStripMenuItem.Click += new System.EventHandler(this.显示后n条数据ToolStripMenuItem_Click);
            // 
            // DatabaseManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 426);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DatabaseManagerForm";
            this.Text = "开关机时间记录 管理主窗口";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DatabaseManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.SqlServerResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDatabaseDataSetBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem 开机记录时间插件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入与导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入所有数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出所有数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 展示所有数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示后15条ToolStripMenuItem1;
        private System.ComponentModel.BackgroundWorker dbBackgroundWorker;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem 返回主界面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入一条开机记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 填补空处ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除所有记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除指定一条记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除最后一条记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 提交手动修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 直接修改下方数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 高级选项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 执行SQL语句ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 释放数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 高级功能ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 日志管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据可视化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 注释管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除主表数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除日志数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除注释数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看已连接数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报错窗口预览ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入开机记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入关机记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 命令行选项使用ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 激活禁止系统休眠ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运行SQL脚本ToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker clearCacheBackgroundWorker;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTime;
        private System.Windows.Forms.ToolStripMenuItem 精准查找显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存下方表格ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 安装ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 卸载ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示后n条数据ToolStripMenuItem;
    }
}