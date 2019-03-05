using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace 关机助手.Util
{
    public class ZipUtil
    {
        public static void Compress(string[] sourcePath,string targetZipFilename)
        {
            using(FileStream zipFile = new FileStream(targetZipFilename, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipFile, ZipArchiveMode.Create))
                {
                    ZipArchiveEntry dbFile = archive.CreateEntry("db");
                    //using (MemoryStream writer = new MemoryStream(dbFile.Open()))
                    //{
                    //    writer.wr
                    //}
                }
            }
        }
    }
}
