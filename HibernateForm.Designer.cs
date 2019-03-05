namespace 关机助手
{
    partial class HibernateForm
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
            if (disposing && ( components != null ))
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
            this.label1 = new System.Windows.Forms.Label();
            this.button启用 = new System.Windows.Forms.Button();
            this.button禁用 = new System.Windows.Forms.Button();
            this.button获得系统授权 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "系统休眠功能状态：";
            // 
            // button启用
            // 
            this.button启用.Location = new System.Drawing.Point(217, 11);
            this.button启用.Name = "button启用";
            this.button启用.Size = new System.Drawing.Size(75, 23);
            this.button启用.TabIndex = 1;
            this.button启用.Text = "启用";
            this.button启用.UseVisualStyleBackColor = true;
            this.button启用.Click += new System.EventHandler(this.button启用_Click);
            // 
            // button禁用
            // 
            this.button禁用.Location = new System.Drawing.Point(298, 11);
            this.button禁用.Name = "button禁用";
            this.button禁用.Size = new System.Drawing.Size(75, 23);
            this.button禁用.TabIndex = 2;
            this.button禁用.Text = "禁用";
            this.button禁用.UseVisualStyleBackColor = true;
            this.button禁用.Click += new System.EventHandler(this.button禁用_Click);
            // 
            // button获得系统授权
            // 
            this.button获得系统授权.Enabled = false;
            this.button获得系统授权.Location = new System.Drawing.Point(379, 11);
            this.button获得系统授权.Name = "button获得系统授权";
            this.button获得系统授权.Size = new System.Drawing.Size(89, 23);
            this.button获得系统授权.TabIndex = 3;
            this.button获得系统授权.Text = "获得系统授权";
            this.button获得系统授权.UseVisualStyleBackColor = true;
            this.button获得系统授权.Click += new System.EventHandler(this.button获得系统授权_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 4;
            // 
            // HibernateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 44);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button获得系统授权);
            this.Controls.Add(this.button禁用);
            this.Controls.Add(this.button启用);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "HibernateForm";
            this.Text = "系统休眠功能";
            this.Load += new System.EventHandler(this.HibernateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button启用;
        private System.Windows.Forms.Button button禁用;
        private System.Windows.Forms.Button button获得系统授权;
        private System.Windows.Forms.Label label2;
    }
}