using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MilitiaOrganizationSystem
{
    public class MilitiaListViewBiz
    {//listview业务逻辑层
        private static MilitiaEditDialog militiaEditDlg = new MilitiaEditDialog();//编辑民兵对话框,应该只有主界面才会调用
        private static OptionForm ofDlg = new OptionForm();//设置界面，设置页面最大显示数量以及显示的属性

        private ListView militia_ListView;
        private bool sort = false;//排序
        private SqlBiz sqlBiz;//数据库业务逻辑层

        private Condition condition;
        //此页面的查询条件

        private XmlNodeList parameters = MilitiaXmlConfig.parameters;//民兵的属性列表
        public List<int> displayedParameterIndexs { get; set; }//需显示的参数下标

        public int pageSize { get; set; }//每页显示多少民兵
        public int page { get; set; }//第几页
        public int maxPage { get; set; }//在加载第一页的时候初始化，为最大页数

        private string currentDatabase;//当前的数据库
        private int currentSkip;//当前数据库和所跳过的数量


        public MilitiaListViewBiz(ListView listView, SqlBiz sBz, Condition condition)
        {//构造函数,listView控件, 数据库业务逻辑层, 条件
            militia_ListView = listView;
            displayedParameterIndexs = MilitiaXmlConfig.getAllDisplayedParameterIndexs();
            sqlBiz = sBz;

            this.condition = condition;//查询条件

            page = 1;//页
            pageSize = 20;//每页显示最大数量

            bindEvent();

            firstPage();//刷新为第一页
            
            FormBizs.mListBizs.Add(this);//添加到biz池中
        }

        ~MilitiaListViewBiz()
        {//析构,从biz池中移除
            FormBizs.mListBizs.Remove(this);
        }

        private void bindEvent()
        {//绑定事件
            militia_ListView.ColumnClick += Militia_ListView_ColumnClick;//点击排序
        }

        private void Militia_ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // 创建一个ListView排序类的对象，并设置militia_ListView的排序器
            ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();


            if (sort == false)
            {//sort是用来翻转的，无论是true还是false，都会排序
                sort = true;
                lvwColumnSorter.Order = SortOrder.Descending;
            }
            else
            {
                sort = false;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }
            lvwColumnSorter.SortColumn = e.Column;//排序的列
            militia_ListView.ListViewItemSorter = lvwColumnSorter;//赋值之后会自动排序
        }

        private void addColumnHeader()
        {//添加表头
            militia_ListView.Columns.Clear();//先清除
            foreach(int index in displayedParameterIndexs)
            {//根据MilitiaXmlConfig类的配置文件添加表头
                XmlNode parameter = parameters[index];
                ColumnHeader ch = new ColumnHeader();
                ch.Text = parameter.Attributes["name"].Value;   //设置列标题 
                ch.Width = 120;    //设置列宽度 
                ch.TextAlign = HorizontalAlignment.Left;   //设置列的对齐方式 
                militia_ListView.Columns.Add(ch);    //将列头添加到ListView控件。 
            }
        }

        public void updateItem(ListViewItem lvi)
        {//用tag更新显示
            Militia militia = (Militia)lvi.Tag;
            MilitiaReflection mr = new MilitiaReflection(militia);//反射
            lvi.ImageIndex = 0;//图片

            lvi.SubItems.Clear();//这样clear会删除第一个key,故后面还需要对key赋值

            string[] items = new string[displayedParameterIndexs.Count - 1];
            for(int i = 0; i < items.Length; i++)
            {
               items[i] = "";
            }
            lvi.SubItems.AddRange(items);//加上子项

            for(int i = 0; i < displayedParameterIndexs.Count; i++)
            {//更新所有属性
                int index = displayedParameterIndexs[i];
                XmlNode node = parameters[index];
                string value = "";
                try
                {
                    value = mr.getProperty(node.Attributes["property"].Value).ToString();
                }
                catch
                {

                }

                switch (node.Attributes["type"].Value)
                {//type为enum的属性,需要将value转为对应的字符串显示
                    case "enum":
                        XmlNode selectNode = node.SelectSingleNode("selection[@value='" + value + "']");
                        if (selectNode != null)
                        {
                            value = selectNode.Attributes["name"].Value;
                        }
                        break;
                    case "place"://place，需要转为中文名
                        value = PlaceXmlConfig.getPlaceName(value);
                        break;
                    default://其他的不用转化，直接使用
                        break;
                }

                lvi.SubItems[i].Text = value;//赋值给显示
            }

            lvi.Name = militia.Id;//对key赋值，设置查询的Key
        }

        public void loadMilitiaList(List<Militia> mList)
        {//清空原来的，加载现在的
            militia_ListView.Clear();//先清除所有

            addColumnHeader();//添加头

            militia_ListView.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 

            foreach (Militia militia in mList)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.Tag = militia;//设置Tag

                //lvi.Name = militia.Id;//不用再设置查询的Key, updateItem会设置

                updateItem(lvi);//更新显示

                militia_ListView.Items.Add(lvi);
            }

            militia_ListView.EndUpdate();  //结束数据处理，UI界面一次性绘制。 

        }

        private void addOneMilitia(Militia militia)
        {//为民兵对象添加一个item显示
            ListViewItem lvi = new ListViewItem();
            lvi.Tag = militia;
            //lvi.Name = militia.Id;//之前一定是存了数据库的, updateItem会设置key,不用在这里设置
            updateItem(lvi);
            militia_ListView.Items.Add(lvi);
            militia_ListView.SelectedItems.Clear();//清空是为了单选
            lvi.Selected = true;
        }

        public void addOne()
        {//添加一个民兵
            Militia militia = new Militia();
            
            if(militiaEditDlg.showEditDlg(militia) == DialogResult.OK)
            {
                sqlBiz.addMilitia(militia);//先加入数据库，这样才有了Id，才能使用

                addOneMilitia(militia);
            }
           
        }

        private void editOne(ListViewItem lvi, int focusIndex = 0)
        {//编辑一个民兵,focusIndex是弹出编辑对话框需要focus到哪个编辑框
            Militia militia = (Militia)lvi.Tag;

            if(militiaEditDlg.showEditDlg(militia, focusIndex) == DialogResult.OK)
            {
                updateItem(lvi);
                sqlBiz.updateMilitia(militia);
            }

            //通知其他界面的listview刷新民兵
            FormBizs.updateMilitiaItem(militia);
        }

        public void editSelectedItems()
        {//编辑所有选中的item
            foreach(ListViewItem lvi in militia_ListView.SelectedItems)
            {
                editOne(lvi);
            }
        }

        public void deleSelectedItems()
        {//删除所有选中的item
            foreach(ListViewItem lvi in militia_ListView.SelectedItems)
            {
                Militia militia = (Militia)lvi.Tag;
                sqlBiz.deleteMilitia(militia);  
            }
        }

        public ListViewItem findItemWithMilitia(Militia militia)
        {//根据民兵对象，查找此界面的民兵(根据key)
            ListViewItem[] lvis = militia_ListView.Items.Find(militia.Id, false);
            foreach(ListViewItem lvi in lvis)
            {
                if(((Militia)lvi.Tag).Place == militia.Place)
                {//Place即为数据库，如果相等，说明是要找的
                    return lvi;
                }
            }
            return null;//没找到
        }


        public void updateMilitiaItem(Militia militia)
        {//刷新一个民兵的显示，（可能在分组界面更改了分组），函数被分组界面调用
            ListViewItem lvi = findItemWithMilitia(militia);
            
            if (lvi != null)
            {
                lvi.Tag = militia;
                if(militia.isSatisfy(condition))
                {//如果满足当前的条件，才更新显示
                    updateItem(lvi);
                } else
                {//如果不满足条件，则删除这个lvi
                    lvi.Remove();
                }
            }
        }

        public void removeMilitiaItem(Militia militia)
        {//通过民兵指定删除界面上对应的item
            ListViewItem lvi = findItemWithMilitia(militia);

            if (lvi != null)
            {
                lvi.Remove();

            }
        }

        public void setoption()
        {//弹出设置界面
            if(ofDlg.showOptionDialog(this) == DialogResult.OK)
            {//ok后更新显示
                refreshCurrentPage();
            }
        }

        public void changeCondition(Label conditionLabel)
        {//改变筛选条件，其中conditionLabel是用来显示条件的标签
            ConditionForm cf = new ConditionForm(condition);
            if (cf.ShowDialog() == DialogResult.OK)
            {
                conditionLabel.Text = condition.ToString();

                FormBizs.pbf.Increase("正在查询,请稍候...");
                

                firstPage();

                FormBizs.pbf.Increase("查询完毕");
                FormBizs.pbf.Completed();
            }
        }

        public void refreshCurrentPage()
        {//刷新本页
            if(LoginXmlConfig.ClientType == "省军分区")
            {//省军分区特殊对待，因为省军分区每个数据库查一遍太慢了
                List<Militia> mList = sqlBiz.currentPage(condition.lambdaCondition, condition.place, currentDatabase, currentSkip, pageSize, out currentDatabase, out currentSkip);

                loadMilitiaList(mList);
            } else
            {//其他的会返回最大页数
                int sum;
                List<Militia> mList = sqlBiz.queryByContition(condition.lambdaCondition, (page - 1) * pageSize, pageSize, out sum, condition.place);
                maxPage = sum / pageSize + (sum % pageSize == 0 ? 0 : 1);//最大页数
                if(maxPage == 0)
                {
                    maxPage = 1;
                }
                if(page > maxPage)
                {   
                    page = maxPage;
                }
                loadMilitiaList(mList);
            }

            FormBizs.pbf.Completed();
        }

        public void firstPage()
        {//第一页
            FormBizs.pbf.Increase("开始刷新列表...");
            if (LoginXmlConfig.ClientType == "省军分区")
            {
                page = 1;
                List<Militia> mList = sqlBiz.firstPage(condition.lambdaCondition, condition.place, pageSize, out currentDatabase, out currentSkip);
                loadMilitiaList(mList);
            } else
            {
                page = 1;
                refreshCurrentPage();
            }

            FormBizs.pbf.Completed();
        }

        public void lastPage()
        {//上一页
            FormBizs.pbf.Increase("开始刷新列表...");
            if (LoginXmlConfig.ClientType == "省军分区")
            {
                page--;
                List<Militia> mList = sqlBiz.lastPage(condition.lambdaCondition, condition.place, currentDatabase, currentSkip, pageSize, out currentDatabase, out currentSkip);

                if (mList.Count == 0)
                {//说明是第一页
                    MessageBox.Show("已到第一页");
                    firstPage();
                }
                else
                {
                    loadMilitiaList(mList);
                }
            } else
            {
                if (page > 1)
                {
                    page--;
                }
                refreshCurrentPage();
            }

            FormBizs.pbf.Completed();
        }

        public void nextPage()
        {//下一页
            FormBizs.pbf.Increase("开始刷新列表...");
            if (LoginXmlConfig.ClientType == "省军分区")
            {
                page++;
                List<Militia> mList = sqlBiz.nextPage(condition.lambdaCondition, condition.place, currentDatabase, currentSkip, pageSize, out currentDatabase, out currentSkip);
                if (mList.Count == 0)
                {//说明已经到最后一页
                    MessageBox.Show("已到最后一页");
                    finalPage();
                }
                else
                {
                    loadMilitiaList(mList);
                }
            } else
            {
                if (page < maxPage)
                {
                    page++;
                }
                refreshCurrentPage();
            }

            FormBizs.pbf.Completed();

        }

        public void finalPage()
        {//最后一页
            FormBizs.pbf.Increase("开始刷新列表...");
            if (LoginXmlConfig.ClientType == "省军分区")
            {
                page = -1;
                List<Militia> mList = sqlBiz.finalPage(condition.lambdaCondition, condition.place, pageSize, out currentDatabase, out currentSkip);
                loadMilitiaList(mList);
            }
            else
            {
                page = maxPage;
                refreshCurrentPage();
            }

            FormBizs.pbf.Completed();
        }

        public void toPage(int p)
        {//跳页
            if (p >= 1 && p <= maxPage)
            {
                page = p;
            }
            refreshCurrentPage();
        }



        class ListViewColumnSorter : IComparer
        {//给当前页面排序类,对数据库不起作用

            /// <summary>
            /// 指定按照哪个列排序
            /// </summary>
            private int ColumnToSort;

            /// <summary>
            /// 指定排序的方式
            /// </summary>
            private SortOrder OrderOfSort;

            /// <summary>
            /// 声明CaseInsensitiveComparer类对象，
            /// 参见ms-help://MS.VSCC.2003/MS.MSDNQTR.2003FEB.2052/cpref/html/frlrfSystemCollectionsCaseInsensitiveComparerClassTopic.htm
            /// </summary>
            private CaseInsensitiveComparer ObjectCompare;


            /// <summary>
            /// 构造函数
            /// </summary>
            public ListViewColumnSorter()
            {
                // 默认按第一列排序
                ColumnToSort = 0;

                // 排序方式为不排序
                OrderOfSort = SortOrder.None;

                // 初始化CaseInsensitiveComparer类对象
                ObjectCompare = new CaseInsensitiveComparer();
            }


            /// <summary>
            /// 重写IComparer接口.
            /// </summary>
            /// <param name="x">要比较的第一个对象</param>
            /// <param name="y">要比较的第二个对象</param>
            /// <returns>比较的结果.如果相等返回0，如果x大于y返回1，如果x小于y返回-1</returns>
            public int Compare(object x, object y)
            {
                int compareResult;
                ListViewItem listviewX, listviewY;

                // 将比较对象转换为ListViewItem对象
                listviewX = (ListViewItem)x;
                listviewY = (ListViewItem)y;

                // 比较
                compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

                // 根据上面的比较结果返回正确的比较结果
                if (OrderOfSort == SortOrder.Ascending)
                {
                    // 因为是正序排序，所以直接返回结果
                    return compareResult;
                }
                else if (OrderOfSort == SortOrder.Descending)
                {
                    // 如果是反序排序，所以要取负值再返回
                    return (-compareResult);
                }
                else
                {
                    // 如果相等返回0
                    return 0;
                }
            }


            /// <summary>
            /// 获取或设置按照哪一列排序.
            /// </summary>
            public int SortColumn
            {
                set
                {
                    ColumnToSort = value;
                }
                get
                {
                    return ColumnToSort;
                }
            }


            /// <summary>
            /// 获取或设置排序方式.
            /// </summary>
            public SortOrder Order
            {

                set
                {
                    OrderOfSort = value;
                }
                get
                {
                    return OrderOfSort;
                }
            }
        }



        public void Militia_ListView_DragOver(object sender, DragEventArgs e)
        {//悬于..之上的时候
            MoveTag mt = (MoveTag)e.Data.GetData(typeof(MoveTag));
            if (mt.source == this)
            {//如果是从自己移过来的，则啥也不做
                e.Effect = DragDropEffects.None;
            }
            else
            {//否则移动
                e.Effect = DragDropEffects.Move;
            }
        }

        public void Militia_ListView_DragDrop(object sender, DragEventArgs e)
        {//鼠标松开放下时，自动的，好像当e.effect==None时不会调用这个函数
            MoveTag mt = (MoveTag)e.Data.GetData(typeof(MoveTag));
            List<Militia> mList = mt.moveMilitias;
            militia_ListView.BeginUpdate();//开始更新界面
            foreach (Militia militia in mList)
            {
                if (militia.Id == null)
                {//是删除后移动过来的
                    if (MessageBox.Show("民兵：" + militia.info() + " 已经被删除，是否恢复它并继续操作？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        militia.Group = "未分组";
                        sqlBiz.addMilitia(militia);
                        if (militia.isSatisfy(condition))
                        {//如果满足条件，就添加到显示
                            this.addOneMilitia(militia);
                        }
                    }
                    continue;
                }
                //在之前让原分组界面的个数减少1
                FormBizs.groupBiz.reduceCount(militia);
                ListViewItem lvi = this.findItemWithMilitia(militia);
                militia.Group = "未分组";
                FormBizs.updateMilitiaItem(militia);//通知所有的民兵列表更新
                sqlBiz.updateMilitia(militia);//保存
                if (lvi != null)
                {
                    lvi.Tag = militia;
                    this.updateItem(lvi);
                    if (!militia.isSatisfy(condition))
                    {//不满足筛选条件，则不能显示在这个界面
                        lvi.Remove();
                    }
                }
                else if (militia.isSatisfy(condition))
                {//满足筛选条件，就添加显示
                    this.addOneMilitia(militia);
                }
            }
            militia_ListView.EndUpdate();//结束更新界面
        }

        public void Militia_ListView_DragEnter(object sender, DragEventArgs e)
        {//移动进入界面时
            militia_ListView.Focus();
            e.Effect = DragDropEffects.Move;
        }

        public void Militia_ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {//移动选中的items

            if (e.Button == MouseButtons.Left)
            {//左键移动
                List<Militia> mList = new List<Militia>();
                foreach (ListViewItem lvi in militia_ListView.SelectedItems)
                {
                    Militia militia = (Militia)lvi.Tag;
                    mList.Add(militia);
                }
                MoveTag mt = new MoveTag(this, mList);
                if (militia_ListView.DoDragDrop(mt, DragDropEffects.Move) == DragDropEffects.Move)
                {//移动成功后
                    foreach (ListViewItem lvi in militia_ListView.SelectedItems)
                    {
                        Militia militia = (Militia)lvi.Tag;
                        //if militia 不符合筛选条件，则删掉这个item
                        if (!militia.isSatisfy(condition))
                        {
                            lvi.Remove();
                        }
                        else
                        {
                            this.updateItem(lvi);//如果符合条件，则刷新显示
                        }
                    }
                }
            }
        }

        public void Militia_ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {//双击编辑
            ListViewItem lvi = militia_ListView.GetItemAt(e.X, e.Y);
            int subIndex = lvi.SubItems.IndexOf(lvi.GetSubItemAt(e.X, e.Y));
            if (lvi != null)
            {
                this.editOne(lvi, this.displayedParameterIndexs[subIndex]);//弹出编辑窗口，并指定光标在subIndex处
            }
        }

        public void importXMLGroupTask_Click(object sender, EventArgs e)
        {//导入分组任务（添加任务）
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "xml文件(*.xml)|*.xml";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fileDialog.FileName;//已经选择了文件
                try
                {
                    FormBizs.groupBiz.addXmlGroupTask(file);
                }
                catch
                {
                    MessageBox.Show("导入xml文件出现异常！", "异常警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }
    }
}
