using System;
using System.Diagnostics;
using System.IO;

namespace 关机助手.Util
{
    public class ConsoleWriter
    {
        private static bool consoleOpenned = false;
        private static String printerFullFilename = "关机助手(终端版).exe";

        /// <summary>
        /// 开始向控制台输出
        /// </summary>
        public static void OpenWrite()
        {
            if (consoleOpenned)
                return;
            if (BinaryWriterUtil.WriteFileToDisk(Properties.Resources.printer, printerFullFilename))
                consoleOpenned = true;

        }

        /// <summary>
        /// 停止向控制台输出
        /// </summary>
        public static void CloseWrite()
        {
            if (!consoleOpenned)
                return;
            if (BinaryWriterUtil.RemoveFileFromDisk(printerFullFilename))
                consoleOpenned = false;
        }

        /// <summary>
        /// 输出单行字符串
        /// </summary>
        /// <param name="str">要输出到终端的字符串</param>
        public static void WriteLine(String str)
        {
            WriteLines(new String[] { str });
        }

        /// <summary>
        /// 输出多行字符串
        /// </summary>
        /// <param name="strings">要输出的字符串数组</param>
        public static void WriteLines(String[] strings)
        {
            if (strings == null)
                return;

            if (consoleOpenned == false)
                OpenWrite();
            
            Process process = new Process();
            process.StartInfo.FileName = Directory.GetCurrentDirectory()+"\\"+ printerFullFilename;
            //process.StartInfo.UseShellExecute = false;
            //process.StartInfo.RedirectStandardInput = true;
            //process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process.StartInfo.Arguments = ConcatStr(strings);
            try
            {
                process.Start();
            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.ToString(), "错误信息", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            process.WaitForExit();
            CloseWrite();
        }

        /// <summary>
        /// 将输出串转换成打印程序的输入参数的字符串
        /// </summary>
        /// <param name="strings">需要输出到终端的内容</param>
        /// <returns></returns>
        private static string ConcatStr(String[] strings)
        {
            string result = "";
            foreach(String str in strings)
            {
                result += "\"" + str + "\"" + " ";
            }
            return result;
        }
    }
}
