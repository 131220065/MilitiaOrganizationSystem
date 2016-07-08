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

        public List<Militia> getAllMilitias()
        {//获取所有民兵信息,仅限基层和区县使用这个接口(导出时使用)
            int sum;
            List<Militia> mList = sqlDao.queryByContition(x => x.Id != null, 0, 1000, out sum);
            while(mList.Count < sum)
            {
                mList.AddRange(sqlDao.queryByContition(x => x.Id != null, mList.Count, 1000, out sum));
            }
            return mList;
        }

        public List<string> getDatabasesByPlace(string Place)
        {//根据Militia.Place指定要查找的数据库集合, 调用此函数时， Place应不为空
            return getDatabases();
            /**if(Place == null || Place == "")
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
            return databases;*/
        }

        public List<Militia> queryByContition(System.Linq.Expressions.Expression<Func<Militia, bool>> lambdaContition, int skip, int take, out int sum, string Place = null)
        {//根据条件分页查询
            List<string> databases = getDatabasesByPlace(Place);//根据Place指定数据库组
            int[] sums = new int[databases.Count];//每个数据库group下民兵的总数
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

            //MessageBox.Show("开始分组");
            //sqlDao.getAggregateNums(lambdaContition);

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


        /*public List<Militia> getMilitiasByGroup(string group, int skip, int take, out int sum, string Place = null)
        {//根据组名获取民兵（应该没有使用它）
            List<string> databases = getDatabasesByPlace(Place);//除System之外的数据库名
            int[] sums = new int[databases.Count];//每个数据库group下民兵的总数
            for(int i = 0; i < databases.Count; i++)
            {//获取每个数据库的总数
                sqlDao.getMilitiasOfGroup(group, 0, 1, out sums[i], databases[i]);
            }

            sum = sums.Sum();//所有数据库中group下民兵总数的和
            int skipNum = 0;
            int databaseIndex = getIndexOfDatabase(sums, skip, out skipNum);
            if(databaseIndex >= sums.Length)
            {
                return new List<Militia>();
            }
            List<Militia> mList = sqlDao.getMilitiasOfGroup(group, skipNum, take, out sums[databaseIndex], databases[databaseIndex]);
            databaseIndex++;//下一个数据库
            while(mList.Count < take && databaseIndex < sums.Length)
            {//如果不够，则继续从下一个数据库取数据
                mList.AddRange(sqlDao.getMilitiasOfGroup(group, 0, take - mList.Count, out sums[databaseIndex], databases[databaseIndex]));
                databaseIndex++;
            }

            return mList;
        }*/

        public void BulkInsertMilitias(List<Militia> mList)
        {//批量插入默认数据库
            sqlDao.bulkInsertMilitias(mList);
        }

        public void backupAllDb(string folder)
        {//将所有数据库备份到folder下
            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            List<string> databases = getDatabases();
            foreach(string database in databases)
            {
                sqlDao.backupOneDB(database, folder);
            }
        }

        public void restoreDbs(List<string> databaseFolders)
        {
            foreach (string database in databaseFolders)
            {
                sqlDao.restoreOneDB(database);
                Directory.Delete(database, true);
            }
        }

        public List<string> exportAsXmlFile(string fileName)
        {//将数据库里的所有民兵信息写入xml文件中
            if(!Directory.Exists(Path.GetDirectoryName(fileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }
            List<string> exportFiles = new List<string>();
            int count = 0;
            int sum = 1;
            while(count < sum)
            {
                List<Militia> mList = sqlDao.getMilitias(count, 5000, out sum);
                count += mList.Count;
                string exportFile = fileName + count;
                FileTool.MilitiaListToXml(mList, exportFile);
                exportFiles.Add(exportFile);
            }

            return exportFiles;
        }


        public void exportAsSource(string folder)
        {//将全部数据库（System除外，因为没有权限根本复制不了）复制到folder文件夹下
         /*DirectoryInfo dirInfo = new DirectoryInfo(DataDir);
         DirectoryInfo[] dis = dirInfo.GetDirectories();
         foreach (DirectoryInfo di in dis)
         {
             if (di.Name != "System2")
             {
                 //FileTool.CopyFolder(di.FullName, folder);
                 //sqlDao.backupOneDB(di.Name, folder);
             }
         }*/
         sqlDao.copyDbTo(folder);

            //sqlDao.exportDocumentDataBase(folder);
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

        public void exportAsFile(string file)
        {
            sqlDao.exportToFile(file);
        }
        public void importFormFile(string file)
        {
            sqlDao.importFromFile(file);
        }

        public void importFromMilitiaXml(string fileName)
        {//从xml导入民兵到数据库中
            List<Militia> mList = FileTool.XmlToMilitiaList(fileName);
            if(mList == null)
            {
                throw new Exception("导入民兵xml文件异常");
            } else
            {
                BulkInsertMilitias(mList);
            }
            
        }

        public List<string> importUnzip(UnZip unzip)
        {//导入zip文件，解压到unzip中
            return unzip.unzipAll();
        }

        public List<string> importFromSource(string folder)
        {//folder下全部是数据库文件夹，导入此文件夹下的所有数据库文件夹(复制到DataBases文件夹下)

            string[] dbpaths = Directory.GetDirectories(folder);
            List<string> importedDataBases = new List<string>();//导入成功的数据库
            for(int i = 0; i < dbpaths.Length; i++)
            {
                string dbpath = dbpaths[i];
                try
                {
                    //sqlDao.restoreOneDB(dbpath);
                    FileTool.CopyFolder(dbpath, DataDir);
                    int startIndex = dbpath.LastIndexOf('\\') + 1;

                    importedDataBases.Add(dbpath.Substring(startIndex));
                } catch(Exception e)
                {

                }
                
            }
            //MessageBox.Show(importedDataBases[0]);
            return importedDataBases;
            /*string[] dumpFiles = Directory.GetFiles(folder, "*.dump");
            foreach(string dumpFile in dumpFiles)
            {
                sqlDao.importToDocumentDataBase(dumpFile);
            }*/

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

        /**private void mergeAndSaveConfilictMilitias(List<Militias_CredentialNumbers.Result> rMainList, List<Militias_CredentialNumbers.Result> rList, Dictionary<string, List<Militias_CredentialNumbers.Result>> dict)
        {//将rList归并到rMainList，并将冲突的民兵根据身份证号存进dict中
            /string mString = "";
            foreach(Militias_CredentialNumbers.Result r in rMainList)
            {
                mString += r.CredentialNumber + ",";
            }
            mString += "\n";
            foreach(Militias_CredentialNumbers.Result r in rList)
            {
                mString += r.CredentialNumber + ",";
            }
            MessageBox.Show(mString);/
            if(rList.Count == 0)
            {//不必归并
                return;
            }
            if(rMainList.Count == 0)
            {
                rMainList.AddRange(rList);
                return;
            }
            int i = 0, j = 0;
            while(j < rList.Count && string.Compare(rMainList[i].CredentialNumber, rList[j].CredentialNumber) > 0)
            {
                j++;
            }
            rMainList.InsertRange(0, rList.GetRange(0, j));
            i = j;
            //主链表的第i个身份证号应该小于等于次链表的
            while(i < rMainList.Count && j < rList.Count)
            {//归并，i是rMainList的序号，j是rList的序号
                Militias_CredentialNumbers.Result rMain = rMainList[i];
                Militias_CredentialNumbers.Result r = rList[j];
                int re = string.Compare(rMain.CredentialNumber, r.CredentialNumber);
                if(re == 0)
                {//相等，产生了冲突
                    List<Militias_CredentialNumbers.Result> dictRList;
                    if(!dict.TryGetValue(rMain.CredentialNumber, out dictRList))
                    {
                        dict[rMain.CredentialNumber] = new List<Militias_CredentialNumbers.Result>();
                        dictRList = dict[rMain.CredentialNumber];
                    }
                    if(!dictRList.Contains(rMain))
                    {
                        dictRList.Add(rMain);
                    }
                    if(!dictRList.Contains(r))
                    {
                        dictRList.Add(r);
                    }
                    j++;
                } else if(re < 0)
                {//主链表元素小于次链表,说明j应该插入于i之后,但不知道是不是在i的更后面,所以i++
                    i++;//让主链表元素加，直到刚好大于次链表，则此时应该插入(将j插入i的前面)
                } else
                {//主链表元素刚刚大于次链表元素(肯定是刚刚大于,因为马上让他不大于)
                    int jStart = j;//将j之后的一段小于i的都插入到i前面去
                    j++;//j已经比较过了，所以j++
                    while (j < rList.Count && string.Compare(rMainList[i].CredentialNumber, rList[j].CredentialNumber) > 0)
                    {//直到主链表元素小于等于次链表元素，就可以插入Range了
                        j++;
                    }
                    int jCount = j - jStart;
                    rMainList.InsertRange(i, rList.GetRange(jStart, jCount));//插入到i前面
                    i += jCount;//插入之后序号也变了
                    //此时rMain[i]应该小于等于r[j]，或者j到顶了
                }
            }

            if(j < rList.Count)
            {//说明次链表还没有归并完
                rMainList.InsertRange(rMainList.Count, rList.GetRange(j, rList.Count - j));
            }
        }**/

        /**public List<List<Militia>> getConflictMilitiasBetweenDatabases()
        {//找出所有数据库之间的身份证号冲突,只在省市军分区调用
            Dictionary<string, List<Militias_CredentialNumbers.Result>> dict = new Dictionary<string, List<Militias_CredentialNumbers.Result>>();//记录冲突的Results

            //List<Militias_CredentialNumbers.Result> rAllList = new List<Militias_CredentialNumbers.Result>();

            List<string> databases = getDatabases();
            
            List<Militias_CredentialNumbers.Result>[] drLists = new List<Militias_CredentialNumbers.Result>[databases.Count];

            for(int i = 0; i < databases.Count; i++)
            {
                //MessageBox.Show(databases[i]);
                drLists[i] = sqlDao.getAllCredentialNumbers(databases[i]);
            }
            //MessageBox.Show("获得了身份证号");
            int interval = 1; //归并间隔
            while(interval < databases.Count)
            {//两两归并，最后总的归并在drLists[0]上
                int doubleInterval = 2 * interval;
                for(int i = 0; i + interval < databases.Count; i += doubleInterval)
                {
                    mergeAndSaveConfilictMilitias(drLists[i], drLists[i + interval], dict);
                }

                interval = doubleInterval;
            }

            List<List<Militia>> mLList = new List<List<Militia>>();
            foreach(string key in dict.Keys)
            {
                List<Militia> mList = new List<Militia>();
                foreach(Militias_CredentialNumbers.Result r in dict[key])
                {
                    mList.AddRange(sqlDao.getMilitiasByCredentialNumber(r.CredentialNumber, r.DbName));
                }
                mLList.Add(mList);//添加
            }

            return mLList;
        }*/

        /*public List<List<Militia>> getConflictMilitiasOfMainDatabase()
        {//检测主数据库的冲突,应该只在基层和区县调用
            List<Militias_CredentialNumbers.Result> rList = sqlDao.getAllCredentialNumbers();//从小到大排序的身份证号

            List<List<Militia>> mLList = new List<List<Militia>>();

            for(int i = 0; i < rList.Count - 1; i++)
            {
                if(rList[i].CredentialNumber == rList[i + 1].CredentialNumber)
                {
                    mLList.Add(sqlDao.getMilitiasByCredentialNumber(rList[i].CredentialNumber));

                    i++;
                    while(i < rList.Count - 1 && rList[i].CredentialNumber == rList[i + 1].CredentialNumber)
                    {
                        i++;
                    }
                }
            }

            return mLList;
        }*/

        /*public List<List<Militia>> getConflictMilitias(int skip, int take, out int sum)
        {//数据库本身的索引方法,分页
            List<Militias_ConflictCredentialNumbers.Result> rList = sqlDao.getConflictCredentialNumbers(skip, take, out sum);//从小到大排序的身份证号

            List<List<Militia>> mLList = new List<List<Militia>>();

            foreach(Militias_ConflictCredentialNumbers.Result r in rList)
            {
                mLList.Add(sqlDao.getMilitiasByCredentialNumber(r.CredentialNumber));
            }

            return mLList;
        }*/

        /*public List<List<Militia>> getConflictMilitias()
        {
            int sum;
            return getConflictMilitias(0, 30, out sum);
        }*/

        public List<List<Militia>> getConflictMilitias()
        {
            Dictionary<string, List<char>> conflictDict = sqlDao.cd.conflictDict;
            List<List<Militia>> mlList = new List<List<Militia>>();
            foreach (KeyValuePair<string, List<char>> kvp in conflictDict)
            {
                List<Militia> mList = new List<Militia>();
                foreach (char i in kvp.Value)
                {//通过身份证，数据库下标（i)获取民兵列表
                    mList.AddRange(sqlDao.getMilitiasByCredentialNumber(kvp.Key));
                }
                mlList.Add(mList);
            }
            return mlList;
        }

        /*public List<List<Militia>> getConflictMilitias()
        {//找出所有数据库之间,以及之内的身份证号冲突
         //字典树方法
            DictTree dt = new DictTree();

            Dictionary<string, List<char>> conflictDict = new Dictionary<string, List<char>>();
           
            List<string> databases = getDatabases();

            for (int i = 0; i < databases.Count; i++)
            {
                int sum = 1;
                int count = 0;
                while(count < sum)
                {
                List<Militias_CredentialNumbers.Result> rList = sqlDao.getCredentialNumbers(count, 1000, out sum, databases[i]);
                count += rList.Count;//更新count，访问完所有数据
                //List<Militias_CredentialNumbers.Result> rList = sqlDao.getAllCredentialNumbers(databases[i]);
                foreach (Militias_CredentialNumbers.Result r in rList)
                    {
                        char conflictI;
                        if (!dt.insert(r.CredentialNumber, (char)i, out conflictI))
                        {//如果有冲突（插入失败)
                            List<char> results;
                            if (!conflictDict.TryGetValue(r.CredentialNumber, out results))
                            {//如果没有加入冲突字典，则新建一个，再添加进冲突字典
                                results = new List<char>();
                                results.Add((char)i);
                                if (!results.Contains(conflictI))
                                {//有可能是同一数据库
                                    results.Add(conflictI);
                                }
                                conflictDict[r.CredentialNumber] = results;
                            }
                            else
                            {//如果已经加入了冲突字典，那么只需要添加现在的
                                if (!results.Contains((char)i))
                                {//有可能是同一数据库的冲突，所以
                                    results.Add((char)i);
                                }
                            }
                        }
                    }
                }
                
            }

            //冲突检测完毕

            List<List<Militia>> mlList = new List<List<Militia>>();
            foreach(KeyValuePair<string, List<char>> kvp in conflictDict)
            {
                List<Militia> mList = new List<Militia>();
                foreach(char i in kvp.Value)
                {//通过身份证，数据库下标（i)获取民兵列表
                    mList.AddRange(sqlDao.getMilitiasByCredentialNumber(kvp.Key, databases[(int)i]));
                }
                mlList.Add(mList);
            }
            return mlList;
        }*/

        /**public List<List<Militia>> getConflictMilitiasOfMainDatabase()
        {//字典树方法查询
            DictTree dt = new DictTree();
            Dictionary<string, List<Militias_CredentialNumbers.Result>> conflictDict = new Dictionary<string, List<Militias_CredentialNumbers.Result>>();//记录冲突的Result
            List<Militias_CredentialNumbers.Result> rList = sqlDao.getAllCredentialNumbers();
            foreach(Militias_CredentialNumbers.Result r in rList)
            {
                if(!dt.insert(r.CredentialNumber))
                {//如果有冲突（插入失败)
                    List<Militias_CredentialNumbers.Result> results;
                    if(!conflictDict.TryGetValue(r.CredentialNumber, out results))
                    {//如果已经加入了冲突字典，则什么也不做
                        //如果没有加入冲突字典，则新建一个，再添加进冲突字典
                        results = new List<Militias_CredentialNumbers.Result>();
                        results.Add(r);
                        conflictDict[r.CredentialNumber] = results;
                    }
                }
            }

            //添加进了冲突字典

        }*/

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
            /*List<FacetValue> fList = new List<FacetValue>();
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
            return fDict;*/
            //单数据库的话，仅需toDictionary
            /*FacetValue fv = fList.Aggregate(delegate (FacetValue fv1, FacetValue fv2)
            {
                fv1.Hits += fv2.Hits;
                return fv1;
            });
            System.Windows.MessageBox.Show("sum = " + fv.Hits);*/
            Dictionary<string, FacetValue> fdict = sqlDao.getAggregateNums(lambdaContition, propertyName).ToDictionary(x => x.Range);
            return fdict;
        }

    }
}
