using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Yunt.XmlCheck.XmlModel
{
    [XmlRoot("output")]
    public class Xml
    {
        [XmlAttribute("dev_id")]
        public string EmbeddedDeviceIndex { get; set; }
        [XmlElement("table")]
        public List<Table> Tables { get; set; }
    }
    [XmlType("table")]
    public class Table
    {
        [XmlElement("server")]
        public Server Server { get; set; }
        [XmlElement("device")]
        public List<Device> Devices { get; set; }
        [XmlElement("machine")]
        public List<Machine> Machines { get; set; }
        [XmlElement("data")]
        public List<Data> Datas { get; set; }

    }

    #region Server
    [XmlType("server")]
    public class Server
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlElement("url")]
        public string Url { get; set; }
        [XmlElement("outqueue")]
        public OutQueue OutQueue { get; set; }

        [XmlElement("channel")]
        public Channel Channel { get; set; }
    }
    [XmlType("outqueue")]
    public class OutQueue
    {
        [XmlAttribute("config")]
        public string Config { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("bind")]
        public Bind Bind { get; set; }
    }

    [XmlType("bind")]
    public class Bind
    {
        [XmlAttribute("exchange")]
        public string Exchange { get; set; }
        [XmlAttribute("route_key")]
        public string RouteKey { get; set; }
    }


    [XmlType("channel")]
    public class Channel
    {

        [XmlElement("indata")]
        public List<Indata> Indatas { get; set; }

        [XmlElement("outdata")]
        public List<Outdata> Outdatas { get; set; }
    }
    [XmlType("indata")]
    public class Indata
    {
        /// <summary>
        /// 数据表单Index
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("log_id")]
        public string LogId { get; set; }
        [XmlAttribute("route_key")]
        public string RouteKey { get; set; }
        [XmlAttribute("timer")]
        public string Timer { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("index")]
        public List<Indexs> Indexs { get; set; }
    }
    [XmlType("index")]
    public class Indexs
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlText]
        public string Index { get; set; }
    }
    [XmlType("outdata")]
    public class Outdata
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("route_key")]
        public string RouteKey { get; set; }
        [XmlAttribute("reply_to")]
        public string ReplyTo { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }
    }
    #endregion

    #region Device

    [XmlType("device")]
    public class Device
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }
        //[XmlElement("timer")]
        //public string Timer { get; set; }
        [XmlElement("protocol")]
        public string Protocol { get; set; }
        [XmlElement("path")]
        public List<Paths> Paths { get; set; }
    }

    [XmlType("path")]
    public class Paths
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlText]
        public string Path { get; set; }
    }

    #endregion

    #region Machine

    [XmlType("machine")]
    public class Machine
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("type")]
        public MachineType MachineType { get; set; }
    }

    [XmlType("type")]
    public class MachineType
    {
        [XmlAttribute("id")]
        public string MachineTypeId { get; set; }
        [XmlText]
        public string MachineBacks { get; set; }
    }

    #endregion

    #region Data
    [XmlType("data")]
    public class Data
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlElement("device_id")]
        public string DeviceId { get; set; }
        [XmlElement("value")]
        public string Value { get; set; }
        [XmlElement("accur")]
        public string Accur { get; set; }
        [XmlElement("dat_bit_start")]
        public string DatBitStart { get; set; }
        [XmlElement("dat_bit_end")]
        public string DatBitEnd { get; set; }
        [XmlElement("type")]
        public string DataType { get; set; }
     
        [XmlElement("desc")]
        public Desc Desc { get; set; }

        [XmlElement("mahcine_id")]
        public string MachineId { get; set; }
        [XmlElement("phy")]
        public Physic Physic { get; set; }
        [XmlElement("range")]
        public string Range { get; set; }

        [XmlElement("remark")]
        public string Remark { get; set; }
        
        [XmlElement("unit")]
        public string Unit { get; set; }

        [XmlElement("precision")]
        public string Precision { get; set; }
        [XmlElement("format")]
        public Format Format { get; set; }

    }
    [XmlType("desc")]
    public class Desc
    {
        [XmlAttribute("id")]
        public string DescEn { get; set; }
        [XmlText]
        public string DescCn { get; set; }
    }

    [XmlType("phy")]
    public class Physic
    {

        [XmlAttribute("id")]
        public string PhysicId { get; set; }
        [XmlText]
        public string PhysicName { get; set; }
    }

    [XmlType("format")]
    public class Format
    {

        [XmlAttribute("id")]
        public string FormatId { get; set; }
        [XmlText]
        public string FormatName { get; set; }
    }
    #endregion
}
