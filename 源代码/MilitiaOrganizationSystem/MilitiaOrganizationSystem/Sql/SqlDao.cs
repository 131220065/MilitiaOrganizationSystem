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

        public SqlDao(string db)
        {
            this.dbName = db;

            newStore();
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
        }
        

        public void saveMilitia(Militia militia)
        {//保存一个民兵，若Id相同，会覆盖数据库里的(省市须指定数据库)
            if(militia.Place == null)
            {//赋值采集地（采集地就是数据库名）
                militia.Place = dbName;
            }

            using (var session = store.OpenSession())
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
                    bulkInsert.Store(m);
                }
            }
        }


        public void deleteOneMilitia(Militia militia)
        {//从数据库中删除一个民兵，（省市级别须指定数据库）

            using (var session = store.OpenSession())
            {
                session.Delete(militia.Id);
                session.SaveChanges();
            }
        }

        public List<Militia> loadMilitias(List<string> ids)
        {
            using (var session = store.OpenSession())
            {
                List<Militia> mList = new List<Militia>();
                foreach(string id in ids)
                {
                    mList.Add(session.Load<Militia>(id));
                }
                return mList;
            }
        }

        public void importFromFile(string file, DictTree dt)
        {//从文件中批量导入民兵数据
            using (var bulkInsert = store.BulkInsert())
            {
                StreamReader sr = new StreamReader(file);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Militia m = MilitiaReflection.stringToMilitia(line);
                    m.Id = null;//让数据库新建一个文档
                    bulkInsert.Store(m);
                    dt.insertAndDetectConflicts(m);
                }
                sr.Close();
            }
        }

        public void exportToFile(string file)
        {//导出到文件中,每次导出一万个民兵
            int count = 0;
            int sum = 1;
            while (count < sum)
            {
                StreamWriter sw = new StreamWriter(file + count);//文件名是file+count
                List<Militia> mList = getMilitias(count, 10000, out sum);
                count += mList.Count;
                foreach (Militia m in mList)
                {
                    sw.WriteLine(MilitiaReflection.militiaToString(m));
                }
                sw.Close();//写入文件成功
                store.Dispose();//关闭再打开一次，才会省内存。。不知道为什么
                newStore();
            }
            
        }
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

        public List<Militia> queryByContition(Expression<Func<Militia, bool>> lambdaContition, int skip, int take, out int sum)
        {//通过lambda表达式查询数据库里的民兵
            //将等待最新注释掉了，刷新应该不慢
            using(var session = store.OpenSession())
            {
                RavenQueryStatistics stats;
                var mList =  session.Query<Militia>()
                    //.Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Statistics(out stats).Where(lambdaContition).Skip(skip).Take(take).ToList();
                sum = stats.TotalResults;

                return mList;
            }
        }

        public List<Militia> getMilitias(int skip, int take, out int sum)
        {//直接从数据库里取数据，不用任何条件,且take的大小限制为0~10000
            //这个是导出用的，所以还是要等待最新的数据
            using (var session = store.OpenSession())
            {
                RavenQueryStatistics stats;
                var militias = session.Query<Militia>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Statistics(out stats).Skip(skip).Take(take).ToList();
                sum = stats.TotalResults;

                return militias;
            }
        }

        public void  detectConflicts(DictTree dt)
        {
            using (var session = store.OpenSession())
            {
                RavenQueryStatistics stats;
                var rList = session.Query<Militias_CredentialNumbers.Result, Militias_CredentialNumbers>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Statistics(out stats)
                    .ProjectFromIndexFieldsInto<Militias_CredentialNumbers.Result>();

                System.Windows.MessageBox.Show("拿到了count = " + rList.Count() + stats.IsStale);
                foreach(Militias_CredentialNumbers.Result r in rList)
                {
                    if(r.CredentialNumber.StartsWith("511602199411233632"))
                    {
                        System.Windows.MessageBox.Show("y");
                    }
                    dt.insertAndDetectConflicts(r.CredentialNumber, r.Id);
                }
                
            }
        }

        public List<FacetValue> getGroupNums()
        {//通过静态索引查询组内民兵个数,组的叶节点总个数不超过一万
            using (var session = store.OpenSession())
            {
                List<FacetValue> fList;
                var gfacetResults = session.Query<Militias_Groups.Result, Militias_Groups>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .ProjectFromIndexFieldsInto<Militias_Groups.Result>()
                    .AggregateBy(x => x.Group).CountOn(x => x.Group).ToList();
                
                fList = gfacetResults.Results["Group"].Values;

                return fList;
            }

        }

        /*public List<FacetValue> getConflictNums(out bool isStale)
        {//用动态聚合的方式检测冲突
            using (var session = store.OpenSession())
            {
                RavenQueryStatistics stats;
                var fList = session.Query<Militias_CredentialNumbers.Result, Militias_CredentialNumbers>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Statistics(out stats)
                    .ProjectFromIndexFieldsInto<Militias_CredentialNumbers.Result>()
                    .AggregateBy(x => x.CredentialNumber).CountOn(x => x.CredentialNumber)
                    .ToList().Results["CredentialNumber"].Values.Where(x => x.Hits > 1);
                
                isStale = stats.IsStale;
                return fList.ToList();
            }
        }*/

        public List<FacetValue> getAggregateNums(Expression<Func<Militia, bool>> lambdaContition, string propertyName)
        {//统计,默认类的个数不超过一万

            var parameter = Expression.Parameter(typeof(Militia), "x");
            var property = Expression.Property(parameter, propertyName);
            var propertyExpression = Expression.Lambda<Func<Militia, object>>(property, parameter);
            //以上是生成选项,即按照哪个属性统计
            //统计还是要等待最新的数据
            using (var session = store.OpenSession())
            {
                var gfacetResults = session.Query<Militia, Militias_All>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Where(lambdaContition)
                    .AggregateBy(propertyExpression).CountOn(x => x.Group).ToList();

                return gfacetResults.Results[propertyName].Values;
            }
        }

        /*public List<Militia> getMilitiasByCredentialNumber(string CredentialNumber, out bool isStale)
        {//根据身份证号获取民兵
            //这个也要等待最新
            using (var session = store.OpenSession())
            {
                RavenQueryStatistics stats;
                var mList = session.Query<Militias_CredentialNumbers.Result, Militias_CredentialNumbers>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Statistics(out stats)
                    .Where(x => x.CredentialNumber == CredentialNumber)
                    .Skip(0).Take(1000)
                    .OfType<Militia>()
                    .ToList();

                isStale = stats.IsStale;
                return mList;
            }
        }*/

    }

    public class Militias_CredentialNumbers : AbstractIndexCreationTask<Militia>
    {
        public class Result
        {
            public string CredentialNumber { get; set; } //身份证号
            public string Id { get; set; }
        }

        public Militias_CredentialNumbers()
        {
            Map = militias => from militia in militias
                              select new
                              {
                                  CredentialNumber = militia.CredentialNumber,
                                  Id = militia.Id
                              };
        }
    }

    public class Militias_Groups : AbstractIndexCreationTask<Militia>
    {//组索引
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
    {//统计索引
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
