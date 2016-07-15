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
    public partial class ChildConditionForm : BasicForm
    {//子条件界面，其他子条件界面继承于它，主要是为了重写关闭的逻辑
        protected bool closeForm;

        public ChildConditionForm()
        {
            InitializeComponent();
            closeForm = true;//是否关闭
            FormClosing += ChildConditionForm_FormClosing;
        }

        private void ChildConditionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!closeForm)
            {//closeForm设置为false只起一次作用
                closeForm = true;
                e.Cancel = true;
            }
        }
    }
}
