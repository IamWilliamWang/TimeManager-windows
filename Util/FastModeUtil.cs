using System;
using System.Diagnostics;
using System.IO;

namespace 关机助手.Util
{
    public class FastModeUtil
    {
        /// <summary>
        /// 不显示窗体直接记录关机事件并根据参数执行关机
        /// </summary>
        /// <param name="countdownSeconds">关机倒计时（按秒计）</param>
        public static void ShutdownWithSeconds(int countdownSeconds)
        {
            //TimeLogUtil.Tik();
            SqlExecuter.记录关机事件();

            //TimeLogUtil.Tok(writeErrorLog:true);

            ShutdownUtil.CancelShutdownCommand();
            ShutdownUtil.RunShutdownCommand(ShutdownUtil.Mode.关机, countdownSeconds);
        }


        public static void ShutdownWithMinutes_DelayMode(int countdownMinute)
        {
            if(countdownMinute>=60) // bug needs fix.
                SqlExecuter.记录延迟关机事件("1:" + (countdownMinute - 60) + ":0");
            else
                SqlExecuter.记录延迟关机事件("0:" + countdownMinute + ":0");

            ShutdownUtil.CancelShutdownCommand();
            ShutdownUtil.RunShutdownCommand(ShutdownUtil.Mode.关机, countdownMinute * 60);
        }

        public static void ShutdownWithMinutes_DelayMode(float countdownMinute)
        {
            ShutdownWithMinutes_DelayMode((int)countdownMinute);// bug needs fix.
        }

        /// <summary>
        /// 启动终端模式
        /// </summary>
        /// <param name="args">向程序输送的参数组</param>
        public static void RunConsoleApplication(String[] args)
        {
            bool recordPoweronTime = false;
            int? countDownSeconds = null;
            String comment = null;
            bool cancel = false;
            String delayDuration = null;
            String mdfFullname = null;

            if (File.Exists("C:\\Users\\william\\DONOTWRITEDATA"))
            {
                File.Delete( "C:\\Users\\william\\DONOTWRITEDATA");
                return;
            }

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "/s":
                    case "-s":
                        if (countDownSeconds != null)
                        {
                            ConsoleOutputUtil.WriteLine("错误！[-s] 与 [-m]不可同时使用！");
                            return;
                        }
                        countDownSeconds = (int)Math.Floor(float.Parse(args[++i]));
                        break;
                    case "/m":
                    case "-m":
                        if (countDownSeconds != null)
                        {
                            ConsoleOutputUtil.WriteLine("错误！[-s] 与 [-m]不可同时使用！");
                            return;
                        }
                        countDownSeconds = (int)Math.Floor(float.Parse(args[++i]) * 60);
                        break;
                    case "/c":
                    case "-c":
                        comment = args[++i];
                        break;
                    case "/a":
                    case "-a":
                        cancel = true;
                        break;
                    case "/d":
                    case "-d":
                        delayDuration = args[++i];
                        break;
                    case "/k":
                    case "-k":
                        recordPoweronTime = true;
                        if(i<args.Length-1 && !"/-".Contains(args[i + 1][0].ToString())) //有自定义数据库文件完整路径的参数
                        {
                            mdfFullname = args[++i];
                        }
                        break;
                    default:
                        PrintHelp();
                        break;
                }
            }
            if (mdfFullname != null)
            {
                SqlServerConnection.OpenConnection(mdfFullname);
            }
            if (recordPoweronTime)
                Util.SqlExecuter.记录开机事件();
            if (countDownSeconds != null)
                FastModeUtil.ShutdownWithSeconds(countDownSeconds ?? 0);
            if (cancel == true)
                ShutdownUtil.CancelShutdownCommand();
            if (comment != null)
                ConsoleOutputUtil.WriteLine(comment);
            if (delayDuration != null)
                FastModeUtil.ShutdownWithMinutes_DelayMode(int.Parse(delayDuration));
            
            SqlServerConnection.CloseConnection();
            Environment.Exit(0);
        }

        /// <summary>
        /// 输出程序帮助
        /// </summary>
        static void PrintHelp()
        {
            String[] helps = {"[关机助手(终端版 v1.4)]",
                "提示：参数列表如下",
                "计秒关机：[-s] 关机时间(秒)",
                "计分关机：[-m] 关机时间(分)",
                "输出提示: [-c] 执行成功后输出的内容",
                "撤销关机：[-a] ",
                "延迟计时：[-d] 记录真正的关机时间(分)",
                "记录开机: [-k] 记录当前的开机时间",
                "或自定义: [-k] 数据库的完整地址"};

            Util.ConsoleOutputUtil.WriteLines(helps);
        }
    }
}
