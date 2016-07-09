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

            sqlDao.deleteOneMilitia(militia);//从数据库中删除

            FormBizs.latestMilitiaForm.newOperationOn(militia, "被删除");
        }

        public List<Militia> queryByContition(System.Linq.Expressions.Expression<Func<Militia, bool>> lambdaContition, int skip, int take, out int sum)
        {//根据条件分页查询
            return sqlDao.queryByContition(lambdaContition, skip, take, out sum);
        }

        public void BulkInsertMilitias(List<Militia> mList)
        {//批量插入默认数据库
            sqlDao.bulkInsertMilitias(mList);
        }

        public void exportAsFile(string file)
        {
            sqlDao.exportToFile(file);
        }
        public void importFormFile(string file, DictTree dt)
        {
            sqlDao.importFromFile(file, dt);
        }

        public List<Militia> loadMilitias(List<string> ids)
        {
            return sqlDao.loadMilitias(ids);
        }

        public void detectConflicts(DictTree dt)
        {
            sqlDao.detectConflicts(dt);
        }

        /*public List<List<Militia>> getConflictMilitias(out bool isStale)
        {//动态聚合来查冲突
            List<List<Militia>> mLList = new List<List<Militia>>();

            List<FacetValue> fList = sqlDao.getConflictNums(out isStale);
            MessageBox.Show("冲突" + fList.Count + "个");
            foreach(FacetValue fv in fList)
            {
                List<Militia> mList = sqlDao.getMilitiasByCredentialNumber(fv.Range, out isStale);
                mLList.Add(mList);
            }
            
            return mLList;
        }*/

        public Dictionary<string, FacetValue> getGroupNums()
        {//获取某些数据库中的所有组中民兵的个数
            List<FacetValue> fList = sqlDao.getGroupNums();

            Dictionary<string, FacetValue> fDict = fList.ToDictionary(x => x.Range);

            return fDict;
        }

        public Dictionary<string, FacetValue> getEnumStatistics(System.Linq.Expressions.Expression<Func<Militia, bool>> lambdaContition, string propertyName, string Place = null)
        {//根据某个属性，统计各属性值的民兵个数
            Dictionary<string, FacetValue> fdict = sqlDao.getAggregateNums(lambdaContition, propertyName).ToDictionary(x => x.Range);
            return fdict;
        }

    }
}
