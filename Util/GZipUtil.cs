using System;
using System.IO;
using System.IO.Compression;

namespace 关机助手.Util
{
    public class GZipUtil
    {
        /// <summary>
        /// 压缩该文件夹下的所有文件，每个文件压缩成一个单个文件
        /// </summary>
        /// <param name="directorySelected">要压缩文件所在的文件夹</param>
        public static void Compress(DirectoryInfo directorySelected)
        {
            foreach (FileInfo fileToCompress in directorySelected.GetFiles())
            {
                Compress(fileToCompress, new FileInfo(fileToCompress.FullName), ".gz", false);
            }
        }

        /// <summary>
        /// 将原始文件压缩成gz文件，文件名前缀不变
        /// </summary>
        /// <param name="fileToCompress">原始的文件</param>
        public static void Compress(FileInfo fileToCompress)
        {
            Compress(fileToCompress, new FileInfo(ConcatFileName(fileToCompress,".gz", false)));
        }

        /// <summary>
        /// 将原始文件压缩成gz文件
        /// </summary>
        /// <param name="fileToCompress">原始的文件</param>
        /// <param name="compressedFile">压缩后的文件</param>
        public static void Compress(FileInfo fileToCompress, FileInfo compressedFile)
        {
            Compress(fileToCompress, compressedFile, ".gz", false);
        }

        /// <summary>
        /// 将原始文件压缩成用户自定义结尾的压缩文件
        /// </summary>
        /// <param name="fileForCompress">原始的文件</param>
        /// <param name="compressedFile">压缩后的文件</param>
        /// <param name="newExtension">新拓展名</param>
        /// <param name="removeOriginalExtension">是否移除原来的拓展名</param>
        public static void Compress(FileInfo fileForCompress, FileInfo compressedFile, String newExtension, Boolean removeOriginalExtension)
        {
            
            using (FileStream originalFileStream = fileForCompress.OpenRead())
            {
                //if ((File.GetAttributes(fileToCompress.FullName) &
                //    FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != newExtension)
                {

                    using (FileStream compressedFileStream = File.Create(compressedFile.FullName))
                    {
                        using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                            CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(compressionStream);

                        }
                    }
                    FileInfo info = compressedFile;
                    Console.WriteLine("[日志] 已将 {0} 文件 {1} 压缩至 {2} 字节。",
                    fileForCompress.Name, fileForCompress.Length.ToString(), info.Length.ToString());
                }
            }
        }

        public static byte[] CompressBytes(byte[] bytes)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(bytes), CompressionMode.Compress))
            {
                using (MemoryStream outputStream = new MemoryStream())
                {
                    stream.CopyTo(outputStream);
                    return outputStream.ToArray();
                }
            }
        }

        /// <summary>
        /// 拼接新文件名，返回符合规范的文件名
        /// </summary>
        /// <param name="compressedFile">原文件</param>
        /// <param name="newExtension">新拓展</param>
        /// <param name="removeOriginalExtension">是否去掉原拓展名</param>
        /// <returns></returns>
        private static string ConcatFileName(FileInfo compressedFile, string newExtension, bool removeOriginalExtension)
        {
            String newFileName = "";
            if (removeOriginalExtension)
            {
                newFileName = compressedFile.FullName.Substring(0, compressedFile.FullName.LastIndexOf('.'));
            }
            else
                newFileName = compressedFile.FullName + newExtension;

            return newFileName;
        }

        /// <summary>
        /// 解压缩gz文件，新文件名为旧名去掉其后缀名
        /// </summary>
        /// <param name="fileToDecompress">需要解压的文件</param>
        public static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                Decompress(fileToDecompress, newFileName);
            }
        }

        /// <summary>
        /// 解压指定文件至用户自定义文件
        /// </summary>
        /// <param name="fileToDecompress">要解压的gz文件</param>
        /// <param name="decompressedFileFullname">新文件的全路径</param>
        public static void Decompress(FileInfo fileToDecompress, String decompressedFileFullname)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                using (FileStream decompressedFileStream = File.Create(decompressedFileFullname))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine("[日志] 解压文件: {0}", fileToDecompress.Name);
                    }
                }
            }
        }

        public static byte[] DecompressBytes(byte[] bytes)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress))
            {
                using (MemoryStream outputStream = new MemoryStream())
                {
                    stream.CopyTo(outputStream);
                    return outputStream.ToArray();
                }
            }
        }
    }
}
