using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace MilitiaOrganizationSystem
{
    static class GroupXmlConfig
    {//分组对应的文件
        public const string xmlGroupFile = "xmlGroupFile.xml";//分组的配置文件
        public static XmlDocument xmlDoc { get; set; }//加载groupxml文件的
        public static XmlNode rootNode { get; set; }//根节点

        public static void initial()
        {//初始化
            xmlDoc = new XmlDocument();
            if (File.Exists(xmlGroupFile))
            {//如果文件存在，则加载到xmlDoc中
                xmlDoc.Load(xmlGroupFile);
                rootNode = xmlDoc.DocumentElement;
            }
            else
            {//如果不存在，则新建一个空的xml文件
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(dec);
                rootNode = xmlDoc.CreateElement("root");
                xmlDoc.AppendChild(rootNode);
                saveXml();
            }
        }

        public static void loadToTreeViewSimplely(TreeView groupTreeView)
        {//将分组文件朴素地加载到treeView上
            addXmlNodeToTreeNode(rootNode, groupTreeView.Nodes);
        }

        public static string getNodePath(XmlNode node)
        {//获取某个node的路径
            string path = node.Attributes["name"].Value;
            while (node.ParentNode.Attributes["name"] != null)
            {
                path = node.ParentNode.Attributes["name"].Value + "/" + path;
                node = node.ParentNode;
            }
            return path;
        }

        public static XmlNode getNodeByPath(string path)
        {//根据路径获取xml结点
            string[] nodeNames = path.Split(new char[] { '/' });
            XmlNode node = rootNode;
            foreach(string nodeName in nodeNames)
            {
                node = node.SelectSingleNode("team[@name='" + nodeName + "']");
                if(node == null)
                {
                    return null;
                }
            }
            return node;
        }

        private static void addXmlNodeToTreeNode(XmlNode root, TreeNodeCollection rootNodes)
        {//将root下的所有节点加载到treeView中
            foreach (XmlNode node in root.ChildNodes)
            {
                TreeNode treeNode = rootNodes.Add(node.Attributes["name"].Value);

                GroupTag tag = new GroupTag(node);

                treeNode.Tag = tag;//记录节点

                addXmlNodeToTreeNode(node, treeNode.Nodes);
            }
        }

        public static void saveXml()
        {//保存文件
            xmlDoc.Save(xmlGroupFile);
        }

    }
}
