using System;
using System.Windows.Forms;

namespace 关机助手
{
    public partial class ConfigurationForm : Form
    {
        public ConfigurationForm()
        {
            InitializeComponent();
        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            // 如果没有配置文件，就生成一个
            if (Util.ConfigManager.RawText == "")
                if (DialogResult.Yes == MessageBox.Show("检测到您没有配置文件，是否生成范例？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    if (Util.ConfigManager.InitConfigFile())
                        MessageBox.Show("配置文件生成完毕，请将括号处替换至有效值！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // MainFormAutoDarkMode
            this.listBoxName.Items.Add("主界面自动开启黑暗模式");
            if (Util.ConfigManager.MainFormConfigLoaded)
                this.listBoxValue.Items.Add(Util.ConfigManager.MainFormAutoDarkMode ? "自动开启" : "不自动开启");
            else
                this.listBoxValue.Items.Add("不自动开启");
            // MainFormHideInTaskbar
            this.listBoxName.Items.Add("主界面自动脱离任务栏控制");
            if (Util.ConfigManager.MainFormConfigLoaded)
                this.listBoxValue.Items.Add(Util.ConfigManager.MainFormHideInTaskbar ? "自动脱离" : "不自动脱离");
            else
                this.listBoxValue.Items.Add("不自动脱离");
            // MainFormHideNotifyIcon
            this.listBoxName.Items.Add("主界面自动隐藏右下角图标");
            if (Util.ConfigManager.MainFormConfigLoaded)
                this.listBoxValue.Items.Add(Util.ConfigManager.MainFormHideNotifyIcon ? "自动隐藏" : "不自动隐藏");
            else
                this.listBoxValue.Items.Add("不自动隐藏");
            // MainDefaultComboBoxIndex
            this.listBoxName.Items.Add("首页的模式选择默认为第几个");
            if (Util.ConfigManager.MainFormConfigLoaded)
            {
                int index = Util.ConfigManager.MainDefaultComboBoxIndex;
                this.listBoxValue.Items.Add(index == -1 ? "未设置" : (index + 1).ToString());
            }
            else
                this.listBoxValue.Items.Add("未设置");
            // MainFormOpacity
            this.listBoxName.Items.Add("主界面透明度");
            if (Util.ConfigManager.MainFormConfigLoaded)
            {
                int opacity = Util.ConfigManager.MainFormOpacity;
                this.listBoxValue.Items.Add(opacity == -1 ? "未设置" : opacity.ToString());
            }
            else
                this.listBoxValue.Items.Add("未设置");
            // MainFormAutoShutdownSeconds
            this.listBoxName.Items.Add("启动程序后几秒后执行关机");
            if (Util.ConfigManager.MainFormConfigLoaded)
            {
                int seconds = Util.ConfigManager.MainFormAutoShutdownSeconds;
                this.listBoxValue.Items.Add(seconds == -1 ? "未设置" : seconds.ToString());
            }
            else
                this.listBoxValue.Items.Add("未设置");
            // CacheManagerFromPath
            this.listBoxName.Items.Add("日志管理器默认合并源文件");
            if (Util.ConfigManager.CacheManagerConfigLoaded)
                this.listBoxValue.Items.Add(Util.ConfigManager.CacheManagerFromPath == "" ? "未设置" : Util.ConfigManager.CacheManagerFromPath);
            else
                this.listBoxValue.Items.Add("未设置");
            // CacheManagerToPath
            this.listBoxName.Items.Add("日志管理器默认合并目标文件");
            if (Util.ConfigManager.CacheManagerConfigLoaded)
                this.listBoxValue.Items.Add(Util.ConfigManager.CacheManagerToPath == "" ? "未设置" : Util.ConfigManager.CacheManagerToPath);
            else
                this.listBoxValue.Items.Add("未设置");
            // CacheManagerAutoMerge
            this.listBoxName.Items.Add("日志管理器自动执行合并");
            if (Util.ConfigManager.CacheManagerConfigLoaded)
                this.listBoxValue.Items.Add(Util.ConfigManager.CacheManagerAutoMerge ? "自动合并" : "不自动合并");
            else
                this.listBoxValue.Items.Add("未设置");
        }

        private void 配置文件格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.Resources.ConfigExample);
        }
    }
}
