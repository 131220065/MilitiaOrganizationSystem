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
    public partial class ConflictMilitiasForm : Form
    {
        private int count;
        private List<List<Militia>> mlList;
        private XmlNodeList parameters = MilitiaXmlConfig.parameters;
        private List<int> displayedParameterIndexs = MilitiaXmlConfig.getAllDisplayedParameterIndexs();

        private bool closeForm;

        private void addColumnHeader()
        {//添加表头
            conflictGroup_ListView.Columns.Clear();//先清除
            foreach (int index in displayedParameterIndexs)
            {//根据MilitiaXmlConfig类的配置文件添加表头
                XmlNode parameter = parameters[index];
                ColumnHeader ch = new ColumnHeader();
                ch.Text = parameter.Attributes["name"].Value;   //设置列标题 
                ch.Width = 120;    //设置列宽度 
                ch.TextAlign = HorizontalAlignment.Left;   //设置列的对齐方式 
                conflictGroup_ListView.Columns.Add(ch);    //将列头添加到ListView控件。 
            }
        }

        private void loadMilitiaList(List<List<Militia>> llm)
        {
            mlList = llm;

            count += mlList.Count;//count记录总数

            foreach (List<Militia> mList in mlList)
            {
                ListViewGroup lvg = conflictGroup_ListView.Groups.Add(mList[0].CredentialNumber, mList[0].CredentialNumber);
                foreach (Militia m in mList)
                {
                    ListViewItem lvi = new ListViewItem(lvg);
                    lvi.Tag = m;
                    lvi.Group = lvg;
                    updateItem(lvi);
                    conflictGroup_ListView.Items.Add(lvi);
                }
            }
        }

        public ConflictMilitiasForm(List<List<Militia>> llm)
        {//冲突检测界面
            InitializeComponent();

            closeForm = true;

            count = 0;//最开始时是0

            addColumnHeader();

            loadMilitiaList(llm);//加载

            FormClosing += ConflictMilitiasForm_FormClosing;//关闭窗口
            conflictGroup_ListView.ItemCheck += ConflictGroup_ListView_ItemCheck;//选择
        }

        private void ConflictGroup_ListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListViewItem thisLvi = conflictGroup_ListView.Items[e.Index];
            if (!thisLvi.Checked)
            {
                foreach(ListViewItem lvi in thisLvi.Group.Items)
                {
                    if(lvi.Checked)
                    {
                        lvi.Checked = false;
                    }
                }
            }
        }

        private void ConflictMilitiasForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!closeForm)
            {
                e.Cancel = true;
                closeForm = true;
            }
        }

        private void updateItem(ListViewItem lvi)
        {//用tag更新显示
            Militia militia = (Militia)lvi.Tag;
            MilitiaReflection mr = new MilitiaReflection(militia);//反射
            lvi.ImageIndex = 0;//图片

            lvi.SubItems.Clear();
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
                catch (Exception e)
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

            lvi.Name = militia.Id;//设置查询的key
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("共有" + mlList.Count + "个冲突, 您处理了" + conflictGroup_ListView.CheckedIndices.Count + "项.\n"
                + "将删除未选中的民兵，确认？", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (ListViewItem lvi in conflictGroup_ListView.Items)
                {
                    if (!lvi.Checked)
                    {
                        //FormBizs.removeMilitiaItem((Militia)lvi.Tag);
                        FormBizs.sqlBiz.deleteMilitia((Militia)lvi.Tag);
                    }
                }

                MessageBox.Show("执行成功");
                closeForm = true;//与下面冲突
                /*conflictGroup_ListView.Items.Clear();
                int sum;
                List<List<Militia>> llm = FormBizs.sqlBiz.getConflictMilitias(count, 30, out sum);
                if(llm.Count == 0)
                {
                    MessageBox.Show("已处理完所有冲突");
                    closeForm = true;
                } else
                {
                    closeForm = false;//不关闭
                    loadMilitiaList(llm);
                }*/
            } else
            {
                closeForm = false;
            }
        }
    }
}
