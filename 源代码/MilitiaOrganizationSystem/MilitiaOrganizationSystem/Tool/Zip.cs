using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace MilitiaOrganizationSystem
{
    public class Zip
    {
        private ZipOutputStream s;//流
        public Zip(string strZip, string psd, int level)
        {
            //strZip是要压缩的文件名路径,会创建或覆盖; 
            //psd是压缩文件的密码,之后会被加密
            //level是压缩的级别
            s = new ZipOutputStream(File.Create(strZip));//初始化流
            s.SetLevel(level);
            s.Password = md5.encrypt(psd);//设置密码
        }

        private void zipOnlyFile(string file, ZipOutputStream s, int startIndex)
        {//压缩一个文件, startIndex用以生成相对路径
         //打开压缩文件
            FileStream fs = File.OpenRead(file);

            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);//转为字节流？

            string tempfile = file.Substring(startIndex);//相对路径

            ZipEntry entry = new ZipEntry(tempfile);

            entry.DateTime = DateTime.Now;
            entry.Size = fs.Length;

            fs.Close();

            s.PutNextEntry(entry);

            s.Write(buffer, 0, buffer.Length);
        }

        private void zip(string strFile, ZipOutputStream s, int startIndex)
        {
            //压缩文件夹,startIndex是此文件夹下开始的下标
            if (File.Exists(strFile))
            {
                //如果压缩的是文件，则直接压缩文件
                zipOnlyFile(strFile, s, startIndex);
            }
            else
            {
                //否则压缩文件夹
                string[] filenames = Directory.GetFileSystemEntries(strFile);
                foreach (string file in filenames)
                {
                    zip(file, s, startIndex);
                }

            }
        }

        public void addFileOrFolder(string strFile)
        {
            //向压缩文件里加文件或文件夹
            int startIndex = strFile.LastIndexOf(Path.GetFileName(strFile));
            zip(strFile, s, startIndex);
        }

        public void close()
        {
            //关闭，必须调用
            s.Finish();
            s.Close();
        }
    }
}
