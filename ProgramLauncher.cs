using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using 关机助手.Util;

namespace 关机助手
{
    static class ProgramLauncher
    {
        #region 外部类调用模块
        public static string SystemUserName { get { return Environment.UserName; } }
        public static string Version(int 版本号保留几个点)
        {
            if (版本号保留几个点 < 0 || 版本号保留几个点 > 3)
                throw new ArgumentException("参数输入不正确");

            string original = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            if (版本号保留几个点 == 3)
                return original;
            int len = 0;
            for(int i = 0; i <= 版本号保留几个点; i++)
            {
                len = original.IndexOf('.', len+1);
            }
            return original.Substring(0, len);
        }
        #endregion

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // 处理未捕获的异常   
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            // 处理主线程异常   
            Application.ThreadException += Application_ThreadException;
            // 开启显示
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                if (args.Length == 0)
                    Application.Run(new MainForm());
                else if (args.Length == 1 || args[0] == "MessageUnabled")
                {
                    var mainForm = new MainForm();
                    mainForm.重复开启软件检查 = false;
                    Application.Run(mainForm);
                }
                else
                    FastModeUtil.RunConsoleApplication(args);
            }
            catch(Exception e)
            {
                ExceptionForm.ShowDialog(e);
            }
        }

        #region 主线程异常处理函数
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ExceptionForm.ShowDialog(e.Exception);
        }
        #endregion
    }
}
