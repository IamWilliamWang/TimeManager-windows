using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关机小程序
{
    class SystemCommandUtil
    {
        /**
         * @parms command需要执行的dos指令
         * @returns 执行过程中显示的字符串
         */
        public static String ExcuteCommand(String command)
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
    }
    
}
