#region Code File Comment

// SOLUTION   ： 云统物联网V3
// PROJECT    ： Yunt.Redis
// FILENAME   ： HostItem.cs
// AUTHOR     ： soft-zh
// CREATE TIME： 2018-3-14 9:42
// COPYRIGHT  ： 版权所有 (C) 云统信息科技有限公司 http://www.unitoon.cn/ 2018~2020

#endregion

#region using namespace

using System.ComponentModel;
using Microsoft.Extensions.Configuration;

#endregion

namespace Yunt.Redis.Config
{
    public class HostItem
    {
      
        [Description("主机地址")]
        public  string Host { get; set; }


        [Description("链接数")]
        public int Connections { get; set; } = 60;

    }
    public class ReadHostItem
    {
    
        [Description("读主机地址")]
        public string ReadHost { get; set; } 

        [Description("链接数")]
        public int Connections { get; set; } = 60;


    }
    public class WriteHostItem
    {
    
        [Description("写主机地址")]
        public string WriteHost { get; set; }


        [Description("链接数")]
        public int Connections { get; set; } = 60;


    }
}