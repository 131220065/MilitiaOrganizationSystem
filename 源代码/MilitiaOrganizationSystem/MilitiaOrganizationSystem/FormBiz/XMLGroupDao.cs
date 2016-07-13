using Raven.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MilitiaOrganizationSystem
{
    class XMLGroupDao
    {//分组任务xml访问层
        private XmlDocument xmlDoc;
        private XmlNode rootNode;//根节点
        private SqlBiz sqlBiz;//数据库业务逻辑层

        private TreeView groups_TreeView;//想了想还是把这个加上吧，这个类的对象应该是绑定了一个TreeView的

        /**private long maxId;//组中最大id

        private void setId(XmlNode node)
        {
            XmlAttribute idAttr = xmlDoc.CreateAttribute("Id");
            idAttr.Value = maxId.ToString();
            maxId++;
            node.Attributes.SetNamedItem(idAttr);//添加id
        }*/

        private void saveXml()
        {//保存xml文件
            GroupXmlConfig.saveXml();
        }

        public string getToolTipText(XmlNode node)
        {//返回鼠标移到节点上面所显示的东西
            string ToolTipText = "";
            foreach (XmlAttribute attr in node.Attributes)
            {
                ToolTipText += attr.Name + "=\"" + attr.Value + "\" ";
            }

            return ToolTipText;
        }
        
        public XMLGroupDao(SqlBiz sBiz, TreeView tv)
        {//构造函数,传进需要保存的xml文件名,之后就从这个文件中读取和保存
            xmlDoc = GroupXmlConfig.xmlDoc;
            rootNode = GroupXmlConfig.rootNode;
            sqlBiz = sBiz;//数据库业务逻辑层
            groups_TreeView = tv;
        }

        public void loadToTreeView()
        {
            Dictionary<string, FacetValue> fDict = sqlBiz.getGroupNums();
            addXmlNodeToTreeNode(rootNode, groups_TreeView.Nodes, fDict);
        }

        public void addCountUpToAllParent(TreeNode tn, int Count)
        {
            while(tn.Parent != null)
            {
                GroupTag gt = (GroupTag)tn.Parent.Tag;
                gt.Count += Count;
                tn.Parent.Text = gt.info();

                tn = tn.Parent;//迭代
            }
            
        }

        private void addXmlNodeToTreeNode(XmlNode root, TreeNodeCollection rootNodes, Dictionary<string, FacetValue> fDic)
        {//将root下的所有节点加载到treeView中
            foreach (XmlNode node in root.ChildNodes)
            {

                TreeNode treeNode = rootNodes.Add(node.Attributes["name"].Value);

                treeNode.ToolTipText = getToolTipText(node);

                treeNode.Name = GroupXmlConfig.getNodePath(node);//查找TreeNode的Key

                GroupTag tag = new GroupTag(node);

                treeNode.Text = tag.info();

                if (!node.HasChildNodes)
                {//是叶节点,则获取此组下的民兵，并将民兵添加到treeView中
                    //应该是获取数量，并显示到节点上
                    FacetValue fv;
                    if(fDic.TryGetValue(treeNode.Name, out fv))
                    {
                        tag.Count += fv.Hits;//本身加
                        addCountUpToAllParent(treeNode, fv.Hits);//所有父节点加
                    }    
                    
                    treeNode.Text = tag.info();              
                }

                treeNode.Tag = tag;//记录节点

                addXmlNodeToTreeNode(node, treeNode.Nodes, fDic);
            }
        }

        public void modifyGroupName(XmlNode node, string newName)
        {//编辑组名
            node.Attributes["name"].Value = newName;
            saveXml();
        }

        public void modifyAllAttribute(XmlNode node, Dictionary<string, string> newAttributes)
        {//编辑组的所有属性
            foreach(KeyValuePair<string, string> attr in newAttributes)
            {
                XmlAttribute xmlAttr = xmlDoc.CreateAttribute(attr.Key);
                xmlAttr.Value = attr.Value;
                node.Attributes.SetNamedItem(xmlAttr);
            }
            saveXml();
            
        }
        
        public void deleteGroup(XmlNode node)
        {//删除组
            node.ParentNode.RemoveChild(node);//删除xml里面
            saveXml();
        }

        public GroupTag addGroupFromRoot(string groupName)
        {//从根节点增加组
            XmlElement xmlNode = xmlDoc.CreateElement("team");
           
            /**setId(xmlNode);*/
 
            xmlNode.SetAttribute("name", "新建组");
            rootNode.AppendChild(xmlNode);
            saveXml();
            return new GroupTag(xmlNode);
        }

        public GroupTag addGroupFromParent(XmlNode parentNode, string groupName)
        {//从父节点增加组
            XmlElement xmlNode = xmlDoc.CreateElement("team");

            /**setId(xmlNode);//设置Id*/

            xmlNode.SetAttribute("name", "新建组");
            parentNode.AppendChild(xmlNode);
            saveXml();

            return new GroupTag(xmlNode);
        }

        private void mergeNode(XmlNode xmlNode, XmlNode xdNode)
        {//将xdNode的子节点合并到xmlNode的子节点中
            foreach(XmlNode xdChildNode in xdNode.ChildNodes)
            {
                bool isFinded = false;
                foreach(XmlNode xmlChildNode in xmlNode.ChildNodes)
                {
                    //MessageBox.Show(xdChildNode.Attributes["name"].Value + " " + xmlChildNode.Attributes["name"].Value);
                    if(xdChildNode.Attributes["name"].Value == xmlChildNode.Attributes["name"].Value)
                    {//这个节点有相同的
                        if(xdChildNode.HasChildNodes)
                        {//有子节点
                            mergeNode(xmlChildNode, xdChildNode);
                        }
                        isFinded = true;
                    }
                }

                if(!isFinded)
                {//如果没有找到与xdChildNode相同的子节点，则将xdChildNode添加到xdNode的子节点中
                    XmlNode newNode = xmlNode.AppendChild(xmlDoc.ImportNode(xdChildNode, true));
                    
                }
            }

        }

        public void addXml(string xmlFile)
        {//将xmlFile合并到xmlDoc中并保存,如果database=null，则不会更新界面
            XmlDocument xd = new XmlDocument();
            xd.Load(xmlFile);//加载

            XmlNode xdRoot = xd.DocumentElement;//根节点

            mergeNode(rootNode, xdRoot);

            saveXml();
        }

        public void exportXml(string fileName)
        {//将xml文件导出
            xmlDoc.Save(fileName);
        }
    }

}
