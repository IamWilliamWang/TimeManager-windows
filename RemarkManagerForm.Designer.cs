namespace 关机助手
{
    partial class RemarkManagerForm
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
            this.dataGridViewDatas = new System.Windows.Forms.DataGridView();
            this.dataGridViewRemarks = new System.Windows.Forms.DataGridView();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.textBoxContent = new System.Windows.Forms.TextBox();
            this.labelId = new System.Windows.Forms.Label();
            this.labelContent = new System.Windows.Forms.Label();
            this.button提交修改 = new System.Windows.Forms.Button();
            this.button提交添加 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRemarks)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewDatas
            // 
            this.dataGridViewDatas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDatas.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewDatas.Name = "dataGridViewDatas";
            this.dataGridViewDatas.RowTemplate.Height = 23;
            this.dataGridViewDatas.Size = new System.Drawing.Size(471, 320);
            this.dataGridViewDatas.TabIndex = 0;
            // 
            // dataGridViewRemarks
            // 
            this.dataGridViewRemarks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRemarks.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewRemarks.Name = "dataGridViewRemarks";
            this.dataGridViewRemarks.RowTemplate.Height = 23;
            this.dataGridViewRemarks.Size = new System.Drawing.Size(471, 320);
            this.dataGridViewRemarks.TabIndex = 3;
            // 
            // textBoxId
            // 
            this.textBoxId.Location = new System.Drawing.Point(503, 28);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(133, 21);
            this.textBoxId.TabIndex = 4;
            // 
            // textBoxContent
            // 
            this.textBoxContent.Location = new System.Drawing.Point(503, 68);
            this.textBoxContent.Multiline = true;
            this.textBoxContent.Name = "textBoxContent";
            this.textBoxContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxContent.Size = new System.Drawing.Size(133, 196);
            this.textBoxContent.TabIndex = 5;
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(501, 14);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(143, 12);
            this.labelId.TabIndex = 6;
            this.labelId.Text = "要添加/修改的注释序号：";
            // 
            // labelContent
            // 
            this.labelContent.AutoSize = true;
            this.labelContent.Location = new System.Drawing.Point(501, 53);
            this.labelContent.Name = "labelContent";
            this.labelContent.Size = new System.Drawing.Size(65, 12);
            this.labelContent.TabIndex = 7;
            this.labelContent.Text = "注释内容：";
            // 
            // button提交修改
            // 
            this.button提交修改.Location = new System.Drawing.Point(503, 322);
            this.button提交修改.Name = "button提交修改";
            this.button提交修改.Size = new System.Drawing.Size(133, 35);
            this.button提交修改.TabIndex = 8;
            this.button提交修改.Text = "确认修改";
            this.button提交修改.UseVisualStyleBackColor = true;
            this.button提交修改.Click += new System.EventHandler(this.button提交_Click);
            // 
            // button提交添加
            // 
            this.button提交添加.Location = new System.Drawing.Point(503, 281);
            this.button提交添加.Name = "button提交添加";
            this.button提交添加.Size = new System.Drawing.Size(133, 35);
            this.button提交添加.TabIndex = 9;
            this.button提交添加.Text = "确认添加";
            this.button提交添加.UseVisualStyleBackColor = true;
            this.button提交添加.Click += new System.EventHandler(this.button提交添加_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(485, 352);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridViewRemarks);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(477, 326);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "所有注释";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewDatas);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(477, 326);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "原始数据";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // RemarkManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 369);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button提交添加);
            this.Controls.Add(this.button提交修改);
            this.Controls.Add(this.labelContent);
            this.Controls.Add(this.labelId);
            this.Controls.Add(this.textBoxContent);
            this.Controls.Add(this.textBoxId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RemarkManagerForm";
            this.Text = "注释管理器";
            this.Load += new System.EventHandler(this.RemarkManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRemarks)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDatas;
        private System.Windows.Forms.DataGridView dataGridViewRemarks;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.TextBox textBoxContent;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label labelContent;
        private System.Windows.Forms.Button button提交修改;
        private System.Windows.Forms.Button button提交添加;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}