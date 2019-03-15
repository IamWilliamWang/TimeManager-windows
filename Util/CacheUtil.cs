using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关机助手.Util
{
    class CacheUtil
    {
        public static char CacheSpliter { get { return splitChar; } } //缓存分割字符
        public static string CacheFilename { get { return DbFilename; } } //缓存文件名
        
        private static string DbFilename { get; set; } = "TimeDatabase.cache";
        private const char splitChar = '鋝';

        /// <summary>
        /// 将'GETDATE()'变成当前时间字符串
        /// </summary>
        /// <param name="originalSql"></param>
        /// <returns></returns>
        private static string DateReplacer(string originalSql)
        {
            //区别对待系统时间显示上午下午与不显示的
            if (DateTime.Now.ToLongTimeString().Contains("午") == false)
                return originalSql.Replace("GETDATE()", "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
            return originalSql.Replace("GETDATE()", "'" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString() + "'");
        }

        /// <summary>
        /// 追加Cache内容
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="hideFile"></param>
        public static void AppendCache(string sql, bool hideFile = true)
        {
            AppendCache(sql, CacheUtil.DbFilename, hideFile);
        }

        /// <summary>
        /// 追加Cache内容，自定义Cache文件路径
        /// </summary>
        /// <param name="cacheFilename"></param>
        /// <param name="sql"></param>
        /// <param name="hideFile"></param>
        public static void AppendCache(string sql, string cacheFilename, bool hideFile = true)
        {
            sql = DateReplacer(sql);
            using (FileStream stream = new FileStream(cacheFilename, FileMode.Append))
                using (StreamWriter streamWriter = new StreamWriter(stream))
                    streamWriter.Write(sql + splitChar);
            if (hideFile)
                File.SetAttributes(cacheFilename, FileAttributes.Hidden);
        }

        /// <summary>
        /// 获得Cache每行文本
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllLines()
        {
            return GetAllLines(DbFilename);
        }

        /// <summary>
        /// 获得Cache每行文本，自定义Cache文件路径
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllLines(string cacheFilename)
        {
            if (File.Exists(cacheFilename) == false)
                return null;
            return File.ReadAllText(cacheFilename).Split(new[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 设定Cache每行文本
        /// </summary>
        /// <param name="lines"></param>
        public static void SetAllLines(string[] lines, bool hideCache = true)
        {
            SetAllLines(lines, CacheFilename, hideCache);
        }

        /// <summary>
        /// 设定Cache每行文本，自定义Cache文件路径
        /// </summary>
        /// <param name="lines"></param>
        public static void SetAllLines(string[] lines, string cacheFilename, bool hideCache = true)
        {
            StringBuilder savedContent = new StringBuilder();
            foreach (String line in lines)
            {
                if (line == "")
                    continue;
                savedContent.Append(line);
                savedContent.Append(splitChar);
            }
            File.Delete(cacheFilename);
            File.WriteAllText(cacheFilename, savedContent.ToString());
            if (hideCache)
                File.SetAttributes(cacheFilename, FileAttributes.Hidden);
        }

        /// <summary>
        /// 清除所有缓存并提交到数据库
        /// </summary>
        /// <returns></returns>
        public static int CleanDbAndExecuteTasks()
        {
            return CleanDbAndExecuteTasks(DbFilename);
        }

        /// <summary>
        /// 清除所有缓存并提交到数据库，自定义Cache文件路径
        /// </summary>
        /// <returns></returns>
        public static int CleanDbAndExecuteTasks(string cacheFilename)
        {
            int effectedRows = 0;
            string[] commands = GetAllLines(cacheFilename);
            if (commands == null)
                return -1;

            foreach (string str in commands)
            {
                int tmp = SqlServerConnection.ExecuteUpdate(str);
                effectedRows += tmp > 0 ? tmp : 0;
            }
            File.Delete(cacheFilename);
            return effectedRows;
        }

        //public delegate void CacheRowsExecutedEventHandler(object sender, EventArgs e);
        //public event CacheRowsExecutedEventHandler CacheRowsExecutedEvent;
    }
}
