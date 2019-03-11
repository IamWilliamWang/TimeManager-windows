namespace 关机助手补丁
{
    partial class PatchForm
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
            this.button开机 = new System.Windows.Forms.Button();
            this.button关机 = new System.Windows.Forms.Button();
            this.label源 = new System.Windows.Forms.Label();
            this.label目标 = new System.Windows.Forms.Label();
            this.textBox源 = new System.Windows.Forms.TextBox();
            this.textBox目标 = new System.Windows.Forms.TextBox();
            this.button合并 = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // button开机
            // 
            this.button开机.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button开机.Location = new System.Drawing.Point(39, 21);
            this.button开机.Name = "button开机";
            this.button开机.Size = new System.Drawing.Size(148, 63);
            this.button开机.TabIndex = 0;
            this.button开机.Text = "开机";
            this.button开机.UseVisualStyleBackColor = true;
            this.button开机.Click += new System.EventHandler(this.button开机_Click);
            // 
            // button关机
            // 
            this.button关机.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button关机.Location = new System.Drawing.Point(235, 21);
            this.button关机.Name = "button关机";
            this.button关机.Size = new System.Drawing.Size(140, 63);
            this.button关机.TabIndex = 1;
            this.button关机.Text = "关机";
            this.button关机.UseVisualStyleBackColor = true;
            this.button关机.Click += new System.EventHandler(this.button关机_Click);
            // 
            // label源
            // 
            this.label源.AutoSize = true;
            this.label源.Location = new System.Drawing.Point(13, 107);
            this.label源.Name = "label源";
            this.label源.Size = new System.Drawing.Size(29, 12);
            this.label源.TabIndex = 2;
            this.label源.Text = "源：";
            // 
            // label目标
            // 
            this.label目标.AutoSize = true;
            this.label目标.Location = new System.Drawing.Point(13, 132);
            this.label目标.Name = "label目标";
            this.label目标.Size = new System.Drawing.Size(41, 12);
            this.label目标.TabIndex = 3;
            this.label目标.Text = "目标：";
            // 
            // textBox源
            // 
            this.textBox源.AllowDrop = true;
            this.textBox源.Location = new System.Drawing.Point(58, 104);
            this.textBox源.Name = "textBox源";
            this.textBox源.Size = new System.Drawing.Size(295, 21);
            this.textBox源.TabIndex = 4;
            this.textBox源.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox源_DragDrop);
            this.textBox源.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_DragEnter);
            // 
            // textBox目标
            // 
            this.textBox目标.AllowDrop = true;
            this.textBox目标.Location = new System.Drawing.Point(58, 129);
            this.textBox目标.Name = "textBox目标";
            this.textBox目标.Size = new System.Drawing.Size(295, 21);
            this.textBox目标.TabIndex = 5;
            this.textBox目标.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox目标_DragDrop);
            this.textBox目标.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_DragEnter);
            // 
            // button合并
            // 
            this.button合并.Location = new System.Drawing.Point(359, 103);
            this.button合并.Name = "button合并";
            this.button合并.Size = new System.Drawing.Size(49, 47);
            this.button合并.TabIndex = 6;
            this.button合并.Text = "合并";
            this.button合并.UseVisualStyleBackColor = true;
            this.button合并.Click += new System.EventHandler(this.button合并_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除文件ToolStripMenuItem,
            this.另存为ToolStripMenuItem,
            this.移动文件ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(125, 70);
            // 
            // 删除文件ToolStripMenuItem
            // 
            this.删除文件ToolStripMenuItem.Name = "删除文件ToolStripMenuItem";
            this.删除文件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除文件ToolStripMenuItem.Text = "删除文件";
            this.删除文件ToolStripMenuItem.Click += new System.EventHandler(this.删除文件ToolStripMenuItem_Click);
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.另存为ToolStripMenuItem.Text = "另存为";
            this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.另存为ToolStripMenuItem_Click);
            // 
            // 移动文件ToolStripMenuItem
            // 
            this.移动文件ToolStripMenuItem.Name = "移动文件ToolStripMenuItem";
            this.移动文件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.移动文件ToolStripMenuItem.Text = "移动文件";
            this.移动文件ToolStripMenuItem.Click += new System.EventHandler(this.移动文件ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 104);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.button合并);
            this.Controls.Add(this.textBox目标);
            this.Controls.Add(this.textBox源);
            this.Controls.Add(this.label目标);
            this.Controls.Add(this.label源);
            this.Controls.Add(this.button关机);
            this.Controls.Add(this.button开机);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MouseWheel += this.FormMouseWheel;
            this.Name = "Form1";
            this.Text = "关机助手补丁程序";
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button开机;
        private System.Windows.Forms.Button button关机;
        private System.Windows.Forms.Label label源;
        private System.Windows.Forms.Label label目标;
        private System.Windows.Forms.TextBox textBox源;
        private System.Windows.Forms.TextBox textBox目标;
        private System.Windows.Forms.Button button合并;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 删除文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动文件ToolStripMenuItem;
    }
}

