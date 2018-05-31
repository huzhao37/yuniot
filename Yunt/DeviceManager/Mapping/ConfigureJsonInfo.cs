using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.Mapping
{
   public class ConfigureJsonInfo
    {
        public List<device> device { get; set; }
        public List<phy> phy { get; set; }
        public List<func> func { get; set; }
    }

    public class device
    {
        public int id { get; set; }
        public string sid { get; set; }
        public string name { get; set; }

        public List<string> func { get; set; }
    }
    public class phy
    {
        public int id { get; set; }
        public string name { get; set; }
        public string unit { get; set; }
        public int format { get; set; }
        public float accur { get; set; }

        public List<string> func { get; set; }
    }
    public class func
    {
        public string sid { get; set; }
        public string name { get; set; }
    }

    //public class devicefunc
    //{
    //    public string sid { get; set; }
    //    public string name { get; set; }
    //}

  
}
