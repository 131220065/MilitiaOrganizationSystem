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
    public partial class ChildConditionForm_group : ChildConditionForm
    {
        private Condition.ChildCondition childCondition;

        public ChildConditionForm_group(Condition.ChildCondition cc)
        {//分组选择界面
            InitializeComponent();

            childCondition = cc;

            TreeNode tn = groupTreeView.Nodes.Add("未分组");
            tn.Name = "未分组";
            GroupXmlConfig.loadToTreeViewSimplely(groupTreeView);
            groupTreeView.ExpandAll();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {//ok，给条件赋值
            childCondition.Values.Clear();
            TreeNode selectNode = groupTreeView.SelectedNode;
            if(selectNode.Name == "未分组")
            {
                childCondition.Values.Add("未分组");
            } else
            {
                System.Xml.XmlNode xmlNode = ((GroupTag)selectNode.Tag).tagXmlNode;
                childCondition.Values.Add(GroupXmlConfig.getNodePath(xmlNode));
            }
            childCondition.Method = "StartsWith";
        }
    }

}
