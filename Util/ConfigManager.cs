using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace 关机助手.Util
{
    class ConfigManager
    {
        // 单例使得Config只会读取一次
        private static readonly ConfigManager This = new ConfigManager();
        private const String configName = "关机助手.config"; // Config文件名
        XmlDocument xml = new XmlDocument();
        CacheManagerConfig cacheManagerConfig = new CacheManagerConfig();
        MainConfig mainConfig = new MainConfig();

        #region 内部数据变量
        private class CacheManagerConfig
        {
            internal bool configLoaded = false;
            internal String cacheFromPath = "";
            internal String cacheToPath = "";
            internal bool cacheAutoMerge = false;
        }
        private class MainConfig
        {
            internal bool configLoaded = false;
            internal bool mainDark = false;
            internal bool mainHideInTaskbar = false;
            internal bool mainHideNotifyIcon = false;
            internal int mainAutoShutdownSeconds = -1;
            internal int mainOpacity = -1;
        }
        #endregion

        #region 外部调用接口
        /* 外部调用接口只能获取而不能改变 */
        /// <summary>
        /// CacheManager配置是否加载完毕
        /// </summary>
        public static bool CacheManagerConfigLoaded { get { return This.cacheManagerConfig.configLoaded; } }
        /// <summary>
        /// 配置文件中CacheManager的源文件名
        /// </summary>
        public static String CacheManagerFromPath {
            get {
                if (!This.cacheManagerConfig.configLoaded)  throw new MethodAccessException("Please check configLoaded before call of this function!");
                return This.cacheManagerConfig.cacheFromPath;
            }
        }
        /// <summary>
        /// 配置文件中CacheManager的目标文件名
        /// </summary>
        public static String CacheManagerToPath {
            get {
                if (!This.cacheManagerConfig.configLoaded) throw new MethodAccessException("Please check configLoaded before call of this function!");
                return This.cacheManagerConfig.cacheToPath;
            }
        }
        /// <summary>
        /// CacheManager是否需要自动合并
        /// </summary>
        public static bool CacheManagerAutoMerge {
            get {
                if (!This.cacheManagerConfig.configLoaded) throw new MethodAccessException("Please check configLoaded before call of this function!");
                return This.cacheManagerConfig.cacheAutoMerge;
            }
        }
        /// <summary>
        /// MainForm的配置是否加载完毕
        /// </summary>
        public static bool MainFormConfigLoaded { get { return This.mainConfig.configLoaded; } }
        /// <summary>
        /// MainForm是否设置为DarkMode
        /// </summary>
        public static bool MainFormAutoDarkMode {
            get {
                if (!This.mainConfig.configLoaded) throw new MethodAccessException("Please check configLoaded before call of this function!");
                return This.mainConfig.mainDark;
            }
        }
        /// <summary>
        /// MainForm自动关机秒，如果未找到相关配置则返回-1
        /// </summary>
        public static int MainFormAutoShutdownSeconds {
            get {
                if (!This.mainConfig.configLoaded) throw new MethodAccessException("Please check configLoaded before call of this function!");
                return This.mainConfig.mainAutoShutdownSeconds;
            }
        }
        /// <summary>
        /// MainForm的透明度，如果未找到相关配置则返回-1
        /// </summary>
        public static int MainFormOpacity {
            get {
                if (!This.mainConfig.configLoaded) throw new MethodAccessException("Please check configLoaded before call of this function!");
                return This.mainConfig.mainOpacity;
            }
        }
        /// <summary>
        /// MainForm是否隐藏在任务栏中的图标
        /// </summary>
        public static bool MainFormHideInTaskbar {
            get {
                if (!This.mainConfig.configLoaded) throw new MethodAccessException("Please check configLoaded before call of this function!");
                return This.mainConfig.mainHideInTaskbar;
            }
        }
        /// <summary>
        /// MainForm是否隐藏在右下角的图标
        /// </summary>
        public static bool MainFormHideNotifyIcon {
            get {
                if (!This.mainConfig.configLoaded) throw new MethodAccessException("Please check configLoaded before call of this function!");
                return This.mainConfig.mainHideNotifyIcon;
            }
        }
        /// <summary>
        /// 获得配置文件的原文，无配置文件则返回""
        /// </summary>
        public static string RawText { get { if (!File.Exists(configName)) return ""; else return File.ReadAllText(configName); } }
        #endregion

        private bool Contains(XmlNode parent, string childName)
        {
            return parent.SelectSingleNode(childName) != null;
        }

        /// <summary>
        /// 初始化配置文件（范例）
        /// </summary>
        /// <returns></returns>
        public static bool InitConfigFile()
        {
            File.WriteAllText(configName, Properties.Resources.ConfigExample);
            return true;
        }

        /// <summary>
        /// 加载所有属性
        /// </summary>
        private ConfigManager()
        {
            try
            {
                xml.Load(configName);
            }
            catch (IOException)
            {
                return;
            }
            try
            {
                // 选择根节点
                XmlNode root = xml.SelectSingleNode("/Config");
                // 加载CacheManager相关配置
                if (Contains(root, "CacheManager"))
                {
                    bool emptyConfig = true;
                    XmlNode cacheManagerNode = root["CacheManager"];
                    if (Contains(cacheManagerNode, "From"))
                    {
                        this.cacheManagerConfig.cacheFromPath = cacheManagerNode["From"].InnerText;
                        emptyConfig = false;
                    }
                    if (Contains(cacheManagerNode, "To"))
                    {
                        this.cacheManagerConfig.cacheToPath = cacheManagerNode["To"].InnerText;
                        emptyConfig = false;
                    }
                    if (Contains(cacheManagerNode, "AutoMerge"))
                    {
                        this.cacheManagerConfig.cacheAutoMerge = cacheManagerNode["AutoMerge"].InnerText == "true";
                        emptyConfig = false;
                    }
                    this.cacheManagerConfig.configLoaded = !emptyConfig;
                }
                // 加载Main相关配置
                if (Contains(root, "Main"))
                {
                    bool emptyConfig = true;
                    XmlNode mainNode = root["Main"];
                    if (Contains(mainNode, "DarkMode"))
                    {
                        this.mainConfig.mainDark = mainNode["DarkMode"].InnerText == "true";
                        emptyConfig = false;
                    }
                    if (Contains(mainNode, "AutoShutdownSeconds"))
                    {
                        try {
                            this.mainConfig.mainAutoShutdownSeconds = (int)float.Parse(mainNode["AutoShutdownSeconds"].InnerText);
                        }
                        catch { this.mainConfig.mainAutoShutdownSeconds = -1; }
                        emptyConfig = false;
                    }
                    if (Contains(mainNode, "Opacity"))
                    {
                        try {
                            this.mainConfig.mainOpacity = (int)float.Parse(mainNode["Opacity"].InnerText);
                        }
                        catch { this.mainConfig.mainOpacity = -1; }
                        emptyConfig = false;
                    }
                    if (Contains(mainNode, "HideInTaskbar"))
                    {
                        this.mainConfig.mainHideInTaskbar = mainNode["HideInTaskbar"].InnerText == "true";
                        emptyConfig = false;
                    }
                    if (Contains(mainNode, "HideNotifyIcon"))
                    {
                        this.mainConfig.mainHideNotifyIcon = mainNode["HideNotifyIcon"].InnerText == "true";
                        emptyConfig = false;
                    }
                    this.mainConfig.configLoaded = !emptyConfig;
                }
            }
            catch (XPathException e)
            {
                throw e;
            }
        }

        #region 属性操作(未实现)
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="department">哪个部分（Form）使用该新属性</param>
        /// <param name="attributeKey">属性的键</param>
        /// <param name="attributeValue">属性的值</param>
        public static void AttributeAdd(string department, string attributeKey, string attributeValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改属性
        /// </summary>
        /// <param name="department">要修改的属性在哪个部分</param>
        /// <param name="attributeKey">属性的键</param>
        /// <param name="attributeValue">属性的新值</param>
        public static void AttributeChange(string department, string attributeKey, string attributeValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="department">哪个部分（Form）删除该属性</param>
        /// <param name="attributeKey">属性的键</param>
        public static void AttributeDelete(string department, string attributeKey)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
