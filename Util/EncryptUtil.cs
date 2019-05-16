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
        [Obsolete]
        /// <summary>
        /// 使用位处理加密一般文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
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

        [Obsolete]
        /// <summary>
        /// 使用位处理解密一般文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static bool DecryptFile(string filename)
        {
            return EncryptFile(filename);
        }
        
        /// <summary>
        /// 使用HC128算法加密一般文件
        /// </summary>
        /// <param name="filename">文件名</param>
        public static bool EncryptFile_HC128(string filename)
        {
            new HC128Executor(filename).Encrypt();
            return true;
        }

        /// <summary>
        /// 使用HC128算法加密一般文件
        /// </summary>
        public static bool EncryptFile_HC128(string sourceFile,string targetFile)
        {
            new HC128Executor(sourceFile, targetFile).Encrypt();
            return true;
        }

        /// <summary>
        /// 使用HC128算法解密一般文件
        /// </summary>
        /// <param name="filename">文件名</param>
        public static bool DecryptFile_HC128(string filename)
        {
            new HC128Executor(filename).Decrypt();
            return true;
        }

        /// <summary>
        /// 使用HC128算法解密一般文件
        /// </summary>
        /// <param name="filename">文件名</param>
        public static bool DecryptFile_HC128(string sourceFile, string targetFile)
        {
            new HC128Executor(sourceFile, targetFile).Decrypt();
            return true;
        }
        #region HC128部分
        /// <summary>
        /// HC128算法便捷执行类
        /// </summary>
        public class HC128Executor
        {
            public String inputFileName { get; set; } = null;
            public String outputFileName { get; set; } = null;
            private const int contentBytesLength = 1024;

            Byte[] key = new Byte[16];
            public HC128Executor(String inputFilename) : this(inputFilename, inputFilename) { }
            
            public HC128Executor(String inputFilename,String outputFilename)
            {
                this.inputFileName = inputFilename;
                this.outputFileName = outputFilename;

            }
            /// <summary>
            /// 执行加密算法
            /// </summary>
            public void Encrypt()
            {
                Byte[] inputFileContent = File.ReadAllBytes(this.inputFileName);

                Generator generator = new Generator(key);
                HC128Algorithm coding = new HC128Algorithm(generator);
                Byte[] outputFileContent = coding.executeEncoding(inputFileContent);

                File.WriteAllBytes(this.outputFileName, outputFileContent);
            }
            /// <summary>
            /// 执行解密算法
            /// </summary>
            public void Decrypt() => Encrypt();
        }
        private class Generator //四个参数
        {
            public Byte[] key = new Byte[16];
            public Byte[] iv = new Byte[16];
            public Byte[] message = new Byte[1024];
            public Byte[] ciphertext = new Byte[1024];
            public UInt64 msglength;

            /// <summary>
            /// 使用默认的密钥流生成Generator
            /// </summary>
            public Generator()
            {
                this.flushKey();
                this.flushIv();
                this.flushMessage();
                this.setDefaultMsglength();

            }

            /// <summary>
            /// 使用给定的密钥流生成Generator
            /// </summary>
            /// <param name="key"></param>
            public Generator(Byte[] key)
            {
                this.key = key;
                this.flushIv();
                this.flushMessage();
                this.setDefaultMsglength();

            }

            private void flushKey()
            {
                for (int i = 0; i < key.Count(); i++)
                {
                    key[i] = 0;
                }
            }

            private void flushIv()
            {
                /*set the value of the key and iv*/
                for (int i = 0; i < 16; i++)
                {
                    iv[i] = 0;
                }
            }

            private void flushMessage()
            {
                /*set the value of message to 0 so that the ciphertext contains the keystream*/
                for (int i = 0; i < 1024; i++) message[i] = 0;
            }

            private void setDefaultMsglength()
            {
                /*generate the first 1024 keystream bytes*/
                msglength = 1024;
            }
        }
        private class ConvertUtil
        {
            public static Byte[] HexStrToBytes(String hexStr)
            {
                Byte[] key = new Byte[hexStr.Length / 2];
                for (int keyStringIndex = 0; keyStringIndex < hexStr.Length; keyStringIndex += 2)
                {
                    key[keyStringIndex / 2] = Convert.ToByte(hexStr.Substring(keyStringIndex, 2), 16);
                }
                return key;
            }

            /// <summary>
            /// 将UInt32 转换成 Byte[]
            /// </summary>
            /// <param name="uintNumber"></param>
            /// <returns></returns>
            public static Byte[] ConvertUInt32ToBytes(UInt32 uintNumber)
            {
                Byte[] b = new Byte[4];
                b[0] = (byte)(uintNumber);
                b[1] = (byte)(uintNumber >> 8);
                b[2] = (byte)(uintNumber >> 16);
                b[3] = (byte)(uintNumber >> 24);
                return b;
            }

            /// <summary>
            /// 将Byte[] 转换成 UInt32[]
            /// </summary>
            /// <param name="bytes"></param>
            /// <returns></returns>
            public static UInt32[] ConvertBytesToUInt32s(Byte[] bytes)
            {
                uint bytesLength = (uint)(bytes.Length);
                UInt32[] uInt32s = new UInt32[bytesLength / 4];
                for (int uInt32sIndex = 0; uInt32sIndex < bytesLength / 4; uInt32sIndex += 1)
                {
                    UInt32 tp = BitConverter.ToUInt32(bytes, uInt32sIndex * 4);
                    uInt32s[uInt32sIndex] = tp;
                }
                return uInt32s;
            }
        }
        private class HC128Algorithm
        {
            #region 最底层的代码，又原C代码直接翻译而来，请勿修改
            class HC128_State //define HC128_State structure
            {
                public UInt32[] P = new UInt32[512];
                public UInt32[] Q = new UInt32[512];
                public UInt32 counter1024;     /*counter1024 = i mod 1024 */
                public UInt32 keystreamword;   /*a 32-bit keystream word*/
            }
            //#defines
            UInt32 ROTR32(UInt32 x, int n)
            {
                return (((x) >> (n)) | ((x) << (32 - (n))));
            }
            UInt32 ROTL32(UInt32 x, int n)
            {
                return (((x) << (n)) | ((x) >> (32 - (n))));
            }
            UInt32 f1(UInt32 x)
            {
                return (ROTR32(x, 7) ^ ROTR32(x, 18) ^ (x >> 3));
            }
            UInt32 f2(UInt32 x)
            {
                return (ROTR32(x, 17) ^ ROTR32(x, 19) ^ (x >> 10));
            }

            /*g1 and g2 functions as defined in the HC-128 document*/
            UInt32 g1(UInt32 x, UInt32 y, UInt32 z)
            {
                return ((ROTR32((x), 10) ^ ROTR32((z), 23)) + ROTR32((y), 8));
            }
            UInt32 g2(UInt32 x, UInt32 y, UInt32 z)
            {
                return ((ROTL32((x), 10) ^ ROTL32((z), 23)) + ROTL32((y), 8));
            }


            /*function h1*/
            UInt32 h1(HC128_State state, UInt32 u)
            {
                UInt32 tem;
                Byte a, c;
                a = (Byte)((u));
                c = (Byte)((u) >> 16);
                tem = state.Q[a] + state.Q[256 + c];
                return (tem);
            }

            /*function h2*/
            UInt32 h2(HC128_State state, UInt32 u)
            {
                UInt32 tem;
                Byte a, c;
                a = (Byte)((u));
                c = (Byte)((u) >> 16);
                tem = state.P[a] + state.P[256 + c];
                return (tem);
            }

            /* one step of HC-128: 
               state is updated;
               a 32-bit keystream word is generated and stored in "state.keystreamword";
            */
            void OneStep(HC128_State state)
            {
                UInt32 i, i3, i10, i12, i511;

                i = state.counter1024 & 0x1ff;
                i3 = (i - 3) & 0x1ff;
                i10 = (i - 10) & 0x1ff;
                i12 = (i - 12) & 0x1ff;
                i511 = (i - 511) & 0x1ff;

                if (state.counter1024 < 512)
                {
                    state.P[i] = state.P[i] + g1(state.P[i3], state.P[i10], state.P[i511]);
                    state.keystreamword = h1(state, state.P[i12]) ^ state.P[i];
                }
                else
                {
                    state.Q[i] = state.Q[i] + g2(state.Q[i3], state.Q[i10], state.Q[i511]);
                    state.keystreamword = h2(state, state.Q[i12]) ^ state.Q[i];
                }
                state.counter1024 = (state.counter1024 + 1) & 0x3ff;
            }
            /* one step of HC-128 in the intitalization stage: 
               a 32-bit keystream word is generated to update the state;
             */
            void InitOneStep(HC128_State state)
            {
                UInt32 i, i3, i10, i12, i511;

                i = state.counter1024 & 0x1ff;
                i3 = (i - 3) & 0x1ff;
                i10 = (i - 10) & 0x1ff;
                i12 = (i - 12) & 0x1ff;
                i511 = (i - 511) & 0x1ff;

                if (state.counter1024 < 512)
                {
                    state.P[i] = state.P[i] + g1(state.P[i3], state.P[i10], state.P[i511]);
                    state.P[i] = h1(state, state.P[i12]) ^ state.P[i];
                }
                else
                {
                    state.Q[i] = state.Q[i] + g2(state.Q[i3], state.Q[i10], state.Q[i511]);
                    state.Q[i] = h2(state, state.Q[i12]) ^ state.Q[i];
                }
                state.counter1024 = (state.counter1024 + 1) & 0x3ff;
            }


            /*this function initialize the state using 128-bit key and 128-bit IV*/
            void Initialization(HC128_State state, UInt32[] key, Byte[] iv)
            {

                UInt32[] W = new UInt32[1024 + 256];
                int i;

                /*expand the key and iv into the state*/

                for (i = 0; i < 4; i++) { W[i] = key[i]; W[i + 1] = key[i]; }
                for (i = 0; i < 4; i++) { W[i + 2] = iv[i]; W[i + 3] = iv[i]; }

                for (i = 16; i < 1024 + 256; i++) W[i] = (UInt32)(f2(W[i - 2]) + W[i - 7] + f1(W[i - 15]) + W[i - 16] + i);

                for (i = 0; i < 512; i++) state.P[i] = W[i + 256];
                for (i = 0; i < 512; i++) state.Q[i] = W[i + 256 + 512];

                state.counter1024 = 0;

                /*update the cipher for 1024 steps without generating output*/
                for (i = 0; i < 1024; i++) InitOneStep(state);
            }

            /* this function encrypts a message*/
            void EncryptMessage(HC128_State state, Byte[] message, Byte[] ciphertext, UInt64 msglength)
            {
                UInt64 i;
                UInt32 j;
                int messageIndex = 0, ciphertextIndex = 0;

                /*encrypt a message, each time 4 bytes are encrypted*/
                for (i = 0; (i + 4) <= msglength; i += 4, messageIndex += 4, ciphertextIndex += 4)
                {
                    /*generate 32-bit keystream and store it in state.keystreamword*/
                    OneStep(state);

                    {
                        /*encrypt 32 bits of the message*/

                        Byte[] ciphertextBytes = ConvertUtil.ConvertUInt32ToBytes(BitConverter.ToUInt32(message, messageIndex) ^ state.keystreamword);
                        for (int byteIndex = 0; byteIndex < 4; byteIndex++)
                            ciphertext[ciphertextIndex + byteIndex] = ciphertextBytes[byteIndex];
                    }
                }
                /*encrypt the last message block if the message length is not multiple of 4 bytes*/
                if ((msglength & 3) != 0)
                {
                    OneStep(state);
                    for (j = 0; j < (msglength & 3); j++)
                    {

                        ciphertext[j] = (byte)(message[j] ^ BitConverter.GetBytes(state.counter1024)[j]);

                    }
                }
            }

            /* this function encrypts a message,
               there are four inputs to this function: a 128-bit key, a 128-bit iv, a message, the message length in bytes
               one output from this function: ciphertext
            */
            void stuffCiphertext(Byte[] key, Byte[] iv, Byte[] message, Byte[] ciphertext, UInt64 msglength)
            {
                HC128_State state = new HC128_State();

                /*initializing the state*/
                Initialization(state, ConvertUtil.ConvertBytesToUInt32s(key), iv);

                /*encrypt a message*/
                EncryptMessage(state, message, ciphertext, msglength);
            }
            #endregion
            Byte[] getCiphertext() //执行上述代码并返回密钥Byte[]
            {
                stuffCiphertext(generator.key, generator.iv, generator.message, generator.ciphertext, generator.msglength);
                return generator.ciphertext;
            }

            /// <summary>
            /// 对已有的明文进行加密，返回密文字节流
            /// </summary>
            /// <param name="明文"></param>
            /// <returns>密文</returns>
            public Byte[] executeEncoding(Byte[] 明文)
            {
                Byte[] keyStream = this.getCiphertext(); //执行HC128算法，获得密钥流
                Byte[] 密文 = new Byte[明文.Count()];
                for (int i = 0; i < 明文.Length; i++) //开始加密
                {
                    密文[i] = 明文[i];
                    密文[i] ^= keyStream[i % keyStream.Length];
                }
                return 密文;
            }

            /// <summary>
            /// 对已有的密文进行解密，返回明文字节流
            /// </summary>
            /// <param name="密文"></param>
            /// <returns>明文</returns>
            public Byte[] executeDecoding(Byte[] 密文)
            {
                return executeEncoding(密文);
            }

            public HC128Algorithm(Generator generator)
            {
                this.generator = generator;
            }

            Generator generator; //储存getCiphertext函数所需要的几个参数，主要使用Generator.key
        }
        #endregion
    }
}
