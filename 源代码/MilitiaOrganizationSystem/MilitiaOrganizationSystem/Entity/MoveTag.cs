using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitiaOrganizationSystem
{
    public class MoveTag
    {//移动需要的tag
        public List<Militia> moveMilitias { get; set; }//移动的民兵
        public object source { get; set; }//指定移动动作发生的来源界面

        public MoveTag(object src, List<Militia> mList)
        {//构造函数
            source = src;
            moveMilitias = mList;
        }
    }
}
