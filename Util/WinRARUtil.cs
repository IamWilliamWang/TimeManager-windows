using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关机助手.Util
{
    class WinRARUtil
    {
        private static Exception mException = null; //最后异常记录

        /// <summary>
        /// 从注册表当中获取WinRAR.exe具体文件的FileInfo形式
        /// </summary>
        /// <returns></returns>
        private static FileInfo GetWinRarFile()
        {
            return new FileInfo(GetWinRarPath());
        }

        private static string GetWinRarPath()
        {
            string winrarExeFullfilename = string.Empty;

            string key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe";
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(key);
            if (registryKey != null)
            {
                winrarExeFullfilename = registryKey.GetValue("").ToString();
            }
            registryKey.Close();
            return winrarExeFullfilename;
        }

        /// <summary>
        /// 调用WinRAR压缩文件
        /// </summary>
        /// <param name="sourceFullFilenames">要进行压缩的含路径的(多个)文件名</param>
        /// <param name="targetFullFilename">目标文件名</param>
        /// <returns></returns>
        public static bool CompressFile(string[] sourceFullFilenames, string targetFullFilename = null)
        {
            try
            {
                string sourceFullFilename = "";
                foreach(string fileItem in sourceFullFilenames)
                {
                    sourceFullFilename += "\"" + fileItem + "\" ";
                }
                targetFullFilename = "\"" + targetFullFilename + "\"";
                ProcessStartInfo startInfo = new ProcessStartInfo();
                FileInfo winrarExe = GetWinRarFile();
                startInfo.FileName = winrarExe.Name;
                startInfo.WorkingDirectory = winrarExe.DirectoryName;
                startInfo.Arguments = "a -ep -m5 ";
                if (targetFullFilename != null)
                    startInfo.Arguments += targetFullFilename + " " + sourceFullFilename;
                else if (targetFullFilename == null)
                {
                    if (sourceFullFilename.LastIndexOf(".") != -1)
                        startInfo.Arguments += sourceFullFilename.Remove(sourceFullFilename.LastIndexOf(".")) + ".rar " + sourceFullFilename;
                    else
                        startInfo.Arguments += sourceFullFilename + ".rar " + sourceFullFilename;
                }

                Process compressProcess = new Process();
                compressProcess.StartInfo = startInfo;
                if (!compressProcess.Start())
                    return false;
                return compressProcess.WaitForExit(int.MaxValue);
            }
            catch(Exception e)
            {
                mException = e;
                return false;
            }
        }

        /// <summary>
        /// 调用WinRAR解压文件
        /// </summary>
        /// <param name="sourceFullFilename">要进行解压的含路径的单个压缩包文件名</param>
        /// <returns></returns>
        public static bool DecompressFile(string sourceFullFilename, string targetFolder=null)
        {
            sourceFullFilename = "\"" + sourceFullFilename + "\"";
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                FileInfo unrarExe = new FileInfo(GetWinRarPath().Replace("WinRAR.exe","UnRAR.exe"));
                startInfo.FileName = unrarExe.Name;
                startInfo.WorkingDirectory = unrarExe.DirectoryName;
                if(targetFolder==null)
                    startInfo.Arguments = "e -o+" + sourceFullFilename + " \"" + sourceFullFilename.Remove(sourceFullFilename.LastIndexOf('\\'))+"\"";
                else
                    startInfo.Arguments = "e -o+" + sourceFullFilename + " \"" + targetFolder+"\"";
                Process decompressProcess = new Process();
                decompressProcess.StartInfo = startInfo;
                if (!decompressProcess.Start())
                    return false;
                return decompressProcess.WaitForExit(int.MaxValue);
            }
            catch (Exception e)
            {
                mException = e;
                return false;
            }
        }

        /// <summary>
        /// 获取最后一次的异常记录，如果没有则返回null
        /// </summary>
        /// <returns></returns>
        public static Exception LatestException()
        {
            return mException;
        }
    }
}
