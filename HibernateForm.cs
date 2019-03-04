using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 关机助手.Util;

namespace 关机助手
{
    public partial class HibernateForm : Form
    {
        private bool state;
        public delegate void HibernateStateChangedEventHandler(bool state);
        public event HibernateStateChangedEventHandler HibernateStateChangedEvent;

        public HibernateForm()
        {
            InitializeComponent();
            this.HibernateStateChangedEvent += new HibernateStateChangedEventHandler(changeHibernateState);
            this.HibernateStateChangedEvent += new HibernateStateChangedEventHandler(changeButtonsEnabled);
        }

        private void changeHibernateState(bool newState)
        {
            this.state = newState;
            if (newState)
                this.label2.Text = "已启用";
            else
                this.label2.Text = "已禁用";
        }

        private void changeButtonsEnabled(bool newState)
        {
            if(newState)
            {
                this.button启用.Enabled = false;
                this.button禁用.Enabled = true;
            }
            else
            {
                this.button禁用.Enabled = false;
                this.button启用.Enabled = true;
            }
        }

        private void HibernateForm_Load(object sender, EventArgs e)
        {
            bool? state = checkHibernateState();
            if(state == null)
            {
                MessageBox.Show("无法检测系统的休眠是否开启", "无法检测", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            this.HibernateStateChangedEvent(state ?? false);
            
        }

        private bool? checkHibernateState()
        {
            string detackRestult = Util.SystemCommandUtil.ExecuteCommand("powercfg -a");
            int index1 = detackRestult.IndexOf("快速启动");
            int index2 = detackRestult.IndexOf("此系统上没有以下睡眠状态");
            if (index1 == -1 || index2 == -1)
                return null;
            if (index1 < index2)
                return true;
            else
                return false;
        }

        private void button启用_Click(object sender, EventArgs e)
        {
            bool oldState = this.state;
            if (SystemCommandUtil.ExecuteCommand("powercfg /HIBERNATE on").IndexOf("无法执行操作") == -1)
            {
                if(this.checkHibernateState() == oldState)
                {
                    this.button获得系统授权.Enabled = true;
                    if (DialogResult.OK == MessageBox.Show("尝试失败，需要获得管理员权限，是否允许并重启本程序？", "失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
                    {
                        MainForm.restartWithAdminRight(true);
                    }
                }
                else
                {
                    MessageBox.Show("已激活完成。", "激活休眠功能", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.HibernateStateChangedEvent(!oldState);
                }
            }
            else
                MessageBox.Show("激活休眠失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button禁用_Click(object sender, EventArgs e)
        {
            var oldState = this.state;
            if (SystemCommandUtil.ExecuteCommand("powercfg /HIBERNATE off").IndexOf("无法执行操作") == -1)
            {
                if (this.checkHibernateState() == oldState)
                {
                    this.button获得系统授权.Enabled = true;
                    if (DialogResult.OK == MessageBox.Show("尝试失败，需要获得管理员权限，是否允许并重启本程序？", "失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
                    {
                        MainForm.restartWithAdminRight(true);
                    }
                }
                else
                {
                    MessageBox.Show("已禁用完成。", "禁用休眠功能", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.HibernateStateChangedEvent(!oldState);
                }
            }
            else
                MessageBox.Show("禁用休眠失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button获得系统授权_Click(object sender, EventArgs e)
        {
            MainForm.restartWithAdminRight(true);
        }
    }
    
}
