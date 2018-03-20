using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager.Core
{
   public static class RegularEx
    {
        /// <summary>
        /// 判断是否是英文
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsEnlish(this string key)
        {
            var regEnglish = new Regex("^[a-zA-Z]");
       
            return regEnglish.IsMatch(key);
        }
        /// <summary>
        /// 判断是否是中文
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsChina(this string key)
        {
            var regChina = new Regex("^[^\x00-\xFF]");        

            return regChina.IsMatch(key);
        }
    }
}
