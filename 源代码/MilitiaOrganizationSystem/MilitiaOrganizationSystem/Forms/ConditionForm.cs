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
    public partial class ConditionForm : Form
    {
        private System.Xml.XmlNodeList parameters = MilitiaXmlConfig.parameters;

        private System.Xml.XmlNodeList provinces = PlaceXmlConfig.provinces();
        private System.Xml.XmlNodeList cities = null;//城市
        private System.Xml.XmlNodeList districts = null;//区县初始化为空

        public Condition condition { get; set; }//条件

        public ConditionForm(Condition currentCondition)
        {
            InitializeComponent();

            initialPlaceCombobox();//初始化采集地显示

            //初始化属性显示
            for(int i = 0; i < parameters.Count; i++)
            {//添加属性名
                System.Xml.XmlNode xn = parameters[i];
                parasCheckBox.Items.Add(xn.Attributes["name"].Value);
                foreach(Condition.ChildCondition cc in currentCondition.ccList)
                {
                    if(cc.parameterNode == xn)
                    {
                        parasCheckBox.SetItemChecked(i, true);
                        break;
                    }
                }
            }

            condition = currentCondition;//直接引用

            string placeName = PlaceXmlConfig.getPlaceName(condition.place);
            string[] placeNames = placeName.Split(new char[] { '/' });
            pCombobox.SelectedItem = placeNames[0];
            if(placeNames.Length >= 2)
            {
                cCombobox.SelectedItem = placeNames[1];
                if(placeNames.Length == 3)
                {
                    dCombobox.SelectedItem = placeNames[2];
                }
            }

            //显示到listbox
            foreach (Condition.ChildCondition cc in condition.ccList)
            {
                conditionListBox.Items.Add(cc);
            }

            parasCheckBox.MouseClick += ParasCheckBox_MouseClick;
            parasCheckBox.MouseDoubleClick += ParasCheckBox_MouseDoubleClick;

            conditionListBox.MouseDoubleClick += ConditionListBox_MouseDoubleClick;
        }

        private void initialPlaceCombobox()
        {
            foreach (System.Xml.XmlNode p in provinces)
            {
                pCombobox.Items.Add(p.Attributes["ProvinceName"].Value);
            }

            pCombobox.SelectedIndex = 9;//江苏省  

            cities = PlaceXmlConfig.cities("10");//江苏省的城市
            cCombobox.Items.Add("不限");
            foreach (System.Xml.XmlNode city in cities)
            {
                cCombobox.Items.Add(city.Attributes["CityName"].Value);
            }
            cCombobox.SelectedIndex = 0;

            dCombobox.Items.Add("不限");
            dCombobox.SelectedIndex = 0;

            pCombobox.SelectedIndexChanged += PCombobox_SelectedIndexChanged;
            cCombobox.SelectedIndexChanged += CCombobox_SelectedIndexChanged;
        }

        private void CCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {//选择市变化时,更新区县下拉项
            dCombobox.Items.Clear();
            dCombobox.Items.Add("不限");
            if(cCombobox.SelectedIndex == 0)
            {
                districts = null;
            } else
            {
                districts = PlaceXmlConfig.districts(cities[cCombobox.SelectedIndex - 1].Attributes["ID"].Value);
                foreach(System.Xml.XmlNode d in districts)
                {
                    dCombobox.Items.Add(d.Attributes["DistrictName"].Value);
                }
            }
            dCombobox.SelectedIndex = 0;
        }

        private void PCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {//选择省变化时,更新市下拉项
            cCombobox.Items.Clear();
            cCombobox.Items.Add("不限");
            cities = PlaceXmlConfig.cities(provinces[pCombobox.SelectedIndex].Attributes["ID"].Value);//城市
            foreach (System.Xml.XmlNode city in cities)
            {
                cCombobox.Items.Add(city.Attributes["CityName"].Value);
            }
            cCombobox.SelectedIndex = 0;
        }

        private DialogResult showChildConditionDialog(Condition.ChildCondition cc)
        {
            System.Xml.XmlNode paraNode = cc.parameterNode;
            Form ccForm = null;
            switch(paraNode.Attributes["type"].Value)
            {
                case "enum":
                    ccForm = new ChildConditionForm_enum(cc);
                    break;
                case "string":
                    ccForm = new ChildConditionForm_string(cc);
                    break;
                case "group":
                    ccForm = new ChildConditionForm_group(cc);
                    break;
                case "place":
                    ccForm = new ChildConditionForm_place(cc);
                    break;
                default:
                    return DialogResult.Cancel;//不做任何事
            }
            return ccForm.ShowDialog();
        }

        private void ConditionListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {//弹出条件编辑
            Condition.ChildCondition cc = (Condition.ChildCondition)conditionListBox.SelectedItem;
            if(cc == null)
            {//为空则什么都不做
                return;
            }
            if(showChildConditionDialog(cc) == DialogResult.OK)
            {
                conditionListBox.Items[conditionListBox.SelectedIndex] = cc;//刷新了显示数据
            }
        }

        private void ParasCheckBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {//双击选中，并打开一个ChildCondition对话框
            string name = (string)parasCheckBox.SelectedItem;
            Condition.ChildCondition cc = new Condition.ChildCondition(MilitiaXmlConfig.getNodeByName(name));
            if (showChildConditionDialog(cc) == DialogResult.OK)
            {
                parasCheckBox.SetItemChecked(parasCheckBox.SelectedIndex, true);

                conditionListBox.Items.Add(cc);
            }
            else
            {
                if(parasCheckBox.GetItemChecked(parasCheckBox.SelectedIndex))
                {
                    parasCheckBox.SetItemChecked(parasCheckBox.SelectedIndex, false);
                } else
                {
                    parasCheckBox.SetItemChecked(parasCheckBox.SelectedIndex, true);
                }
                
            }
        }

        private void ParasCheckBox_MouseClick(object sender, MouseEventArgs e)
        {//阻止默认的单击选中
            if(parasCheckBox.GetItemChecked(parasCheckBox.SelectedIndex))
            {
                parasCheckBox.SetItemChecked(parasCheckBox.SelectedIndex, false);
            } else
            {
                parasCheckBox.SetItemChecked(parasCheckBox.SelectedIndex, true);
            }
            
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {//ok了，生成条件
            condition.ccList = conditionListBox.Items.Cast<Condition.ChildCondition>().ToList();
            condition.place = provinces[pCombobox.SelectedIndex].Attributes["ID"].Value;
            if(cCombobox.SelectedIndex > 0)
            {
                condition.place += "-" + cities[cCombobox.SelectedIndex - 1].Attributes["ID"].Value;
                if(dCombobox.SelectedIndex > 0)
                {
                    condition.place += "-" + districts[dCombobox.SelectedIndex - 1].Attributes["ID"].Value;
                }
            }
            condition.generateLambdaCondition();
        }

        private void deleteCondition_Click(object sender, EventArgs e)
        {//删除listBox里的条件
            conditionListBox.Items.Remove(conditionListBox.SelectedItem);
        }
    }
}
