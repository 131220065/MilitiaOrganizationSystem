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

        public GroupTag(XmlNode node)
        {
            this.tagXmlNode = node;
            Count = 0;
        }

        public string info()
        {//用于显示
            return tagXmlNode.Attributes["name"].Value + "(" + "已有民兵" + Count + "人)";
        }


    }
}
