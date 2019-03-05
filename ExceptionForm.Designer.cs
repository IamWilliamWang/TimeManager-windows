namespace 关机助手
{
    partial class ExceptionForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelInformation = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonIgnore = new System.Windows.Forms.Button();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.contextMenuStripExit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.直接退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修复后退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.修复后重启ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.直接重启ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStripExit.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::关机助手.Properties.Resources.ErrorMessage;
            this.pictureBox1.Location = new System.Drawing.Point(8, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 51);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // labelInformation
            // 
            this.labelInformation.Location = new System.Drawing.Point(63, 66);
            this.labelInformation.Name = "labelInformation";
            this.labelInformation.Size = new System.Drawing.Size(356, 34);
            this.labelInformation.TabIndex = 1;
            this.labelInformation.Text = "这里是具体的异常说明";
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(8, 103);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(103, 26);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "报告错误(&S)";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonIgnore
            // 
            this.buttonIgnore.Location = new System.Drawing.Point(219, 103);
            this.buttonIgnore.Name = "buttonIgnore";
            this.buttonIgnore.Size = new System.Drawing.Size(95, 26);
            this.buttonIgnore.TabIndex = 3;
            this.buttonIgnore.Text = "忽略(&I)";
            this.buttonIgnore.UseVisualStyleBackColor = true;
            this.buttonIgnore.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // buttonQuit
            // 
            this.buttonQuit.ContextMenuStrip = this.contextMenuStripExit;
            this.buttonQuit.Location = new System.Drawing.Point(320, 103);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(94, 26);
            this.buttonQuit.TabIndex = 4;
            this.buttonQuit.Text = "退出(&Q)";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // contextMenuStripExit
            // 
            this.contextMenuStripExit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.直接退出ToolStripMenuItem,
            this.修复后退出ToolStripMenuItem,
            this.直接重启ToolStripMenuItem,
            this.修复后重启ToolStripMenuItem});
            this.contextMenuStripExit.Name = "contextMenuStripExit";
            this.contextMenuStripExit.Size = new System.Drawing.Size(181, 114);
            // 
            // 直接退出ToolStripMenuItem
            // 
            this.直接退出ToolStripMenuItem.Name = "直接退出ToolStripMenuItem";
            this.直接退出ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.直接退出ToolStripMenuItem.Text = "直接退出";
            this.直接退出ToolStripMenuItem.Click += new System.EventHandler(this.直接退出ToolStripMenuItem_Click);
            // 
            // 修复后退出ToolStripMenuItem
            // 
            this.修复后退出ToolStripMenuItem.Name = "修复后退出ToolStripMenuItem";
            this.修复后退出ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.修复后退出ToolStripMenuItem.Text = "修复后退出";
            this.修复后退出ToolStripMenuItem.Click += new System.EventHandler(this.修复后退出ToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Location = new System.Drawing.Point(8, 135);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(410, 145);
            this.textBox1.TabIndex = 5;
            this.textBox1.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(341, 36);
            this.label1.TabIndex = 6;
            this.label1.Text = "应用程序中发生了未经处理的异常。如果点击“忽略”，应用程\r\n序将忽略此错误并尝试继续。如果点击“退出”，应用程序将立\r\n即关闭。如果需要向开发者报告这个错误，请点" +
    "“报告错误”。";
            // 
            // 修复后重启ToolStripMenuItem
            // 
            this.修复后重启ToolStripMenuItem.Name = "修复后重启ToolStripMenuItem";
            this.修复后重启ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.修复后重启ToolStripMenuItem.Text = "修复后重启";
            this.修复后重启ToolStripMenuItem.Click += new System.EventHandler(this.修复后重启ToolStripMenuItem_Click);
            // 
            // 直接重启ToolStripMenuItem
            // 
            this.直接重启ToolStripMenuItem.Name = "直接重启ToolStripMenuItem";
            this.直接重启ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.直接重启ToolStripMenuItem.Text = "直接重启";
            this.直接重启ToolStripMenuItem.Click += new System.EventHandler(this.直接重启ToolStripMenuItem_Click);
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(425, 286);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.buttonIgnore);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.labelInformation);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionForm";
            this.Text = "Microsoft .NET Framework";
            this.Load += new System.EventHandler(this.ExceptionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStripExit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelInformation;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonIgnore;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripExit;
        private System.Windows.Forms.ToolStripMenuItem 直接退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修复后退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 直接重启ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修复后重启ToolStripMenuItem;
    }
}