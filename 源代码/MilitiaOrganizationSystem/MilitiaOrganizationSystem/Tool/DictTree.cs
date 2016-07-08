using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitiaOrganizationSystem
{
    /*class RootNode
    {
        public string Id { get; set; }
        public Dictionary<string, string> rootDict;
        public RootNode()
        {
            Id = null;
            rootDict = new Dictionary<string, string>();
        }
    }
    class Node
    {
        public string Id { get; set; }//存入数据库中的Id，也是dict中的value
        public Dictionary<string, string> nodeDict;
        public Node()
        {
            Id = null;
            nodeDict = new Dictionary<string, string>();
        }
    }*/
    class ConflictDict
    {
        public string Id { get; set; }//存入数据库的Id
        public Dictionary<string, List<char>> conflictDict { get; set; }
        public ConflictDict()
        {
            Id = null;
            conflictDict = new Dictionary<string, List<char>>();
        }
    }
    class DictTree
    {//字典树，用于检测身份证号冲突，长度都一致才不会有bug
        public string Id { get; set; }//存入数据库的Id
        private Dictionary<char, Node> root;//根结点(存首字母）
        private class Node
        {
            public Dictionary<char, Node> nodeDict { get; set; }
            public Node()
            {
                nodeDict = new Dictionary<char, Node>();
            }
        }
        public DictTree()
        {
            root = new Dictionary<char, Node>();
        }
        /*public bool insert(string credinumber)
        {//将身份证号插入到这个字典树中，如果原来已经存在身份证号，则插入失败，否则插入成功
            Dictionary<char, Node> nodeDict = root;
            Node node;
            bool isExist = true;
            for(int i = 0; i < credinumber.Length; i++)
            {//如果身份证号已经存在，那么nodeDict一直能拿到相应的Node
                if(!nodeDict.TryGetValue(credinumber[i], out node))
                {//没有这个，说明一定不存在这个身份证号
                    isExist = false;
                    node = new Node();//新建一个Node
                    nodeDict[credinumber[i]] = node;//让这个有值
                }
                nodeDict = node.nodeDict;
            }
            return !isExist;
        }*/

        public bool insert(string credinumber, char index, out char conflictI)
        {//插入身份证号，如果原来已经存在，返回原来的conflicI,这个存储于最后一个结点的字典中
            Dictionary<char, Node> nodeDict = root;
            Node node;
            bool isExist = true;
            for (int i = 0; i < credinumber.Length; i++)
            {//如果身份证号已经存在，那么nodeDict一直能拿到相应的Node
                if (!nodeDict.TryGetValue(credinumber[i], out node))
                {//没有这个，说明一定不存在这个身份证号
                    isExist = false;
                    node = new Node();//新建一个Node
                    nodeDict[credinumber[i]] = node;//让这个有值
                }
                nodeDict = node.nodeDict;
            }
            nodeDict[index] = null;//最后一个结点的字典上存有index
            if(isExist)
            {//冲突
                conflictI = nodeDict.Keys.First();
                return false;//插入失败
            } else
            {//不冲突
                conflictI = index;
                return true;//插入成功
            }

        }
    }
}
