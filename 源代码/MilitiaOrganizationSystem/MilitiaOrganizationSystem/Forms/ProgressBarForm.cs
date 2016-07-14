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
    public partial class ProgressBarForm : Form
    {//进度条界面
        private bool closeForm;//指示窗口是否可以关闭

        public ProgressBarForm(int vMax)      //设定进度条的最大值
        {
            InitializeComponent();
            closeForm = false;
            this.progressBar1.Maximum = vMax;
            this.label1.Text = "已完成 0 / " + vMax;

            FormClosing += ProgressBarForm_FormClosing;
        }

        public void setMaxValue(int vMax)
        {///设置最大值
            this.progressBar1.Maximum = vMax;
        }

        private void ProgressBarForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!closeForm)
            {
                e.Cancel = true;
            }
        }

        public bool Increase(int nValue, string nInfo)      //进度条变化函数，外部接口
        {
            if (nValue > 0)
            {
                if (progressBar1.Value + nValue < progressBar1.Maximum)
                {
                    progressBar1.Value += nValue;
                    this.textBox1.AppendText(nInfo);
                    this.label1.Text = "已完成 " + progressBar1.Value + " / " + progressBar1.Maximum;
                    Application.DoEvents();
                    progressBar1.Update();
                    progressBar1.Refresh();
                    this.textBox1.Update();
                    this.textBox1.Refresh();
                    this.label1.Update();
                    this.label1.Refresh();
                    return true;
                }
                else
                {
                    progressBar1.Value = progressBar1.Maximum;
                    //this.textBox1.AppendText(nInfo);
                    closeForm = true;//可以关闭
                    this.Close();                           //执行完之后，自动关闭子窗体
                    return false;
                }
            }
            return false;
        }
    }
}
