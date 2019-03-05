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
        public static char CacheSpliter { get { return splitChar; } }
        public static string CacheFilename { get { return DbFilename; } }
        
        private static string DbFilename { get; set; } = "TimeDatabase.cache";
        private const char splitChar = '鋝';

        private static string DateReplacer(string str)
        {
            //区别对待系统时间显示上午下午与不显示的
            if (DateTime.Now.ToLongTimeString().Contains("午") == false)
                return str.Replace("GETDATE()", "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
            return str.Replace("GETDATE()", "'" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString() + "'");
        }

        public static void Insert(string str)
        {
            str = DateReplacer(str);
            using (FileStream stream = new FileStream(DbFilename, FileMode.Append))
            using (StreamWriter streamWriter = new StreamWriter(stream))
                streamWriter.Write(str + splitChar);

            File.SetAttributes(DbFilename, FileAttributes.Hidden);
        }

        public static string[] GetAllLines()
        {
            if (File.Exists(DbFilename) == false)
                return null;
            return File.ReadAllText(DbFilename).Split(new[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static void SetAllLines(string[] lines)
        {
            StringBuilder savedContent = new StringBuilder();
            foreach (String line in lines)
            {
                if (line == "")
                    continue;
                savedContent.Append(line);
                savedContent.Append(splitChar);
            }
            File.Delete(CacheFilename);
            File.WriteAllText(CacheFilename, savedContent.ToString());
            File.SetAttributes(CacheFilename, FileAttributes.Hidden);
        }

        public static int CleanDbAndExecuteTasks()
        {
            int effectedRows = 0;
            string[] commands = GetAllLines();
            if (commands == null)
                return -1;

            foreach (string str in commands)
            {
                int tmp = SqlServerConnection.ExecuteUpdate(str);
                effectedRows += tmp > 0 ? tmp : 0;
            }
            File.Delete(DbFilename);
            return effectedRows;
        }

        //public delegate void CacheRowsExecutedEventHandler(object sender, EventArgs e);
        //public event CacheRowsExecutedEventHandler CacheRowsExecutedEvent;
    }
}
