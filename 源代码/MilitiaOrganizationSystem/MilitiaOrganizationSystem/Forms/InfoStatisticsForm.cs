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
    public partial class InfoStatisticsForm : Form
    {//统计界面
        private System.Xml.XmlNodeList parameters = MilitiaXmlConfig.parameters;
        private XmlNode statisticsParameter = null;

        private Condition condition;//统计界面的筛选条件

        public InfoStatisticsForm(Condition c)
        {
            InitializeComponent();

            //初始化属性显示
            for (int i = 0; i < parameters.Count; i++)
            {//添加属性名
                System.Xml.XmlNode xn = parameters[i];
                propertyCombobox.Items.Add(xn.Attributes["name"].Value);
            }

            condition = new Condition(c);//复制传过来的条件
            conditionLabel.Text = condition.ToString();

            propertyCombobox.SelectedIndexChanged += PropertyCombobox_SelectedIndexChanged;
        }

        private void PropertyCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {//选择了某个属性时
            catagoriesListBox.Items.Clear();
            categoryNum.Text = "0个";
            statisticsParameter = parameters[propertyCombobox.SelectedIndex];
            switch(statisticsParameter.Attributes["type"].Value)
            {
                case "enum":
                    foreach(XmlNode xn in statisticsParameter.ChildNodes)
                    {
                        catagoriesListBox.Items.Add(xn.Attributes["name"].Value);
                    }
                    categoryNum.Text = statisticsParameter.ChildNodes.Count + "个";
                    break;
                default:
                    catagoriesListBox.Items.Add("无分类");
                    break;
            }
        }

        private void btn_statistics_Click(object sender, EventArgs e)
        {//统计
            
            if(statisticsParameter == null)
            {
                return;
            }

            statisticsListBox.Items.Clear();
            int sum = 0;//总数
            Dictionary<string, Raven.Abstractions.Data.FacetValue> fdict;//分组
            switch (statisticsParameter.Attributes["type"].Value)
            {
                case "place":
                    fdict
                        = FormBizs.sqlBiz.getEnumStatistics(
                            condition.lambdaCondition,
                            statisticsParameter.Attributes["property"].Value,
                            condition.place);
                    foreach (KeyValuePair<string, Raven.Abstractions.Data.FacetValue> kvp in fdict)
                    {
                        int num = kvp.Value.Hits;
                        statisticsListBox.Items.Add(PlaceXmlConfig.getPlaceName(kvp.Key) + ": " + num + "人");
                        sum += num;
                    }
                    sumLabel.Text = sum + "人";
                    break;
                case "enum":
                    fdict
                        = FormBizs.sqlBiz.getEnumStatistics(
                            condition.lambdaCondition, 
                            statisticsParameter.Attributes["property"].Value, 
                            condition.place);
                    /*foreach(KeyValuePair<string, Raven.Abstractions.Data.FacetValue> kvp in fdict)
                    {
                        int num = kvp.Value.Hits;
                        statisticsListBox.Items.Add(kvp.Key + ": " + num + "人");
                        sum += 1;
                    }*/
                    foreach (XmlNode xn in statisticsParameter.ChildNodes)
                    {
                        int num = 0;
                        Raven.Abstractions.Data.FacetValue fv = null;
                        if(fdict.TryGetValue(xn.Attributes["value"].Value, out fv))
                        {//数量
                            num = fv.Hits;
                        }
                        statisticsListBox.Items.Add(xn.Attributes["name"].Value + ": " + num + "人");
                        sum += num;
                    }
                    sumLabel.Text = sum + "人";
                    break;
                case "group":
                    fdict
                        = FormBizs.sqlBiz.getEnumStatistics(
                            condition.lambdaCondition,
                            statisticsParameter.Attributes["property"].Value,
                            condition.place);
                    foreach (KeyValuePair<string, Raven.Abstractions.Data.FacetValue> kvp in fdict)
                    {
                        int num = kvp.Value.Hits;
                        statisticsListBox.Items.Add(kvp.Key + ": " + num + "人");
                        sum += num;
                    }
                    sumLabel.Text = sum + "人";
                    break;
                default:
                    //FormBizs.sqlBiz.queryByContition(condition.lambdaCondition, 0, 1, out sum);
                    sumLabel.Text = sum + "人";
                    statisticsListBox.Items.Add("总数: " + sum + "人");
                    break;
            }
        }

        private void conditionLabel_Click(object sender, EventArgs e)
        {//能变换条件，因为condition是复制过来的，所以不会影响宿主
            ConditionForm cf = new ConditionForm(condition);
            if (cf.ShowDialog() == DialogResult.OK)
            {
                conditionLabel.Text = condition.ToString();
            }
        }
    }
}
