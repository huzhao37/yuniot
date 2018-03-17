using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.config
{
    public partial class Camera
    {
        public long CameraId { get; set; }
        public long? Time { get; set; }
        public string ChannelNo { get; set; }
        public string ChannelName { get; set; }
        public string DvrSerialnum { get; set; }
        public string Name { get; set; }
        public string CameraSerialnum { get; set; }
    }
}
