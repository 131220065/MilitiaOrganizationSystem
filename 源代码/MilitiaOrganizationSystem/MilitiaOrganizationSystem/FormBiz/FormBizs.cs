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
        public const string exportMilitiaFileName = "militiaList";

        private static TimeSpan importTime = new TimeSpan();
        private static TimeSpan unzipTime = new TimeSpan();
        private static TimeSpan detectTime = new TimeSpan();


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

        private static void exportAsFolder(string exportFolder)
        {//作为文件夹导出
            if (Directory.Exists(exportFolder))
            {
                DialogResult re = MessageBox.Show("备份" + Path.GetFileName(exportFolder) + "已经存在,是否覆盖此备份？", "警告", MessageBoxButtons.OKCancel);
                if (re == DialogResult.OK)
                {
                    Directory.Delete(exportFolder, true);
                    Directory.CreateDirectory(exportFolder);
                }
                else
                {
                    return;
                }
            }
            Directory.CreateDirectory(exportFolder);

            if (LoginXmlConfig.ClientType == "基层")
            {
                sqlBiz.exportAsFile(exportFolder + "\\" + exportMilitiaFileName);
            }
            else
            {
                sqlBiz.backupAllDb(exportFolder);
            }
            groupBiz.exportXmlGroupTask(exportFolder + "\\" + exportGroupFileName);
        }

        private static void exportAsZipFile(string zipFile)
        {//作为这个文件导出
            
            if (File.Exists(zipFile))
            {
                if (MessageBox.Show("将覆盖" + zipFile + ", 确认？", "警告", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
            }
            DateTime startExportTime = DateTime.Now;

            Zip zip = new Zip(zipFile, "hello", 1);

            if(Directory.Exists("export"))
            {//删除export文件夹再导出
                Directory.Delete("export", true);
            }

            exportAsFolder("export");

            zip.addFileOrFolder("export");
            zip.close();

            MessageBox.Show("总时间为: " + (DateTime.Now - startExportTime));
        }

        public static void exportToFolder()
        {//导出到文件夹
            FolderBrowserDialog fbdlg = new FolderBrowserDialog();
            fbdlg.Description = "请选择要导出的文件路径";
            if (fbdlg.ShowDialog() == DialogResult.OK)
            {
                string folder = fbdlg.SelectedPath;

                //下面是导出为文件夹
                string exportFolder = folder + "\\" + PlaceXmlConfig.getPlaceName(LoginXmlConfig.Place).Replace('/', '-') + "（" + LoginXmlConfig.ClientType;//导出的文件夹
                if(LoginXmlConfig.ClientType == "基层")
                {//如果是基层，加一个随机数
                    exportFolder += LoginXmlConfig.Id;
                }
                exportFolder += "）";

                exportAsFolder(exportFolder);

                MessageBox.Show("导出完成");
            }
        }

        public static void exportToFile()
        {

            FolderBrowserDialog fbdlg = new FolderBrowserDialog();
            fbdlg.Description = "请选择要导出的文件路径";
            if (fbdlg.ShowDialog() == DialogResult.OK)
            {
                string folder = fbdlg.SelectedPath;

                //下面是导出为zip
                string zipFile = folder + "\\" + PlaceXmlConfig.getPlaceName(LoginXmlConfig.Place).Replace('/', '-') + "（" + LoginXmlConfig.ClientType;
                if(LoginXmlConfig.ClientType == "基层")
                {
                    zipFile += LoginXmlConfig.Id;
                }
                zipFile += "）.zip";

                exportAsZipFile(zipFile);

                MessageBox.Show("导出完成");
            }
            
        }

        private static void importFormFolder(string folder)
        {//从文件夹中导入
            if (!Directory.Exists(folder))
            {
                return;
            }
            if (LoginXmlConfig.ClientType == "区县人武部")
            {//区县人武部导入文件
                sqlBiz.importFormFile(folder + "\\" + exportMilitiaFileName);
            } else
            {//其他导入数据库
                sqlBiz.restoreDbs(folder);
            }

            groupBiz.addXmlGroupTask(folder + "\\" + exportGroupFileName);//导入分组任务

        }

        public static void importFromFolder()
        {//从文件夹导入
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string folder = fbd.SelectedPath;


                    DateTime startImportTime = DateTime.Now;

                    importFormFolder(folder);

                    //detectConflicts();//检测冲突
                    //从文件夹导入还是不自动检查冲突了吧

                    groupBiz.refresh();//刷新分组显示

                    MessageBox.Show("totalTime = " + (DateTime.Now - startImportTime));
                    MessageBox.Show("导入成功");
                } catch
                {
                    MessageBox.Show("导入出现异常");
                }
                
            }
            
        }

        public static void importFormFiles()
        {//选择多个文件并导入
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Multiselect = true;//支持多选
            ofdlg.Filter = "民兵编组系统导出文件(*.dump)|*.*";
            if (ofdlg.ShowDialog() == DialogResult.OK)
            {
                string[] files = ofdlg.FileNames;
                try
                {
                    DateTime startImportTime = DateTime.Now;

                    foreach (string file in files)
                    {
                        importFromFile(file, "hello");
                    }

                    importTime = DateTime.Now - startImportTime;

                    detectConflicts();//冲突检测

                    groupBiz.refresh();//刷新分组界面显示

                    MessageBox.Show("importTime = " + importTime + ", totalUnzipTime = " + unzipTime + ", detectTime = " + detectTime
                        + "\n" + "totalTime = " + (DateTime.Now - startImportTime));
                } catch
                {
                    MessageBox.Show("导入出现异常");
                }
                
            }

            
        }

        private static void importFromFile(string importFile, string psd)
        {//从一个文件导入
            if (Directory.Exists("import"))
            {
                Directory.Delete("import", true);
            }
            Directory.CreateDirectory("import");
            DateTime startUnzipTime = DateTime.Now;
            UnZip unzip = new UnZip(importFile, "import", psd);//解压到数据库中
            unzip.unzipAll();//解压所有
            unzip.close();
            unzipTime += DateTime.Now - startUnzipTime;

            //解压完毕后
            importFormFolder("import/export");

            Directory.Delete("import", true);//导入之后，删除
        }

        public static void detectConflicts()
        {//检测冲突
            DateTime startDetectTime = DateTime.Now;
            Dictionary<string, List<string>> conflictDict = sqlBiz.getConflicts();
            detectTime = DateTime.Now - startDetectTime;

            if(conflictDict.Count == 0)
            {
                MessageBox.Show("没有检测到冲突");
            } else
            {
                MessageBox.Show("检测到" + conflictDict.Count + "个冲突");
                ConflictMilitiasForm cmf = new ConflictMilitiasForm(conflictDict);
                cmf.ShowDialog();
            }
        }


    }
}
