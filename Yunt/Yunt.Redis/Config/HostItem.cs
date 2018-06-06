

using System;
using System.ComponentModel;

namespace Yunt.Redis.Config
{
    public class HostItem : MarshalByRefObject
    {
      
        [Description("主机地址")]
        public  string Host { get; set; }


        [Description("链接数")]
        public int Connections { get; set; } = 60;

    }
    public class ReadHostItem : MarshalByRefObject
    {
    
        [Description("读主机地址")]
        public string ReadHost { get; set; } 

        [Description("链接数")]
        public int Connections { get; set; } = 60;


    }
    public class WriteHostItem : MarshalByRefObject
    {
    
        [Description("写主机地址")]
        public string WriteHost { get; set; }


        [Description("链接数")]
        public int Connections { get; set; } = 60;


    }
}