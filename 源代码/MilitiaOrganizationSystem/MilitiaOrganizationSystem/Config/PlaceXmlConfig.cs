using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MilitiaOrganizationSystem
{
    static class PlaceXmlConfig
    {//全国省市区信息类，从xml文件中读取
        //文件路径
        private const string provinceXmlFile = "全国省市区数据库/Provinces.xml";
        private const string cityXmlFile = "全国省市区数据库/Cities.xml";
        private const string districtXmlFile = "全国省市区数据库/Districts.xml";

        //XMlDocument，将一直驻留内存
        private static XmlDocument provinceXmlDoc = null;
        private static XmlDocument cityXmlDoc = null;
        private static XmlDocument districXmlDoc = null;


        public static void initial()
        {//初始化
            if(provinceXmlDoc == null)
            {
                provinceXmlDoc = new XmlDocument();
                provinceXmlDoc.Load(provinceXmlFile);
            }
            if(cityXmlDoc == null)
            {
                cityXmlDoc = new XmlDocument();
                cityXmlDoc.Load(cityXmlFile);
            }
            if(districXmlDoc == null)
            {
                districXmlDoc = new XmlDocument();
                districXmlDoc.Load(districtXmlFile);
            }
        }

        public static XmlNodeList provinces()
        {//返回所有的省结点
            return provinceXmlDoc.DocumentElement.ChildNodes;
        }

        public static XmlNodeList cities(string PID)
        {//根据省ID返回省下的城市结点
            return cityXmlDoc.DocumentElement.SelectNodes("City[@PID='" + PID + "']");
        }

        public static XmlNodeList districts(string CID)
        {//根据城市ID返回城市下的所有区县结点
            return districXmlDoc.DocumentElement.SelectNodes("District[@CID='" + CID + "']");
        }

        public static string getPCD_ID(string placeName)
        {//根据地区名获取PCDID
            //可以缺省市和区县
            string PCD_ID = "";
            string[] placeNames = placeName.Split(new char[] { '/' });
            XmlNode pNode = provinceXmlDoc.DocumentElement.SelectSingleNode("Province[@ProvinceName='" + placeNames[0] + "']");
            if(pNode == null)
            {
                return placeName;
            }
            PCD_ID += pNode.Attributes["ID"].Value;
            if(placeNames.Length >= 2)
            {
                XmlNode cNode = cityXmlDoc.DocumentElement.SelectSingleNode("City[@CityName='" + placeNames[1] + "']");
                if(cNode == null)
                {
                    return placeName;
                }
                PCD_ID += "-" + cNode.Attributes["ID"].Value;
                if(placeNames.Length == 3)
                {
                    XmlNode dNode = districXmlDoc.DocumentElement.SelectSingleNode("District[@DistrictName='" + placeNames[2] + "']");
                    if(dNode == null)
                    {
                        return placeName;
                    }
                    PCD_ID += "-" + dNode.Attributes["ID"].Value;
                } else
                {
                    return placeName;
                }
            }
            return PCD_ID;
        }

        public static string getPlaceName(string PCD_ID)
        {//根据联合的PID-CID-DID，返回地点中文名，如北京市/北京市/东城区
            //可以缺省DID和CID
            string[] IDS = PCD_ID.Split(new char[] { '-' });
            XmlNode provinceNode = provinceXmlDoc.DocumentElement.SelectSingleNode("Province[@ID='" + IDS[0] + "']");
            try
            {
                if (IDS.Length == 1)
                {
                    return provinceNode.Attributes["ProvinceName"].Value;
                }
                else if (IDS.Length == 2)
                {
                    XmlNode cityNode = cityXmlDoc.DocumentElement.SelectSingleNode("City[@ID='" + IDS[1] + "']");
                    return provinceNode.Attributes["ProvinceName"].Value + "/" + cityNode.Attributes["CityName"].Value;
                }
                else if (IDS.Length == 3)
                {
                    XmlNode cityNode = cityXmlDoc.DocumentElement.SelectSingleNode("City[@ID='" + IDS[1] + "']");
                    XmlNode districtNode = districXmlDoc.DocumentElement.SelectSingleNode("District[@ID='" + IDS[2] + "']");
                    return provinceNode.Attributes["ProvinceName"].Value + "/" + cityNode.Attributes["CityName"].Value + "/" + districtNode.Attributes["DistrictName"].Value;
                }
                else
                {
                    return PCD_ID;
                }
            } catch
            {
                return PCD_ID;
            }
        }


        public static List<string> getPCDIDOfCity(string CityPCDID)
        {//测试用，获取本市所有的区县的PCDID
            string[] cityids = CityPCDID.Split(new char[] { '-' });
            XmlNodeList districtNodes = districts(cityids[1]);//通过市Id获取区县列表
            List<string> ids = new List<string>();
            foreach(XmlNode districtNode in districtNodes)
            {
                ids.Add(CityPCDID + "-" + districtNode.Attributes["ID"].Value);
            }
            return ids;
        }

        public static List<string> getJiangsuPCDID()
        {//获取所有江苏省的区县的PCDID,测试用
            List<string> ids = new List<string>();
            string provinceId = "10";
            XmlNodeList jiangsuCities = cities("10");
            foreach(XmlNode city in jiangsuCities)
            {
                string cityId = city.Attributes["ID"].Value;
                XmlNodeList ds = districts(cityId);
                foreach(XmlNode d in ds)
                {
                    string districId = d.Attributes["ID"].Value;
                    ids.Add(provinceId + "-" + cityId + "-" + districId);
                }
            }
            return ids;
        }
    }
}
