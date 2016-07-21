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

        public static SqlBiz sqlBiz;//一个程序有且仅有一个sqlBiz
        public static XMLGroupTreeViewBiz groupBiz;//有且仅有一个groupBiz
        public static List<MilitiaListViewBiz> mListBizs;
        //民兵列表业务逻辑层，有多个，包括主页和点击分组出来的页面

        public static LatestMilitiaForm latestMilitiaForm;//最新操作的民兵界面

        public static ProgressBarForm pbf;//全局的进度条界面

        public static ClientForm mainForm { get; set; }//主界面

        public static void initial()
        {//初始化
            sqlBiz = null;
            groupBiz = null;
            mListBizs = new List<MilitiaListViewBiz>();
            latestMilitiaForm = new LatestMilitiaForm();
            pbf = new ProgressBarForm(1);
        }

        public static void updateMilitiaItem(Militia militia)
        {//更新所有民兵列表界面上的Item显示
            foreach(MilitiaListViewBiz mlvb in mListBizs)
            {
                mlvb.updateMilitiaItem(militia);
            }
        }

        public static void removeMilitiaItem(Militia militia)
        {//通知所有民兵列表界面删除某个民兵
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
                sqlBiz.exportCredentialNumbersToFolder(exportFolder);//导出身份证号
            }

            pbf.Increase( "正在导出分组任务...");
            groupBiz.exportXmlGroupTask(exportFolder + "\\" + exportGroupFileName);
            pbf.Increase( "导出分组任务完毕");
        }

        private static bool exportAsZipFile(string zipFile)
        {//作为这个文件导出
            
            if (File.Exists(zipFile))
            {
                if (MessageBox.Show("将覆盖" + zipFile + ", 确认？", "警告", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return false;
                }
            }

            Zip zip = new Zip(zipFile, "hello", 1);

            if(Directory.Exists("export"))
            {//删除export文件夹再导出
                Directory.Delete("export", true);
            }

            exportAsFolder("export");
            
            pbf.Increase( "正在压缩...");

            zip.addFileOrFolder("export");
            zip.close();

            pbf.Increase( "压缩完毕");

            return true;
            
        }

        /*public static void exportToFolder()
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
        }*/

        public static void exportToFile()
        {//导出到文件中(对外)

            FolderBrowserDialog fbdlg = new FolderBrowserDialog();
            fbdlg.Description = "请选择要导出的文件路径";
            if (fbdlg.ShowDialog() == DialogResult.OK)
            {
                string folder = fbdlg.SelectedPath;

                //下面是导出为zip
                string zipFile = folder + "\\" + PlaceXmlConfig.getPlaceName(LoginXmlConfig.Place).Replace('/', '-') + "（" + LoginXmlConfig.ClientType;
                if(LoginXmlConfig.ClientType == "基层")
                {
                    zipFile += LoginXmlConfig.BasicLevelName;
                }
                switch(LoginXmlConfig.ClientType)
                {
                    case "基层":
                        zipFile += "）.basicdump";
                        break;
                    case "区县人武部":
                        zipFile += ").districtdump";
                        break;
                    case "市军分区":
                        zipFile += ").citydump";
                        break;
                    default:
                        MessageBox.Show("ClientType error");
                        break;
                }

                if(exportAsZipFile(zipFile))
                {
                    MessageBox.Show("导出完成");
                }

                pbf.Completed();//进度条完成
            }
            
        }

        private static bool importFormFolder(string folder)
        {//从文件夹中导入,与exportAsFolder对应
            if (!Directory.Exists(folder))
            {
                return false;
            }
            if (LoginXmlConfig.ClientType == "区县人武部")
            {//区县人武部导入文件
                if(!sqlBiz.importFormFile(folder + "\\" + exportMilitiaFileName))
                {//没有导入成功
                    return false;
                }
            } else
            {//其他导入数据库
                List<string> currentDatabases = sqlBiz.getDatabases();
                string[] databaseFolders = Directory.GetDirectories(folder);
                foreach(string databaseFolder in databaseFolders)
                {
                    if (!Path.GetFileName(databaseFolder).StartsWith(LoginXmlConfig.Place))
                    {//说明导入的数据库不属于本客户端管理
                        MessageBox.Show("本客户端无权导入此数据库！");
                        return false;
                    }
                }
                foreach (string databaseFolder in databaseFolders)
                {
                    if (currentDatabases.Contains(Path.GetFileName(databaseFolder)))
                    {
                        DialogResult re = MessageBox.Show("检测到有数据库冲突，将覆盖所有冲突的数据库，确认？\n覆盖意味着将删除原来相应数据库中的民兵及分组", "警告", MessageBoxButtons.OKCancel);
                        if(re != DialogResult.OK)
                        {//选否则导入失败
                            return false;
                        } else
                        {
                            break;//不再检测数据库冲突，直接覆盖
                        }
                        
                    }
                }
                sqlBiz.restoreDbs(folder);//恢复数据库
                sqlBiz.importCredentialNumbersFromFolder(folder);//导入身份证号
            }
            
            pbf.Increase( "正在导入分组任务...");
            groupBiz.addXmlGroupTask(folder + "\\" + exportGroupFileName);//导入分组任务
            pbf.Increase( "导入分组任务完毕");
            return true;
        }

        /*public static void importFromFolder()
        {//从文件夹导入(对外)
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string folder = fbd.SelectedPath;

                    if(!importFormFolder(folder))
                    {
                        MessageBox.Show("导入失败!可能" + folder + "已经存在！");
                        return;
                    }

                    //detectConflicts();//检测冲突
                    //从文件夹导入还是不自动检查冲突了吧

                    pbf.Increase( "正在刷新分组界面...");
                    groupBiz.refresh();//刷新分组显示
                    pbf.Increase( "分组界面刷新完毕");
                    
                    MessageBox.Show("导入成功");
                } catch
                {
                    MessageBox.Show("导入出现异常");
                }
            }
        }*/

        public static void importFormFiles()
        {//选择多个文件并导入
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Multiselect = true;//支持多选
            switch(LoginXmlConfig.ClientType)
            {
                case "区县人武部":
                    ofdlg.Filter = "民兵编组系统导出文件(*.basicdump)|*.basicdump";//导入基层
                    break;
                case "市军分区":
                    ofdlg.Filter = "民兵编组系统导出文件(*.districtdump)|*.districtdump";//导入区县
                    break;
                case "省军分区":
                    ofdlg.Filter = "民兵编组系统导出文件(*.citydump)|*.citydump";
                    break;
                default:
                    MessageBox.Show("ClientType error");
                    break;
            }
            
            if (ofdlg.ShowDialog() == DialogResult.OK)
            {
                string[] files = ofdlg.FileNames;

                pbf.Increase( "正在导入...");

                foreach (string file in files)
                {
                    try
                    {
                        importFromFile(file, "hello");
                        pbf.Increase("导入" + Path.GetFileName(file) + "完毕");
                    } catch
                    {
                        MessageBox.Show("导入出现异常，可能本客户端没有权限导入" + file + "!");
                    }
                        
                }
                    
                //detectConflicts();//冲突检测
                //导入时先不进行冲突检测，让用户点击按钮，再进行冲突检测吧
                    
                pbf.Increase( "正在刷新分组界面...");
                groupBiz.refresh();//刷新分组界面显示
                pbf.Increase( "刷新分组界面完毕");
                
            }

            
        }

        private static void importFromFile(string importFile, string psd)
        {//从一个文件导入
            if (Directory.Exists("import"))
            {
                Directory.Delete("import", true);
            }
            Directory.CreateDirectory("import");

            pbf.Increase( "正在解压...");
            UnZip unzip = new UnZip(importFile, "import", psd);//解压到数据库中
            unzip.unzipAll();//解压所有
            unzip.close();
            pbf.Increase( "解压完毕");

            //解压完毕后
            if(!importFormFolder("import/export"))
            {
                MessageBox.Show("导入失败！可能" + importFile + "已经存在");
                return;
            }

            Directory.Delete("import", true);//导入之后，删除
        }

        public static void detectConflicts()
        {//检测冲突
            Dictionary<string, List<string>> conflictDict = sqlBiz.getConflicts();
            pbf.Completed();//进度条完成检测冲突
            if (conflictDict.Count == 0)
            {
                MessageBox.Show("没有检测到冲突");
            } else
            {
                MessageBox.Show("检测到" + conflictDict.Count + "个冲突");
                ConflictMilitiasForm cmf = new ConflictMilitiasForm(conflictDict);
                cmf.ShowDialog();
            }
        }

        public static void showLatestMilitias()
        {//显示最近操作的民兵列表界面
            latestMilitiaForm.Show();
            latestMilitiaForm.Focus();
        }

        public static void showAboutDlg()
        {
            AboutBox1 Aboutbox = new AboutBox1();
            Aboutbox.ShowDialog();
        }
    }
}
