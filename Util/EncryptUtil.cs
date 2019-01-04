using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关机助手.Util
{
    class EncryptUtil
    {
        public static bool EncryptFile(string filename)
        {
            if (File.Exists(filename) == false)
                return false;
            byte[] binary = null;
            try { binary = File.ReadAllBytes(filename); }
            catch (OutOfMemoryException)
            {
                FileStream file = new FileStream(filename, FileMode.Open);
                List<byte> targetFileBytes = new List<byte>();
                byte[] buffer = new byte[1024];
                int offset = 0;
                while (true)
                {
                    int readNum = file.Read(buffer, offset, buffer.Length);
                    offset += readNum;
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] ^= 0xff;
                    }
                    targetFileBytes.AddRange(buffer);
                    if (readNum < buffer.Length)
                        break;
                }
                file.Close();
                binary = targetFileBytes.ToArray();
                targetFileBytes.Clear();
            }

            for (int i = 0; i < binary.Length; i++)
                binary[i] ^= 0xff;
            File.WriteAllBytes(filename, binary);
            return true;
        }

        public static bool DecryptFile(string filename)
        {
            return EncryptFile(filename);
        }
    }
}
