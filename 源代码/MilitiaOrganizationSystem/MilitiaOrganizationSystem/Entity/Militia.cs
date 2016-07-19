using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitiaOrganizationSystem
{
    public class Militia
    {//民兵类
        public string Group { get; set; }//分组路径
        public string Place { get; set; }//采集地ID，也是数据库名

        //下面是配置文件里的属性
        public string Id { get; set; }//数据库会自动给它赋值
        public string Name { get; set; } //姓名, type=string
        public string Sex { get; set; } //性别, type=enum
        public string Source { get; set; } //来源, type=enum
        public string Ethnic { get; set; } //民族, type=string
        public string Resvalueence { get; set; } //户籍地, type=string
        public string PoliticalStatus { get; set; } //政治面貌, type=enum
        public string CredentialNumber { get; set; } //身份证号, type=string
        public string Education { get; set; } //文化水平, type=enum
        public string MilitaryForceType { get; set; } //军种, type=enum
        public string MilitaryRank { get; set; } //军衔, type=enum
        public string Available { get; set; } //是否在位, type=enum
        public string EquipmentInfo_Available { get; set; } //是否在位, type=enum
        public string EquipmentInfo_MilitaryProfessionTypeName { get; set; } //地方专业, type=enum
        public string RetirementRank { get; set; } //军衔, type=enum
        public string RetirementMilitaryForceType { get; set; } //军种, type=enum
        public string CadreServiced { get; set; } //是否服役, type=enum
        public string CadreProfessionalTrained { get; set; } //是否经过专业训练, type=enum
        public string CadreAttendedCommittee { get; set; } //是否参加同级党工委, type=enum
        public string CadreTrained { get; set; } //是否经过人武学校培训, type=enum
        public string CadreFullTime { get; set; } //职务性质, type=enum
        public string Trained { get; set; } //是否参训, type=enum
        public string TeamingPosition { get; set; } //职务, type=enum
        public string ReplyStatus { get; set; } //回复状态, type=enum
        public string MilitaryProfessionTypeName { get; set; } //地方专业, type=enum
        public string MilitaryProfessionName { get; set; } //对口专业名称, type=enum
        public string RetirementProfessionType { get; set; } //服役专业, type=enum
        public string RetirementProfessionSmallType { get; set; } //服役专业小类, type=enum
        public string RetirementProfessionName { get; set; } //服役专业名称, type=enum

        public string BasicLevelName { get; set; } //基层名，Place的附属属性，丰富Place的显示的，配置文件里面没有

        public Militia()
        {
            Group = "未分组";//分组
            Place = null;//数据库名初始化为null
            Id = null;
        }


        public string info()
        {
            return "姓名：" + Name + ", 身份证：" + CredentialNumber;
        }

        public bool isEqual(Militia m)
        {//判断两个民兵是否是一模一样
            if(m.Id == Id && m.Place == Place)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool isSatisfy(Condition condition)
        {//判断民兵是否满足condition条件
            if(!Place.StartsWith(condition.place))
            {
                return false;
            }
            return condition.lambdaCondition.Compile()(this);
        }
    }
}
