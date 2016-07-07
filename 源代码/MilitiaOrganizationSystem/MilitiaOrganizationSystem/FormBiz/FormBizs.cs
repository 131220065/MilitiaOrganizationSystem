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
        {//导出之前，先检查冲突
            /*List<List<Militia>> mlList = sqlBiz.getConflictMilitias();//主数据库
            if (mlList.Count > 0)
            {//检测到冲突
                ConflictMilitiasForm cmf = new ConflictMilitiasForm(mlList);
                if (cmf.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("没有处理完冲突，请处理完冲突再导出！");
                    return;
                }
            } else
            {
                MessageBox.Show("没有检查到冲突,可以导出");
            }*/



            FolderBrowserDialog fbdlg = new FolderBrowserDialog();
            fbdlg.Description = "请选择要导出的文件路径";
            if (fbdlg.ShowDialog() == DialogResult.OK)
            {
                string folder = fbdlg.SelectedPath;
                export(folder + "\\" + LoginXmlConfig.Place + ".zip", "hello");
            }
            MessageBox.Show("导出成功");
        }

        private static void export(string fileName, string psd)
        {//fileName为导出文件，psd为压缩密码
            Zip zip = new Zip(fileName, psd, 6);
            /*if(LoginXmlConfig.ClientType == "基层")
            {
                List<string> exportMilitiaFiles = sqlBiz.exportAsXmlFile(exportMilitiaFileName);//为文件
                foreach (string exportFile in exportMilitiaFiles)
                {
                    zip.addFileOrFolder(exportFile);
                    File.Delete(exportFile);//加入压缩文件后，删去文件
                }
            } else
            {//区县人武部，市军分区，省军分区
                sqlBiz.exportZip(zip);
            }*/
            if (!Directory.Exists("export"))
            {
                Directory.CreateDirectory("export");
            }
            sqlBiz.exportAsFile("export/militia");//导入导出单数据库
            zip.addFileOrFolder("export");
            Directory.Delete("export", true);//删除
            
            
            zip.addFileOrFolder(GroupXmlConfig.xmlGroupFile);//导出分组文件
            zip.close();
        }

        /**public static void importOne(string importFolder)
        {

            List<string> importedDatabases = null;
            
            string x = "";//客户端类别
            if(x == "基层")
            {
                return;
            }
            if (x == "区县")
            {
                sqlBiz.importFromMilitiaXml(importFolder + "\\" + exportMilitiaFileName);
            }
            else
            {//复制数据库文件夹到DataBases
                importedDatabases = sqlBiz.importFromSource(importFolder);//导入成功的所有数据库名
            }

            groupBiz.addXmlGroupTask(importFolder + "\\" + exportGroupFileName, importedDatabases);//导入分组任务

        }*/

        public static void import()
        {
            MessageBox.Show("开始导入， time = " + DateTime.Now);
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Multiselect = true;//支持多选
            ofdlg.Filter = "民兵编组系统导出文件(*.dump)|*.*";
            if (ofdlg.ShowDialog() == DialogResult.OK)
            {
                string[] files = ofdlg.FileNames;
                foreach (string file in files)
                {
                    importOne(file, "hello");
                }
            }
            MessageBox.Show("导入完毕， time = " + DateTime.Now);
            List<List<Militia>> mlList = sqlBiz.getConflictMilitias();
            MessageBox.Show("拿到冲突, time = " + DateTime.Now);
            if (mlList.Count == 0)
            {
                MessageBox.Show("没有检测到冲突");
            }
            else
            {
                MessageBox.Show("检测到冲突");
                ConflictMilitiasForm cmf = new ConflictMilitiasForm(mlList);
                cmf.ShowDialog();
            }

            groupBiz.refresh();//刷新分组界面显示
        }

        private static void importOne(string importFile, string psd)
        {//导入一个
            if(!Directory.Exists("import"))
            {
                Directory.CreateDirectory("import");
            }
            UnZip unzip = new UnZip(importFile, "import", psd);//解压到数据库中
            List<string> importedDatabases = sqlBiz.importUnzip(unzip);//开始解压
            unzip.close();
            string[] files = Directory.GetFiles("import/export");
            foreach(string file in files)
            {
                sqlBiz.importFormFile(file);
            }
            groupBiz.addXmlGroupTask("import/" + GroupXmlConfig.xmlGroupFile);
            Directory.Delete("import", true);
            /*string[] files = Directory.GetFiles("import");
            foreach(string file in files)
            {//导入militiaXml或者GroupXml
                if(Path.GetFileName(file).StartsWith(Path.GetFileName(exportMilitiaFileName)))
                {//militiaList
                    sqlBiz.importFromMilitiaXml(file);
                } else if(file == exportGroupFileName)
                {
                    groupBiz.addXmlGroupTask(file);
                }
                //导入之后，删去
                File.Delete(file);
            }

            string[] databases = Directory.GetDirectories("import");
            sqlBiz.restoreDbs(databases.ToList());*/
        }
    }
}
