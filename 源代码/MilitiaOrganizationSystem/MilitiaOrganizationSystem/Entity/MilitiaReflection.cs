using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MilitiaOrganizationSystem
{
    class MilitiaReflection
    {//民兵对象的反射取/赋值
        private static Type mt = typeof(Militia);
        public static PropertyInfo[] properties = mt.GetProperties();
        private static Dictionary<string, PropertyInfo> piDict = properties.ToDictionary(x => x.Name);//属性控制字典
        public static Dictionary<string, PropertyInfo>.KeyCollection keys = piDict.Keys;//所有的属性名称

        private Militia militia;

        public static Militia stringToMilitia(string info)
        {//民兵对象反序列化
            string[] values = info.Split(new char[] { ',' });
            Militia m = new Militia();
            int i = 0;
            foreach(PropertyInfo pi in properties)
            {
                pi.SetValue(m, values[i]);
                i++;
            }
            return m;
        }

        public static string militiaToString(Militia m)
        {//民兵对象序列化
            string info = "";
            foreach(PropertyInfo pi in properties)
            {
                try
                {
                    info += pi.GetValue(m).ToString() + ",";
                } catch
                {
                    info += ",";
                }
                
            }
            return info;
        }

        public MilitiaReflection(Militia m)
        {//构造函数
            militia = m;
        }

        public object getProperty(string property)
        {//反射获取属性值
            return piDict[property].GetValue(militia);
        }

        public void setProperty(string property, object value)
        {//反射设置属性值
            piDict[property].SetValue(militia, value);
        }

    }
}
