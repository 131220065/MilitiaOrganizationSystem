﻿using Raven.Client;
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
using Raven.Abstractions.Extensions;

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

            FormBizs.pbf.Increase("正在启动数据库");

            newStore();

            FormBizs.pbf.Increase("启动数据库完毕");
        }

        /*public void restart()
        {
            store.Dispose();
            newStore();
        }*/

        private void newStore()
        {//新建store并初始化

            store = new EmbeddableDocumentStore()
            {
                DefaultDatabase = dbName
            };

            store.Initialize();
            
            new Militias_All().Execute(store);
        }

        public void saveMilitia(Militia militia, string database)
        {//测试所用，保存民兵到指定数据库,数据库可以不存在
            militia.Place = database;

            FormBizs.sqlBiz.cnDao.addAndSaveCrediNumber(militia.CredentialNumber, database);
            
            bool isExist = true;
            if (!Directory.Exists("Databases/" + database)) {
                //说明不存在
                isExist = false;
            }
            store.DatabaseCommands.GlobalAdmin.EnsureDatabaseExists(database);
            if(!isExist)
            {//不存在的话，还得建立索引
                new Militias_All().Execute(store.DatabaseCommands.ForDatabase(database), store.Conventions);
            }
            saveMilitia(militia);
        }
        

        public void saveMilitia(Militia militia)
        {//保存一个民兵，若Id相同，会覆盖数据库里的(省市须指定数据库)
            if(militia.Place == null)
            {
                militia.Place = dbName;
            }

            string database = militia.Place;//指定数据库

            using (var session = store.OpenSession(database))
            {
                session.Store(militia);
                session.SaveChanges();
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

        public bool isBackupRunning(string database)
        {//判断某个数据库是否正在备份
            BackupStatus status;
            var re = store.DatabaseCommands.ForDatabase(database).Get(BackupStatus.RavenBackupStatusDocumentKey);
            status = re.DataAsJson.JsonDeserialization<BackupStatus>();
            return status.IsRunning;
        }


        public void backupOneDB(string dbName, string exportFolder)
        {//dbName数据库名，exportFolder导出文件夹路径，会在路径下创建一个名为dbName的新文件夹
            string dbFolder = exportFolder + "\\" + dbName;
            if(!Directory.Exists(dbFolder))
            {
                Directory.CreateDirectory(dbFolder);
            }
            DatabaseDocument dd = new DatabaseDocument
            {
                Id = null,//数据库名
                Settings =
                        {
						    { "Raven/ActiveBundles", "Encryption" }
                        },
                SecuredSettings =
                    {
                        { "Raven/Encryption/Key", "d2VsY29tZXRvdGhpc3N5c3RlbQ==" }
                    }
            };
            store.DatabaseCommands.GlobalAdmin.StartBackup(dbFolder, dd, false, dbName);
        }

        public void removeOneDB(string database)
        {//删除一个数据库
            store.DatabaseCommands.GlobalAdmin.DeleteDatabase(database, true);
        }

        public void restoreOneDB(string importFolder)
        {//importFolder是备份数据库的文件夹路径,文件夹名即为数据库名
            DirectoryInfo dirInfo = new DirectoryInfo(importFolder);
  
            Operation operation = store.DatabaseCommands.GlobalAdmin.StartRestore(
                new Raven.Abstractions.Data.DatabaseRestoreRequest
                {
                    BackupLocation = dirInfo.FullName,
                    DatabaseName = dirInfo.Name
                }
            );
            operation.WaitForCompletion();
        }//这个有希望代替直接复制，据说直接复制对数据库会造成损害，但是restore的时候我只能restore一个，连续restore两个就会造成冲突  
        
        public List<Militia> queryByContition(Expression<Func<Militia, bool>> lambdaContition, int skip, int take, out int sum, string database = null)
        {//通过lambda表达式查询数据库database里的东西
            if(database == null)
            {
                database = dbName;
            }
            store.DatabaseCommands.GlobalAdmin.EnsureDatabaseExists(database);
            using(var session = store.OpenSession(database))
            {
                RavenQueryStatistics stats;
                var mList = session.Query<Militia, Militias_All>()
                    //.Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Statistics(out stats).Where(lambdaContition)
                    .Skip(skip).Take(take)
                    .OfType<Militia>()
                    .ToList();
                sum = stats.TotalResults;

                return mList;
            }
        }

        public List<Militia> getMilitias(int skip, int take, out int sum, string database = null)
        {//直接从数据库里取数据，不用任何条件,且take的大小限制为0~10000
            if(database == null)
            {
                database = dbName;
            }
            store.DatabaseCommands.GlobalAdmin.EnsureDatabaseExists(database);
            using (var session = store.OpenSession(database))
            {
                RavenQueryStatistics stats;
                var militias = session.Query<Militia>()
                    //.Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Statistics(out stats).Skip(skip).Take(take).ToList();
                sum = stats.TotalResults;

                return militias;
            }
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
            using (var session = store.OpenSession(database))
            {
                var gfacetResults = session.Query<Militia, Militias_All>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Where(lambdaContition)
                    .AggregateBy(propertyExpression).CountOn(x => x.Group).ToList();
                return gfacetResults.Results[propertyName].Values;
            }
        }

        public List<Militia> getMilitiasByCredentialNumber(string CredentialNumber, string database = null)
        {//根据身份证号获取民兵
            if (database == null)
            {
                database = dbName;
            }

            using (var session = store.OpenSession(database))
            {
                var mList = session.Query<Militia, Militias_All>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow(TimeSpan.FromSeconds(timeoutseconds)))
                    .Where(x => x.CredentialNumber == CredentialNumber)
                    .Skip(0).Take(1000)
                    .OfType<Militia>()
                    .ToList();
                return mList;
            }
        }

    }

    public class Militias_All : AbstractIndexCreationTask<Militia>
    {//统计所用的索引，除了Id，全都弄进来了，可以根据每一个属性查询
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
