namespace 关机助手
{
    partial class ConfigurationForm
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
            this.listBoxName = new System.Windows.Forms.ListBox();
            this.listBoxValue = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxName
            // 
            this.listBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxName.Font = new System.Drawing.Font("新宋体", 13F);
            this.listBoxName.FormattingEnabled = true;
            this.listBoxName.ItemHeight = 17;
            this.listBoxName.Location = new System.Drawing.Point(12, 12);
            this.listBoxName.Name = "listBoxName";
            this.listBoxName.Size = new System.Drawing.Size(244, 429);
            this.listBoxName.TabIndex = 0;
            // 
            // listBoxValue
            // 
            this.listBoxValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxValue.Font = new System.Drawing.Font("新宋体", 13F);
            this.listBoxValue.FormattingEnabled = true;
            this.listBoxValue.ItemHeight = 17;
            this.listBoxValue.Location = new System.Drawing.Point(262, 12);
            this.listBoxValue.Name = "listBoxValue";
            this.listBoxValue.Size = new System.Drawing.Size(328, 429);
            this.listBoxValue.TabIndex = 1;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 449);
            this.Controls.Add(this.listBoxValue);
            this.Controls.Add(this.listBoxName);
            this.Name = "ConfigurationForm";
            this.Text = "配置文件显示";
            this.Load += new System.EventHandler(this.ConfigurationForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxName;
        private System.Windows.Forms.ListBox listBoxValue;
    }
}