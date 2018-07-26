﻿namespace 关机助手
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.开发者模式contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.应用AppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入开机时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入关机时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消指令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxTime = new System.Windows.Forms.ComboBox();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.确认按钮contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.注册关机事件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.销毁关机事件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开启动文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.主界面contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripComboBox透明度 = new System.Windows.Forms.ToolStripComboBox();
            this.隐匿ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.确定button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.记录关机时间checkBox = new System.Windows.Forms.CheckBox();
            this.updateTitleTimer = new System.Windows.Forms.Timer(this.components);
            this.connectSqlServerBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.checkSafeBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip.SuspendLayout();
            this.开发者模式contextMenuStrip.SuspendLayout();
            this.确认按钮contextMenuStrip.SuspendLayout();
            this.主界面contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ContextMenuStrip = this.开发者模式contextMenuStrip;
            this.menuStrip.Font = new System.Drawing.Font("幼圆", 11F);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.数据管理ToolStripMenuItem,
            this.取消指令ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.menuStrip.Size = new System.Drawing.Size(302, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // 开发者模式contextMenuStrip
            // 
            this.开发者模式contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.应用AppToolStripMenuItem});
            this.开发者模式contextMenuStrip.Name = "contextMenuStrip";
            this.开发者模式contextMenuStrip.Size = new System.Drawing.Size(193, 26);
            // 
            // 应用AppToolStripMenuItem
            // 
            this.应用AppToolStripMenuItem.Enabled = false;
            this.应用AppToolStripMenuItem.Name = "应用AppToolStripMenuItem";
            this.应用AppToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.应用AppToolStripMenuItem.Text = "开发者模式(应用App)";
            this.应用AppToolStripMenuItem.Click += new System.EventHandler(this.应用AppToolStripMenuItem_Click);
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插入开机时间ToolStripMenuItem,
            this.插入关机时间ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 19);
            this.文件ToolStripMenuItem.Text = "插入";
            this.文件ToolStripMenuItem.Click += new System.EventHandler(this.文件ToolStripMenuItem_Click);
            // 
            // 插入开机时间ToolStripMenuItem
            // 
            this.插入开机时间ToolStripMenuItem.Name = "插入开机时间ToolStripMenuItem";
            this.插入开机时间ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.插入开机时间ToolStripMenuItem.Text = "插入开机时间";
            this.插入开机时间ToolStripMenuItem.Click += new System.EventHandler(this.插入开机时间ToolStripMenuItem_Click);
            // 
            // 插入关机时间ToolStripMenuItem
            // 
            this.插入关机时间ToolStripMenuItem.Name = "插入关机时间ToolStripMenuItem";
            this.插入关机时间ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.插入关机时间ToolStripMenuItem.Text = "插入关机时间";
            this.插入关机时间ToolStripMenuItem.Click += new System.EventHandler(this.插入关机时间ToolStripMenuItem_Click);
            // 
            // 数据管理ToolStripMenuItem
            // 
            this.数据管理ToolStripMenuItem.Name = "数据管理ToolStripMenuItem";
            this.数据管理ToolStripMenuItem.Size = new System.Drawing.Size(83, 19);
            this.数据管理ToolStripMenuItem.Text = "数据管理";
            this.数据管理ToolStripMenuItem.Click += new System.EventHandler(this.管理主窗口ToolStripMenuItem_Click);
            // 
            // 取消指令ToolStripMenuItem
            // 
            this.取消指令ToolStripMenuItem.Name = "取消指令ToolStripMenuItem";
            this.取消指令ToolStripMenuItem.Size = new System.Drawing.Size(83, 19);
            this.取消指令ToolStripMenuItem.Text = "取消关机";
            this.取消指令ToolStripMenuItem.Click += new System.EventHandler(this.取消指令ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(83, 19);
            this.退出ToolStripMenuItem.Text = "拓展功能";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.更多ToolStripMenuItem_Click);
            // 
            // comboBoxTime
            // 
            this.comboBoxTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxTime.FormattingEnabled = true;
            this.comboBoxTime.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBoxTime.Location = new System.Drawing.Point(93, 63);
            this.comboBoxTime.Name = "comboBoxTime";
            this.comboBoxTime.Size = new System.Drawing.Size(109, 22);
            this.comboBoxTime.TabIndex = 1;
            this.comboBoxTime.Text = "0";
            this.comboBoxTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxTime_KeyPress);
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMode.Font = new System.Drawing.Font("Tahoma", 9F);
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Items.AddRange(new object[] {
            "休眠",
            "关机",
            "重启",
            "延缓"});
            this.comboBoxMode.Location = new System.Drawing.Point(13, 63);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(71, 22);
            this.comboBoxMode.TabIndex = 2;
            this.comboBoxMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxMode_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.ContextMenuStrip = this.确认按钮contextMenuStrip;
            this.buttonOK.Location = new System.Drawing.Point(213, 61);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // 确认按钮contextMenuStrip
            // 
            this.确认按钮contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.注册关机事件ToolStripMenuItem,
            this.销毁关机事件ToolStripMenuItem,
            this.打开启动文件夹ToolStripMenuItem});
            this.确认按钮contextMenuStrip.Name = "contextMenuStrip1";
            this.确认按钮contextMenuStrip.Size = new System.Drawing.Size(161, 70);
            // 
            // 注册关机事件ToolStripMenuItem
            // 
            this.注册关机事件ToolStripMenuItem.Name = "注册关机事件ToolStripMenuItem";
            this.注册关机事件ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.注册关机事件ToolStripMenuItem.Text = "注册关机事件";
            this.注册关机事件ToolStripMenuItem.Click += new System.EventHandler(this.注册关机事件ToolStripMenuItem_Click);
            // 
            // 销毁关机事件ToolStripMenuItem
            // 
            this.销毁关机事件ToolStripMenuItem.Name = "销毁关机事件ToolStripMenuItem";
            this.销毁关机事件ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.销毁关机事件ToolStripMenuItem.Text = "销毁关机事件";
            this.销毁关机事件ToolStripMenuItem.Click += new System.EventHandler(this.销毁关机事件ToolStripMenuItem_Click);
            // 
            // 打开启动文件夹ToolStripMenuItem
            // 
            this.打开启动文件夹ToolStripMenuItem.Name = "打开启动文件夹ToolStripMenuItem";
            this.打开启动文件夹ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.打开启动文件夹ToolStripMenuItem.Text = "打开启动文件夹";
            this.打开启动文件夹ToolStripMenuItem.Click += new System.EventHandler(this.打开启动文件夹ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "模式选择：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "设置倒计时(分钟)：";
            // 
            // 主界面contextMenuStrip
            // 
            this.主界面contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox透明度,
            this.隐匿ToolStripMenuItem,
            this.帮助ToolStripMenuItem,
            this.退出ToolStripMenuItem1});
            this.主界面contextMenuStrip.Name = "contextMenuStripMainForm";
            this.主界面contextMenuStrip.Size = new System.Drawing.Size(182, 121);
            // 
            // toolStripComboBox透明度
            // 
            this.toolStripComboBox透明度.Items.AddRange(new object[] {
            "0",
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60",
            "65",
            "70",
            "75",
            "80",
            "85",
            "90",
            "95",
            "100"});
            this.toolStripComboBox透明度.Name = "toolStripComboBox透明度";
            this.toolStripComboBox透明度.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox透明度.Text = "透明度";
            this.toolStripComboBox透明度.TextChanged += new System.EventHandler(this.toolStripComboBox透明度_TextChanged);
            // 
            // 隐匿ToolStripMenuItem
            // 
            this.隐匿ToolStripMenuItem.Name = "隐匿ToolStripMenuItem";
            this.隐匿ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.隐匿ToolStripMenuItem.Text = "隐匿";
            this.隐匿ToolStripMenuItem.Click += new System.EventHandler(this.显现ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.帮助ToolStripMenuItem.Text = "升级日志";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem1
            // 
            this.退出ToolStripMenuItem1.Name = "退出ToolStripMenuItem1";
            this.退出ToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.退出ToolStripMenuItem1.Text = "退出";
            this.退出ToolStripMenuItem1.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dateTimePicker1.Location = new System.Drawing.Point(93, 99);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(109, 21);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dateTimePicker1_KeyPress);
            // 
            // 确定button2
            // 
            this.确定button2.Location = new System.Drawing.Point(213, 99);
            this.确定button2.Name = "确定button2";
            this.确定button2.Size = new System.Drawing.Size(75, 23);
            this.确定button2.TabIndex = 7;
            this.确定button2.Text = "确定";
            this.确定button2.UseVisualStyleBackColor = true;
            this.确定button2.Click += new System.EventHandler(this.button2OK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "指定时间关机";
            // 
            // 记录关机时间checkBox
            // 
            this.记录关机时间checkBox.AutoSize = true;
            this.记录关机时间checkBox.Checked = true;
            this.记录关机时间checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.记录关机时间checkBox.Location = new System.Drawing.Point(207, 39);
            this.记录关机时间checkBox.Name = "记录关机时间checkBox";
            this.记录关机时间checkBox.Size = new System.Drawing.Size(96, 16);
            this.记录关机时间checkBox.TabIndex = 9;
            this.记录关机时间checkBox.Text = "记录关机时间";
            this.记录关机时间checkBox.UseVisualStyleBackColor = true;
            // 
            // updateTitleTimer
            // 
            this.updateTitleTimer.Interval = 77;
            this.updateTitleTimer.Tick += new System.EventHandler(this.updateTitleTimer_Tick);
            // 
            // connectSqlServerBackgroundWorker
            // 
            this.connectSqlServerBackgroundWorker.WorkerReportsProgress = true;
            this.connectSqlServerBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.connectSqlServerBackgroundWorker_DoWork);
            this.connectSqlServerBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.connectSqlServerBackgroundWorker_RunWorkerCompleted);
            // 
            // checkSafeBackgroundWorker
            // 
            this.checkSafeBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.checkSafeBackgroundWorker_DoWork);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(302, 97);
            this.ContextMenuStrip = this.主界面contextMenuStrip;
            this.Controls.Add(this.记录关机时间checkBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.确定button2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxMode);
            this.Controls.Add(this.comboBoxTime);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Opacity = 0.96D;
            this.Text = "关机助手 3.10";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.开发者模式contextMenuStrip.ResumeLayout(false);
            this.确认按钮contextMenuStrip.ResumeLayout(false);
            this.主界面contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 取消指令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxTime;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip 确认按钮contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 注册关机事件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 销毁关机事件ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip 主界面contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox透明度;
        private System.Windows.Forms.ToolStripMenuItem 隐匿ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开启动文件夹ToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button 确定button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip 开发者模式contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 应用AppToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.CheckBox 记录关机时间checkBox;
        private System.Windows.Forms.ToolStripMenuItem 数据管理ToolStripMenuItem;
        private System.Windows.Forms.Timer updateTitleTimer;
        private System.ComponentModel.BackgroundWorker connectSqlServerBackgroundWorker;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker checkSafeBackgroundWorker;
        private System.Windows.Forms.ToolStripMenuItem 插入开机时间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入关机时间ToolStripMenuItem;
    }
}
