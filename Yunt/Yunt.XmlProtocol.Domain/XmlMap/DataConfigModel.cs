using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Yunt.XmlProtocol.Domain.XmlMap
{
    [XmlRoot("configs")]
    public class DataConfigModel
    {
        [XmlElement("config")]
        public List<Config> Configs { get; set; }

    }
    [XmlType("config")]
    public class Config
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlElement("data")]
        public List<Datas> Datas { get; set; }

    }

    [XmlType("data")]
    public class Datas
    {
        [XmlAttribute("type")]
        public int TypeId { get; set; }
        [XmlElement("count")]
        public int Count { get; set; }
    }
}
