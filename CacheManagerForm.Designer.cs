namespace 关机助手
{
    partial class CacheManagerForm
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
            this.textBoxCache = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClearCache = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除缓存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动缓存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为缓存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入开机缓存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入关机缓存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox缓存编辑 = new System.Windows.Forms.GroupBox();
            this.groupBox合并 = new System.Windows.Forms.GroupBox();
            this.button合并 = new System.Windows.Forms.Button();
            this.textBox目标 = new System.Windows.Forms.TextBox();
            this.textBox源 = new System.Windows.Forms.TextBox();
            this.label目标 = new System.Windows.Forms.Label();
            this.label源 = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.groupBox缓存编辑.SuspendLayout();
            this.groupBox合并.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxCache
            // 
            this.textBoxCache.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCache.Location = new System.Drawing.Point(6, 20);
            this.textBoxCache.Multiline = true;
            this.textBoxCache.Name = "textBoxCache";
            this.textBoxCache.Size = new System.Drawing.Size(522, 281);
            this.textBoxCache.TabIndex = 1;
            this.textBoxCache.TextChanged += new System.EventHandler(this.textBoxCache_TextChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(169, 307);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(182, 35);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "保存修改";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClearCache
            // 
            this.buttonClearCache.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearCache.Location = new System.Drawing.Point(357, 307);
            this.buttonClearCache.Name = "buttonClearCache";
            this.buttonClearCache.Size = new System.Drawing.Size(171, 35);
            this.buttonClearCache.TabIndex = 3;
            this.buttonClearCache.Text = "清空缓存并提交";
            this.buttonClearCache.UseVisualStyleBackColor = true;
            this.buttonClearCache.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpenFile.Location = new System.Drawing.Point(6, 307);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(157, 35);
            this.buttonOpenFile.TabIndex = 4;
            this.buttonOpenFile.Text = "直接打开缓存文件";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.插入开机缓存ToolStripMenuItem,
            this.插入关机缓存ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(558, 25);
            this.menuStrip.TabIndex = 5;
            this.menuStrip.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除缓存ToolStripMenuItem,
            this.移动缓存ToolStripMenuItem,
            this.另存为缓存ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 删除缓存ToolStripMenuItem
            // 
            this.删除缓存ToolStripMenuItem.Name = "删除缓存ToolStripMenuItem";
            this.删除缓存ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除缓存ToolStripMenuItem.Text = "删除缓存";
            this.删除缓存ToolStripMenuItem.Click += new System.EventHandler(this.删除缓存ToolStripMenuItem_Click);
            // 
            // 移动缓存ToolStripMenuItem
            // 
            this.移动缓存ToolStripMenuItem.Name = "移动缓存ToolStripMenuItem";
            this.移动缓存ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.移动缓存ToolStripMenuItem.Text = "移动缓存";
            this.移动缓存ToolStripMenuItem.Click += new System.EventHandler(this.移动缓存ToolStripMenuItem_Click);
            // 
            // 另存为缓存ToolStripMenuItem
            // 
            this.另存为缓存ToolStripMenuItem.Name = "另存为缓存ToolStripMenuItem";
            this.另存为缓存ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.另存为缓存ToolStripMenuItem.Text = "另存为缓存";
            this.另存为缓存ToolStripMenuItem.Click += new System.EventHandler(this.另存为缓存ToolStripMenuItem_Click);
            // 
            // 插入开机缓存ToolStripMenuItem
            // 
            this.插入开机缓存ToolStripMenuItem.Name = "插入开机缓存ToolStripMenuItem";
            this.插入开机缓存ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.插入开机缓存ToolStripMenuItem.Text = "插入开机缓存";
            this.插入开机缓存ToolStripMenuItem.Click += new System.EventHandler(this.插入开机缓存ToolStripMenuItem_Click);
            // 
            // 插入关机缓存ToolStripMenuItem
            // 
            this.插入关机缓存ToolStripMenuItem.Name = "插入关机缓存ToolStripMenuItem";
            this.插入关机缓存ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.插入关机缓存ToolStripMenuItem.Text = "插入关机缓存";
            this.插入关机缓存ToolStripMenuItem.Click += new System.EventHandler(this.插入关机缓存ToolStripMenuItem_Click);
            // 
            // groupBox缓存编辑
            // 
            this.groupBox缓存编辑.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox缓存编辑.Controls.Add(this.textBoxCache);
            this.groupBox缓存编辑.Controls.Add(this.buttonClearCache);
            this.groupBox缓存编辑.Controls.Add(this.buttonOpenFile);
            this.groupBox缓存编辑.Controls.Add(this.buttonSave);
            this.groupBox缓存编辑.Location = new System.Drawing.Point(12, 28);
            this.groupBox缓存编辑.Name = "groupBox缓存编辑";
            this.groupBox缓存编辑.Size = new System.Drawing.Size(534, 348);
            this.groupBox缓存编辑.TabIndex = 6;
            this.groupBox缓存编辑.TabStop = false;
            this.groupBox缓存编辑.Text = "缓存文件内容";
            // 
            // groupBox合并
            // 
            this.groupBox合并.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox合并.Controls.Add(this.button合并);
            this.groupBox合并.Controls.Add(this.textBox目标);
            this.groupBox合并.Controls.Add(this.textBox源);
            this.groupBox合并.Controls.Add(this.label目标);
            this.groupBox合并.Controls.Add(this.label源);
            this.groupBox合并.Location = new System.Drawing.Point(12, 382);
            this.groupBox合并.Name = "groupBox合并";
            this.groupBox合并.Size = new System.Drawing.Size(534, 71);
            this.groupBox合并.TabIndex = 5;
            this.groupBox合并.TabStop = false;
            this.groupBox合并.Text = "缓存文件合并";
            // 
            // button合并
            // 
            this.button合并.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button合并.Location = new System.Drawing.Point(479, 19);
            this.button合并.Name = "button合并";
            this.button合并.Size = new System.Drawing.Size(49, 47);
            this.button合并.TabIndex = 11;
            this.button合并.Text = "合并";
            this.button合并.UseVisualStyleBackColor = true;
            this.button合并.Click += new System.EventHandler(this.button合并_Click);
            // 
            // textBox目标
            // 
            this.textBox目标.AllowDrop = true;
            this.textBox目标.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox目标.Location = new System.Drawing.Point(88, 45);
            this.textBox目标.Name = "textBox目标";
            this.textBox目标.Size = new System.Drawing.Size(385, 21);
            this.textBox目标.TabIndex = 10;
            this.textBox目标.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox目标_DragDrop);
            this.textBox目标.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_DragEnter);
            // 
            // textBox源
            // 
            this.textBox源.AllowDrop = true;
            this.textBox源.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox源.Location = new System.Drawing.Point(88, 23);
            this.textBox源.Name = "textBox源";
            this.textBox源.Size = new System.Drawing.Size(385, 21);
            this.textBox源.TabIndex = 9;
            this.textBox源.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox源_DragDrop);
            this.textBox源.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_DragEnter);
            // 
            // label目标
            // 
            this.label目标.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label目标.AutoSize = true;
            this.label目标.Location = new System.Drawing.Point(15, 48);
            this.label目标.Name = "label目标";
            this.label目标.Size = new System.Drawing.Size(65, 12);
            this.label目标.TabIndex = 8;
            this.label目标.Text = "目标文件：";
            // 
            // label源
            // 
            this.label源.AutoSize = true;
            this.label源.Location = new System.Drawing.Point(15, 26);
            this.label源.Name = "label源";
            this.label源.Size = new System.Drawing.Size(53, 12);
            this.label源.TabIndex = 7;
            this.label源.Text = "源文件：";
            // 
            // CacheManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 460);
            this.Controls.Add(this.groupBox合并);
            this.Controls.Add(this.groupBox缓存编辑);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "CacheManagerForm";
            this.Text = "缓存管理器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CacheManagerForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CacheManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.CacheManagerForm_Load);
            this.DoubleClick += new System.EventHandler(this.CacheManagerForm_DoubleClick);
            this.Resize += new System.EventHandler(this.CacheManagerForm_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBox缓存编辑.ResumeLayout(false);
            this.groupBox缓存编辑.PerformLayout();
            this.groupBox合并.ResumeLayout(false);
            this.groupBox合并.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxCache;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClearCache;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 插入开机缓存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入关机缓存ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox缓存编辑;
        private System.Windows.Forms.GroupBox groupBox合并;
        private System.Windows.Forms.Button button合并;
        private System.Windows.Forms.TextBox textBox目标;
        private System.Windows.Forms.TextBox textBox源;
        private System.Windows.Forms.Label label目标;
        private System.Windows.Forms.Label label源;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除缓存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动缓存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为缓存ToolStripMenuItem;
    }
}