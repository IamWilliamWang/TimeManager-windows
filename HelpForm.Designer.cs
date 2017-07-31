namespace 关机小程序
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
            this.label1 = new System.Windows.Forms.Label();
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
            this.textBoxUpdateLog.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxUpdateLog.Location = new System.Drawing.Point(12, 113);
            this.textBoxUpdateLog.Multiline = true;
            this.textBoxUpdateLog.Name = "textBoxUpdateLog";
            this.textBoxUpdateLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxUpdateLog.Size = new System.Drawing.Size(449, 224);
            this.textBoxUpdateLog.TabIndex = 1;
            this.textBoxUpdateLog.Text = resources.GetString("textBoxUpdateLog.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::关机小程序.Properties.Resources.icon_main1;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(98, 95);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(116, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "关机小程序 v3.1.4";
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 349);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBoxUpdateLog);
            this.Controls.Add(this.button返回);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpForm";
            this.Text = "关于 关机小程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHelp_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button返回;
        private System.Windows.Forms.TextBox textBoxUpdateLog;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}