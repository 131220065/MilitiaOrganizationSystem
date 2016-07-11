using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace MilitiaOrganizationSystem
{
    public class FileTool
    {
        public static void CopyFolder(string strFromPath, string strToPath)
        {//将一个文件夹及其所有子文件、文件夹复制到另一个文件夹下
            if (!Directory.Exists(strFromPath) || !Directory.Exists(strToPath))
            {
                return;
            }
            DirectoryInfo dirInfo = new DirectoryInfo(strFromPath);
            if (!Directory.Exists(strToPath + "\\" + dirInfo.Name))
            {
                Directory.CreateDirectory(strToPath + "\\" + dirInfo.Name);
            }
            FileInfo[] fis = dirInfo.GetFiles();
            foreach (FileInfo fi in fis)
            {//对文件的拷贝
                fi.CopyTo(strToPath + "\\" + dirInfo.Name + "\\" + fi.Name, true);
                //File.Copy(fi.FullName, strToPath + "\\" + dirInfo.Name + "\\" + fi.Name, true);
            }
            DirectoryInfo[] dis = dirInfo.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {//对子文件夹的拷贝
                CopyFolder(di.FullName, strToPath + "\\" + dirInfo.Name);
            }

        }

        public static void MilitiaListToXml(List<Militia> mList, string xmlFile)
        {
            XmlDocument militiaDoc = new XmlDocument();
            XmlDeclaration dec = militiaDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            militiaDoc.AppendChild(dec);
            XmlElement militiaRoot = militiaDoc.CreateElement("root");
            militiaDoc.AppendChild(militiaRoot);
            //以上是新建xml文档对象

            foreach (Militia militia in mList)
            {
                MilitiaReflection mr = new MilitiaReflection(militia);//反射
                XmlElement militiaNode = militiaDoc.CreateElement("militia");
                foreach(string key in MilitiaReflection.keys)
                {
                    if(key == "Id")
                    {//Id不存入Xml文件中
                        continue;
                    }
                    XmlAttribute attr = militiaDoc.CreateAttribute(key);
                    try
                    {
                        attr.Value = mr.getProperty(key).ToString();
                    } catch
                    {
                        attr.Value = "";
                    }
                    
                    militiaNode.Attributes.Append(attr);
                }

                militiaRoot.AppendChild(militiaNode);
            }

            militiaDoc.Save(xmlFile);
        }

        public static List<Militia> XmlToMilitiaList(string xmlFile)
        {//xml文件必然是<root><militia Name="" Sex="".. />...</root>的形式
            XmlDocument militiaDoc = new XmlDocument();
            List<Militia> mList = new List<Militia>();
            try
            {
                militiaDoc.Load(xmlFile);
                XmlNode rootNode = militiaDoc.DocumentElement;
                foreach(XmlNode militiaNode in rootNode.ChildNodes)
                {
                    Militia militia = new Militia();
                    MilitiaReflection mr = new MilitiaReflection(militia);//反射
                    foreach(XmlAttribute xa in militiaNode.Attributes)
                    {
                        mr.setProperty(xa.Name, xa.Value);//设置属性
                    }
                    mList.Add(militia);
                }

                return mList;
            } catch
            {//有任何异常，即返回null
                return null;
            }
        }

    }
}
