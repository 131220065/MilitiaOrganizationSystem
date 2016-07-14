using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace MilitiaOrganizationSystem
{
    static class LoginXmlConfig
    {//登录需要配置的文件，存有密码，客户端类型和采集地信息
        private const string xmlFile = "config.xml";//文件路径
        private const string initialPsd = "12345";//初始密码

        public static string Place { get; set; }//省市区县的ID,格式：PID-CID-DID,也是数据库名
        public static string Psd { get; set; }//加密后的密码
        public static string ClientType { get; set; }//基层，区县人武部，市军分区，省军分区

        public static string Id { get; set; }//随机生成的id号,区县可能用到，区分不同的区县

        private static XmlDocument xmlDoc;//xml文件Doc

        private static void save()
        {//保存文件
            xmlDoc.Save(xmlFile);
        }

        public static void initial()
        {//初始化
            xmlDoc = new XmlDocument();
            XmlNode rootNode = null;
            if(File.Exists(xmlFile))
            {//如果文件存在，则加载到xmlDoc中
                xmlDoc.Load(xmlFile);
                rootNode = xmlDoc.DocumentElement;
            } else
            {//如果不存在，则新建
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(dec);
                rootNode = xmlDoc.CreateElement("root");
                xmlDoc.AppendChild(rootNode);

                XmlElement typeNode = xmlDoc.CreateElement("ClientType");
                XmlAttribute typeAttribute = xmlDoc.CreateAttribute("value");
                typeAttribute.Value = "";
                typeNode.Attributes.Append(typeAttribute);
                rootNode.AppendChild(typeNode);

                XmlElement placeNode = xmlDoc.CreateElement("Place");
                XmlAttribute placeAttribute = xmlDoc.CreateAttribute("value");
                placeAttribute.Value = "";
                placeNode.Attributes.Append(placeAttribute);
                rootNode.AppendChild(placeNode);

                XmlElement psdNode = xmlDoc.CreateElement("Psd");
                XmlAttribute psdAttribute = xmlDoc.CreateAttribute("value");
                psdAttribute.Value = md5.encrypt(initialPsd);
                psdNode.Attributes.Append(psdAttribute);
                rootNode.AppendChild(psdNode);

                XmlElement idNode = xmlDoc.CreateElement("Id");
                XmlAttribute idAttribute = xmlDoc.CreateAttribute("value");
                idAttribute.Value = new Random().Next().ToString();
                idNode.Attributes.Append(idAttribute);
                rootNode.AppendChild(idNode);

                save();//保存
            }
            //获取初始化的几个
            Place = rootNode.SelectSingleNode("Place").Attributes["value"].Value;
            Psd = rootNode.SelectSingleNode("Psd").Attributes["value"].Value;
            ClientType = rootNode.SelectSingleNode("ClientType").Attributes["value"].Value;
            Id = rootNode.SelectSingleNode("Id").Attributes["value"].Value;
        }

        public static void setPsd(string clientType, string place, string psd)
        {//保存设置
            ClientType = clientType;
            Place = place;
            Psd = md5.encrypt(psd);

            XmlNode rootNode = xmlDoc.DocumentElement;
            rootNode.SelectSingleNode("Place").Attributes["value"].Value = Place;
            rootNode.SelectSingleNode("Psd").Attributes["value"].Value = Psd;
            rootNode.SelectSingleNode("ClientType").Attributes["value"].Value = ClientType;

            save();
        }

        public static bool loginSuccess(string psd)
        {//判断是否登录成功
            if(md5.encrypt(psd) == Psd)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
