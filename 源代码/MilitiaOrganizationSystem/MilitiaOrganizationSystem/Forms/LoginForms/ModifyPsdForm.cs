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
    public partial class ModifyPsdForm : BasicForm
    {
        private bool closeForm;
        
        public ModifyPsdForm()
        {
            InitializeComponent();

            closeForm = true;
            
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

        private void btn_ok_Click(object sender, EventArgs e)
        {
            string psd = "";
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
                    LoginXmlConfig.setPsd(LoginXmlConfig.ClientType, LoginXmlConfig.Place, psd);
                } else
                {
                    MessageBox.Show("两次输入的密码不一致！请重新确认！");
                    closeForm = false;
                }
            } else
            {
                MessageBox.Show("原密码不正确！");
                closeForm = false;
            }
        }
    }
}
