using System;
using System.IO;

namespace 关机助手.Util
{
    class LogError
    {
        public static readonly String LogFileName = "ErrorLog.txt";
        public static bool Log(String logContent, Exception exception = null)
        {
            try
            {
                using (FileStream stream = new FileStream(LogFileName, FileMode.Append))
                {
                    StreamWriter writer = new StreamWriter(stream);
                    writer.WriteLine("********" + DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒") + "********");
                    writer.WriteLine("错误信息：" + logContent);
                    writer.WriteLine("详细信息：");
                    writer.WriteLine(exception);
                    writer.WriteLine();
                    writer.Flush();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
