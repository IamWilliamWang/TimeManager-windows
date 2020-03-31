using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace 关机助手.Util
{
    public class FastModeExecutor
    {
        private static DatabaseAgency dbAgency = new DatabaseAgency();

        /// <summary>
        /// 不显示窗体直接记录关机事件并根据参数执行关机
        /// </summary>
        /// <param name="countdownSeconds">关机倒计时（按秒计）</param>
        public static void ShutdownWithSeconds(int countdownSeconds)
        {
            SqlExecuter.记录关机事件();
            ShutdownUtil.CancelShutdownCommand();
            ShutdownUtil.RunShutdownCommand(ShutdownUtil.Mode.关机, countdownSeconds);
        }

        public static void ShutdownWithSeconds_DelayMode(int delaySeconds)
        {
            SqlExecuter.记录延迟关机事件(delaySeconds);
            ShutdownUtil.CancelShutdownCommand();
            ShutdownUtil.RunShutdownCommand(ShutdownUtil.Mode.关机, delaySeconds);
        }

        [Obsolete]
        public static void ShutdownWithMinutes_DelayMode(int countdownMinute)
        {
            if(countdownMinute>=60) // bug needs fix.
                SqlExecuter.记录延迟关机事件("1:" + (countdownMinute - 60) + ":0");
            else
                SqlExecuter.记录延迟关机事件("0:" + countdownMinute + ":0");

            ShutdownUtil.CancelShutdownCommand();
            ShutdownUtil.RunShutdownCommand(ShutdownUtil.Mode.关机, countdownMinute * 60);
        }
        [Obsolete]
        public static void ShutdownWithMinutes_DelayMode(float countdownMinute)
        {
            ShutdownWithMinutes_DelayMode((int)countdownMinute);// bug needs fix.
        }

        private static int GetSecondFromTimeStringContainsMS(String stringContainsMS)
        {
            stringContainsMS = stringContainsMS.ToLower();
            if (stringContainsMS[stringContainsMS.Length - 1] == 's')
                return (int)(float.Parse(stringContainsMS.Substring(0, stringContainsMS.Length - 1)));
            else if (stringContainsMS[stringContainsMS.Length - 1] == 'm')
                return 60 * (int)(float.Parse(stringContainsMS.Substring(0, stringContainsMS.Length - 1)));
            else
                return -1;
        }

        /// <summary>
        /// 启动终端模式
        /// </summary>
        /// <param name="args">向程序输送的参数组</param>
        public static void RunConsoleApplication(String[] args)
        {
            // 传入的要使用的变量
            int? 关机倒计时秒 = null;
            int? delay时间秒 = null;
            String 成功后弹出的字符串 = null;
            bool 取消关机 = false;
            bool 记录开机时间 = false;
            String mdf文件 = null;
            bool 睡眠 = false;
            bool 休眠 = false;
            bool 禁用缓存 = false;
            bool 离线模式 = false;
            bool 显示缓存 = false;
            bool 删除缓存 = false;
            // 内部使用的变量
            String cache文件 = null;
            String 失败后弹出的字符串 = null;

            // 检查跳过处理
            if (File.Exists("C:\\Users\\"+ProgramLauncher.SystemUserName+"\\DONOTWRITEDATA"))
            {
                File.Delete( "C:\\Users\\"+ProgramLauncher.SystemUserName+"\\DONOTWRITEDATA");
                return;
            }
            
            // 解析所有参数并赋值
            for (int i = 0; i < args.Length; i++)
            {
                try
                {
                    switch (args[i])
                    {
                        case "-s":case "/s":
                        case "--shutdown_seconds":
                            关机倒计时秒 = FastModeExecutor.GetSecondFromTimeStringContainsMS(args[++i]);
                            if (关机倒计时秒 == -1)
                            {
                                失败后弹出的字符串 = "执行失败！时间请以s（秒）或m（分钟）结尾";
                                i = args.Length; //强制跳出大循环
                            }
                            break;
                        case "-d":case "/d":
                        case "--shutdown_delay":
                            delay时间秒 = FastModeExecutor.GetSecondFromTimeStringContainsMS(args[++i]);
                            if (delay时间秒 == -1)
                            {
                                失败后弹出的字符串 = "执行失败！时间请以s（秒）或m（分钟）结尾";
                                i = args.Length; //强制跳出大循环
                            }
                            break;
                        case "-c":case "/c":
                        case "--comment":
                            成功后弹出的字符串 = args[++i].Replace("\\n","\n");
                            break;
                        case "-a":case "/a":
                        case "--cancel_all":
                            取消关机 = true;
                            break;
                        case "-k":case "/k":
                        case "--start":
                            记录开机时间 = true;
                            if (HasNext(args, i))
                            {
                                失败后弹出的字符串 = "此功能已被废弃，请使用-db或-ca！";
                                i = args.Length; //强制跳出大循环
                            }
                            //{ //有自定义数据库文件完整路径的参数
                            //    mdf文件 = args[++i];
                            //    cache文件 = mdf文件.Replace(".mdf", ".cache");
                            //}
                            break;
                        case "-h":case "/h":
                        case "--hibernate":
                            休眠 = true;
                            break;
                        case "-sleep":case "/sleep":
                        case "--sleep":
                            睡眠 = true;
                            break;
                        case "-db":case "/db":
                        case "--database_filename":
                            if (HasNext(args, i)) 
                            {
                                mdf文件 = args[++i];
                                cache文件 = mdf文件.Replace(".mdf", ".cache");
                            }
                            break;
                        case "-ca":case "/ca":
                        case "--cache":case "/cache":
                            if (HasNext(args, i))
                                cache文件 = args[++i];
                            break;
                        case "-dc":case "/dc":
                        case "--disable_cache":
                            禁用缓存 = true;
                            cache文件 = null; //如果禁用缓存，删除缓存文件信息
                            break;
                        case "-offline":case "/offline":
                        case "--offline":
                            离线模式 = true;
                            break;
                        case "-sc":case "/sc":
                        case "--show_cache":
                            显示缓存 = true;
                            break;
                        case "-del":case "/del":
                        case "--delete_cache":
                            删除缓存 = true;
                            break;
                        default:
                            PrintHelp();
                            break;
                    }
                }
                catch(System.IndexOutOfRangeException)
                {
                    ConsoleWriter.WriteLine("无法执行。原因：缺少参数错误！");
                }
            }

            // 先判断是否有失败
            if (失败后弹出的字符串 != null)
            {
                ConsoleWriter.WriteLine(失败后弹出的字符串 + "\n如需其他帮助请使用-help");
                return;
            }
            /* 首先先判断是否必要调用数据库，如果必要则打开数据库，清除cache文件信息，保证后方调用都使用数据库。 */
            // 在非离线模式 and 禁用缓存时才会打开数据库。
            if (离线模式 == false && 禁用缓存 == true) 
            {
                if (mdf文件 != null)
                    dbAgency.OpenConnection(mdf文件); //如果指定的数据库文件确实有用，就只在这里使用
                else
                    dbAgency.OpenConnection();
            }
            // 传入的是文件夹，添加完整文件名
            if (mdf文件 != null && !mdf文件.EndsWith(".mdf", true, null)) 
            {
                if (!mdf文件.EndsWith("\\"))
                    mdf文件 += "\\";
                mdf文件 += "TimeDatabase.mdf";
            }
            if (cache文件 != null && !cache文件.EndsWith(".cache", true, null)) 
            {
                if (!cache文件.EndsWith("\\"))
                    cache文件 += "\\";
                cache文件 += "TimeDatabase.cache";
            }
            // 调整工作路径、修改cache文件名
            if (cache文件 == null)
            {
                // 设置程序运行的文件夹为CurrentDirectory
                System.IO.Directory.SetCurrentDirectory(System.Windows.Forms.Application.StartupPath);
            }
            // 设置cache所在文件夹为CurrentDirectory
            else if (cache文件.Contains("\\"))
            {
                System.IO.Directory.SetCurrentDirectory(cache文件.Substring(0, cache文件.LastIndexOf("\\")));
            }
            // 如果cache名称不是默认值，则修改Cache的名称
            if (cache文件 != null && cache文件 != "" && Cache.CacheFilename != cache文件) 
                Cache.CacheFilename = cache文件;
            // 检查开机时间
            if (记录开机时间 == true)
            {
                if (离线模式) //离线模式优先
                    ConsoleWriter.WriteLine("离线模式下记录开机时间已被禁止。");
                else
                {
                    if (!SqlExecuter.记录开机事件("[Table]")) //未指定数据库时的默认调用。（调用数据库或缓存由内部处理）
                        失败后弹出的字符串 = "错误！未能记录开机时间！";
                }
            }
            // 检查关机时间
            if (关机倒计时秒 != null)
            {
                if (离线模式)
                    ShutdownUtil.RunShutdownCommand(ShutdownUtil.Mode.关机, 关机倒计时秒 ?? 0);
                else
                    FastModeExecutor.ShutdownWithSeconds(关机倒计时秒 ?? 0); //内部实现自动处理是否调用缓存
            }
            // 检查延迟时间
            if (delay时间秒 != null)
            {
                if (离线模式)
                    ConsoleWriter.WriteLine("暂不支持离线模式下的延迟时间，请使用窗体版本。");
                else
                    ShutdownWithSeconds_DelayMode(delay时间秒 ?? 0); //内部实现自动处理是否调用缓存
            }
            // 检查是否取消关机，与上方连续使用可以做到记录时间却不调用系统关机的目的
            if (取消关机 == true)
                ShutdownUtil.CancelShutdownCommand();
            if (睡眠 == true)
            {
                if (离线模式)
                    ShutdownUtil.RunSuspendCommand(ShutdownUtil.Mode.睡眠);
                else if(cache文件 == null)
                {
                    SqlExecuter.记录关机事件();
                    ShutdownUtil.RunSuspendCommand(ShutdownUtil.Mode.睡眠);
                    休眠结束(); //因为重启后数据库连接状态未改变，所以不需要分类讨论
                }
                else //指定cache文件特殊对待
                {
                    //String shutdownSql = SqlExecuter.UsefulSqlExpressions.UpdateShutdownTimeSQL();
                    //Cache.AppendCache(shutdownSql, cache文件);
                    SqlExecuter.记录关机事件();
                    ShutdownUtil.RunSuspendCommand(ShutdownUtil.Mode.睡眠);
                    //String poweronSql = SqlExecuter.UsefulSqlExpressions.InsertPowerOnTimeSQL();
                    //Cache.AppendCache(poweronSql, cache文件);
                    休眠结束();
                }
            }
            // 检查休眠
            if (休眠 == true)
            {
                if (离线模式)
                    ShutdownUtil.RunSuspendCommand(ShutdownUtil.Mode.休眠);
                else if (cache文件 == null)
                {
                    SqlExecuter.记录关机事件();
                    ShutdownUtil.RunSuspendCommand(ShutdownUtil.Mode.休眠);
                    休眠结束(); //因为重启后数据库连接状态未改变，所以不需要分类讨论
                }
                else //指定cache文件特殊对待
                {
                    //String shutdownSql = SqlExecuter.UsefulSqlExpressions.UpdateShutdownTimeSQL();
                    //Cache.AppendCache(shutdownSql, cache文件);
                    SqlExecuter.记录关机事件();
                    ShutdownUtil.RunSuspendCommand(ShutdownUtil.Mode.休眠);
                    //String poweronSql = SqlExecuter.UsefulSqlExpressions.InsertPowerOnTimeSQL();
                    //Cache.AppendCache(poweronSql, cache文件);
                    休眠结束();
                }
            }

            if (显示缓存)
            {
                if (cache文件 == null)
                    ConsoleWriter.WriteLines(Cache.GetAllLines());
                else
                    ConsoleWriter.WriteLines(Cache.GetAllLines(cache文件));
            }
            if (删除缓存)
            {
                if (cache文件 == null)
                    File.Delete(Cache.CacheFilename);
                else
                    File.Delete(cache文件);
            }   
            if(!离线模式)
                dbAgency.CloseConnection();
            if (成功后弹出的字符串 != null)
                ConsoleWriter.WriteLine(成功后弹出的字符串);
            Environment.Exit(0);
        }

        private static void 休眠结束()
        {
            new Thread(休眠结束后的工作).Start();
        }

        private static void 休眠结束后的工作()
        {
            Thread.Sleep(10000);
            SqlExecuter.记录开机事件();
        }

        private static bool HasNext(String[] args, int i)
        {
            if (i < args.Length - 1)
            {
                char nextFirstChar = args[i + 1][0];
                if (nextFirstChar != '-' && nextFirstChar != '/')
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 输出程序帮助
        /// </summary>
        static void PrintHelp()
        {
            String[] infos =
            {
"关机助手(终端版 v2.1)","使用说明：",
"|       选项     |             完整选项           |         含义                                      |    示例",
"|-s [sec/min]s/m |--shutdown_seconds [sec/min]s/m |倒计时关机(秒)                                     |-s 60s or -s 1m",
"|-d [sec/min]s/m |--shutdown_delay [sec/min]s/m   |记录被delay后的关机时间                            |-d 30s or -d 0.5m ",
"|-c [string]     |--comment [string]              |执行成功后弹出的字符串(换行请使用\\n表达)                     |-s 2.5m -c 150秒后将关机",
"|-a              |--cancel_all                    |销毁所有倒计时                                     |-a",
"|-k              |--start                         |记录当前的开机时间                                 |-k",
"|-h              |--hibernate                     |休眠电脑(记录关机和下次开机时间)                   |-h",
"|-sleep          |--sleep                         |睡眠电脑(记录关机和下次开机时间)                   |-sleep",
"|-db [dbFilename]|--database_filename [dbFilename]|设定数据库文件名(不使用-dc会自动检测对应的缓存文件)|-db D:\\database.mdf",
"|-ca [cachename] |--cache [cachename]             |设定数据库缓存文件名                               |-ca D:\\database.cache",
"|-dc             |--disable_cache                 |强制禁止使用缓存                                   |-dc",
"|-offline        |--offline                       |离线模式，不记录任何时间                           |-offline",
"|-sc             |--show_cache                    |显示缓存文件内容（可指定缓存文件）                 |-sc -ca my_cache.cache",
"|-del            |--delete_cache                  |删除缓存文件（可指定缓存文件）                     |-del -ca my_cache.cache"
            };

            Util.ConsoleWriter.WriteLines(infos);
        }
    }
}
