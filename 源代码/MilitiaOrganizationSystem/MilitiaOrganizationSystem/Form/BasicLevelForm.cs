﻿using System;
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
    public partial class BasicLevelForm : Form
    {
        public const string xmlGroupFile = "xmlGroupFile.xml";//分组的配置文件
        public const string dbName = "basicLevelDB";//数据库名

        public static SqlBiz sqlBiz = new SqlBiz(dbName);//静态的数据库

        private XMLGroupTaskForm xmlGroupTaskForm;//分组界面
        private Condition condition = new Condition();//筛选条件

        private MilitiaListViewBiz listViewBiz;//民兵信息列表的业务逻辑层，用于对listView的增删改，存入数据库

        public BasicLevelForm()
        {//构造函数
            InitializeComponent();
            xmlGroupTaskForm = null;
            listViewBiz = new MilitiaListViewBiz(militia_ListView, sqlBiz, x => x.Group == "未分组");//需指定数据库
            /*//从数据库中加载未分组民兵信息到显示
            listViewBiz.loadNotGroupedMilitiasInDb();*/

            militia_ListView.MouseDoubleClick += Militia_ListView_MouseDoubleClick;
            militia_ListView.ItemDrag += Militia_ListView_ItemDrag;

            militia_ListView.DragEnter += Militia_ListView_DragEnter;
            militia_ListView.DragOver += Militia_ListView_DragOver;
            militia_ListView.DragDrop += Militia_ListView_DragDrop;
            

        }

        private void Militia_ListView_DragOver(object sender, DragEventArgs e)
        {
            //MessageBox.Show((sender == militia_ListView) + "");
            MoveTag mt = (MoveTag)e.Data.GetData(typeof(MoveTag));
            if(mt.source == this)
            {//如果是从自己移过来的
                e.Effect = DragDropEffects.None;
            } else
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void Militia_ListView_DragDrop(object sender, DragEventArgs e)
        {//自动的，好像当e.effect==None时不会调用这个函数
            MoveTag mt = (MoveTag)e.Data.GetData(typeof(MoveTag));
            List<Militia> mList = mt.moveMilitias;
            militia_ListView.BeginUpdate();//开始更新界面
            foreach(Militia militia in mList)
            {
                //在之前让原分组界面的个数减少1
                FormBizs.groupBiz.reduceCount(militia);
                ListViewItem lvi = listViewBiz.findItemWithMilitia(militia);
                militia.Group = "未分组";
                FormBizs.updateMilitiaItem(militia);//通知所有的民兵列表更新
                sqlBiz.updateMilitia(militia);//保存
                if (lvi != null)
                {
                    lvi.Tag = militia;
                    listViewBiz.updateItem(lvi);
                } else
                {
                    listViewBiz.addOneMilitia(militia);
                }

                if(!condition.match(militia))
                {//不满足筛选条件，则不能显示在这个界面
                    lvi.Remove();
                }
            }
            militia_ListView.EndUpdate();//结束更新界面
        }

        private void Militia_ListView_DragEnter(object sender, DragEventArgs e)
        {
            this.Focus();
            e.Effect = DragDropEffects.Move;
        }

        private void Militia_ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {//移动选中的items

            if(e.Button == MouseButtons.Left)
            {
                List<Militia> mList = new List<Militia>();
                foreach(ListViewItem lvi in militia_ListView.SelectedItems)
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
                        if(!condition.match(militia))
                        {
                            lvi.Remove();
                        }
                    }
                }
            }
        }

        private void Militia_ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {//双击编辑
            ListViewItem lvi = militia_ListView.GetItemAt(e.X, e.Y);
            int subIndex = lvi.SubItems.IndexOf(lvi.GetSubItemAt(e.X, e.Y));
            if(lvi != null)
            {
                listViewBiz.editOne(lvi, subIndex);//弹出编辑窗口，并指定光标在subIndex处
            }
        }

        private void BasicLevelForm_Load(object sender, EventArgs e)
        {//加载时,同时打开分组界面
            /**if (File.Exists(xmlGroupFile))
            {//判断分组任务是否已经存在，如果存在，即加载分组任务
                xmlGroupTaskForm = new XMLGroupTaskForm(xmlGroupFile);
                xmlGroupTaskForm.Show();
            }*/
            xmlGroupTaskForm = new XMLGroupTaskForm(xmlGroupFile);
            xmlGroupTaskForm.Show();
        }

        private void importXMLGroupTask_Click(object sender, EventArgs e)
        {//导入分组任务（添加任务）
            /**if(xmlGroupTaskForm != null)
            {
                DialogResult re = MessageBox.Show("分组任务已存在，是否删除之前的分组数据，重新导入编组任务？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if(re == DialogResult.Cancel)
                {
                    return;
                }
            }*/

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fileDialog.FileName;//已经选择了文件
                //MessageBox.Show("已选择文件:" + file, "选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                /**XMLGroupTaskForm lastForm = xmlGroupTaskForm;
                try
                {
                    xmlGroupTaskForm = new XMLGroupTaskForm(file);
                } catch(Exception xmlExeption)
                {
                    MessageBox.Show("导入xml文件出现异常！", "异常警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (lastForm != null)
                {//既然加载成功，那就关闭以前的，且在数据库里删除以前的分组信息
                    lastForm.Close();
                    //数据库操作
                }
                xmlGroupTaskForm.Show();*/
                try
                {
                    xmlGroupTaskForm.addXmlGroupTask(file);
                } catch(Exception xmlExeption)
                {
                    MessageBox.Show("导入xml文件出现异常！", "异常警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }

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

        private void importFromXml_Click(object sender, EventArgs e)
        {
            militia_ListView.Clear();
            listViewBiz.loadMilitiaList(sqlBiz.getAllMilitias());
        }

        private void export_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdlg = new FolderBrowserDialog();
            fbdlg.Description = "请选择要导出的文件路径";
            if(fbdlg.ShowDialog() == DialogResult.OK)
            {
                string folder = fbdlg.SelectedPath;
                FormBizs.export(folder, "Name");
            }
        }

        private void import_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdlg = new FolderBrowserDialog();
            fbdlg.Description = "请选择要导入的文件路径";
            if (fbdlg.ShowDialog() == DialogResult.OK)
            {
                string folder = fbdlg.SelectedPath;
                FormBizs.importOne(folder);
            }
        }

        private void updatePageUpDown()
        {
            pageUpDown.Value = listViewBiz.page;
            pageUpDown.Maximum = listViewBiz.maxPage;
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
    }
}
