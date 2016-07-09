using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;

namespace MilitiaOrganizationSystem
{
    static class MilitiaXmlConfig
    {//配置民兵信息类，从xml民兵信息配置文件中读取并处理
        private const string xmlMilitiaConfigFile = "Parameters.xml";
        private static XmlDocument xmlDoc = null;
        private static XmlNode rootNode;

        public static XmlNodeList parameters { get; set; }
        

        public static void initial()
        {
            if(xmlDoc == null)
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlMilitiaConfigFile);
                rootNode = xmlDoc.DocumentElement;
                parameters = rootNode.SelectNodes("parameter");
            }
        }

        public static List<int> getAllDisplayedParameterIndexs()
        {//返回可以所有可以显示的参数下标
            List<int> iList = new List<int>();
            for(int i = 0; i < parameters.Count; i++)
            {
                iList.Add(i);
            }
            return iList;
        }

        public static List<int> getEditParameterIndexs()
        {//返回需要编辑的参数下标
            List<int> iList = getAllDisplayedParameterIndexs();
            iList.RemoveRange(parameters.Count - 2, 2);
            return iList;
        }

        public static XmlNode getNodeByProperty(string propertyName)
        {
            return rootNode.SelectSingleNode("parameter[@property='" + propertyName + "']");
        }

        public static XmlNode getNodeByName(string name)
        {
            return rootNode.SelectSingleNode("parameter[@name='" + name + "']");
        }

        public static string getTypeOf(string propertyName)
        {
            XmlNode node = rootNode.SelectSingleNode("parameter[@property='" + propertyName + "']");
            return node.Attributes["type"].Value;
        }

        

        /*public static void generateMilitiaToXml(int n, string xmlFile)
        {//生成n个民兵信息到xmlFile
            Random rand = new Random();
            XmlNodeList xList = parameters();

            XmlDocument militiaDoc = new XmlDocument();
            XmlDeclaration dec = militiaDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            militiaDoc.AppendChild(dec);
            XmlElement militiaRoot = militiaDoc.CreateElement("root");
            militiaDoc.AppendChild(militiaRoot);

            for (int i = 0; i < n; i++)
            {
                XmlElement militia = militiaDoc.CreateElement("militia");
                foreach (XmlNode node in xList)
                {
                    XmlAttribute attr = militiaDoc.CreateAttribute(node.Attributes["property"].Value);
                    if (node.Attributes["type"].Value == "string")
                    {
                        byte[] buffer = new byte[8];
                        rand.NextBytes(buffer);
                        attr.Value = System.Text.Encoding.Unicode.GetString(buffer);
                    }
                    else if (node.Attributes["type"].Value == "enum")
                    {
                        attr.Value = node.ChildNodes[rand.Next(node.ChildNodes.Count)].Attributes["value"].Value;
                    }
                    militia.Attributes.Append(attr);

                }
                XmlAttribute attr2 = militiaDoc.CreateAttribute("Group");
                string[] a = { "1", "2", "3" };//分组id
                attr2.Value = a[rand.Next(a.Length)];
                militia.Attributes.Append(attr2);

                militiaRoot.AppendChild(militia);
            }

            militiaDoc.Save(xmlFile);
        }*/

        public static List<Militia> generateMilitias(int n)
        {
            Random rand = new Random();
            XmlNodeList xList = parameters;
            List<Militia> mList = new List<Militia>();
            for (int i = 0; i < n; i++)
            {
                Militia militia = new Militia();
                MilitiaReflection mr = new MilitiaReflection(militia);//反射类

                foreach (XmlNode node in xList)
                {
                    string type = node.Attributes["type"].Value;
                    string property = node.Attributes["property"].Value;
                    switch (type)
                    {
                        case "enum":
                            mr.setProperty(property, node.ChildNodes[rand.Next(node.ChildNodes.Count)].Attributes["value"].Value);
                            break;
                        case "int":
                            mr.setProperty(property, rand.Next(100));
                            break;
                        case "group":
                            mr.setProperty(property, "未分组");
                            break;
                        case "place":
                            //不赋值
                            break;
                        default://当做string处理
                            char[] arr = new char[] { '1', '2', '3', '4', '7', '8', '9', '5', '0', '6', 'X' };//身份证号
                            string value = "";
                            for(int k = 0; k < 18; k++)
                            {
                                value += arr[rand.Next(arr.Length)];
                            }
                            mr.setProperty(node.Attributes["property"].Value, value);
                            break;
                    }
                }

                mList.Add(militia);
            }
            return mList;
        }

        /*public static List<Militia> generateMilitias(int n)
        {//生成n个民兵对象为list
            Random rand = new Random();
            XmlNodeList xList = parameters();
            List<Militia> mList = new List<Militia>();
            for (int i = 0; i < n; i++)
            {
                Militia militia = new Militia();
                foreach (XmlNode node in xList)
                {       
                    if (node.Attributes["type"].Value == "string")
                    {
                        byte[] buffer = new byte[8];
                        rand.NextBytes(buffer);
                        militia.InfoDic[node.Attributes["property"].Value] = System.Text.Encoding.Unicode.GetString(buffer);
                    }
                    else if (node.Attributes["type"].Value == "enum")
                    {
                        militia.InfoDic[node.Attributes["property"].Value] = node.ChildNodes[rand.Next(node.ChildNodes.Count)].Attributes["value"].Value;
                    }
                }
                
                militia.Group = "未分组";

                mList.Add(militia);
            }
            return mList;
        }*/

        
    }
}
