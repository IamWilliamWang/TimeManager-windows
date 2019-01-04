using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using 关机助手.Util;

namespace 关机助手
{
    static class ProgramLauncher
    {
        public static string SystemUserName { get { return Environment.UserName; } }
        public static string Version(int strDotCount)
        {
            if (strDotCount < 0 || strDotCount > 3)
                throw new ArgumentException("参数输入不正确");

            string original = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            if (strDotCount == 3)
                return original;
            int len = 0;
            for(int i = 0; i <= strDotCount; i++)
            {
                len = original.IndexOf('.', len+1);
            }
            return original.Substring(0, len);
        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //处理未捕获的异常   
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常   
            Application.ThreadException += Application_ThreadException;
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                if (args.Length == 0)
                    Application.Run(new MainForm());
                else
                    FastModeUtil.RunConsoleApplication(args);
            }
            catch(Exception e)
            {
                ExceptionForm.ShowDialog(e);
            }
        }
        

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ExceptionForm.ShowDialog(e.Exception);
        }
    }
}
