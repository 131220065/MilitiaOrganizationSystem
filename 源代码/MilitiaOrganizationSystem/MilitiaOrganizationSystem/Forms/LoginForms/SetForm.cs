using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Xml;
using System.Runtime.Serialization;
using System.IO;
using System.Net;

namespace MilitiaOrganizationSystem
{
    public partial class SetForm : BasicForm
    {
        private bool closeForm;

        private System.Xml.XmlNodeList provinces = PlaceXmlConfig.provinces();
        private System.Xml.XmlNodeList cities = null;//城市
        private System.Xml.XmlNodeList districts = null;//区县初始化为空


        public SetForm()
        {
            InitializeComponent();

            closeForm = true;

            clientTypeCombobox.Items.Add("省军分区");
            clientTypeCombobox.Items.Add("市军分区");
            clientTypeCombobox.Items.Add("区县人武部");
            clientTypeCombobox.Items.Add("基层");


            foreach (System.Xml.XmlNode p in provinces)
            {
                comboBox_sheng.Items.Add(p.Attributes["ProvinceName"].Value);
            }

            comboBox_sheng.SelectedIndex = 9;//江苏省

            cities = PlaceXmlConfig.cities("10");//江苏省的城市
            foreach (System.Xml.XmlNode city in cities)
            {
                comboBox_shi.Items.Add(city.Attributes["CityName"].Value);
            }


            comboBox_sheng.SelectedIndexChanged += comboBox_sheng_SelectedIndexChanged;
            comboBox_shi.SelectedIndexChanged += comboBox_shi_SelectedIndexChanged;
            FormClosing += SetForm_FormClosing;
        }

        private void SetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!closeForm)
            {
                closeForm = true;
                e.Cancel = true;
            }
        }

        private void comboBox_sheng_SelectedIndexChanged(object sender, EventArgs e)
        {//选择省变化时，更新市下拉项
            comboBox_shi.Items.Clear();
            comboBox_xian.Items.Clear();
            cities = PlaceXmlConfig.cities(provinces[comboBox_sheng.SelectedIndex].Attributes["ID"].Value);//城市
            foreach (System.Xml.XmlNode city in cities)
            {
                comboBox_shi.Items.Add(city.Attributes["CityName"].Value);
            }
        }

        private void comboBox_shi_SelectedIndexChanged(object sender, EventArgs e)
        {//选择市变化，更新区县下拉项
            comboBox_xian.Items.Clear();
            districts = PlaceXmlConfig.districts(cities[comboBox_shi.SelectedIndex].Attributes["ID"].Value);
            foreach (System.Xml.XmlNode d in districts)
            {
                comboBox_xian.Items.Add(d.Attributes["DistrictName"].Value);
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            string clientType = "";
            string place = "";
            string psd = "";
            if(clientTypeCombobox.SelectedIndex < 0)
            {
                MessageBox.Show("请选择客户端身份！");
                closeForm = false;
                return;
            }
            clientType = clientTypeCombobox.Items[clientTypeCombobox.SelectedIndex].ToString();
            switch (clientType)
            {
                case "省军分区":
                    place = provinces[comboBox_sheng.SelectedIndex].Attributes["ID"].Value;
                    break;
                case "市军分区":
                    if (comboBox_shi.SelectedIndex < 0)
                    {
                        MessageBox.Show("请选择市");
                        comboBox_shi.Focus();
                        closeForm = false;
                        return;
                    }
                    place = provinces[comboBox_sheng.SelectedIndex].Attributes["ID"].Value;
                    place += "-" + cities[comboBox_shi.SelectedIndex].Attributes["ID"].Value;
                    break;
                case "区县人武部":

                case "基层":
                    if (comboBox_shi.SelectedIndex < 0)
                    {
                        MessageBox.Show("请选择市");
                        comboBox_shi.Focus();
                        closeForm = false;
                        return;
                    }
                    if (comboBox_xian.SelectedIndex < 0)
                    {
                        MessageBox.Show("请选择区县");
                        comboBox_xian.Focus();
                        closeForm = false;
                        return;
                    }
                    place = provinces[comboBox_sheng.SelectedIndex].Attributes["ID"].Value;
                    place += "-" + cities[comboBox_shi.SelectedIndex].Attributes["ID"].Value;
                    place += "-" + districts[comboBox_xian.SelectedIndex].Attributes["ID"].Value;
                    break;
            }
            if(LoginXmlConfig.loginSuccess(initialPsdBox.Text))
            {
                if(psdCombobox.Text.Trim() == "")
                {
                    MessageBox.Show("密码不能为空！");
                    closeForm = false;
                }
                else if(psdCombobox.Text == psd2Combobox.Text)
                {//成功
                    closeForm = true;
                    psd = psdCombobox.Text;
                    LoginXmlConfig.setPsd(clientType, place, psd);
                } else
                {
                    MessageBox.Show("两次输入的密码不一致！请重新确认！");
                    closeForm = false;
                }
            } else
            {
                MessageBox.Show("初始密码不正确！");
                closeForm = false;
            }
        }
    }
}
