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
    public partial class ChildConditionForm_place : ChildConditionForm
    {//地区选择子条件界面
        private System.Xml.XmlNodeList provinces = PlaceXmlConfig.provinces();
        private System.Xml.XmlNodeList cities = null;//城市
        private System.Xml.XmlNodeList districts = null;//区县初始化为空

        private Condition.ChildCondition childCondition;
        private string PCD_ID;//地区id

        public ChildConditionForm_place(Condition.ChildCondition cc)
        {
            InitializeComponent();

            childCondition = cc;

            labelPropertyName.Text = cc.parameterNode.Attributes["name"].Value + ": ";//标签显示

            initialPlaceCombobox();//初始化采集地显示

            if(childCondition.Values.Count > 0)
            {//根据传入的子条件对象初始化对应的显示
                string placeName = PlaceXmlConfig.getPlaceName(childCondition.Values[0]);
                string[] placeNames = placeName.Split(new char[] { '/' });
                pCombobox.SelectedItem = placeNames[0];
                if (placeNames.Length >= 2)
                {
                    cCombobox.SelectedItem = placeNames[1];
                    if (placeNames.Length == 3)
                    {
                        dCombobox.SelectedItem = placeNames[2];
                    }
                }
            }
        }

        private void initialPlaceCombobox()
        {//初始化地区选项
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

        private void btn_ok_Click(object sender, EventArgs e)
        {//ok，给条件赋值
            PCD_ID = provinces[pCombobox.SelectedIndex].Attributes["ID"].Value;
            if (cCombobox.SelectedIndex > 0)
            {
                PCD_ID += "-" + cities[cCombobox.SelectedIndex - 1].Attributes["ID"].Value;
                if (dCombobox.SelectedIndex > 0)
                {
                    PCD_ID += "-" + districts[dCombobox.SelectedIndex - 1].Attributes["ID"].Value;
                }
            }
            childCondition.Values.Clear();
            childCondition.Values.Add(PCD_ID);//添加
            childCondition.Method = "StartsWith";//方法依然是StartsWith
        }
    }
}
