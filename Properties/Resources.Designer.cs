﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace 关机助手.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("关机助手.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot; ?&gt;
        ///&lt;!--填充所有括号，不用的属性行可删除--&gt;
        ///&lt;Config&gt;
        ///	&lt;Main&gt;
        ///		&lt;DarkMode&gt;（自动开启暗黑模式，true/false）&lt;/DarkMode&gt;
        ///		&lt;HideInTaskbar&gt;（是否脱离任务栏控制，true/false）&lt;/HideInTaskbar&gt;
        ///		&lt;HideNotifyIcon&gt;（是否隐藏右下角图标，true/false）&lt;/HideNotifyIcon&gt;
        ///		&lt;Opacity&gt;（主界面的透明度）&lt;/Opacity&gt;
        ///		&lt;DefaultComboBoxIndex&gt;（首页的模式选择默认为第几个）&lt;/DefaultComboBoxIndex&gt;
        ///		&lt;AutoShutdownSeconds&gt;（自动执行几秒后关机）&lt;/AutoShutdownSeconds&gt;
        ///	&lt;/Main&gt;
        ///	&lt;CacheManager&gt;
        ///		&lt;From&gt;（合并源文件名）&lt;/From&gt;
        ///		&lt;To&gt;（合并目标文件名）&lt;/To&gt;
        ///		&lt;AutoMerge&gt;（自动执行合并操作，true/false）&lt;/A [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string ConfigExample {
            get {
                return ResourceManager.GetString("ConfigExample", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ::::::::::::::::::::::::::::::::::::::::::::
        ///:: Elevate.cmd - Version 4
        ///:: Automatically check &amp; get admin rights
        ///::::::::::::::::::::::::::::::::::::::::::::
        /// @echo off
        /// CLS
        /// ECHO.
        /// ECHO =============================
        /// ECHO Running Admin shell
        /// ECHO =============================
        ///
        ///:init
        /// setlocal DisableDelayedExpansion
        /// set cmdInvoke=1
        /// set winSysFolder=System32
        /// set &quot;batchPath=%~0&quot;
        /// for %%k in (%0) do set batchName=%%~nk
        /// set &quot;vbsGetPrivileges=%temp%\OEgetPriv_%batchName%.vbs&quot;
        /// setlocal E [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string ElevatedCmd {
            get {
                return ResourceManager.GetString("ElevatedCmd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找 System.Byte[] 类型的本地化资源。
        /// </summary>
        internal static byte[] EmptyDB {
            get {
                object obj = ResourceManager.GetObject("EmptyDB", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   查找 System.Drawing.Bitmap 类型的本地化资源。
        /// </summary>
        internal static System.Drawing.Bitmap ErrorMessage {
            get {
                object obj = ResourceManager.GetObject("ErrorMessage", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查找类似 F:\Visual Studio 2015\关机助手\bin\Debug\关机助手.exe 的本地化字符串。
        /// </summary>
        internal static string ExeDevelopFullFilename {
            get {
                return ResourceManager.GetString("ExeDevelopFullFilename", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似于 (图标) 的 System.Drawing.Icon 类型的本地化资源。
        /// </summary>
        internal static System.Drawing.Icon icon_main {
            get {
                object obj = ResourceManager.GetObject("icon_main", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   查找 System.Drawing.Bitmap 类型的本地化资源。
        /// </summary>
        internal static System.Drawing.Bitmap icon_main1 {
            get {
                object obj = ResourceManager.GetObject("icon_main1", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查找类似 TimeDatabase.mdf 的本地化字符串。
        /// </summary>
        internal static string MdfFilename {
            get {
                return ResourceManager.GetString("MdfFilename", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找 System.Byte[] 类型的本地化资源。
        /// </summary>
        internal static byte[] printer {
            get {
                object obj = ResourceManager.GetObject("printer", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   查找类似 C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\Recorder.exe 的本地化字符串。
        /// </summary>
        internal static string RecorderFullFilename {
            get {
                return ResourceManager.GetString("RecorderFullFilename", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\autoshutdown.cmd 的本地化字符串。
        /// </summary>
        internal static string RecorderShellFullFilename {
            get {
                return ResourceManager.GetString("RecorderShellFullFilename", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找 System.Byte[] 类型的本地化资源。
        /// </summary>
        internal static byte[] 开机小程序 {
            get {
                object obj = ResourceManager.GetObject("开机小程序", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
