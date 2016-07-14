using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilitiaOrganizationSystem
{
    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            initial();//初始化静态类

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (LoginXmlConfig.ClientType == "")
            {//检测到没有设置，于是打开设置界面
                SetForm sf = new SetForm();
                if(sf.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            } else
            {//登录输入口令
                LoginForm lf = new LoginForm();
                if(lf.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            Form mainForm = null;
            switch(LoginXmlConfig.ClientType)
            {
                case "省军分区":
                    mainForm = new ProvinceForm();
                    break;
                case "市军分区":
                    mainForm = new CityForm();
                    break;
                case "区县人武部":
                    mainForm = new DistrictForm();
                    break;
                case "基层":
                    mainForm = new BasicLevelForm();
                    break;
            }

            Application.Run(mainForm);
        }

        static void initial()
        {//静态初始化
            MilitiaXmlConfig.initial();
            PlaceXmlConfig.initial();
            GroupXmlConfig.initial();
            LoginXmlConfig.initial();
        }
    }
}
