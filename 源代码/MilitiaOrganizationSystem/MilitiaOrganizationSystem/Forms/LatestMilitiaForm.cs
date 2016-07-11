using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MilitiaOrganizationSystem
{
    public partial class LatestMilitiaForm : Form
    {
        private XmlNodeList parameters = MilitiaXmlConfig.parameters;
        private List<int> displayedParameterIndexs = MilitiaXmlConfig.getAllDisplayedParameterIndexs();

        public LatestMilitiaForm()
        {
            InitializeComponent();

            addColumnHeader();

            FormClosing += LatestMilitiaForm_FormClosing;
            latestMilitias_listview.ItemDrag += LatestMilitias_listview_ItemDrag;
        }

        private void LatestMilitias_listview_ItemDrag(object sender, ItemDragEventArgs e)
        {//移动,可以分组，可以从删除中恢复
            if (e.Button == MouseButtons.Left)
            {
                List<Militia> mList = new List<Militia>();
                foreach (ListViewItem lvi in latestMilitias_listview.SelectedItems)
                {
                    Militia militia = (Militia)lvi.Tag;
                    mList.Add(militia);
                }

                MoveTag mt = new MoveTag(this, mList);

                if (DoDragDrop(mt, DragDropEffects.Move) == DragDropEffects.Move)
                {//移动成功后
                    foreach (ListViewItem lvi in latestMilitias_listview.SelectedItems)
                    {
                        if((lvi.Name == null || lvi.Name == "") && ((Militia)(lvi.Tag)).Id != null)
                        {//如果原来是null，后来变为了不是null，说明是恢复了.应该删除此项
                            lvi.Remove();
                        }
                    }
                }
            }
        }

        private void LatestMilitiaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void newOperationOn(Militia militia, string operation)
        {
            ListViewItem lvi = findItemWithMilitia(militia);
            if(lvi == null)
            {//在最前面新添加
                lvi = new ListViewItem();
                lvi.Tag = militia;
            } else
            {
                lvi.Remove();
            }
            if(operation == "被删除")
            {//如果是被删除,那么Id赋值为null，Name在updateItem时也会赋值为null
                militia.Id = null;
                lvi.Tag = militia;
            }
            lvi.ToolTipText = operation;
            updateItem(lvi);
            latestMilitias_listview.Items.Insert(0, lvi);
        }

        private void addColumnHeader()
        {//添加表头
            latestMilitias_listview.Columns.Clear();//先清除
            foreach (int index in displayedParameterIndexs)
            {//根据MilitiaXmlConfig类的配置文件添加表头
                XmlNode parameter = parameters[index];
                ColumnHeader ch = new ColumnHeader();
                ch.Text = parameter.Attributes["name"].Value;   //设置列标题 
                ch.Width = 120;    //设置列宽度 
                ch.TextAlign = HorizontalAlignment.Left;   //设置列的对齐方式 
                latestMilitias_listview.Columns.Add(ch);    //将列头添加到ListView控件。 
            }
        }

        private ListViewItem findItemWithMilitia(Militia militia)
        {//根据民兵对象，查找此界面的民兵
            if(militia.Id == null)
            {//是删除的
                return null;
            }
            ListViewItem[] lvis = latestMilitias_listview.Items.Find(militia.Id, false);
            foreach (ListViewItem lvi in lvis)
            {
                if (((Militia)lvi.Tag).Place == militia.Place)
                {//Place即为数据库，如果相等，说明是要找的
                    return lvi;
                }
            }
            return null;//没找到
        }


        private void updateItem(ListViewItem lvi)
        {//用tag更新显示
            Militia militia = (Militia)lvi.Tag;
            MilitiaReflection mr = new MilitiaReflection(militia);//反射
            lvi.ImageIndex = 0;//图片

            lvi.SubItems.Clear();//会删去lvi.Name!!!!

            string[] items = new string[displayedParameterIndexs.Count - 1];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = "";
            }
            lvi.SubItems.AddRange(items);

            for (int i = 0; i < displayedParameterIndexs.Count; i++)
            {//更新其他属性,第一个属性显示在主属性上了
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
                    case "place":
                        value = PlaceXmlConfig.getPlaceName(value);
                        break;
                    default:
                        break;
                }

                lvi.SubItems[i].Text = value;
            }


            lvi.Name = militia.Id;//添加查询的Key
        }
    }
}
