using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关机助手.Util
{
    class Cache
    {
        public static char CacheSpliter { get { return splitChar; } } //缓存分割字符
        public static string CacheFilename { get { return dbFilename; } set {
                if (value.Contains("\\")) dbFilename = value.Substring(value.LastIndexOf("\\") + 1);
                else dbFilename = value;
            } } //缓存文件名
        
        private static string dbFilename = "TimeDatabase.cache";
        private const char splitChar = '鋝';

        public static BackupCreater Backup { get; set; }
         = new BackupCreater(Cache.CacheFilename, 备份后缀名:".cache.backup", interval: int.MaxValue, hideBackup: true);
        /// <summary>
        /// 将'GETDATE()'变成当前时间字符串
        /// </summary>
        /// <param name="originalSql"></param>
        /// <returns></returns>
        private static string ReplaceGETDATE(string originalSql)
        {
            //区别对待系统时间显示上午下午与不显示的
            if (DateTime.Now.ToLongTimeString().Contains("午") == false)
                return originalSql.Replace("GETDATE()", "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
            return originalSql.Replace("GETDATE()", "'" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString() + "'");
        }

        public static bool ExistCache(string cacheFileName = "TimeDatabase.cache")
        {
            return File.Exists(cacheFileName);
        }
        
        /// <summary>
        /// 追加Cache内容
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="hideFile"></param>
        public static void AppendCache(string sql, bool hideFile = true)
        {
            AppendCache(sql, Cache.CacheFilename, hideFile);
        }

        /// <summary>
        /// 追加Cache内容，自定义Cache文件路径
        /// </summary>
        /// <param name="cacheFilename"></param>
        /// <param name="sql"></param>
        /// <param name="hideFile"></param>
        public static void AppendCache(string sql, string cacheFilename, bool hideFile = true)
        {
            sql = ReplaceGETDATE(sql);
            using (FileStream stream = new FileStream(cacheFilename, FileMode.Append))
                using (StreamWriter streamWriter = new StreamWriter(stream))
                    streamWriter.Write(sql + splitChar);
            if (hideFile)
                File.SetAttributes(cacheFilename, FileAttributes.Hidden);
        }

        /// <summary>
        /// 获得Cache每行文本，如果找不到文件则返回null
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllLines()
        {
            return GetAllLines(CacheFilename);
        }

        /// <summary>
        /// 获得Cache每行文本，如果找不到文件则返回null，可自定义Cache文件路径
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllLines(string cacheFilename)
        {
            if (File.Exists(cacheFilename) == false)
                return null;
            string[] result = File.ReadAllText(cacheFilename).Split(new[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);
            return result.Length != 0 ? result : null;
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
        /// 备份一次Cache文件
        /// </summary>
        /// <param name="cacheFilename"></param>
        public static bool BackupMyCache(string cacheFilename)
        {
            if (File.Exists(Cache.Backup.Original文件名) == false)
                return false;
            Backup.Original文件名 = cacheFilename;
            Backup.StartOnce();
            return true;
        }

        /// <summary>
        /// 清理所有Cache文件
        /// </summary>
        public static void CleanBackupCache()
        {
            if (File.Exists(Cache.Backup.Backup文件名))
                Backup.DeleteBackup();
        }

        /// <summary>
        /// 清除所有缓存并提交到数据库
        /// </summary>
        /// <returns></returns>
        public static int CleanDbAndExecuteTasks()
        {
            return CleanDbAndExecuteTasks(CacheFilename);
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
            try
            {
                effectedRows = SqlServerConnection.ExecuteSqlWithGoUseTran(commands);
                File.Delete(cacheFilename);
                return effectedRows;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
