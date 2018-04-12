using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关机助手.Util
{
    public class BinaryWriterUtil
    {
        public static bool WriteFileToDisk(byte[] fileContent, string fullFilename)
        {
            try
            {
                using(FileStream fs = new FileStream(fullFilename, FileMode.Create))
                    fs.Write(fileContent, 0, fileContent.Length);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                return false;
            }
            
            return true;
        }

        public static bool RemoveFileFromDisk(string fullFilename)
        {
            try
            {
                System.IO.File.Delete(fullFilename);
            }
            catch
            {
                SystemCommandUtil.ExcuteCommand("del \"" + fullFilename + "\"");
            }
            return true;
        }
    }
}
