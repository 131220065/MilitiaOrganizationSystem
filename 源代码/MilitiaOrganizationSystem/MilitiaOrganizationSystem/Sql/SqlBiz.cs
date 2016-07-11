using Raven.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MilitiaOrganizationSystem
{
    public class SqlBiz
    {//业务逻辑层
        public const string DataDir = "DataBases";//数据库文件夹

        private SqlDao sqlDao;//数据访问层

        public SqlBiz(string dbName)
        {//构造函数
            sqlDao = new SqlDao(dbName);//根据数据库实例化数据访问层
            FormBizs.sqlBiz = this;//程序中唯一的sqlBiz实例
        }

        public void addMilitias(List<Militia> mList)
        {//测试所用，添加几个民兵
            foreach(Militia m in mList)
            {
                sqlDao.saveMilitia(m);
            }
        }

        public void addMilitia(Militia militia)
        {//增
            sqlDao.saveMilitia(militia);
            FormBizs.latestMilitiaForm.newOperationOn(militia, "新添加");
        }

        public void updateMilitia(Militia militia)
        {//改
            sqlDao.saveMilitia(militia);
            FormBizs.latestMilitiaForm.newOperationOn(militia, "被编辑或改变分组");
        }

        public void deleteMilitia(Militia militia)
        {//删
            FormBizs.groupBiz.reduceCount(militia);//删除分组任务上的treeNode
            FormBizs.removeMilitiaItem(militia);//删除民兵列表界面的lvi
            sqlDao.deleteOneMilitia(militia);

            FormBizs.latestMilitiaForm.newOperationOn(militia, "被删除");
        }

        public List<string> getDatabasesByPlace(string Place)
        {//根据Militia.Place指定要查找的数据库集合, 调用此函数时， Place应不为空
            //return getDatabases();//单数据库
            if(Place == null || Place == "")
            {//如果为空，则未指定数据库，所以返回所有数据库集合
                return getDatabases();
            }
            DirectoryInfo dirInfo = new DirectoryInfo(DataDir);
            DirectoryInfo[] dis = dirInfo.GetDirectories();
            List<string> databases = new List<string>();
            foreach (DirectoryInfo di in dis)
            {
                if (di.Name.StartsWith(Place))
                {//以Place开头的就是要找的数据库
                    databases.Add(di.Name);
                }
            }
            return databases;
        }

        public List<Militia> queryByContition(System.Linq.Expressions.Expression<Func<Militia, bool>> lambdaContition, int skip, int take, out int sum, string Place = null)
        {//根据条件分页查询
            List<string> databases = getDatabasesByPlace(Place);//根据Place指定数据库组
            int[] sums = new int[databases.Count];//每个数据库下民兵的总数
            for (int i = 0; i < databases.Count; i++)
            {//获取每个数据库的总数
                sqlDao.queryByContition(lambdaContition, 0, 1, out sums[i], databases[i]);
            }

            sum = sums.Sum();//所有数据库中group下民兵总数的和
            int skipNum = 0;
            int databaseIndex = getIndexOfDatabase(sums, skip, out skipNum);
            if (databaseIndex >= sums.Length)
            {
                return new List<Militia>();
            }
            List<Militia> mList = sqlDao.queryByContition(lambdaContition, skipNum, take, out sums[databaseIndex], databases[databaseIndex]);
            databaseIndex++;//下一个数据库
            while (mList.Count < take && databaseIndex < sums.Length)
            {//如果不够，则继续从下一个数据库取数据
                mList.AddRange(sqlDao.queryByContition(lambdaContition, 0, take - mList.Count, out sums[databaseIndex], databases[databaseIndex]));
                databaseIndex++;
            }
            return mList;
        }

        private int getIndexOfDatabase(int [] sums, int skip, out int skipNum)
        {//获取应该从哪个数据库跳过skipNum个结果查找
            //sum是各个数据库民兵的总数
            int skipSum = 0;
            for(int i = 0; i < sums.Length; i++)
            {
                skipSum += sums[i];
                if(skip < skipSum)
                {
                    skipNum = skip + sums[i] - skipSum;//第i个数据库应该跳过skipNum个

                    return i;
                }
            }
            skipNum = 0;
            return sums.Length;
        }

        public void backupAllDb(string folder)
        {//将所有数据库备份到folder下
            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            List<string> databases = getDatabases();
            ProgressBarForm pbf = new ProgressBarForm(databases.Count + 1);
            pbf.Show();//显示进度条
            for(int i = 0; i < databases.Count; i++)
            {
                pbf.Increase(1, "开始导出第" + (i + 1) + "个数据库");
                string database = databases[i];
                sqlDao.backupOneDB(database, folder);
            }
            while (!isAllDbBackupCompleted()) ;//等待全部完成
            pbf.Increase(1, "导出完毕");
        }

        public bool isAllDbBackupCompleted()
        {
            bool isRunning = false;
            List<string> databases = getDatabases();
            
            foreach(string database in databases)
            {//只要有一个数据库正在备份，那么ISRunning就为true
                isRunning = isRunning || sqlDao.isBackupRunning(database);
            }
            return !isRunning;//如果都为false，说明backup完成
        }

        public void restoreDbs(string folder)
        {//恢复folder下的所有数据库
            string[] databaseFolders = Directory.GetDirectories(folder);
            ProgressBarForm pbf = new ProgressBarForm(databaseFolders.Length);
            pbf.Show();
            foreach (string database in databaseFolders)
            {
                //等会在这里写个trycatch
                sqlDao.restoreOneDB(database);
                pbf.Increase(1, "导入" + database + "数据库成功");
            }
        }

        public void exportZip(Zip zip)
        {//将所有出System的数据库导入到zip中
            backupAllDb("export");
            string[] databases = Directory.GetDirectories("export");
            foreach(string database in databases)
            {
                zip.addFileOrFolder(database);
                Directory.Delete(database, true);//删除
            }
        }

        /*public void exportAsFile(string file)
        {//导出为文件，仅基层调用这个
            int sum;
            List<Militia> mList = sqlDao.getMilitias(0, 10000, out sum);
            FileTool.MilitiaListToXml(mList, file);//xml文件
        }
        public void importFormFile(string file)
        {//从文件中导入，仅区县人武部调用
            List<Militia> mList = FileTool.XmlToMilitiaList(file);
            foreach (Militia m in mList)
            {
                sqlDao.saveMilitia(m);
            }
        }*/

        public void exportAsFile(string file)
        {//导出为文件，仅基层调用这个
            StreamWriter sw = new StreamWriter(file);
            int sum;
            List<Militia> mList = sqlDao.getMilitias(0, 10000, out sum);
            ProgressBarForm pbf = new ProgressBarForm(mList.Count / 100);
            pbf.Show();
            for (int i = 0; i < mList.Count; i++)
            {
                Militia m = mList[i];
                sw.WriteLine(MilitiaReflection.militiaToString(m));
                if((i + 1) % 100 == 0)
                {
                    pbf.Increase(1, "已导出100个民兵");
                }
            }
            sw.Close();
        }
        public void importFormFile(string file)
        {//从文件中导入，仅区县人武部调用
            StreamReader sr = new StreamReader(file);
            string line;
            ProgressBarForm pbf = new ProgressBarForm(1);
            pbf.Show();//进度条
            int i = 0;
            while ((line = sr.ReadLine()) != null)
            {
                Militia m = MilitiaReflection.stringToMilitia(line);
                m.Id = null;//赋值为null，然后让数据库重新分配id
                sqlDao.saveMilitia(m);

                i++;
                if(i % 100 == 0)
                {
                    pbf.setMaxValue(i / 100 + 1);
                    pbf.Increase(1, "导入了100个民兵");
                }
            }
            pbf.Increase(1, "导入完毕");
            sr.Close();
        }

        private List<string> getDatabases()
        {//获取除System之外的所有数据库名
            DirectoryInfo dirInfo = new DirectoryInfo(DataDir);
            DirectoryInfo[] dis = dirInfo.GetDirectories();
            List<string> databases = new List<string>();
            foreach (DirectoryInfo di in dis)
            {
                if (di.Name != "System")
                {
                    databases.Add(di.Name);
                }
            }
            return databases;
        }

        public List<Militia> getMilitiasByCredentialNumber(string creditNumber, string database)
        {//根据身份证号查民兵
            return sqlDao.getMilitiasByCredentialNumber(creditNumber, database);
        }

        public Dictionary<string, List<string>> getConflicts()
        {//找出所有数据库之间,以及之内的身份证号冲突
         //字典树方法
            ConflictDetector cd = new ConflictDetector();
                
            List<string> databases = getDatabases();//所有数据库

            foreach(string database in databases)
            {
                List<Militias_CredentialNumbers.Result> rList = sqlDao.getCredentialNumbers(database);
                foreach(Militias_CredentialNumbers.Result r in rList)
                {
                    cd.insertAndDetectConflicts(r.CredentialNumber, database);
                }
            }
            //冲突检测完毕

            return cd.conflictDict;

            /*Dictionary<string, List<string>> conflictDict = cd.conflictDict;

            List<List<Militia>> mlList = new List<List<Militia>>();
            foreach(KeyValuePair<string, List<string>> kvp in conflictDict)
            {
                List<Militia> mList = new List<Militia>();
                foreach(string database in kvp.Value)
                {//通过身份证，数据库获取民兵列表
                    mList.AddRange(sqlDao.getMilitiasByCredentialNumber(kvp.Key, database));
                }
                mlList.Add(mList);
            }
            return mlList;*/
        }

        public Dictionary<string, FacetValue> getGroupNums()
        {//获取所有数据库中的所有组中民兵的个数
            List<string> databases = getDatabases();
            return getGroupNums(databases);
        }

        public Dictionary<string, FacetValue> getGroupNums(List<string> databases)
        {//获取某些数据库中的所有组中民兵的个数
            List<FacetValue> fList = new List<FacetValue>();
            foreach (string database in databases)
            {
                fList.AddRange(sqlDao.getGroupNums(database));
            }
            Dictionary<string, FacetValue> fDict = new Dictionary<string, FacetValue>();
            IEnumerable<IGrouping<string, FacetValue>> iigf = fList.GroupBy(x => x.Range);//分组
            foreach (IGrouping<string, FacetValue> igf in iigf)
            {
                fDict[igf.Key] = igf.Aggregate(delegate (FacetValue fv1, FacetValue fv2)
                {
                    fv1.Hits += fv2.Hits;
                    return fv1;
                });
            }
            return fDict;
        }

        public Dictionary<string, FacetValue> getEnumStatistics(System.Linq.Expressions.Expression<Func<Militia, bool>> lambdaContition, string propertyName, string Place = null)
        {//根据某个属性，统计各属性值的民兵个数
            List<FacetValue> fList = new List<FacetValue>();
            List<string> databases = getDatabasesByPlace(Place);
            foreach(string database in databases)
            {
                fList.AddRange(sqlDao.getAggregateNums(lambdaContition, propertyName, database));
            }
            Dictionary<string, FacetValue> fDict = new Dictionary<string, FacetValue>();
            IEnumerable<IGrouping<string, FacetValue>> iigf = fList.GroupBy(x => x.Range);//分组
            foreach (IGrouping<string, FacetValue> igf in iigf)
            {
                fDict[igf.Key] = igf.Aggregate(delegate (FacetValue fv1, FacetValue fv2)
                {
                    fv1.Hits += fv2.Hits;
                    return fv1;
                });
            }
            return fDict;
        }

    }
}
