using System;

namespace 关机助手
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.button返回 = new System.Windows.Forms.Button();
            this.textBoxUpdateLog = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelVersions = new System.Windows.Forms.Label();
            this.textBoxUpdateLogCommandVersion = new System.Windows.Forms.TextBox();
            this.labelCopyright = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button返回
            // 
            this.button返回.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button返回.Location = new System.Drawing.Point(366, 49);
            this.button返回.Name = "button返回";
            this.button返回.Size = new System.Drawing.Size(86, 33);
            this.button返回.TabIndex = 0;
            this.button返回.Text = "确定";
            this.button返回.UseVisualStyleBackColor = true;
            this.button返回.Click += new System.EventHandler(this.button返回_Click);
            // 
            // textBoxUpdateLog
            // 
            this.textBoxUpdateLog.AcceptsReturn = true;
            this.textBoxUpdateLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUpdateLog.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxUpdateLog.Location = new System.Drawing.Point(0, 125);
            this.textBoxUpdateLog.Multiline = true;
            this.textBoxUpdateLog.Name = "textBoxUpdateLog";
            this.textBoxUpdateLog.ReadOnly = true;
            this.textBoxUpdateLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxUpdateLog.Size = new System.Drawing.Size(473, 190);
            this.textBoxUpdateLog.TabIndex = 1;
            this.textBoxUpdateLog.Text = resources.GetString("textBoxUpdateLog.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::关机助手.Properties.Resources.icon_main1;
            this.pictureBox1.Location = new System.Drawing.Point(21, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(103, 95);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // labelVersions
            // 
            this.labelVersions.AutoSize = true;
            this.labelVersions.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelVersions.Location = new System.Drawing.Point(149, 9);
            this.labelVersions.Name = "labelVersions";
            this.labelVersions.Size = new System.Drawing.Size(217, 93);
            this.labelVersions.TabIndex = 3;
            this.labelVersions.Text = "关机助手:\r\n窗体版 v{Version}\r\n终端版 v1.6.1";
            // 
            // textBoxUpdateLogCommandVersion
            // 
            this.textBoxUpdateLogCommandVersion.AcceptsReturn = true;
            this.textBoxUpdateLogCommandVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUpdateLogCommandVersion.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxUpdateLogCommandVersion.Font = new System.Drawing.Font("幼圆", 10.5F);
            this.textBoxUpdateLogCommandVersion.Location = new System.Drawing.Point(0, 323);
            this.textBoxUpdateLogCommandVersion.Multiline = true;
            this.textBoxUpdateLogCommandVersion.Name = "textBoxUpdateLogCommandVersion";
            this.textBoxUpdateLogCommandVersion.ReadOnly = true;
            this.textBoxUpdateLogCommandVersion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxUpdateLogCommandVersion.Size = new System.Drawing.Size(473, 78);
            this.textBoxUpdateLogCommandVersion.TabIndex = 2;
            this.textBoxUpdateLogCommandVersion.Text = resources.GetString("textBoxUpdateLogCommandVersion.Text");
            // 
            // labelCopyright
            // 
            this.labelCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCopyright.Location = new System.Drawing.Point(26, 404);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(424, 20);
            this.labelCopyright.TabIndex = 4;
            this.labelCopyright.Text = "©2016-{Year} William Technology Co.,Ltd. All Rights Reserved.";
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 427);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.textBoxUpdateLogCommandVersion);
            this.Controls.Add(this.labelVersions);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBoxUpdateLog);
            this.Controls.Add(this.button返回);
            this.Name = "HelpForm";
            this.Text = "关于 关机助手";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHelp_FormClosing);
            this.Load += new System.EventHandler(this.HelpForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button返回;
        private System.Windows.Forms.TextBox textBoxUpdateLog;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelVersions;
        private System.Windows.Forms.TextBox textBoxUpdateLogCommandVersion;
        private System.Windows.Forms.Label labelCopyright;
    }
}