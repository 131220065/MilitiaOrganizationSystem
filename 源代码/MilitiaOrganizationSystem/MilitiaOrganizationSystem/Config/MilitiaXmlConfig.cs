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
        private const string xmlMilitiaConfigFile = "Parameters.xml";//文件路径
        private static XmlDocument xmlDoc = null;
        private static XmlNode rootNode;//根节点

        public static XmlNodeList parameters { get; set; }//参数列表
        

        public static void initial()
        {//初始化
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
        {//根据属性名获取节点
            return rootNode.SelectSingleNode("parameter[@property='" + propertyName + "']");
        }

        public static XmlNode getNodeByName(string name)
        {//根据名称获取节点
            return rootNode.SelectSingleNode("parameter[@name='" + name + "']");
        }

        public static List<Militia> generateMilitias(int n)
        {//随机生成n个民兵
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
                            char[] arr = new char[] { '1', '2', '3', '4', '7', '8', '9', '5', '0', '6', 'X'};//身份证号就这几个字符
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
        
    }
}
