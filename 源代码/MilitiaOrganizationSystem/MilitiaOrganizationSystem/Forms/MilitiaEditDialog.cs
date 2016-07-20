using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MilitiaOrganizationSystem
{
    public partial class MilitiaEditDialog : BasicForm
    {//编辑民兵界面
        private List<ComboBox> cList;//comboBox的list
        private List<int> parameterIndexs;//需编辑的参数下标
        private XmlNodeList parameters;
        private Militia militia;

        private bool closeForm = true;//是否关闭，在将要关闭时会起作用

        private ComboBox MilitaryProfessionNameCombobox = null;//地方专业的下一级：对口专业名称combobox

        private ComboBox RetirementProfessionSmallTypeCombobox = null;//服役专业小类，服役专业的下一级
        private ComboBox RetirementProfessionNameCombobox = null;//服役专业名称，服役专业小类的下一级

        public MilitiaEditDialog()
        {
            militia = null;
            cList = new List<ComboBox>();

            InitializeComponent();

            parameterIndexs = MilitiaXmlConfig.getEditParameterIndexs();//配置文件需编辑的参数

            parameters = MilitiaXmlConfig.parameters;
            
            int y = 10;//控件纵坐标

            foreach(int index in parameterIndexs)
            {
                XmlNode xmlNode = parameters[index];

                Label label = new Label();//标签
                label.Text = xmlNode.Attributes["name"].Value;
                label.Dock = DockStyle.Fill;
                label.Anchor = AnchorStyles.Top & AnchorStyles.Bottom;
                label.AutoSize = true;
                ComboBox comboBox = new ComboBox();
                comboBox.Dock = DockStyle.Fill;
                comboBox.Tag = xmlNode;
                string type = xmlNode.Attributes["type"].Value;
                switch(type)
                {
                    case "enum":

                        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;//一定先设为下拉形式

                        string propertyName = xmlNode.Attributes["property"].Value;
                        //处理层级关系
                        if (propertyName == "MilitaryProfessionTypeName")
                        {//地方专业
                            comboBox.SelectedIndexChanged += MilitaryProfessionTypeNameComboBox_SelectedIndexChanged;
                        } else if(propertyName == "MilitaryProfessionName")
                        {//地方专业的下一级：对口专业名称
                            MilitaryProfessionNameCombobox = comboBox;//赋值
                            comboBox.Items.Add(xmlNode.ChildNodes[0].Attributes["name"].Value);
                            comboBox.SelectedIndex = 0;
                            break;
                        } else if(propertyName == "RetirementProfessionType")
                        {//服役专业
                            comboBox.SelectedIndexChanged += RetirementProfessionTypeComboBox_SelectedIndexChanged;
                        } else if(propertyName == "RetirementProfessionSmallType")
                        {//服役专业小类,服役专业的下一级
                            RetirementProfessionSmallTypeCombobox = comboBox;//赋值
                            comboBox.Items.Add(xmlNode.ChildNodes[0].Attributes["name"].Value);
                            comboBox.SelectedIndex = 0;
                            RetirementProfessionSmallTypeCombobox.SelectedIndexChanged += RetirementProfessionSmallTypeCombobox_SelectedIndexChanged;
                            break;
                        } else if(propertyName == "RetirementProfessionName")
                        {//服役专业名称，服役专业小类的下一级
                            RetirementProfessionNameCombobox = comboBox;
                            comboBox.Items.Add(xmlNode.ChildNodes[0].Attributes["name"].Value);
                            comboBox.SelectedIndex = 0;
                            break;
                        }


                        for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                        {
                            XmlNode selectNode = xmlNode.ChildNodes[i];
                            comboBox.Items.Add(selectNode.Attributes["name"].Value);
                        }

                        comboBox.SelectedIndex = 0;
                        break;
                    case "place":
                        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                        comboBox.Items.Add("点击编辑");
                        comboBox.SelectedIndex = 0;
                        comboBox.MouseClick += ComboBox_MouseClick;
                        break;
                    default:
                        comboBox.Text = "";
                        break;
                }

                
                tlp.Controls.Add(label);
                tlp.Controls.Add(comboBox);

                cList.Add(comboBox);

                y += 50;//下一个属性
            }

            for(int i = 0; i < tlp.RowStyles.Count; i++)
            {
                tlp.RowStyles[i].Height = 30;
            }

            FormClosing += MilitiaEditDialog_FormClosing;
        }

        private void changeDropDownList(ComboBox parentCombobox, ComboBox currentCombobox)
        {//通过上一级的combobox选值，改变这一级的combobox的下拉显示
            XmlNode parentNode = (XmlNode)parentCombobox.Tag;
            XmlNode selectedNode = parentNode.SelectSingleNode("selection[@name='" + (string)parentCombobox.SelectedItem + "']");
            string parentValue = selectedNode.Attributes["value"].Value;

            currentCombobox.Items.Clear();

            XmlNode currentNode = (XmlNode)currentCombobox.Tag;
            XmlNodeList xnl = currentNode.SelectNodes("selection[starts-with(@value, '" + parentValue + "')]");
            foreach (XmlNode xn in xnl)
            {
                currentCombobox.Items.Add(xn.Attributes["name"].Value);
            }
            currentCombobox.SelectedIndex = 0;
        }

        private void RetirementProfessionSmallTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {//服役专业小类发生改变时,改变服役专业名称下拉显示
            if (RetirementProfessionNameCombobox != null)
            {
                ComboBox parentCombobox = (ComboBox)sender;

                ComboBox currentCombobox = RetirementProfessionNameCombobox;

                changeDropDownList(parentCombobox, currentCombobox);
            }
        }

        private void RetirementProfessionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {//服役专业改变时,改变服役专业小类的下拉显示
            if(RetirementProfessionSmallTypeCombobox != null)
            {
                ComboBox parentCombobox = (ComboBox)sender;

                ComboBox currentCombobox = RetirementProfessionSmallTypeCombobox;

                changeDropDownList(parentCombobox, currentCombobox);
            }
        }

        private void MilitaryProfessionTypeNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {//选择地方专业改变时,改变对口专业名称的下拉显示
            if(MilitaryProfessionNameCombobox != null)
            {
                ComboBox parentCombobox = (ComboBox)sender;

                ComboBox currentCombobox = MilitaryProfessionNameCombobox;

                changeDropDownList(parentCombobox, currentCombobox);
            }
        }

        private void MilitiaEditDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!closeForm)
            {
                closeForm = true;
                e.Cancel = true;
            }
        }

        private void ComboBox_MouseClick(object sender, MouseEventArgs e)
        {//编辑地区属性
            ComboBox cb = (ComboBox)sender;
            PlaceChooseForm pcf = new PlaceChooseForm(((XmlNode)cb.Tag).Attributes["name"].Value, cb.Text);
            if(pcf.ShowDialog() == DialogResult.OK)
            {
                cb.Items[0] = PlaceXmlConfig.getPlaceName(pcf.PCD_ID);
            }
        }

        public DialogResult showEditDlg(Militia oneMilitia, int focusIndex = 0)
        {//show编辑框
            closeForm = true;//先可以直接关闭

            militia = oneMilitia;
            MilitiaReflection mr = new MilitiaReflection(militia);//反射

            for(int k = 0; k < cList.Count; k++)
            {
                ComboBox comboBox = cList[k];
                XmlNode xmlNode = parameters[parameterIndexs[k]];//第k个要编辑的属性

                string strValue = "";
                try
                {
                    strValue = mr.getProperty(xmlNode.Attributes["property"].Value).ToString();
                }
                catch
                {

                }

                switch(xmlNode.Attributes["type"].Value)
                {
                    case "enum":
                        comboBox.SelectedIndex = 0;

                        XmlNode selectChildNode = xmlNode.SelectSingleNode("selection[@value='" + strValue + "']");

                        try
                        {
                            comboBox.SelectedItem = selectChildNode.Attributes["name"].Value;
                        } catch
                        {
                            comboBox.SelectedIndex = 0;
                        }

                        break;
                    case "place":
                        comboBox.Items[0] = PlaceXmlConfig.getPlaceName(strValue);
                        break;
                    default:
                        comboBox.Text = strValue;
                        break;
                }


            }

            //cList[focusIndex].;//设置光标首先出现的位置,默认是0
            if(focusIndex >= 0 && focusIndex < cList.Count)
            {
                cList[focusIndex].Select();
            }
            

            return ShowDialog();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {//OK了，给传进的militia赋值
            if(militia == null)
            {
                MessageBox.Show("MilitiaEditDialog类使用错误！");
                return;
            }
            XmlNode crediNumberNode = MilitiaXmlConfig.getNodeByProperty("CredentialNumber");
            ComboBox crediCombobox = cList.Where(x => (XmlNode)x.Tag == crediNumberNode).First();
            //取到身份证号的输入框
            Regex regex = new Regex("^[0-9]{17}[0-9X]$");
            if (!regex.IsMatch(crediCombobox.Text))
            {//检查身份证号的格式是否正确，只要不引起数据库冲突检测异常即可（字典树）
                MessageBox.Show("身份证号输入有误，请检查！\n（身份证号长度必须为18位并且只能由数字和X组成\n且X只能在最后一位！）");
                crediCombobox.Focus();
                closeForm = false;
                return;
            }
            //身份证号格式正确，判断身份证号是否改变
            if(militia.Id != null && crediCombobox.Text != militia.CredentialNumber)
            {//民兵不是新建的，且身份证号发生了改变，则调用编辑身份证号的函数
                FormBizs.sqlBiz.cnDao.editCrediNumber(militia.CredentialNumber, crediCombobox.Text, militia.Place);
                //编辑并保存
            }

            MilitiaReflection mr = new MilitiaReflection(militia);//反射

            foreach(ComboBox cbb in cList)
            {
                XmlNode xmlNode = (XmlNode)cbb.Tag;
                string value = "";
                switch(xmlNode.Attributes["type"].Value)
                {
                    case "enum":
                        value = xmlNode.SelectSingleNode("selection[@name='" + (string)cbb.SelectedItem + "']")
                            .Attributes["value"].Value;
                        break;
                    case "place":
                        value = PlaceXmlConfig.getPCD_ID(cbb.Items[0].ToString());
                        break;
                    default:
                        value = cbb.Text;
                        break;
                }
                mr.setProperty(xmlNode.Attributes["property"].Value, value);
            }
        }
    }
}
