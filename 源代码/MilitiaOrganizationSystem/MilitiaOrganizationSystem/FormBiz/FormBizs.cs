using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MilitiaOrganizationSystem
{
    static class FormBizs
    {//全局管理Form的类
        public const string exportGroupFileName = "groupTask.xml";
        public const string exportMilitiaFileName = "export/militiaList";
        



        public static SqlBiz sqlBiz = null;//一个程序有且仅有一个sqlBiz
        public static XMLGroupTreeViewBiz groupBiz = null;//有且仅有一个groupBiz
        public static List<MilitiaListViewBiz> mListBizs = new List<MilitiaListViewBiz>();

        public static LatestMilitiaForm latestMilitiaForm = new LatestMilitiaForm();


        public static void updateMilitiaItem(Militia militia)
        {//更新所有民兵ListView上的Item
            foreach(MilitiaListViewBiz mlvb in mListBizs)
            {
                mlvb.updateMilitiaItem(militia);
            }
        }

        public static void removeMilitiaItem(Militia militia)
        {//通知所有民兵列表删除
            foreach (MilitiaListViewBiz mlvb in mListBizs)
            {
                mlvb.removeMilitiaItem(militia);
            }
        }

        /**public static void export(string folder, string name)
        {//在folder下生成一个文件夹作为导出，文件夹的名称为name
            string exportFolder = folder + "\\" + name;
            if (!Directory.Exists(exportFolder))
            {
                Directory.CreateDirectory(exportFolder);
            }
            groupBiz.exportXmlGroupTask(exportFolder + "\\" + exportGroupFileName);//导出分组任务

            string x = "";//客户端类别
            if(x == "基层")
            {
                sqlBiz.exportAsXmlFile(exportFolder + "\\" + exportMilitiaFileName);//导出数据库民兵信息
            } else
            {
                sqlBiz.exportAsSource(exportFolder);//直接将数据库复制到文件夹下
            }
            
        }*/

        public static void export()
        {//导出
            FolderBrowserDialog fbdlg = new FolderBrowserDialog();
            fbdlg.Description = "请选择要导出的文件路径";
            if (fbdlg.ShowDialog() == DialogResult.OK)
            {
                string folder = fbdlg.SelectedPath;
                export(folder + "\\" + LoginXmlConfig.Place + ".zip", "hello");
            }
        }

        private static void export(string fileName, string psd)
        {//fileName为导出文件，psd为压缩密码
            DateTime startExportTime = DateTime.Now;
            Zip zip = new Zip(fileName, psd, 6);
            if (!Directory.Exists("export"))
            {
                Directory.CreateDirectory("export");
            }
            sqlBiz.exportAsFile("export/militia");//导入导出单数据库
            zip.addFileOrFolder("export");
            Directory.Delete("export", true);//删除

            zip.addFileOrFolder(GroupXmlConfig.xmlGroupFile);//导出分组文件
            zip.close();//导出完毕
            MessageBox.Show("导出完毕, time = " + (DateTime.Now - startExportTime));
        }

        public static void import()
        {//导入
            DateTime startImportTime = DateTime.Now;
            DictTree dt = new DictTree();//用于检测冲突
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Multiselect = true;//支持多选
            ofdlg.Filter = "民兵编组系统导出文件(*.dump)|*.*";
            if (ofdlg.ShowDialog() == DialogResult.OK)
            {
                string[] files = ofdlg.FileNames;
                foreach (string file in files)
                {
                    importOne(file, "hello", dt);
                }
            }
            MessageBox.Show("导入完毕， time = " + (DateTime.Now - startImportTime));

            showConflicts(dt);//检测冲突

            groupBiz.refresh();//刷新分组界面显示
        }

        private static void importOne(string importFile, string psd, DictTree dt)
        {//导入一个
            if(!Directory.Exists("import"))
            {
                Directory.CreateDirectory("import");
            }

            UnZip unzip = new UnZip(importFile, "import", psd);//解压到数据库中
            unzip.unzipAll();//开始解压
            unzip.close();//解压完毕

            string[] files = Directory.GetFiles("import/export");
            foreach(string file in files)
            {
                sqlBiz.importFormFile(file, dt);
            }
            groupBiz.addXmlGroupTask("import/" + GroupXmlConfig.xmlGroupFile);
            Directory.Delete("import", true);//导入完毕后删除
        }

        public static void detectConflict()
        {
            DictTree dt = new DictTree();
            sqlBiz.detectConflicts(dt);
            showConflicts(dt);
        }

        public static void showConflicts(DictTree dt)
        {//检查冲突
            /*bool isStale;
            List<List<Militia>> mlList = sqlBiz.getConflictMilitias(out isStale);
            if (mlList.Count == 0)
            {
                if (isStale)
                {//索引还未计算完毕
                    MessageBox.Show("索引未计算完毕，请稍候点击界面上的“检测冲突”按钮进行冲突检测");
                }
                else
                {
                    MessageBox.Show("没有检测到冲突");
                }
            }
            else
            {
                MessageBox.Show("检测到冲突");
                ConflictMilitiasForm cmf = new ConflictMilitiasForm(mlList, isStale);
                cmf.ShowDialog();
            }*/
            List<List<Militia>> mlList = new List<List<Militia>>();
            Dictionary<string, List<string>> conflictDict = dt.conflictDict;
            foreach(List<string> ids in conflictDict.Values)
            {
                List<Militia> mList = sqlBiz.loadMilitias(ids);
                mlList.Add(mList);
            }
            if(mlList.Count == 0)
            {
                MessageBox.Show("没有检查到冲突");
            } else
            {
                MessageBox.Show("检测到冲突");
                ConflictMilitiasForm cmf = new ConflictMilitiasForm(mlList, true);
                cmf.ShowDialog();
            }
            
        }
    }
}
