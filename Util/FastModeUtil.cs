using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace 关机助手.Util
{
    public class FastModeUtil
    {
        private static SqlConnectionAgency dbAgency = new SqlConnectionAgency();

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
            bool sleep = false;

            if (File.Exists("C:\\Users\\"+ProgramLauncher.SystemUserName+"\\DONOTWRITEDATA"))
            {
                File.Delete( "C:\\Users\\"+ProgramLauncher.SystemUserName+"\\DONOTWRITEDATA");
                return;
            }

            for (int i = 0; i < args.Length; i++)
            {
                try
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
                            if (i < args.Length - 1 && !"/-".Contains(args[i + 1][0].ToString())) //有自定义数据库文件完整路径的参数
                            {
                                mdfFullname = args[++i];
                            }
                            string insertSql = "INSERT INTO [Table](开机时间) VALUES ('GETDATE()')".Replace("GETDATE()",
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                            File.AppendAllText(mdfFullname.Replace(".mdf", ".cache"), insertSql + CacheUtil.CacheSpliter);
                            File.SetAttributes(mdfFullname.Replace(".mdf", ".cache"), FileAttributes.Hidden);
                            return;
                        case "/x":
                        case "-x":
                            sleep = true;
                            break;
                        default:
                            PrintHelp();
                            break;
                    }
                }
                catch(System.IndexOutOfRangeException)
                {
                    ConsoleOutputUtil.OpenWrite();
                    ConsoleOutputUtil.WriteLine("无法执行。原因：缺少参数错误！");
                    ConsoleOutputUtil.CloseWrite();
                }
            }
            if (mdfFullname != null)
            {
                dbAgency.OpenConnection(mdfFullname);
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
            if (sleep == true)
            {
                ShutdownUtil.RunSuspendCommand(ShutdownUtil.Mode.休眠);
                休眠结束();
            }
            dbAgency.CloseConnection();
            Environment.Exit(0);
        }

        private static void 休眠结束()
        {

            new Thread(休眠结束后的工作).Start();
        }

        private static  void 休眠结束后的工作()
        {
            Thread.Sleep(10000);
            SqlExecuter.记录开机事件();
            Environment.Exit(0);
        }

        /// <summary>
        /// 输出程序帮助
        /// </summary>
        static void PrintHelp()
        {
            String[] helps = {
"关机助手(终端版 v1.5)","使用说明：",
"|     选项      |           含义          |         示例          |备注",
"|/s [seconds]   |倒计时关机(秒)           |/s 60                  |",
"|/m [minutes]   |倒计时关机(分钟)         |/m 1                   |",
"|/c [string]    |执行成功后弹出的字符串   |/m 2.5 /c 150秒后将关机|",
"|/a             |销毁所有倒计时           |/a                     |",
"|/d [minutes]   |记录真正的关机时间       |/d 40                  |记录的是现在的时间加上设定的分钟后的时间",
"|/k             |记录当前的开机时间       |/k                     |",
"|/k [dbFilename]|记录开机时间写入指定文件 |/k D:\\database.mdf     |",
"|/x             |休眠电脑                 |/x                     |记录关机并在下次开机时记录开机时间",
"注，/也可换成-"};

            Util.ConsoleOutputUtil.WriteLines(helps);
        }
    }
}
