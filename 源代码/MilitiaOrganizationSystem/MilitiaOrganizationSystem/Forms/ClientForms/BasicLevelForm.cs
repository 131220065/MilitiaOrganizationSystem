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
    public partial class BasicLevelForm : ClientForm
    {//基层主界面
        public static string dbName = LoginXmlConfig.Place;//数据库名

        public static SqlBiz sqlBiz = new SqlBiz(dbName);//静态的数据库

        private XMLGroupTaskForm xmlGroupTaskForm;//分组界面

        private Condition condition;//此界面下的查询条件

        private MilitiaListViewBiz listViewBiz;//民兵信息列表的业务逻辑层，用于对listView的增删改，存入数据库

        public BasicLevelForm()
        {//构造函数
            InitializeComponent();
            this.Text = LoginXmlConfig.ClientType + "主页-" + PlaceXmlConfig.getPlaceName(LoginXmlConfig.Place);
            xmlGroupTaskForm = null;
            condition = new Condition("未分组");
            conditionLabel.Text = condition.ToString();
            listViewBiz = new MilitiaListViewBiz(militia_ListView, sqlBiz, condition);//需指定数据库
            updatePageUpDown();//更新最大页数和本页

            //下面绑定事件
            militia_ListView.MouseDoubleClick += listViewBiz.Militia_ListView_MouseDoubleClick;//双击编辑

            //下面是移动相关
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
        {//右键添加
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

        private void importFromXml_Click(object sender, EventArgs e)
        {//测试所用
            sqlBiz.addMilitias(MilitiaXmlConfig.generateMilitias(1000));
            MessageBox.Show("生成1000个民兵成功");
        }

        private void export_Click(object sender, EventArgs e)
        {//导出
            FormBizs.exportToFile();
        }

        private void updatePageUpDown()
        {//更新显示
            pageUpDown.Maximum = listViewBiz.maxPage;
            pageUpDown.Value = listViewBiz.page;
        }

        private void skipPage_Click(object sender, EventArgs e)
        {
            listViewBiz.toPage((int)pageUpDown.Value);
            updatePageUpDown();
        }

        private void lastPage_Click(object sender, EventArgs e)
        {
            listViewBiz.lastPage();
            updatePageUpDown();
            
        }

        private void currentPage_Click(object sender, EventArgs e)
        {
            listViewBiz.refreshCurrentPage();
            updatePageUpDown();
        }

        private void nextPage_Click(object sender, EventArgs e)
        {
            listViewBiz.nextPage();
            updatePageUpDown();
        }

        private void finalPage_Click(object sender, EventArgs e)
        {
            listViewBiz.finalPage();
            updatePageUpDown();
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
        {//检测冲突，在主数据库里面
            FormBizs.detectConflicts();
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
        {//第一页
            listViewBiz.firstPage();
            updatePageUpDown();
        }

        /*private void exportToDirectory_Click(object sender, EventArgs e)
        {
            FormBizs.exportToFolder();
        }*/
    }
}
