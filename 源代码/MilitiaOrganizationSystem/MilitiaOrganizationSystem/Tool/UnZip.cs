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
    {
        //解压缩
        private ZipInputStream s;
        private string path;//要压缩到的文件夹路径

        public UnZip(string zipFile, string fileDir, string psd)
        {
            //加密解压缩
            path = fileDir;
            s = new ZipInputStream(File.OpenRead(zipFile.Trim()));
            s.Password = md5.encrypt(psd);
        }

        public List<string> unzipAll()
        {
            //返回成功的一级子文件夹的名称，在这个工程里面，实际上就是databases
            List<string> databases = new List<string>();
            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                FileStream streamWriter = null;
                int index = theEntry.Name.IndexOf('\\');
                string rootDir = index >= 0 ? theEntry.Name.Substring(0, index) : "";
                if (rootDir == "")
                {
                    //是一级文件,这个项目中，一级文件只有导出的xmlFile(分组或民兵数据)
                    streamWriter = File.Create(path + "\\" + theEntry.Name);
                }
                else
                {
                    //是多级文件
                    if (!databases.Contains(rootDir))
                    {
                        //加入databases
                        if(Directory.Exists(path + "\\" + rootDir))
                        {
                            //还没有包含在databases中，说明第一次访问这个rootDir，但是如果它已经存在，说明产生了冲突
                            continue;
                        }
                        databases.Add(rootDir);
                    }
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

            return databases;
        }

        public void close()
        {
            s.Close();
        }
    }
}
