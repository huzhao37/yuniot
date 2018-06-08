using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Yunt.Common
{
    /// <summary>
    /// 服务名称
    /// </summary>
   public class ServiceAttribute : Attribute
    {
        private readonly ServiceType _name;

        public ServiceType Name
        {
            get { return _name; }
        }
        public ServiceAttribute(ServiceType type)
        {
            _name = type;
        }

     
       
    }
    /// <summary>
    /// 服务类型
    /// </summary>
    public enum ServiceType
    {
        Auth=0,
        Device,
        Inventory,
        Xml,
        Analysis
    }
}
