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
    class ConfigUtil
    {
        // 单例使得Config只会读取一次
        private static ConfigUtil configuration = new ConfigUtil();
        private static ConfigUtil This { get { return ConfigUtil.configuration; } }
        private const String configName = "关机助手.config"; // Config文件名
        XmlDocument xml = new XmlDocument();

        #region 外部调用接口
        /* 外部调用接口只能获取而不能改变 */
        public static bool ConfigLoaded { get { return This.configLoaded; } } 
        public static String CacheFromPath { get { return This.cacheFromPath; } }
        public static String CacheToPath { get { return This.cacheToPath; } }
        public static bool CacheManagerAutoMerge { get { return This.cacheManagerAutoMerge; } }
        public static bool MainFormAutoDarkMode { get { return This.mainFormDark; } }
        #endregion

        #region 内部数据变量
        private bool configLoaded = false;
        public String cacheFromPath = "";
        public String cacheToPath = "";
        public bool cacheManagerAutoMerge = false;
        public bool mainFormDark = false;
        #endregion
        
        private ConfigUtil()
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
                if (root.SelectSingleNode("CacheManager") != null)
                {
                    XmlNode cacheManagerNode = root["CacheManager"];
                    this.cacheFromPath = cacheManagerNode["From"].InnerText;
                    this.cacheToPath = cacheManagerNode["To"].InnerText;
                    this.cacheManagerAutoMerge = cacheManagerNode["AutoMerge"].InnerText == "true";
                }
                // 加载Main相关配置
                if (root.SelectSingleNode("Main") != null)
                {
                    XmlNode cacheManagerNode = root["Main"];
                    this.mainFormDark = cacheManagerNode["DarkMode"].InnerText == "true";
                }
                // 加载完毕
                this.configLoaded = true;
            }
            catch (XPathException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="department">哪个部分（Form）使用该新属性</param>
        /// <param name="attributeKey">属性的键</param>
        /// <param name="attributeValue">属性的值</param>
        public static void Add(string department, string attributeKey, string attributeValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改属性
        /// </summary>
        /// <param name="department">要修改的属性在哪个部分</param>
        /// <param name="attributeKey">属性的键</param>
        /// <param name="attributeValue">属性的新值</param>
        public static void Change(string department, string attributeKey, string attributeValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="department">哪个部分（Form）删除该属性</param>
        /// <param name="attributeKey">属性的键</param>
        public static void Delete(string department, string attributeKey)
        {
            throw new NotImplementedException();
        }
    }
}
