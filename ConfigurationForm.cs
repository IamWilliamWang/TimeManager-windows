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
            // AutoShutdownSeconds
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
                this.listBoxValue.Items.Add(Util.ConfigManager.CacheManagerAutoMerge ? "是" : "否");
            else
                this.listBoxValue.Items.Add("未设置");
        }
    }
}
