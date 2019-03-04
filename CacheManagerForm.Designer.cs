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
            this.labelCacheContent = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClearCache = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelCacheContent
            // 
            this.labelCacheContent.AutoSize = true;
            this.labelCacheContent.Location = new System.Drawing.Point(13, 13);
            this.labelCacheContent.Name = "labelCacheContent";
            this.labelCacheContent.Size = new System.Drawing.Size(89, 12);
            this.labelCacheContent.TabIndex = 0;
            this.labelCacheContent.Text = "缓存文件内容：";
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(15, 29);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(485, 229);
            this.textBox.TabIndex = 1;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(187, 264);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(136, 35);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "保存修改";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClearCache
            // 
            this.buttonClearCache.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearCache.Location = new System.Drawing.Point(371, 264);
            this.buttonClearCache.Name = "buttonClearCache";
            this.buttonClearCache.Size = new System.Drawing.Size(129, 35);
            this.buttonClearCache.TabIndex = 3;
            this.buttonClearCache.Text = "清空缓存并提交";
            this.buttonClearCache.UseVisualStyleBackColor = true;
            this.buttonClearCache.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpenFile.Location = new System.Drawing.Point(15, 264);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(126, 35);
            this.buttonOpenFile.TabIndex = 4;
            this.buttonOpenFile.Text = "直接打开缓存文件";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // CacheManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 311);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.buttonClearCache);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.labelCacheContent);
            this.Name = "CacheManagerForm";
            this.Text = "缓存管理";
            this.Load += new System.EventHandler(this.CacheManagerForm_Load);
            this.DoubleClick += new System.EventHandler(this.CacheManagerForm_DoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCacheContent;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClearCache;
        private System.Windows.Forms.Button buttonOpenFile;
    }
}