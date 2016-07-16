using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitiaOrganizationSystem
{
    public class ConflictDetector
    {//用于检测身份证号冲突，长度都一致才不会有bug
        public Dictionary<string, List<string>> conflictDict { get; set; }
        //冲突字典
        private object root;//根节点

        public ConflictDetector()
        {//构造函数
            conflictDict = new Dictionary<string, List<string>>();
            root = new object[11];//根节点是长度为11的数组
        }

        private char parse(char ch)
        {
            //身份证号由0~9和X组成，0~9返回ch - '0'，X返回10
            return ch > '9' || ch < '0' ? (char)10 : (char)(ch - '0');
        }
        public void insertAndDetectConflicts(string credentialNumber, string database)
        {//插入并检测冲突
            string conflictDatabase;
            if (!insert(credentialNumber, database, out conflictDatabase))
            {//没有插入成功，表示有冲突
                List<string> databases;
                if (!conflictDict.TryGetValue(credentialNumber, out databases))
                {//字典中还没有
                    databases = new List<string>();
                    databases.Add(conflictDatabase);//把之前的id加上，后面的下面会加
                    conflictDict[credentialNumber] = databases;
                }
                if(!databases.Contains(database))
                {//有可能和之前的是同一数据库的，就忽略
                    databases.Add(database);//加后面的
                }
            }
        }

        private bool insert(string credinumber, string currentDatabase, out string conflictDatabase)
        {//插入身份证号，如果原来已经存在，返回原来的conflictDatabase,这个存储于最后一个结点的字典中
            object[] nodeDict = (object[])root;
            bool isExist = true;
            char dictIndex;
            for (int i = 0; i < credinumber.Length - 1; i++)
            {//如果身份证号已经存在，那么nodeDict一直能拿到相应的Node
                dictIndex = parse(credinumber[i]);//转换为index
                if(nodeDict[dictIndex] == null)
                {//没有这个，说明一定不存在这个身份证号,则新建节点
                    isExist = false;
                    nodeDict[dictIndex] = new object[11];
                }
                nodeDict = (object[])nodeDict[dictIndex];
            }
            dictIndex = parse(credinumber[credinumber.Length - 1]);
            if (nodeDict[dictIndex] == null)
            {//将最后一个赋值为数据库名
                isExist = false;
                nodeDict[dictIndex] = currentDatabase;
            }
            if(isExist)
            {//冲突
                conflictDatabase = (string)nodeDict[dictIndex]; //冲突的话，给conflictDatabase赋值
                return false;//插入失败
            } else
            {//不冲突
                conflictDatabase = "";
                return true;//插入成功
            }

        }
    }
}
