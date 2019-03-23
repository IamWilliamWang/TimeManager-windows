using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace 关机助手
{
    public partial class ExceptionForm : Form
    {
        Exception mException = null;
        Util.DatabaseAgency database = new Util.DatabaseAgency();

        public ExceptionForm(Exception exception)
        {
            InitializeComponent();
            mException = exception;
        }

        private void ExceptionForm_Load(object sender, EventArgs e)
        {
            this.labelInformation.Text = ( CutContent(mException.Message) );
            this.textBox1.Lines = TextBoxContent();
        }

        private string ForamtInformation(string label)
        {
            String newLabel = "";
            for (int i = 0; i < label.Count(); i++)
            {
                if (i != 0 && i % 50 == 0)
                    newLabel += "\n";
                newLabel += label[i];
            }
            return newLabel;
        }

        private string CutContent(String message)
        {
            int toIndex = message.IndexOf(". ");
            if (toIndex == -1)
                toIndex = message.IndexOf('。');
            if (toIndex != -1)
                message = message.Substring(0, toIndex + 1);
            return message;
        }

        private String[] TextBoxContent()
        {
            List<String> array = new List<String>(new String[]
            {
                "有关调用实时(JIT)调试而不是此对话框的详细信息，",
                "请参见此消息的结尾。",
                "",
                "************** 异常文本 **************",
                mException.ToString()
            });
            array.Add("");
            array.Add("");
            array.Add("************** 已加载的程序集 **************");
            Assembly[] currentLoadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach(Assembly ass in currentLoadedAssemblies)
            {
                array.Add(Assemble2String(ass));
                array.Add("----------------------------------------");
            }
            array.Add("\r\n" +
                "************** JIT 调试 **************\r\n" +
                "要启用实时(JIT)调试，\r\n" +
                "该应用程序或计算机的 .config 文件(machine.config)的 system.windows.forms 节中必须设置\r\n" +
                "jitDebugging 值。\r\n" +
                "编译应用程序时还必须启用\r\n" +
                "调试。\r\n" +
                "\r\n" +
                "例如:\r\n" +
                "\r\n" +
                "<configuration>\r\n" +
                "    <system.windows.forms jitDebugging=\"true\" />\r\n" +
                "</configuration>\r\n" +
                "\r\n" +
                "启用 JIT 调试后，任何未经处理的异常\r\n" +
                "都将被发送到在此计算机上注册的 JIT 调试程序，\r\n" +
                "而不是由此对话框处理。\r\n" +
                "\r\n");
            return array.ToArray();
        }

        private String Assemble2String(Assembly ass)
        {
            String[] split = ass.FullName.Split(',');
            StringBuilder result = new StringBuilder();
            result.AppendLine(split[0].Trim());
            result.AppendLine("    程序集版本:" + split[1].Trim().Replace("Version=",""));
            result.AppendLine("    CLR运行版本:"+ass.ImageRuntimeVersion.Replace("v",""));
            result.AppendLine("    程序集语言:" + FormatCulture(split[2].Trim().Replace("Culture=", "")));
            result.AppendLine("    公钥标记:" + split[3].Trim().Replace("PublicKeyToken=", ""));
            result.Append("    动态库位置:"+ass.CodeBase);
            return result.ToString();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            Util.LogUtil.Log(this.labelInformation.Text, mException);
            MessageBox.Show("日志写入成功！请将日志发送给开发人员", "发送报告", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.buttonSend.Enabled = false;
            String current = System.IO.Directory.GetCurrentDirectory();
            OpenFolderAndSelectFile(current + "\\" + Util.LogUtil.LogFileName);
        }

        private String FormatCulture(string culture)
        {
            if (culture == "neutral")
                return "中性";
            else
            {
                try
                {
                    Hashtable table = new Hashtable();
                    table.Add("zh-Hans", "简体中文");
                    table.Add("zh-TW", "繁體中文(中國台灣)");
                    table.Add("zh-CN", "简体中文(中国大陆)");
                    table.Add("zh-HK", "繁體中文(中國香港)");
                    table.Add("zh-SG", "简体中文(新加坡)");
                    table.Add("zh-MO", "繁體中文(中國澳門)");
                    table.Add("zh", "中文");
                    table.Add("zh-Hant", "繁體中文");
                    table.Add("zh-CHS", "简体中文(文言文)");
                    table.Add("zh-CHT", "繁體中文(文言文)");
                    return table[culture].ToString();
                }
                catch (CultureNotFoundException)
                {
                    return culture;
                }
            }
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您是否愿意本程序进行尝试性修复？点击是进行修复后退出，点击否则直接退出程序。", "尝试性修复", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
            {
                Environment.Exit(-1);
                return;
            }

            database.DisposeConnection();
            database.ResetConnection();
            Environment.Exit(-1);
        }

        public static void Show(Exception exception)
        {
            new ExceptionForm(exception).Show();
        }

        public static void ShowDialog(Exception exception)
        {
            new ExceptionForm(exception).ShowDialog();
        }

        private void OpenFolderAndSelectFile(String fileFullName)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/select," + fileFullName;
            System.Diagnostics.Process.Start(psi);
        }

        private void 直接退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void 修复后退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            database.DisposeConnection();
            database.ResetConnection();
            Environment.Exit(0);
        }

        private void 直接重启ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void 修复后重启ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            database.DisposeConnection();
            database.ResetConnection();
            Application.Restart();
        }
    }
}
