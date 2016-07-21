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
    public partial class PlaceChooseForm : BasicForm
    {//选择地区界面,编辑民兵时应该可以使用
        private System.Xml.XmlNodeList provinces = PlaceXmlConfig.provinces();
        private System.Xml.XmlNodeList cities = null;//城市
        private System.Xml.XmlNodeList districts = null;//区县初始化为空

        public string PCD_ID { get; set; }

        private bool closeForm;//判断是否关闭

        public PlaceChooseForm(string propertyName, string PCDID)
        {
            InitializeComponent();

            closeForm = true;

            labelPropertyName.Text = propertyName;

            PCD_ID = PCDID;//地区ID

            initialPlaceCombobox();

            string placeName = PlaceXmlConfig.getPlaceName(PCD_ID);
            string[] placeNames = placeName.Split(new char[] { '/' });
            pCombobox.SelectedItem = placeNames[0];
            if (placeNames.Length >= 2)
            {
                cCombobox.SelectedItem = placeNames[1];
                if (placeNames.Length >= 3)
                {
                    dCombobox.SelectedItem = placeNames[2];
                }
                if(placeNames.Length == 4)
                {
                    place_textBox.Text = placeNames[3];
                }
            }

            FormClosing += PlaceChooseForm_FormClosing;
        }

        private void PlaceChooseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!closeForm)
            {
                closeForm = true;
                e.Cancel = true;
            }
        }

        private void initialPlaceCombobox()
        {
            foreach (System.Xml.XmlNode p in provinces)
            {
                pCombobox.Items.Add(p.Attributes["ProvinceName"].Value);
            }

            pCombobox.SelectedIndex = 9;//江苏省  

            cities = PlaceXmlConfig.cities("10");//江苏省的城市
            cCombobox.Items.Add("未指定");
            foreach (System.Xml.XmlNode city in cities)
            {
                cCombobox.Items.Add(city.Attributes["CityName"].Value);
            }
            cCombobox.SelectedIndex = 0;

            dCombobox.Items.Add("未指定");
            dCombobox.SelectedIndex = 0;

            pCombobox.SelectedIndexChanged += PCombobox_SelectedIndexChanged;
            cCombobox.SelectedIndexChanged += CCombobox_SelectedIndexChanged;
        }

        private void CCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {//选择市变化时,更新区县下拉项
            dCombobox.Items.Clear();
            dCombobox.Items.Add("未指定");
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
            cCombobox.Items.Add("未指定");
            cities = PlaceXmlConfig.cities(provinces[pCombobox.SelectedIndex].Attributes["ID"].Value);//城市
            foreach (System.Xml.XmlNode city in cities)
            {
                cCombobox.Items.Add(city.Attributes["CityName"].Value);
            }
            cCombobox.SelectedIndex = 0;
        }
     
        private void btn_ok_Click(object sender, EventArgs e)
        {//ok了，生成PCDID
            PCD_ID = provinces[pCombobox.SelectedIndex].Attributes["ID"].Value;
            if(cCombobox.SelectedIndex <= 0)
            {
                closeForm = false;
                MessageBox.Show("请选择市");
                return;
            }
            if (dCombobox.SelectedIndex <= 0)
            {
                closeForm = false;
                MessageBox.Show("请选择区县");
                return;
            }
            if (place_textBox.Text.Trim() == "")
            {
                closeForm = false;
                MessageBox.Show("请填写具体地址！");
                return;
            }
            PCD_ID += "-" + cities[cCombobox.SelectedIndex - 1].Attributes["ID"].Value;

            PCD_ID += "-" + districts[dCombobox.SelectedIndex - 1].Attributes["ID"].Value;

            PCD_ID += "|" + place_textBox.Text;
        }
    }
}
