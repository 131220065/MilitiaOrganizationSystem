using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilitiaOrganizationSystem
{
    public partial class ProvinceForm : ClientForm
    {//省军分区主界面
        public static string dbName = LoginXmlConfig.Place;//数据库名

        public static SqlBiz sqlBiz = new SqlBiz(dbName);//静态的数据库

        private XMLGroupTaskForm xmlGroupTaskForm;//分组界面

        private Condition condition;//此界面下的lambda表达式

        private string place { get; set; }//该页面的查询条件之一指定数据库
        //此页面的查询条件

        private MilitiaListViewBiz listViewBiz;//民兵信息列表的业务逻辑层，用于对listView的增删改，存入数据库

        public ProvinceForm()
        {//构造函数
            InitializeComponent();
            this.Text = LoginXmlConfig.ClientType + "主页-" + PlaceXmlConfig.getPlaceName(LoginXmlConfig.Place);
            xmlGroupTaskForm = null;
            condition = new Condition("未分组");
            conditionLabel.Text = condition.ToString();
            listViewBiz = new MilitiaListViewBiz(militia_ListView, sqlBiz, condition);//需指定数据库
            updatePageUpDown();
            /*//从数据库中加载未分组民兵信息到显示
            listViewBiz.loadNotGroupedMilitiasInDb();*/

            militia_ListView.MouseDoubleClick += listViewBiz.Militia_ListView_MouseDoubleClick;
            militia_ListView.ItemDrag += listViewBiz.Militia_ListView_ItemDrag;

            militia_ListView.DragEnter += listViewBiz.Militia_ListView_DragEnter;
            militia_ListView.DragOver += listViewBiz.Militia_ListView_DragOver;
            militia_ListView.DragDrop += listViewBiz.Militia_ListView_DragDrop;
        }

        private void BasicLevelForm_Load(object sender, EventArgs e)
        {//加载时,同时打开分组界面
            xmlGroupTaskForm = new XMLGroupTaskForm();
            xmlGroupTaskForm.Show();
        }

        private void importXMLGroupTask_Click(object sender, EventArgs e)
        {//导入分组任务（添加任务）
            listViewBiz.importXMLGroupTask_Click(sender, e);
        }

        private void modify_Click(object sender, EventArgs e)
        {
            listViewBiz.editSelectedItems();
        }

        private void add_Click(object sender, EventArgs e)
        {
            listViewBiz.addOne();
        }

        private void dele_Click(object sender, EventArgs e)
        {
            listViewBiz.deleSelectedItems();
        }

        private void rAdd_Click(object sender, EventArgs e)
        {
            listViewBiz.addOne();
        }

        private void rEdit_Click(object sender, EventArgs e)
        {
            listViewBiz.editSelectedItems();
        }

        private void rDele_Click(object sender, EventArgs e)
        {
            listViewBiz.deleSelectedItems();
        }

        private void export_Click(object sender, EventArgs e)
        {//导出
            FormBizs.exportToFile();
        }

        private void import_Click(object sender, EventArgs e)
        {//导入
            FormBizs.importFormFiles();
            listViewBiz.firstPage();
            updatePageUpDown();
        }

        private void updatePageUpDown()
        {//更新显示
            pageLabel.Text = listViewBiz.page.ToString();
        }

        private void lastPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            listViewBiz.lastPage();
            updatePageUpDown();
            btn.Enabled = true;
            
        }

        private void currentPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            listViewBiz.refreshCurrentPage();
            updatePageUpDown();
            btn.Enabled = true;
        }

        private void nextPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            listViewBiz.nextPage();
            updatePageUpDown();
            btn.Enabled = true;
        }

        private void finalPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            listViewBiz.finalPage();
            updatePageUpDown();
            btn.Enabled = true;
        }

        private void options_Click(object sender, EventArgs e)
        {//打开设置界面
            listViewBiz.setoption();
        }

        private void conditionLabel_Click(object sender, EventArgs e)
        {//打开筛选条件界面
            listViewBiz.changeCondition(conditionLabel);
            updatePageUpDown();
        }

        private void doConflict_Click(object sender, EventArgs e)
        {//检测冲突，在数据库之间
            doConflict.Enabled = false;
            FormBizs.detectConflicts();
            doConflict.Enabled = true;
        }

        private void latestMilitias_Click(object sender, EventArgs e)
        {//最近编辑的民兵
            FormBizs.showLatestMilitias();
        }

        private void stastistics_Click(object sender, EventArgs e)
        {//打开统计界面
            InfoStatisticsForm isf = new InfoStatisticsForm(condition);
            isf.Show();
        }

        private void firstPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            listViewBiz.firstPage();
            updatePageUpDown();
            btn.Enabled = true;
        }

        private void importDirectory_Click(object sender, EventArgs e)
        {
            FormBizs.importFromFolder();
            this.Enabled = false;
            listViewBiz.firstPage();
            updatePageUpDown();
            this.Enabled = true;
        }

        private void exportDirectory_Click(object sender, EventArgs e)
        {
            FormBizs.exportToFolder();
        }
    }
}
