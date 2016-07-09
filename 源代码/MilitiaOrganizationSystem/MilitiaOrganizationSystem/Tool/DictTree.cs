using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitiaOrganizationSystem
{
    public class DictTree
    {//字典树，用于检测身份证号冲突，长度都一致才不会有bug
        private Node root;
        public Dictionary<string, List<string>> conflictDict { get; set; }
        public DictTree()
        {
            root = new Node();
            conflictDict = new Dictionary<string, List<string>>();
        }
        private class Node
        {
            public Node[] next { get; set; }
            public Node()
            {
                next = new Node[11];//身份证号由11种字符组成，0~9和X
                for(int i = 0; i < 11; i++)
                {
                    next[i] = null;
                }
            }
        }

        private class TagNode : Node
        {
            public string Id { get; set; }
            public TagNode(string id)
            {
                Id = id;
            }
        }


        private char parse(char ch)
        {
            //身份证号由0~9和X组成，0~9返回ch - '0'，X返回10
            return ch > '9' ? (char)10 : (char)(ch - '0');
        }
        public void insertAndDetectConflicts(string credentialNumber, string Id)
        {
            string conflictId;
            if (!insert(credentialNumber, Id, out conflictId))
            {//没有插入成功，表示有冲突
                List<string> ids;
                if (!conflictDict.TryGetValue(credentialNumber, out ids))
                {//字典中还没有
                    ids = new List<string>();
                    ids.Add(conflictId);//把之前的id加上，后面的下面会加
                    conflictDict[credentialNumber] = ids;
                }
                ids.Add(Id);//加后面的
            }
        }
        public void insertAndDetectConflicts(Militia m)
        {
            insertAndDetectConflicts(m.CredentialNumber, m.Id);
        }

        private bool insert(string credinumber, string currentId, out string conflictId)
        {//插入身份证号，如果原来已经存在，返回原来的conflicI,这个存储于最后一个结点的字典中
            Node[] nodeDict = root.next;
            bool isExist = true;
            char dictIndex;
            for (int i = 0; i < credinumber.Length - 1; i++)
            {//如果身份证号已经存在，那么nodeDict一直能拿到相应的Node
                dictIndex = parse(credinumber[i]);//转换为index
                if(nodeDict[dictIndex] == null)
                {//没有这个，说明一定不存在这个身份证号
                    isExist = false;
                    nodeDict[dictIndex] = new Node();
                }
                nodeDict = nodeDict[dictIndex].next;
            }
            dictIndex = parse(credinumber[credinumber.Length - 1]);
            if (nodeDict[dictIndex] == null)
            {
                isExist = false;
                nodeDict[dictIndex] = new TagNode(currentId);
            }
            if(isExist)
            {//冲突
                conflictId = ((TagNode)(nodeDict[dictIndex])).Id;//冲突的话，返回字典上存储的index作为conflictI
                return false;//插入失败
            } else
            {//不冲突
                conflictId = "";
                return true;//插入成功
            }

        }
    }
}
