using System;
using System.Collections.Generic;
using System.Text;

namespace gbat.Demo.ConsoleApp1
{
   public class AppSetting
    {
        public AuthSetting AuthSetting { get; set; }
    }

    public class AuthSetting
    {
        public string RedisConn { get; set; }

        public string MySqlConn { get; set; }
    }
}
