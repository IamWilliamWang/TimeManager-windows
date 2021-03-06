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
            this.插入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入开机时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入关机时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.管理器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.立刻取消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消关机ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消休眠睡眠ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部取消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.拓展功能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxTime = new System.Windows.Forms.ComboBox();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.确认按钮contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.注册关机事件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.销毁关机事件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开启动文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label模式选择 = new System.Windows.Forms.Label();
            this.label设置倒计时 = new System.Windows.Forms.Label();
            this.主界面contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.安全模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.暗黑模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.缓存管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.窗口置顶ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获得管理员权限ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.禁止一次开机记时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.升级日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.源头管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.切断数据库连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.外链数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.附加功能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox不透明度 = new System.Windows.Forms.ToolStripComboBox();
            this.隐藏主窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.隐藏右下角图标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.任务栏隐匿ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.确定button2 = new System.Windows.Forms.Button();
            this.label指定时间 = new System.Windows.Forms.Label();
            this.记录关机时间checkBox = new System.Windows.Forms.CheckBox();
            this.updateTitleTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip.SuspendLayout();
            this.确认按钮contextMenuStrip.SuspendLayout();
            this.主界面contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("幼圆", 11F);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插入ToolStripMenuItem,
            this.管理器ToolStripMenuItem,
            this.立刻取消ToolStripMenuItem,
            this.拓展功能ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.menuStrip.Size = new System.Drawing.Size(295, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // 插入ToolStripMenuItem
            // 
            this.插入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插入开机时间ToolStripMenuItem,
            this.插入关机时间ToolStripMenuItem});
            this.插入ToolStripMenuItem.Font = new System.Drawing.Font("幼圆", 11F);
            this.插入ToolStripMenuItem.Name = "插入ToolStripMenuItem";
            this.插入ToolStripMenuItem.Size = new System.Drawing.Size(51, 19);
            this.插入ToolStripMenuItem.Text = "插入";
            // 
            // 插入开机时间ToolStripMenuItem
            // 
            this.插入开机时间ToolStripMenuItem.Font = new System.Drawing.Font("宋体", 11F);
            this.插入开机时间ToolStripMenuItem.Name = "插入开机时间ToolStripMenuItem";
            this.插入开机时间ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.插入开机时间ToolStripMenuItem.Text = "插入开机时间";
            this.插入开机时间ToolStripMenuItem.Click += new System.EventHandler(this.插入开机时间ToolStripMenuItem_Click);
            // 
            // 插入关机时间ToolStripMenuItem
            // 
            this.插入关机时间ToolStripMenuItem.Font = new System.Drawing.Font("宋体", 11F);
            this.插入关机时间ToolStripMenuItem.Name = "插入关机时间ToolStripMenuItem";
            this.插入关机时间ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.插入关机时间ToolStripMenuItem.Text = "插入关机时间";
            this.插入关机时间ToolStripMenuItem.Click += new System.EventHandler(this.插入关机时间ToolStripMenuItem_Click);
            // 
            // 管理器ToolStripMenuItem
            // 
            this.管理器ToolStripMenuItem.Name = "管理器ToolStripMenuItem";
            this.管理器ToolStripMenuItem.Size = new System.Drawing.Size(67, 19);
            this.管理器ToolStripMenuItem.Text = "管理器";
            this.管理器ToolStripMenuItem.Click += new System.EventHandler(this.管理器ToolStripMenuItem_Click);
            // 
            // 立刻取消ToolStripMenuItem
            // 
            this.立刻取消ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.取消关机ToolStripMenuItem,
            this.取消休眠睡眠ToolStripMenuItem,
            this.全部取消ToolStripMenuItem});
            this.立刻取消ToolStripMenuItem.Name = "立刻取消ToolStripMenuItem";
            this.立刻取消ToolStripMenuItem.Size = new System.Drawing.Size(83, 19);
            this.立刻取消ToolStripMenuItem.Text = "立刻取消";
            // 
            // 取消关机ToolStripMenuItem
            // 
            this.取消关机ToolStripMenuItem.Font = new System.Drawing.Font("宋体", 11F);
            this.取消关机ToolStripMenuItem.Name = "取消关机ToolStripMenuItem";
            this.取消关机ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.取消关机ToolStripMenuItem.Text = "取消关机";
            this.取消关机ToolStripMenuItem.Click += new System.EventHandler(this.取消关机ToolStripMenuItem_Click);
            // 
            // 取消休眠睡眠ToolStripMenuItem
            // 
            this.取消休眠睡眠ToolStripMenuItem.Font = new System.Drawing.Font("宋体", 11F);
            this.取消休眠睡眠ToolStripMenuItem.Name = "取消休眠睡眠ToolStripMenuItem";
            this.取消休眠睡眠ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.取消休眠睡眠ToolStripMenuItem.Text = "取消休眠/睡眠";
            this.取消休眠睡眠ToolStripMenuItem.Click += new System.EventHandler(this.取消休眠睡眠ToolStripMenuItem_Click);
            // 
            // 全部取消ToolStripMenuItem
            // 
            this.全部取消ToolStripMenuItem.Font = new System.Drawing.Font("宋体", 11F);
            this.全部取消ToolStripMenuItem.Name = "全部取消ToolStripMenuItem";
            this.全部取消ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.全部取消ToolStripMenuItem.Text = "全部取消";
            this.全部取消ToolStripMenuItem.Click += new System.EventHandler(this.全部取消ToolStripMenuItem_Click);
            // 
            // 拓展功能ToolStripMenuItem
            // 
            this.拓展功能ToolStripMenuItem.Name = "拓展功能ToolStripMenuItem";
            this.拓展功能ToolStripMenuItem.Size = new System.Drawing.Size(83, 19);
            this.拓展功能ToolStripMenuItem.Text = "拓展功能";
            this.拓展功能ToolStripMenuItem.Click += new System.EventHandler(this.拓展功能ToolStripMenuItem_Click);
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
            "关机",
            "重启",
            "休眠",
            "延缓",
            "睡眠"});
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
            this.buttonOK.Click += new System.EventHandler(this.button确定_Click);
            // 
            // 确认按钮contextMenuStrip
            // 
            this.确认按钮contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.注册关机事件ToolStripMenuItem,
            this.销毁关机事件ToolStripMenuItem,
            this.打开启动文件夹ToolStripMenuItem});
            this.确认按钮contextMenuStrip.Name = "contextMenuStrip1";
            this.确认按钮contextMenuStrip.Size = new System.Drawing.Size(161, 70);
            this.确认按钮contextMenuStrip.Tag = "确认键右击菜单";
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
            // label模式选择
            // 
            this.label模式选择.AutoSize = true;
            this.label模式选择.Location = new System.Drawing.Point(12, 39);
            this.label模式选择.Name = "label模式选择";
            this.label模式选择.Size = new System.Drawing.Size(65, 12);
            this.label模式选择.TabIndex = 4;
            this.label模式选择.Text = "模式选择：";
            // 
            // label设置倒计时
            // 
            this.label设置倒计时.AutoSize = true;
            this.label设置倒计时.Location = new System.Drawing.Point(93, 39);
            this.label设置倒计时.Name = "label设置倒计时";
            this.label设置倒计时.Size = new System.Drawing.Size(113, 12);
            this.label设置倒计时.TabIndex = 5;
            this.label设置倒计时.Text = "设置倒计时(分钟)：";
            this.label设置倒计时.Click += new System.EventHandler(this.label设置倒计时_Click);
            // 
            // 主界面contextMenuStrip
            // 
            this.主界面contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.安全模式ToolStripMenuItem,
            this.暗黑模式ToolStripMenuItem,
            this.缓存管理ToolStripMenuItem,
            this.配置管理ToolStripMenuItem,
            this.窗口置顶ToolStripMenuItem,
            this.获得管理员权限ToolStripMenuItem,
            this.禁止一次开机记时间ToolStripMenuItem,
            this.升级日志ToolStripMenuItem,
            this.源头管理ToolStripMenuItem,
            this.附加功能ToolStripMenuItem,
            this.退出ToolStripMenuItem1});
            this.主界面contextMenuStrip.Name = "contextMenuStripMainForm";
            this.主界面contextMenuStrip.Size = new System.Drawing.Size(180, 246);
            this.主界面contextMenuStrip.Tag = "主页面右击界面";
            // 
            // 安全模式ToolStripMenuItem
            // 
            this.安全模式ToolStripMenuItem.Name = "安全模式ToolStripMenuItem";
            this.安全模式ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.安全模式ToolStripMenuItem.Text = "启动安全模式";
            this.安全模式ToolStripMenuItem.Click += new System.EventHandler(this.安全模式ToolStripMenuItem_Click);
            // 
            // 暗黑模式ToolStripMenuItem
            // 
            this.暗黑模式ToolStripMenuItem.Name = "暗黑模式ToolStripMenuItem";
            this.暗黑模式ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.暗黑模式ToolStripMenuItem.Text = "打开暗黑模式";
            this.暗黑模式ToolStripMenuItem.Click += new System.EventHandler(this.darkModeToolStripMenuItem_Click);
            // 
            // 缓存管理ToolStripMenuItem
            // 
            this.缓存管理ToolStripMenuItem.Name = "缓存管理ToolStripMenuItem";
            this.缓存管理ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.缓存管理ToolStripMenuItem.Text = "缓存管理器";
            this.缓存管理ToolStripMenuItem.Click += new System.EventHandler(this.缓存管理ToolStripMenuItem_Click);
            // 
            // 配置管理ToolStripMenuItem
            // 
            this.配置管理ToolStripMenuItem.Name = "配置管理ToolStripMenuItem";
            this.配置管理ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.配置管理ToolStripMenuItem.Text = "配置内容显示";
            this.配置管理ToolStripMenuItem.Click += new System.EventHandler(this.显示配置文件内容ToolStripMenuItem_Click);
            // 
            // 窗口置顶ToolStripMenuItem
            // 
            this.窗口置顶ToolStripMenuItem.Name = "窗口置顶ToolStripMenuItem";
            this.窗口置顶ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.窗口置顶ToolStripMenuItem.Text = "开启窗口置顶";
            this.窗口置顶ToolStripMenuItem.Click += new System.EventHandler(this.窗口置顶ToolStripMenuItem_Click);
            // 
            // 获得管理员权限ToolStripMenuItem
            // 
            this.获得管理员权限ToolStripMenuItem.Name = "获得管理员权限ToolStripMenuItem";
            this.获得管理员权限ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.获得管理员权限ToolStripMenuItem.Text = "获得管理员权限";
            this.获得管理员权限ToolStripMenuItem.Click += new System.EventHandler(this.获得管理员权限ToolStripMenuItem_Click);
            // 
            // 禁止一次开机记时间ToolStripMenuItem
            // 
            this.禁止一次开机记时间ToolStripMenuItem.Name = "禁止一次开机记时间ToolStripMenuItem";
            this.禁止一次开机记时间ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.禁止一次开机记时间ToolStripMenuItem.Text = "禁止1次开机记时间";
            this.禁止一次开机记时间ToolStripMenuItem.Click += new System.EventHandler(this.禁止一次开机记时间ToolStripMenuItem_Click);
            // 
            // 升级日志ToolStripMenuItem
            // 
            this.升级日志ToolStripMenuItem.Name = "升级日志ToolStripMenuItem";
            this.升级日志ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.升级日志ToolStripMenuItem.Text = "关于&&升级日志";
            this.升级日志ToolStripMenuItem.Click += new System.EventHandler(this.升级日志ToolStripMenuItem_Click);
            // 
            // 源头管理ToolStripMenuItem
            // 
            this.源头管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.切断数据库连接ToolStripMenuItem,
            this.外链数据库ToolStripMenuItem});
            this.源头管理ToolStripMenuItem.Name = "源头管理ToolStripMenuItem";
            this.源头管理ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.源头管理ToolStripMenuItem.Text = "源头管理";
            // 
            // 切断数据库连接ToolStripMenuItem
            // 
            this.切断数据库连接ToolStripMenuItem.Name = "切断数据库连接ToolStripMenuItem";
            this.切断数据库连接ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.切断数据库连接ToolStripMenuItem.Text = "切断数据库连接";
            this.切断数据库连接ToolStripMenuItem.Click += new System.EventHandler(this.切断数据库连接ToolStripMenuItem_Click);
            // 
            // 外链数据库ToolStripMenuItem
            // 
            this.外链数据库ToolStripMenuItem.Name = "外链数据库ToolStripMenuItem";
            this.外链数据库ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.外链数据库ToolStripMenuItem.Text = "外链数据库";
            this.外链数据库ToolStripMenuItem.Click += new System.EventHandler(this.外链数据库ToolStripMenuItem_Click);
            // 
            // 附加功能ToolStripMenuItem
            // 
            this.附加功能ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox不透明度,
            this.隐藏主窗口ToolStripMenuItem,
            this.隐藏右下角图标ToolStripMenuItem,
            this.任务栏隐匿ToolStripMenuItem});
            this.附加功能ToolStripMenuItem.Name = "附加功能ToolStripMenuItem";
            this.附加功能ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.附加功能ToolStripMenuItem.Text = "附加功能";
            // 
            // toolStripComboBox不透明度
            // 
            this.toolStripComboBox不透明度.Items.AddRange(new object[] {
            "0%",
            "5%",
            "10%",
            "15%",
            "20%",
            "25%",
            "30%",
            "35%",
            "40%",
            "45%",
            "50%",
            "55%",
            "60%",
            "65%",
            "70%",
            "75%",
            "80%",
            "85%",
            "90%",
            "95%",
            "100%"});
            this.toolStripComboBox不透明度.Name = "toolStripComboBox不透明度";
            this.toolStripComboBox不透明度.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox不透明度.Text = "不透明度";
            this.toolStripComboBox不透明度.TextChanged += new System.EventHandler(this.toolStripComboBox透明度_TextChanged);
            // 
            // 隐藏主窗口ToolStripMenuItem
            // 
            this.隐藏主窗口ToolStripMenuItem.Name = "隐藏主窗口ToolStripMenuItem";
            this.隐藏主窗口ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.隐藏主窗口ToolStripMenuItem.Text = "隐藏主窗口";
            this.隐藏主窗口ToolStripMenuItem.Click += new System.EventHandler(this.隐藏主窗口ToolStripMenuItem_Click);
            // 
            // 隐藏右下角图标ToolStripMenuItem
            // 
            this.隐藏右下角图标ToolStripMenuItem.Name = "隐藏右下角图标ToolStripMenuItem";
            this.隐藏右下角图标ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.隐藏右下角图标ToolStripMenuItem.Text = "隐藏右下角图标";
            this.隐藏右下角图标ToolStripMenuItem.Click += new System.EventHandler(this.隐藏右下角图标ToolStripMenuItem_Click);
            // 
            // 任务栏隐匿ToolStripMenuItem
            // 
            this.任务栏隐匿ToolStripMenuItem.Name = "任务栏隐匿ToolStripMenuItem";
            this.任务栏隐匿ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.任务栏隐匿ToolStripMenuItem.Text = "脱离任务栏控制";
            this.任务栏隐匿ToolStripMenuItem.Click += new System.EventHandler(this.任务栏隐匿ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem1
            // 
            this.退出ToolStripMenuItem1.Name = "退出ToolStripMenuItem1";
            this.退出ToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
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
            this.确定button2.Click += new System.EventHandler(this.button确认2_Click);
            // 
            // label指定时间
            // 
            this.label指定时间.AutoSize = true;
            this.label指定时间.Location = new System.Drawing.Point(19, 104);
            this.label指定时间.Name = "label指定时间";
            this.label指定时间.Size = new System.Drawing.Size(65, 12);
            this.label指定时间.TabIndex = 8;
            this.label指定时间.Text = "指定时间：";
            // 
            // 记录关机时间checkBox
            // 
            this.记录关机时间checkBox.AutoSize = true;
            this.记录关机时间checkBox.Checked = true;
            this.记录关机时间checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.记录关机时间checkBox.Location = new System.Drawing.Point(213, 39);
            this.记录关机时间checkBox.Name = "记录关机时间checkBox";
            this.记录关机时间checkBox.Size = new System.Drawing.Size(72, 16);
            this.记录关机时间checkBox.TabIndex = 9;
            this.记录关机时间checkBox.Text = "记录时间";
            this.记录关机时间checkBox.UseVisualStyleBackColor = true;
            // 
            // updateTitleTimer
            // 
            this.updateTitleTimer.Interval = 77;
            this.updateTitleTimer.Tick += new System.EventHandler(this.updateTitleTimer_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.主界面contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "关机助手";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(295, 97);
            this.ContextMenuStrip = this.主界面contextMenuStrip;
            this.Controls.Add(this.记录关机时间checkBox);
            this.Controls.Add(this.label指定时间);
            this.Controls.Add(this.确定button2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label设置倒计时);
            this.Controls.Add(this.label模式选择);
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
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Opacity = 0.96D;
            this.Text = "关机助手 {Version}";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.确认按钮contextMenuStrip.ResumeLayout(false);
            this.主界面contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 立刻取消ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 拓展功能ToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxTime;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label模式选择;
        private System.Windows.Forms.Label label设置倒计时;
        private System.Windows.Forms.ContextMenuStrip 确认按钮contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 注册关机事件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 销毁关机事件ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip 主界面contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 打开启动文件夹ToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button 确定button2;
        private System.Windows.Forms.Label label指定时间;
        private System.Windows.Forms.ToolStripMenuItem 升级日志ToolStripMenuItem;
        private System.Windows.Forms.CheckBox 记录关机时间checkBox;
        private System.Windows.Forms.ToolStripMenuItem 管理器ToolStripMenuItem;
        private System.Windows.Forms.Timer updateTitleTimer;
        private System.Windows.Forms.ToolStripMenuItem 插入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入开机时间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入关机时间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 安全模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 禁止一次开机记时间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 源头管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 切断数据库连接ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 外链数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获得管理员权限ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 附加功能ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox不透明度;
        private System.Windows.Forms.ToolStripMenuItem 任务栏隐匿ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缓存管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 暗黑模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置管理ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem 隐藏右下角图标ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消关机ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消休眠睡眠ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部取消ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 隐藏主窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 窗口置顶ToolStripMenuItem;
    }
}

