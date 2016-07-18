using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MilitiaOrganizationSystem
{
    class GroupTag
    {//标记，将xmlNode与treeNode相连

        public XmlNode tagXmlNode { get; set; }//treeNode对应的xmlNode
        public int Count { get; set; }//代表此组下的民兵数量

        private int Sum { get; } //此组下应该包含的民兵数量

        private Stack<XmlNode> xmlNodeStack = new Stack<XmlNode>();     //使用stack的方式替代递归

        public GroupTag(XmlNode node)
        {
            this.tagXmlNode = node;
            Count = 0;
            Sum = getSum(tagXmlNode); //计算sum的值，且sum的值不会改变
        }

        public string info()
        {//用于显示
            //int requiredCount = getRequiredCount(tagXmlNode);
            //return tagXmlNode.Attributes["name"].Value + "   （已有" + Count + "人，"+ requiredCount + "/" + Sum+ ")";
            string sumString = (Sum == 0) ? "无限制" : ("" + Sum);
            return tagXmlNode.Attributes["name"].Value + " (" + Count + "/" + sumString + ")";
        }

        private int getSum(XmlNode pnode)             //得到该分组下共需要的（占据名额的）民兵数量
        {
            if (pnode.Attributes["count"] != null)     //该节点必为叶子节点，含有count属性
            {
                return int.Parse(pnode.Attributes["count"].Value);
            }

            //该节点为无count属性的叶子节点，或非叶节点
            int tmpSum = 0;
            XmlNodeList childNodesList = pnode.ChildNodes;
            if (childNodesList != null && childNodesList.Count != 0)
            {    //含有子节点,将所有子节点入栈
                foreach (XmlNode xnode in childNodesList)
                {
                    xmlNodeStack.Push(xnode);
                }
            }
            
            while(xmlNodeStack.Count != 0)
            {
                XmlNode xnode = xmlNodeStack.Pop();
                if (xnode.Attributes["count"] != null)     //该节点必为叶子节点，含有count属性
                {
                    tmpSum += int.Parse(xnode.Attributes["count"].Value);
                }
                else
                {
                    XmlNodeList childNodesListTmp = xnode.ChildNodes;
                    if (childNodesListTmp != null && childNodesListTmp.Count != 0)
                    {    //含有子节点,将所有子节点入栈
                        foreach (XmlNode xnodeTmp in childNodesListTmp)
                        {
                            xmlNodeStack.Push(xnodeTmp);
                        }
                    }
                }
            }
            return tmpSum;
        }

        /*
        private int getRequiredCount(XmlNode pnode)      //获取该分组下，已分配的占名额的民兵数量
        {
            if (pnode.Attributes["count"] != null)     //该节点必为叶子节点，含有count属性,有上限要求
            {
                return Count;
            }

            //该节点为无count属性无上限要求的叶子节点，或非页节点
            int tmpSum = 0;
            XmlNodeList childNodesList = pnode.ChildNodes;
            if (childNodesList != null && childNodesList.Count != 0)
            {    //含有子节点,将所有子节点入栈
                foreach (XmlNode xnode in childNodesList)
                {
                    xmlNodeStack.Push(xnode);
                }
            }

            while (xmlNodeStack.Count != 0)
            {
                XmlNode xnode = xmlNodeStack.Pop();
                if (xnode.Attributes["count"] != null)     //该节点必为叶子节点，含有count属性
                {
                    tmpSum += int.Parse(xnode.Attributes["currentCount"].Value);
                }
                else
                {
                    XmlNodeList childNodesListTmp = xnode.ChildNodes;
                    if (childNodesListTmp != null && childNodesListTmp.Count != 0)
                    {    //含有子节点,将所有子节点入栈
                        foreach (XmlNode xnodeTmp in childNodesListTmp)
                        {
                            xmlNodeStack.Push(xnodeTmp);
                        }
                    }
                    //不占名额的分组被忽略
                }
            }
            //无上限要求的节点，占名额的明兵数量为0
            return tmpSum;
        }
        */
    }
}
