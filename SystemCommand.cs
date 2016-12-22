using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace 关机小程序
{
    class SystemCommand
    {
        /**
         * @parms command需要执行的dos指令
         * @returns 执行过程中显示的字符串
         */
        public static String system(String command)
        {
            Process process = new Process();

            process.StartInfo.FileName = "cmd";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            process.StandardInput.WriteLine(command);
            process.StandardInput.WriteLine("exit");

            return process.StandardOutput.ReadToEnd();
        }

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
            system(command);
        }

        public enum Mode { 取消, 关机, 重启 };
    }
}
