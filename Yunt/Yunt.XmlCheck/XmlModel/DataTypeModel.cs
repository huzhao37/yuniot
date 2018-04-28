using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Yunt.XmlCheck.XmlModel
{
    [XmlRoot("types")]
    public class DataTypeModel
    {
        [XmlElement("type")]
        public List<Types> Types { get; set; }

    }
    [XmlType("type")]
    public class Types
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlElement("description")]
        public string Description { get; set; }
        [XmlElement("bit")]
        public int Bit { get; set; }
        [XmlElement("inByte")]
        public int InByte { get; set; }
        [XmlElement("outIntArray")]
        public int OutIntArray { get; set; }
    }
}
