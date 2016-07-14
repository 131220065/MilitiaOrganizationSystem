using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace MilitiaOrganizationSystem
{
    public class UnZip
    {//解压缩
        private ZipInputStream s;
        private string path;//要压缩到的文件夹路径

        public UnZip(string zipFile, string fileDir, string psd)
        {//加密解压缩
            path = fileDir;
            s = new ZipInputStream(File.OpenRead(zipFile.Trim()));
            s.Password = md5.encrypt(psd);
        }

        public void unzipAll()
        {//解压全部
            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                FileStream streamWriter = null;
                int index = theEntry.Name.IndexOf('\\');
                string rootDir = index >= 0 ? theEntry.Name.Substring(0, index) : "";
                if (rootDir == "")
                {//是一级文件,这个项目中，一级文件只有导出的xmlFile(分组或民兵数据)
                    streamWriter = File.Create(path + "\\" + theEntry.Name);
                }
                else
                {//是多级文件
                    string dir = path + "\\" + Path.GetDirectoryName(theEntry.Name);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    streamWriter = File.Create(path + "\\" + theEntry.Name);
                }

                int size = 2048;
                byte[] data = new byte[size];
                while (true)
                {
                    size = s.Read(data, 0, data.Length);
                    if (size > 0)
                    {
                        streamWriter.Write(data, 0, size);
                    }
                    else
                    {
                        break;
                    }
                }

                streamWriter.Close();
            }
        }

        public void close()
        {
            s.Close();
        }
    }
}
