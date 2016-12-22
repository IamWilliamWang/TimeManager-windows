namespace 关机小程序
{
    partial class FormHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHelp));
            this.button返回 = new System.Windows.Forms.Button();
            this.textBoxUpdateLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button返回
            // 
            this.button返回.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button返回.Location = new System.Drawing.Point(12, 176);
            this.button返回.Name = "button返回";
            this.button返回.Size = new System.Drawing.Size(358, 57);
            this.button返回.TabIndex = 0;
            this.button返回.Text = "返回";
            this.button返回.UseVisualStyleBackColor = true;
            this.button返回.Click += new System.EventHandler(this.button返回_Click);
            // 
            // textBoxUpdateLog
            // 
            this.textBoxUpdateLog.AcceptsReturn = true;
            this.textBoxUpdateLog.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxUpdateLog.Location = new System.Drawing.Point(13, 13);
            this.textBoxUpdateLog.Multiline = true;
            this.textBoxUpdateLog.Name = "textBoxUpdateLog";
            this.textBoxUpdateLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxUpdateLog.Size = new System.Drawing.Size(358, 146);
            this.textBoxUpdateLog.TabIndex = 1;
            this.textBoxUpdateLog.Text = resources.GetString("textBoxUpdateLog.Text");
            // 
            // FormHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 245);
            this.Controls.Add(this.textBoxUpdateLog);
            this.Controls.Add(this.button返回);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormHelp";
            this.Text = "FormHelp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHelp_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button返回;
        private System.Windows.Forms.TextBox textBoxUpdateLog;
    }
}