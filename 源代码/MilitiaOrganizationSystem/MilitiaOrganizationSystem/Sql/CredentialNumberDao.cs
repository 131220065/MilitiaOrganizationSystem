using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MilitiaOrganizationSystem
{
    public class CredentialNumberDao
    {//身份证号数据文件访问层
        public const string CredinumberFolder = "CredentialNumbers";
        private Dictionary<string, List<string>> crediNumbersDict { get; set; }
        

        public CredentialNumberDao()
        {//构造函数
            if(!Directory.Exists(CredinumberFolder))
            {
                Directory.CreateDirectory(CredinumberFolder);
            }

            crediNumbersDict = new Dictionary<string, List<string>>();//初始化字典
        }

        public void saveChanges(string database)
        {//保存改变
            List<string> cList = crediNumbersDict[database];
            string databaseFile = getDatabaseFile(database);
            StreamWriter sw = new StreamWriter(databaseFile);
            if(cList.Count == 0)
            {
                sw.Write("");
                sw.Close();
                return;
            }
            string str = cList[0];
            for(int i = 1; i < cList.Count; i++)
            {
                str += "," + cList[i];
            }
            sw.Write(str);
            sw.Close();
        }

        private string getDatabaseFile(string database)
        {//通过数据库名获取文件名
            return CredinumberFolder + "\\" + database;
        }

        public List<string> getCredinumbersOfDatabase(string database)
        {//通过数据库名获取身份证号
            string databaseFile = getDatabaseFile(database);
            List<string> cList;
            if (!crediNumbersDict.TryGetValue(database, out cList))
            {
                cList = getCredinumbersFromFile(databaseFile);
                crediNumbersDict[database] = cList;
            }
            return cList;
        }

        private List<string> getCredinumbersFromFile(string databaseFile)
        {//从文件中获取身份证号列表
            
            if(!File.Exists(databaseFile))
            {
                FileStream fs = new FileStream(databaseFile, FileMode.Create);
                fs.Close();
                return new List<string>();
            }
            StreamReader sr = new StreamReader(databaseFile);
            string stream = sr.ReadToEnd();
            sr.Close();
            if(stream == null || stream == "")
            {
                return new List<string>();
            }
            return stream.Split(new char[] { ',' }).ToList();
        }

        public void addAndSaveCrediNumber(string crediNumber, string database)
        {//增加一个身份证号
            string databaseFile = getDatabaseFile(database);
            List<string> cList = getCredinumbersOfDatabase(database);
            cList.Add(crediNumber);
            FileStream fs = new FileStream(databaseFile, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            if(cList.Count == 1)
            {
                sw.Write(crediNumber);
            } else
            {
                sw.Write("," + crediNumber);
            }
            
            
            sw.Close();
            fs.Close();
        }

        public void removeCrediNumber(string crediNumber, string database)
        {//删除一个身份证号
            List<string> cList = getCredinumbersOfDatabase(database);
            cList.Remove(crediNumber);
        }

        public void editCrediNumber(string oldCrediNumber, string newCrediNumber, string database)
        {//修改一个身份证号
            List<string> cList = getCredinumbersOfDatabase(database);
            int index = cList.IndexOf(oldCrediNumber);
            if(index >= 0)
            {
                cList[index] = newCrediNumber;
            }
            saveChanges(database);
        }

        public void removeDatabase(string database)
        {//删除数据库对应的身份证号文件，以及字典中的
            crediNumbersDict.Remove(database);
            string databaseFile = getDatabaseFile(database);
            File.Delete(databaseFile);
        }
    }
}
