using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 关机助手.Util
{
    public class InputBoxFormInner : Form
    {
        /****** InputBoxForm.designer.cs ******/
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
            this.labelContent = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelContent
            // 
            this.labelContent.AutoSize = true;
            this.labelContent.Location = new System.Drawing.Point(12, 12);
            this.labelContent.Name = "labelContent";
            this.labelContent.Size = new System.Drawing.Size(35, 12);
            this.labelContent.TabIndex = 0;
            this.labelContent.Text = "label";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 82);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(449, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(378, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button确定_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(378, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button取消_Click);
            // 
            // InputBoxFormInner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 114);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InputBoxFormInner";
            this.Text = "InputBoxForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelContent;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;


        

        /****** InputBoxForm.cs ******/
        public string BoxText { get; set; }
        public int CharCountPerLine { get; set; } = 30;
        public string Title { get { return this.Text; } set { this.Text = value; } }
        public string HeaderText { get { return this.labelContent.Text; } set { this.labelContent.Text = value; } }
        public string DefaultText { set { this.textBox1.Text = value;this.BoxText = value; } }
        public InputBoxFormInner(string title, string content)
        {
            InitializeComponent();
            this.Text = title;
            this.labelContent.Text = AddNewline(content);
        }

        private string AddNewline(string content)
        {
            StringBuilder stringBuilder = new StringBuilder(content);
            for (int i = CharCountPerLine * (int)(content.Length / CharCountPerLine); i >= 0; i -= CharCountPerLine)
                stringBuilder.Insert(i, "\r\n");
            return stringBuilder.ToString();
        }

        private void button确定_Click(object sender, EventArgs e)
        {
            this.BoxText = this.textBox1.Text;
            this.Close();
        }

        private void button取消_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                this.button确定_Click(sender, e);
        }
        
    }

    class Interaction
    {
        public static string InputBox(string content, string title="输入", int charCountPerline=30, string defaultReturn = "")
        {
            var inputBox = new InputBoxFormInner(title, content);
            inputBox.BoxText = defaultReturn;
            inputBox.CharCountPerLine = charCountPerline;
            inputBox.ShowDialog();
            return inputBox.BoxText;
        }
    }
}
