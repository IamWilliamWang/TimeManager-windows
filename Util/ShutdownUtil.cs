using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace 关机小程序
{
    class ShutdownUtil
    {
        /**
         * 取消指令
         */
        public static void cancelShutdownCommand()
        {
            runShutdownCommand(Mode.取消, -1);
        }

        /**
         * 关机指令
         */
        public static void runShutdownCommand(Mode mode, float seconds)
        {
            runShutdownCommand(mode, (int)seconds);
        }

        /**
         * 关机指令的内部实现
         */
        private static void runShutdownCommand(Mode mode, int seconds)
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
                default:
                    MessageBox.Show("Error!");
                    return;
            }
            SystemCommandUtil.ExcuteCommand(command);
        }

        public enum Mode { 取消, 关机, 重启 };
    }
}
