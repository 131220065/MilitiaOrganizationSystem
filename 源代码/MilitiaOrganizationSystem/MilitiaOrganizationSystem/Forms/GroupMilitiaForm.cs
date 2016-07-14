using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilitiaOrganizationSystem
{
    public partial class GroupMilitiaForm : Form
    {//查看分组弹出的界面
        private SqlBiz sqlBiz;
        private Condition condition;//筛选条件
        private MilitiaListViewBiz listViewBiz;//民兵信息列表业务逻辑层

        public GroupMilitiaForm(SqlBiz sBiz, string group)
        {
            InitializeComponent();
            sqlBiz = sBiz;
            condition = new Condition(group);
            conditionLabel.Text = condition.ToString();
            listViewBiz = new MilitiaListViewBiz(militia_ListView, sqlBiz, condition);//需指定数据库
            updatePageUpDown();

            militia_ListView.ItemDrag += Militia_ListView_ItemDrag;
        }

        private void Militia_ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {//移动选中的items
            if (e.Button == MouseButtons.Left)
            {
                List<Militia> mList = new List<Militia>();
                foreach (ListViewItem lvi in militia_ListView.SelectedItems)
                {
                    Militia militia = (Militia)lvi.Tag;
                    mList.Add(militia);
                }
                MoveTag mt = new MoveTag(this, mList);
                
                if (DoDragDrop(mt, DragDropEffects.Move) == DragDropEffects.Move)
                {//移动成功后
                    foreach (ListViewItem lvi in militia_ListView.SelectedItems)
                    {
                        Militia militia = (Militia)lvi.Tag;
                        //if militia 不符合筛选条件，则删掉这个item
                        if (!condition.lambdaCondition.Compile()(militia))
                        {
                            lvi.Remove();
                        }
                    }
                }
            }
        }

        private void updatePageUpDown()
        {
            if(LoginXmlConfig.ClientType == "省军分区")
            {
                pageUpDown.Visible = false;//隐藏不见
                labelDi.Visible = false;
                labelPage.Visible = false;
                skipPage.Visible = false;
                skipPage.Enabled = false;
            } else
            {
                pageUpDown.Maximum = listViewBiz.maxPage;
                pageUpDown.Value = listViewBiz.page;
            }
            
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

        private void conditionLabel_Click(object sender, EventArgs e)
        {//打开筛选条件界面
            ConditionForm cf = new ConditionForm(condition);
            if (cf.ShowDialog() == DialogResult.OK)
            {
                conditionLabel.Text = condition.ToString();
                listViewBiz.refreshCurrentPage();
            }
        }

        private void statistics_Click(object sender, EventArgs e)
        {
            InfoStatisticsForm isf = new InfoStatisticsForm(condition);
            isf.Show();
        }
    }
}
