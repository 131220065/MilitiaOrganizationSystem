using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilitiaOrganizationSystem
{
    public class ClientForm : Form
    {

        public ClientForm()
        {
            this.Text = LoginXmlConfig.ClientType + "主页-" + PlaceXmlConfig.getPlaceName(LoginXmlConfig.Place);
            FormClosing += ClientForm_FormClosing;
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult re = MessageBox.Show("确认关闭民兵编组系统？", "提示", MessageBoxButtons.OKCancel);
            if(re != DialogResult.OK)
            {//则不关闭
                e.Cancel = true;
            }
        }
    }
}
