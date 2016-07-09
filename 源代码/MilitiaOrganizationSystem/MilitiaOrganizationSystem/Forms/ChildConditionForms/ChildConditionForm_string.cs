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
    public partial class ChildConditionForm_string : ChildConditionForm
    {
        private Condition.ChildCondition childCondition;

        private System.Xml.XmlNode paraNode;
        

        public ChildConditionForm_string(Condition.ChildCondition cc)
        {
            InitializeComponent();

            closeForm = true;

            childCondition = cc;

            paraNode = cc.parameterNode;

            propertyName.Text = paraNode.Attributes["name"].Value;

            if(cc.Method == "StartsWith")
            {
                radio_StartsWith.Checked = true;
                startwithCombobox.Text = cc.Values[0];
            } else if(cc.Method == "EndsWith")
            {
                radio_EndsWith.Checked = true;
                endswithCombobox.Text = cc.Values[0];
            } else if(cc.Method == "Equal")
            {
                radio_Equal.Checked = true;
                equalCombobox.Text = cc.Values[0];
            }
            
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if(radio_StartsWith.Checked)
            {
                if(startwithCombobox.Text == null || startwithCombobox.Text == "")
                {
                    MessageBox.Show("请输入数据！");
                    startwithCombobox.Focus();
                    closeForm = false;
                    return;
                }
                childCondition.Method = "StartsWith";
                childCondition.Values.Clear();
                childCondition.Values.Add(startwithCombobox.Text);
            } else if(radio_EndsWith.Checked)
            {
                if (endswithCombobox.Text == null || endswithCombobox.Text == "")
                {
                    MessageBox.Show("请输入数据！");
                    endswithCombobox.Focus();
                    closeForm = false;
                    return;
                }
                childCondition.Method = "EndsWith";
                childCondition.Values.Clear();
                childCondition.Values.Add(endswithCombobox.Text);
            } else if(radio_Equal.Checked)
            {
                childCondition.Method = "Equal";
                childCondition.Values.Clear();
                childCondition.Values.Add(equalCombobox.Text);
            }
        }
    }
}
