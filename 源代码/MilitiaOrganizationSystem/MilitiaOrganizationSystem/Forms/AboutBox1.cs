using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilitiaOrganizationSystem
{
    partial class AboutBox1 : BasicForm
    {
        public AboutBox1()
        {
            InitializeComponent();
            this.Text = String.Format("关于 {0}", "民兵编队系统");
            //AssemblyTitle == "民兵编队系统";
            this.labelProductName.Text = "民兵编队系统";
            //AssemblyProduct == "民兵编队系统";
            this.labelVersion.Text = String.Format("版本 {0}", "2.0");
            //AssemblyVersion == "2.0";
            this.labelCopyright.Text = "软件工程综合实验";
            //AssemblyCopyright == "软件工程综合实验";
            this.labelCompanyName.Text = "组长：何志强 小组成员：刘闯，杨月洋，石稼轩，王凯军";
            //AssemblyCompany == "组长：何志强 小组成员：刘闯，杨月洋，石稼轩，王凯军";
            this.textBoxDescription.Text = "感谢各位老师同学！";
            //AssemblyDescription == "感谢各位老师同学！";
        }

        #region 程序集特性访问器

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void labelProductName_Click(object sender, EventArgs e)
        {
            ;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
