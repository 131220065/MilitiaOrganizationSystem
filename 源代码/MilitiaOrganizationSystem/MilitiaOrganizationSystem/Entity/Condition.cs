using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MilitiaOrganizationSystem
{
    public class Condition
    {//条件类，描述了民兵需要满足的条件
        public Expression<Func<Militia, bool>> lambdaCondition { get; set; }//页面查询条件
        public string place { get; set; }//该页面的查询条件之一指定数据库
        public List<ChildCondition> ccList { get; set; }
        //查询子条件的集合

        private void initial()
        {//初始化
            ccList = new List<ChildCondition>();
            lambdaCondition = null;
            place = LoginXmlConfig.Place;//初始化为本采集地
        }
        
        public Condition()
        {//构造函数
            initial();
        }

        public Condition(Condition condition)
        {//复制一个condition
            ccList = new List<ChildCondition>();
            ccList.AddRange(condition.ccList);
            place = condition.place;
            lambdaCondition = condition.lambdaCondition;
        }
        
        public Condition(string group)
        {//以分组创建Condition
            initial();

            ChildCondition cc = new ChildCondition(MilitiaXmlConfig.getNodeByProperty("Group"));
            cc.Method = "StartsWith";
            cc.Values.Add(group);

            ccList.Add(cc);
            generateLambdaCondition();//生成lambda表达式
        }

        public class ChildCondition
        {//一个子条件
            public System.Xml.XmlNode parameterNode { get; set; }//属性xml节点
            public string Method { get; set; }//方法
            public List<string> Values { get; set; }//数据


            public ChildCondition(System.Xml.XmlNode xn)
            {//通过属性xml节点创建一个子条件
                Values = new List<string>();
                parameterNode = xn;
            }

            public override string ToString()
            {//重写toString方法，用在显示上
                string info = parameterNode.Attributes["name"].Value + " ";
                switch(parameterNode.Attributes["type"].Value)
                {
                    case "string":
                        //显示string类型的
                        info += Method + " ";
                        info += Values[0];
                        break;
                    case "enum":
                        //enum的要转换为中文
                        info += ": [";
                        foreach (string value in Values)
                        {
                            info += parameterNode.SelectSingleNode("selection[@value='" + value + "']").Attributes["name"].Value + ", ";
                        }
                        info += "]";
                        break;
                    case "group":
                        info += ": ";
                        info += Values[0];
                        break;
                    case "place":
                        //place要转换为中文名称
                        info += ": ";
                        info += PlaceXmlConfig.getPlaceName(Values[0]);
                        break;
                }
                return info;
            }
        }

        private Expression generateExpresionByChildCondition(ParameterExpression parameter, ChildCondition cc)
        {//通过一个子条件生成子条件的lambda表达式
            string propertyName = cc.parameterNode.Attributes["property"].Value;
            var property = Expression.Property(parameter, propertyName);//获取到属性
            Expression expression = null;
            switch(cc.Method)
            {//通过相应的Method调用方法
                case "Equal"://可能是string或enum
                    expression = Expression.Equal(property, Expression.Constant(cc.Values[0]));
                    for(int i = 1; i < cc.Values.Count; i++)
                    {//如果是enum，就要或者
                        expression = Expression.OrElse(expression, Expression.Equal(property, Expression.Constant(cc.Values[i])));
                    }
                    break;
                case "GreaterThanOrEqualAndLessThan"://大于等于，并且小于,这是为int型准备的
                    expression = Expression.AndAlso(
                        Expression.GreaterThanOrEqual(property, Expression.Constant(int.Parse(cc.Values[0]))),
                        Expression.LessThan(property, Expression.Constant(int.Parse(cc.Values[1])))
                        );
                    break;
                default://其他的分类型考虑
                    switch (cc.parameterNode.Attributes["type"].Value)
                    {
                        case "enum"://也不会出现
                            
                            break;
                        case "int"://讲道理不会出现在这里

                            break;
                        default://当做string,要么startwith，要么endswith
                            expression = Expression.Call(property,
                                typeof(string).GetMethod(cc.Method, new Type[] { typeof(string) }),
                                Expression.Constant(cc.Values[0])
                                );
                            break;
                    }
                    break;
            }
            
            return expression;
        }

        public void generateLambdaCondition()
        {//根据子条件列表生成用“且”生成lambda表达式
            var parameter = Expression.Parameter(typeof(Militia), "x");
            Expression expression = Expression.Call(
                Expression.Property(parameter, "Place"),
                typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }),
                Expression.Constant(place)
                );
            foreach(ChildCondition cc in ccList)
            {
                expression = Expression.AndAlso(expression, generateExpresionByChildCondition(parameter, cc));
            }

            lambdaCondition = Expression.Lambda<Func<Militia, bool>>(expression, parameter);
        }

        public override string ToString()
        {//用于显示，首先显示采集地
            string info = "采集地：" + PlaceXmlConfig.getPlaceName(place) + ", ";
            foreach(ChildCondition cc in ccList)
            {
                info += cc.ToString() + ", ";
            }
            return info;
        }
    }
}
