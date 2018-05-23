using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using NewLife.Serialization;

namespace Yunt.Common.Json
{
   public class JsonHelper
    {
        /// <summary> 
        /// JSON文本转对象,泛型方法 
        /// </summary> 
        /// <typeparam name="T">类型</typeparam> 
        /// <param name="jsonText">JSON文本</param> 
        /// <returns>指定类型的对象</returns> 
        public static T JsonToObject<T>(string jsonText)
        {           
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jsonText.ToJsonEntity<T>();
                //return jss.Deserialize<T>(jsonText);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
            }
        }
        /// <summary> 
        /// 将JSON文本转换成数据行 
        /// </summary> 
        /// <param name="jsonText">JSON文本</param> 
        /// <returns>数据行的字典</returns> 
        public static Dictionary<string, object> DataRowFromJson(string jsonText)
        {
            return JsonToObject<Dictionary<string, object>>(jsonText);
        }

    }
}
