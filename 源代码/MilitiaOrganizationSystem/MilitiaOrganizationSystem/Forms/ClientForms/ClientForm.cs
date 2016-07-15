using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilitiaOrganizationSystem
{
    public abstract class ClientForm : BasicForm
    {

        public ClientForm()
        {
            InitializeComponent();
            FormClosing += ClientForm_FormClosing;
            AnimateWindow(this.Handle, 300, AW_CENTER);
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult re = MessageBox.Show("确认关闭民兵编组系统？", "提示", MessageBoxButtons.OKCancel);
            if(re != DialogResult.OK)
            {//则不关闭
                e.Cancel = true;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ClientForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.DoubleBuffered = true;
            this.Name = "ClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }
    }
}
