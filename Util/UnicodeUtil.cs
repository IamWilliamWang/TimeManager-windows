using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关机助手.Util
{
    /// <summary>
    /// 汉字与固定Unicode格式之间互换的帮助类
    /// </summary>
    /// 这个类诞生的原因：由于不能解决将排序规则由默认的SQL_Latin1_General_CP1_CI_AS
    /// 修改为Chinese_PRC_CI_AS的问题，所以数据库文件只存ASCII文本，所以需要一个类将
    /// 字符转换成对应的ASCII字段储存进数据库
    public class UnicodeUtil
    {
        private readonly static string codingType = "utf-8";
        /// <summary>
        /// 判断ch是否是中文
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsChineseChar(char ch)
        {
            return ch > 127;
        }

        public static bool IsChineseString(string content)
        {
            bool result = false;
            if (content.IndexOf("&#x") != -1)
                return true;
            foreach(char ch in content.ToCharArray())
            {
                if (IsChineseChar(ch))
                    result = true;
            }
            return result;
        }

        /// <summary>
        /// 从汉字串转换到16进制，字符类型不建议使用该方法
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetHexFromChs(string s)
        {
            if (( s.Length % 2 ) != 0)
            {
                s += " ";//空格
                         //throw new ArgumentException("s is not valid chinese string!");
            }

            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(codingType);

            byte[] bytes = chs.GetBytes(s);

            string str = "";

            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("&#x{0:x};", bytes[i]);
            }

            return str;
        }

        /// <summary>
        /// 将汉字字符转为utf8编码
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string GetHexFromChs(char ch)
        {
            string result = GetHexFromChs(ch + "");
            if (result.Substring(result.Length - 6) == "&#x20;")
                result = result.Substring(0, result.Length - 6);
            return result;
        }

        /// <summary>
        /// 从16进制转换成汉字，请勿按字节调用
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string GetChsFromHex(string hex)
        {
            if (hex == null)
                throw new ArgumentNullException("hex not found");
            if (hex.Length <= 6)
                throw new ArgumentException("请勿按字节调用该函数，请至少传入两个hex");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格
                            //throw new ArgumentException("hex is not a valid number!", "hex");
            }
            
            hex = hex.Replace("&#x", "").Replace(";", "");

            // 需要将 hex 转换成 byte 数组。
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个 byte。
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                        System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message.
                    throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }

            // 获得 UTF-8，Chinese Simplified。
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(codingType);


            return chs.GetString(bytes);
        }
    }
}
