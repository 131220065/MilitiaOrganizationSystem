using Raven.Client;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Raven.Database.Smuggler;
using Raven.Abstractions.Smuggler;
using Raven.Abstractions.Data;
using System.Linq.Expressions;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.Connection;

namespace MilitiaOrganizationSystem
{
    class SqlDao
    {//数据库访问层
        private string dbName;
        private EmbeddableDocumentStore store;

        private const int timeoutseconds = 600;

        //private RootNode root = null;//字典树的根

        public DictTree dictTree { get; set; }
        public ConflictDict cd { get; set; }//冲突

        public SqlDao(string db)
        {
            this.dbName = db;

            newStore();

            dictTree = getDictTree();//冲突
            cd = getConflictDict();
        }

        public DictTree getDictTree()
        {
            DictTree dt;
            using (var session = store.OpenSession())
            {
                try
                {
                    dt = session.Query<DictTree>().First();
                } catch(Exception e)
                {
                    dt = new DictTree();
                    session.Store(dt);
                    session.SaveChanges();
                }
            }
            return dt;
        }

        public void saveDictTreeAndConflictDict()
        {//保存冲突和字典树
            using (var session = store.OpenSession())
            {
                session.Store(dictTree);
                session.Store(cd);
                session.SaveChanges();
            }
        }

        public void insertCreditNumber(string creditNumber, char i = (char)0)
        {//插入身份证号
            DictTree dt = dictTree;
            Dictionary<string, List<char>> conflictDict = cd.conflictDict;
            char conflictI;
            if (!dt.insert(creditNumber, (char)i, out conflictI))
            {//如果有冲突（插入失败)
                List<char> results;
                if (!conflictDict.TryGetValue(creditNumber, out results))
                {//如果没有加入冲突字典，则新建一个，再添加进冲突字典
                    results = new List<char>();
                    results.Add((char)i);
                    if (!results.Contains(conflictI))
                    {//有可能是同一数据库
                        results.Add(conflictI);
                    }
                    conflictDict[creditNumber] = results;
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

        public ConflictDict getConflictDict()
        {
            ConflictDict cd;
            using (var session = store.OpenSession())
            {
                try
                {
                    cd = session.Query<ConflictDict>().First();
                } catch(Exception e)
                {
                    cd = new ConflictDict();
                    session.Store(cd);
                    session.SaveChanges();
                }
            }
            return cd;
        }

        private void newStore()
        {//新建store并初始化

            store = new EmbeddableDocumentStore()
            {
                DefaultDatabase = dbName
            };
            store.Initialize();
            new Militias_CredentialNumbers().Execute(store);
            new Militias_Groups().Execute(store);
            new Militias_All().Execute(store);
            //new Militias_ConflictCredentialNumbers().Execute(store);

            /*using (var session = store.OpenSession())
            {//字典树
                root = session.Query<RootNode>().FirstOrDefault();
                if(root == null)
                {
                    root = new RootNode();
                }
                session.Store(root);
                session.SaveChanges();
            }*/
            
        }

        /*public void deleCreditNumber(string credinumber)
        {//删除身份证号
            Dictionary<string, string>[] dicts = new Dictionary<string, string>[credinumber.Length];
            Dictionary<string, string> nodeDict = root.rootDict;
            using (var session = store.OpenSession())
            {
                string nodeId;
                Node node;
                int i;
                for (i = 0; i < credinumber.Length; i++)
                {//如果身份证号已经存在，那么nodeDict一直能拿到相应的Node
                    if (!nodeDict.TryGetValue(credinumber[i] + "", out nodeId))
                    {//没有这个，说明一定不存在这个身份证号,则不用删除
                        return;
                    }
                    node = session.Load<Node>(nodeId);
                    nodeDict = node.nodeDict;
                    dicts[i] = nodeDict;
                }
                for(i = credinumber.Length - 1; i >= 1; i--)
                {
                    session.Delete(dicts[i - 1][credinumber[i] + ""]);//删除Id对应的Node
                    if(dicts[i - 1].Count > 1)
                    {//说明还有
                        dicts[i - 1].Remove(credinumber[i] + "");//从字典里删除
                        break;
                    }
                }
                if(i == 0)
                {//说明下一个的个数为1，应删除
                    session.Delete(root.rootDict[credinumber[i] + ""]);
                    root.rootDict.Remove(credinumber[i] + "");
                }
                session.SaveChanges();
            }
        }

        public bool insertCreditNumber(string credinumber, string place, out string conflictPlace)
        {
            using (var session = store.OpenSession())
            {
                Dictionary<string, string> nodeDict = root.rootDict;
                string nodeId;
                Node node;
                bool isExist = true;
                for (int i = 0; i < credinumber.Length; i++)
                {//如果身份证号已经存在，那么nodeDict一直能拿到相应的Node
                    if (!nodeDict.TryGetValue(credinumber[i] + "", out nodeId))
                    {//没有这个，说明一定不存在这个身份证号
                        isExist = false;
                        node = new Node();//新建一个Node
                        session.Store(node);
                        nodeDict[credinumber[i] + ""] = node.Id;//让这个有值
                    } else
                    {
                        node = session.Load<Node>(nodeId);
                    }
                    nodeDict = node.nodeDict;
                }
                nodeDict[place] = null;//最后一个结点的字典上存有数据库名
                session.SaveChanges();//保存修改
                if (isExist)
                {//冲突
                    conflictPlace = nodeDict.Keys.First();
                    return false;//插入失败
                }
                else
                {//不冲突
                    conflictPlace = place;
                    return true;//插入成功
                }
            }
            
        }*/
        

        public void saveMilitia(Militia militia)
        {//保存一个民兵，若Id相同，会覆盖数据库里的(省市须指定数据库)
            if(militia.Place == null)
            {
                militia.Place = dbName;
            }

            string database = militia.Place;//指定数据库

            if(militia.Id == null)
            {//说明民兵是新添加的
                insertCreditNumber(militia.CredentialNumber);//插入并检测冲突
            }

            using (var session = store.OpenSession(database))
            {
                session.Store(militia);
                session.SaveChanges();
            }
        }

        public void bulkInsertMilitias(List<Militia> mList)
        {//批量插入
            using (var bulkInsert = store.BulkInsert())
            {
                foreach (Militia m in mList)
                {
                    if (m.Place == null)
                    {
                        m.Place = dbName;//赋值采集地
                    }
                    insertCreditNumber(m.CredentialNumber);
                    bulkInsert.Store(m);
                }
                saveDictTreeAndConflictDict();
            }
        }


        public void deleteOneMilitia(Militia militia)
        {//从数据库中删除一个民兵，（省市级别须指定数据库）
            string database = militia.Place;//指定数据库

            using (var session = store.OpenSession(database))
            {
                session.Delete(militia.Id);
                session.SaveChanges();
            }
        }

        public void importFromFile(string file)
        {
            using (var bulkInsert = store.BulkInsert())
            {
                StreamReader sr = new StreamReader(file);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Militia m = MilitiaReflection.stringToMilitia(line);
                    m.Id = null;//让数据库新建一个文档

                    insertCreditNumber(m.CredentialNumber);

                    bulkInsert.Store(m);
                }
                sr.Close();
                saveDictTreeAndConflictDict();
            }
            /*int sum;
            getConflictCredentialNumbers(0, 1, out sum);*/

        }

        public void exportToFile(string file)
        {
            
            int count = 0;
            int sum = 1;
            while (count < sum)
            {
                StreamWriter sw = new StreamWriter(file + count);
                List<Militia> mList = getMilitias(count, 5000, out sum);
                count += mList.Count;
                foreach (Militia m in mList)
                {
                    sw.WriteLine(MilitiaReflection.militiaToString(m));
                }
                sw.Close();
                store.Dispose();//关闭再打开一次，看内存
                newStore();
            }
            
        }

        public void backupOneDB(string dbName, string exportFolder)
        {//dbName数据库名，exportFolder导出文件夹路径，会在路径下创建一个名为dbName的新文件夹
            string dbFolder = exportFolder + "\\" + dbName;
            if(!Directory.Exists(dbFolder))
            {
                Directory.CreateDirectory(dbFolder);
            }
            store.DatabaseCommands.GlobalAdmin.StartBackup(dbFolder, new DatabaseDocument(), false, dbName);
        }

        public void restoreOneDB(string importFolder)
        {//importFolder是备份数据库的文件夹路径,文件夹名即为数据库名
            DirectoryInfo dirInfo = new DirectoryInfo(importFolder);
            Operation operation = store.DatabaseCommands.GlobalAdmin.StartRestore(new Raven.Abstractions.Data.DatabaseRestoreRequest
            {
                BackupLocation = dirInfo.FullName,
                DatabaseName = dirInfo.Name,
                
            });
            operation.WaitForCompletion();
        }//这个有希望代替直接复制，据说直接复制对数据库会造成损害，但是restore的时候我只能restore一个，连续restore两个就会造成冲突

        /**public async void exportDocumentDataBase(string exportFolder)
        {
            var dataDumper = new DatabaseDataDumper(
                store.DocumentDatabase,
                new SmugglerDatabaseOptions
                {
                    OperateOnTypes = ItemType.Documents | ItemType.Indexes | ItemType.Attachments | ItemType.Transformers,
                    Incremental = false,
                
                }
            );

            SmugglerExportOptions<RavenConnectionStringOptions> exportOptions = new SmugglerExportOptions<RavenConnectionStringOptions>
            {
                From = new EmbeddedRavenConnectionStringOptions(),
                ToFile = exportFolder + "\\" + dbName + ".dump"
            };

            await dataDumper.ExportData(exportOptions);

        }*///导出只能导出DocumentDataBase或SystemDataBase的数据，我不知道其他数据库该如何导出

        /**public async void importToDocumentDataBase(string dumpFile)
        {
            var dataDumper = new DatabaseDataDumper(
                store.DocumentDatabase,
                new SmugglerDatabaseOptions
                {
                    OperateOnTypes = ItemType.Documents | ItemType.Indexes | ItemType.Attachments | ItemType.Transformers,
                    Incremental = false,

                }
            );
            SmugglerImportOptions<RavenConnectionStringOptions> importOptions = new SmugglerImportOptions<RavenConnectionStringOptions>
            {
                FromFile = dumpFile,
                To = new EmbeddedRavenConnectionStringOptions()
            };

            await dataDumper.ImportData(importOptions);

            MessageBox.Show("complete?");
        }*///import的话如果id相同，会覆盖掉原数据库中的数据

        public void copyDbTo(string folder)
        {//直接将除System之外的数据库复制出去
            store.Dispose();//先释放strore，才能copy

            DirectoryInfo dirInfo = new DirectoryInfo(SqlBiz.DataDir);
            DirectoryInfo[] dis = dirInfo.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                if (di.Name != "System")
                {//除了System数据库，其他的都导出
                    FileTool.CopyFolder(di.FullName, folder);
                    //sqlDao.backupOneDB(di.Name, folder);
                }
            }

            newStore();
        }

        public void zipDb(Zip zip)
        {//直接将除System之外的数据库添加到压缩文件中
            store.Dispose();//先释放strore，才能zip

            DirectoryInfo dirInfo = new DirectoryInfo(SqlBiz.DataDir);
            DirectoryInfo[] dis = dirInfo.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                if (di.Name != "System")
                {//除了System数据库，其他的都导出
                    zip.addFileOrFolder(SqlBiz.DataDir + "\\" + di.Name);
                }
            }

            newStore();
        }
        
        
        public List<Militia> queryByContition(Expression<Func<Militia, bool>> lambdaContition, int skip, int take, out int sum, string database = null)
        {//通过lambda表达式查询数据库database里的东西
            if(database == null)
            {
                database = dbName;
            }
            store.DatabaseCommands.GlobalAdmin.EnsureDatabaseExists(database);
            List<Militia> mList;
            using(var session = store.OpenSession(database))
            {
                RavenQueryStatistics stats;
                mList =  session.Query<Militia>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Statistics(out stats).Where(lambdaContition).Skip(skip).Take(take).ToList();
                sum = stats.TotalResults;
            }
            return mList;
        }

        public List<Militia> getMilitias(int skip, int take, out int sum, string database = null)
        {//直接从数据库里取数据，不用任何条件,且take的大小限制为0~10000
            if(database == null)
            {
                database = dbName;
            }
            store.DatabaseCommands.GlobalAdmin.EnsureDatabaseExists(database);
            List<Militia> militias;
            using (var session = store.OpenSession())
            {
                RavenQueryStatistics stats;
                militias = session.Query<Militia>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Statistics(out stats).Skip(skip).Take(take).ToList();
                sum = stats.TotalResults;
            }
            return militias;
        }

        /*public Dictionary<string, Militias_Groups.Result> getGroups(int skip, int take, out int sum, string database = null)
        {//通过静态索引查询组内民兵个数
            if (database == null)
            {
                database = dbName;
            }

            using(var session = store.OpenSession(database))
            {
                RavenQueryStatistics stats;
                var gDictionary = session.Query<Militias_Groups.Result, Militias_Groups>().Statistics(out stats).Skip(skip).Take(take).ToDictionary(x => x.Group);
                sum = stats.TotalResults;

                return gDictionary;
            }
        }*/

        public List<FacetValue> getGroupNums(string database = null)
        {//通过静态索引查询组内民兵个数
            if (database == null)
            {
                database = dbName;
            }
            store.DatabaseCommands.GlobalAdmin.EnsureDatabaseExists(database);
            List<FacetValue> fList;
            using (var session = store.OpenSession(database))
            {
                
                var gfacetResults = session.Query<Militias_Groups.Result, Militias_Groups>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .ProjectFromIndexFieldsInto<Militias_Groups.Result>()
                    .AggregateBy(x => x.Group).CountOn(x => x.Group).ToList();
                
                fList = gfacetResults.Results["Group"].Values;

                
            }
            return fList;

        }

        public List<FacetValue> getAggregateNums(Expression<Func<Militia, bool>> lambdaContition, string propertyName, string database = null)
        {//统计,默认类的个数不超过5000
            if (database == null)
            {
                database = dbName;
            }

            var parameter = Expression.Parameter(typeof(Militia), "x");
            var property = Expression.Property(parameter, propertyName);
            var propertyExpression = Expression.Lambda<Func<Militia, object>>(property, parameter);
            store.DatabaseCommands.GlobalAdmin.EnsureDatabaseExists(database);
            List<FacetValue> fList;
            using (var session = store.OpenSession(database))
            {
                var gfacetResults = session.Query<Militia, Militias_All>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Where(lambdaContition)
                    .AggregateBy(propertyExpression).CountOn(x => x.Group).ToList();
                /*FacetValue fv = gfacetResults.Results[propertyName].Values.Aggregate(delegate (FacetValue fv1, FacetValue fv2)
                {
                    fv1.Hits += fv2.Hits;
                    return fv1;
                });
                System.Windows.MessageBox.Show("sum = " + fv.Hits);*/
                fList = gfacetResults.Results[propertyName].Values;
            }
            return fList;
        }

        /*public List<Militia> getMilitiasOfGroup(string Group, int skip, int take, out int sum, string database = null)
        {//通过指定的Group(可以是非叶结点)，查询lambda表达式限定下的民兵列表
            if (database == null)
            {
                database = dbName;
            }
            
            using (var session = store.OpenSession(database))
            {
                RavenQueryStatistics stats;
                var militias = session.Query<Militias_Groups.Result, Militias_Groups>().Statistics(out stats)
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Where(x => x.Group.StartsWith(Group)).OfType<Militia>() //转换为militias
                    .Skip(skip).Take(take).ToList();
                sum = stats.TotalResults;

                return militias;
            }
        }*/

        public List<Militias_CredentialNumbers.Result> getCredentialNumbers(int skip, int take, out int sum, string database = null)
        {//获取身份证号
            if (database == null)
            {
                database = dbName;
            }
            store.DatabaseCommands.GlobalAdmin.EnsureDatabaseExists(database);
            List<Militias_CredentialNumbers.Result> credentialNumbers;
            using (var session = store.OpenSession(database))
            {
                RavenQueryStatistics stats;
                credentialNumbers = session.Query<Militias_CredentialNumbers.Result, Militias_CredentialNumbers>()
                    .Statistics(out stats)
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Skip(skip).Take(take)
                    .ProjectFromIndexFieldsInto<Militias_CredentialNumbers.Result>()
                    .ToList();

                sum = stats.TotalResults;

                /*foreach(Militias_CredentialNumbers.Result r in credentialNumbers)
                {//给r.DbName赋值，表示从这个数据库查出来的
                    r.DbName = database;
                }*/

                
            }
            return credentialNumbers;
        }

        /**public List<Militias_CredentialNumbers.Result> getAllCredentialNumbers(string database = null)
        {//获取一个数据库的所有身份证号
            if (database == null)
            {
                database = dbName;
            }

            store.DatabaseCommands.GlobalAdmin.EnsureDatabaseExists(database);

            using (var session = store.OpenSession(database))
            {
                int sum = 1;
                List<Militias_CredentialNumbers.Result> rList = new List<Militias_CredentialNumbers.Result>();

                while(rList.Count < sum)
                {
                    RavenQueryStatistics stats;
                    var credentialNumbers = session.Query<Militias_CredentialNumbers.Result, Militias_CredentialNumbers>()
                        .Statistics(out stats)
                        .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                        .Skip(rList.Count).Take(5000)
                        .ProjectFromIndexFieldsInto<Militias_CredentialNumbers.Result>()
                        //.OrderBy(x => x.CredentialNumber)
                        .ToList();

                    sum = stats.TotalResults;

                    *foreach (Militias_CredentialNumbers.Result r in credentialNumbers)
                    {//给r.DbName赋值，表示从这个数据库查出来的
                        r.DbName = database;
                    }*

                    rList.AddRange(credentialNumbers);
                }
                

                return rList;
            }

        }*/

        public List<Militia> getMilitiasByCredentialNumber(string CredentialNumber, string database = null)
        {//根据身份证号获取民兵
            if (database == null)
            {
                database = dbName;
            }
            List<Militia> mList;
            using (var session = store.OpenSession(database))
            {
                mList = session.Query<Militias_CredentialNumbers.Result, Militias_CredentialNumbers>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Where(x => x.CredentialNumber == CredentialNumber)
                    .Skip(0).Take(1000)
                    .OfType<Militia>()
                    .ToList();
                

                
            }
            return mList;
        }

        /*public List<Militias_ConflictCredentialNumbers.Result> getConflictCredentialNumbers(int skip, int take, out int sum)
        {//获取所有冲突的身份证号,主数据库的
            using (var session = store.OpenSession())
            {
                RavenQueryStatistics stats;
                var credentialNumbers = session.Query<Militias_ConflictCredentialNumbers.Result, Militias_ConflictCredentialNumbers>()
                    .Statistics(out stats)
                    //.Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Where(x => x.Count > 1)
                    .Skip(skip).Take(take)
                    .ToList();

                sum = stats.TotalResults;

                return credentialNumbers;
            }
        }*/

    }

    public class Militias_CredentialNumbers : AbstractIndexCreationTask<Militia>
    {
        public class Result
        {
            public string CredentialNumber { get; set; } //身份证号
            
            public string DbName { get; set; } //数据库名，之后冲突检测的时候使用
        }

        public Militias_CredentialNumbers()
        {
            Map = militias => from militia in militias
                              select new
                              {
                                  CredentialNumber = militia.CredentialNumber,
                                  DbName = militia.Place
                              };

            //Sort(r => r.CredentialNumber, Raven.Abstractions.Indexing.SortOptions.String);
        }
    }

    public class Militias_ConflictCredentialNumbers : AbstractIndexCreationTask<Militia, Militias_ConflictCredentialNumbers.Result>
    {//用数据库检测冲突的索引
        public class Result
        {
            public string CredentialNumber { get; set; } //身份证号
            public int Count { get; set; }
        }

        public Militias_ConflictCredentialNumbers()
        {
            Map = militias => from militia in militias
                              select new
                              {
                                  CredentialNumber = militia.CredentialNumber,
                                  Count = 1
                              };
            Reduce = rs => from r in rs
                           group r by r.CredentialNumber into g
                           select new
                           {
                               CredentialNumber = g.Key,
                               Count = g.Sum(x => x.Count)
                           };
        }
    }

    public class Militias_Groups : AbstractIndexCreationTask<Militia>
    {
        public class Result
        {
            public string Group { get; set; } //组名
        }

        public Militias_Groups()
        {
            Map = militias => from militia in militias
                              select new
                              {
                                  Group = militia.Group
                              };

            
        }
    }

    public class Militias_All : AbstractIndexCreationTask<Militia>
    {
        public Militias_All()
        {
            Map = militias => from militia in militias
                              select new
                              {
                                  Group = militia.Group,
                                  Place = militia.Place,

                                  Name = militia.Name,
                                  Sex = militia.Sex,
                                  Source = militia.Source,
                                  Ethnic = militia.Ethnic,
                                  Resvalueence = militia.Resvalueence,
                                  PoliticalStatus = militia.PoliticalStatus,
                                  CredentialNumber = militia.CredentialNumber,
                                  Education = militia.Education,
                                  MilitaryForceType = militia.MilitaryForceType,
                                  MilitaryRank = militia.MilitaryRank,
                                  Available = militia.Available,
                                  EquipmentInfo_Available = militia.EquipmentInfo_Available,
                                  EquipmentInfo_MilitaryProfessionTypeName = militia.EquipmentInfo_MilitaryProfessionTypeName,
                                  RetirementRank = militia.RetirementRank,
                                  RetirementMilitaryForceType = militia.RetirementMilitaryForceType,
                                  CadreServiced = militia.CadreServiced,
                                  CadreProfessionalTrained = militia.CadreProfessionalTrained,
                                  CadreAttendedCommittee = militia.CadreAttendedCommittee,
                                  CadreTrained = militia.CadreTrained,
                                  CadreFullTime = militia.CadreFullTime,
                                  Trained = militia.Trained,
                                  TeamingPosition = militia.TeamingPosition,
                                  ReplyStatus = militia.ReplyStatus,
                                  MilitaryProfessionTypeName = militia.MilitaryProfessionTypeName,
                                  RetirementProfessionType = militia.RetirementProfessionType,
                                  MilitaryProfessionName = militia.MilitaryProfessionName,
                                  RetirementProfessionSmallType = militia.RetirementProfessionSmallType,
                                  RetirementProfessionName = militia.RetirementProfessionName

                              };
        }
    }
}
