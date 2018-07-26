using System;
using System.Windows.Forms;

namespace 关机助手.Util
{
    class ShutdownUtil
    {
        /**
         * 取消指令
         */
        public static void CancelShutdownCommand()
        {
            RunShutdownCommand(Mode.取消, -1);
        }

        /**
         * 关机指令
         */
        public static void RunShutdownCommand(Mode mode, float seconds)
        {
            RunShutdownCommand(mode, (int)seconds);
        }

        /**
         * 关机指令的内部实现
         */
        private static void RunShutdownCommand(Mode mode, int seconds)
        {
            String command = "shutdown ";
            switch (mode)
            {
                case Mode.关机:
                    command += "-s -t " + seconds;
                    break;
                case Mode.重启:
                    command += "-g -t " + seconds;
                    break;
                case Mode.取消:
                    command += "-a";
                    break;
                case Mode.休眠:
                    command = "rundll32.exe powrprof.dll SetSuspendState";
                    break;
                default:
                    MessageBox.Show("Error!");
                    return;
            }
            SystemCommandUtil.ExcuteCommand(command);
        }

        public static void RunSuspendCommand(Mode mode)
        {
            if(mode == Mode.休眠)
            {
                SystemCommandUtil.ExcuteCommand("rundll32.exe powrprof.dll SetSuspendState");
            }
        }

        public enum Mode { 取消, 关机, 重启, 休眠 };
    }
}
