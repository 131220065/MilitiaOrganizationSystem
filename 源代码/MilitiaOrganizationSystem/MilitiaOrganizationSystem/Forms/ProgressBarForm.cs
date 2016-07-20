using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilitiaOrganizationSystem
{
    public partial class ProgressBarForm : BasicForm
    {//进度条界面
        private bool isClosed;//判断窗口是否已经关闭

        public ProgressBarForm(int vMax)      //设定进度条的最大值
        {
            InitializeComponent();
            this.progressBar.Maximum = vMax;
            isClosed = true;

            FormClosing += ProgressBarForm_FormClosing;
        }

        public void setMaxValue(int vMax)
        {///设置最大值,实际上是加上vMax
            this.progressBar.Maximum += vMax;
        }

        private void ProgressBarForm_FormClosing(object sender, FormClosingEventArgs e)
        {//禁止关闭
            e.Cancel = true;
        }

        public void Increase(string info)
        {//显示一条进度信息
            if(FormBizs.mainForm != null)
                FormBizs.mainForm.Enabled = false;

            progressBar.PerformStep();

            if (progressBar.Value >= progressBar.Maximum - 1)
            {
                progressBar.Maximum++;
                progressBar.Value++;
            }
            progressBar.Update();
            progressBar.Refresh();
            this.textBox.AppendText(info + "\n");
            this.textBox.Update();
            this.textBox.Refresh();
            
            if(isClosed)
            {
                isClosed = false;
                this.Show();
            }
        }

        public void Completed()
        {//完成，隐藏进度条，恢复原来的样子
            progressBar.Value = progressBar.Maximum;
            progressBar.Update();
            progressBar.Refresh();
            this.Hide();
            progressBar.Maximum = 1;
            progressBar.Value = 0;
            textBox.Clear();
            isClosed = true;

            if (FormBizs.mainForm != null)
                FormBizs.mainForm.Enabled = true;
        }
    }
}
