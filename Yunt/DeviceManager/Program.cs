using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeviceManager.Core;
using DeviceManager.Model;


namespace DeviceManager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            XCode.Setting.Current.Migration = XCode.DataAccessLayer.Migration.Off;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            #region 实体类自动生成
            //Motorparams.FromExcel("cc");
            //Motorparams.FromExcel("cy");
            //Motorparams.FromExcel("hvb");
            //Motorparams.FromExcel("ic");
            //Motorparams.FromExcel("jc");
            //Motorparams.FromExcel("mf");
            //Motorparams.FromExcel("vb");
            //Motorparams.FromExcel("vc");
            //Motorparams.FromExcel("pul");

            //InitClassFile("BaseModel", "Yunt.Device.Repository.EF.Models", TableType.Instant);
            //InitClassFile("BaseModel", "Yunt.Device.Repository.EF.Models", TableType.Hour);
            //InitClassFile("BaseModel", "Yunt.Device.Repository.EF.Models", TableType.Day);

            //InitClassFile("AggregateRoot", "Yunt.Device.Domain.Model", TableType.Instant);
            //InitClassFile("AggregateRoot", "Yunt.Device.Domain.Model", TableType.Hour);
            //InitClassFile("AggregateRoot", "Yunt.Device.Domain.Model", TableType.Day);
            //Console.ReadKey();

            #endregion

            #region 演示一：动态生成类。
            ////生成一个类t。
            //Type t = ClassHelper.BuildType("MyClass");
            //#endregion

            //#region 演示二：动态添加属性到类。
            ////先定义两个属性。
            //List<CustPropertyInfo> lcpi = new List<CustPropertyInfo>();
            //CustPropertyInfo cpi;

            //cpi = new CustPropertyInfo("System.String", "S1");
            //lcpi.Add(cpi);
            //cpi = new CustPropertyInfo("System.String", "S2");
            //lcpi.Add(cpi);



            ////再加入上面定义的两个属性到我们生成的类t。
            //t = ClassHelper.AddProperty(t, lcpi);

            ////把它显示出来。
            //DispProperty(t);

            ////再定义两个属性。
            //lcpi.Clear();
            //cpi = new CustPropertyInfo("System.Int32", "I1");
            //lcpi.Add(cpi);
            //cpi = new CustPropertyInfo("System.Int32", "I2");
            //lcpi.Add(cpi);

            ////再加入上面定义的两个属性到我们生成的类t。
            //t = ClassHelper.AddProperty(t, lcpi);

            ////再把它显示出来，看看有没有增加到4个属性。
            //DispProperty(t);
            //#endregion

            //#region 演示三：动态从类里删除属性。
            ////把'S1'属性从类t中删除。
            //t = ClassHelper.DeleteProperty(t, "S1");
            ////显示出来，可以看到'S1'不见了。
            //DispProperty(t);

            //#endregion

            //#region 演示四：动态获取和设置属性值。
            ////先生成一个类t的实例o。
            //object o = ClassHelper.CreateInstance(t);

            ////给S2,I2属性赋值。
            //ClassHelper.SetPropertyValue(o, "S2", "abcd");
            //ClassHelper.SetPropertyValue(o, "I2", 1234);

            ////再把S2,I2的属性值显示出来。
            //Console.WriteLine("S2 = {0}", ClassHelper.GetPropertyValue(o, "S2"));
            //Console.WriteLine("I2 = {0}", ClassHelper.GetPropertyValue(o, "I2"));
            #endregion
        }


        public static void DispProperty(Type t)
        {
            Console.WriteLine("ClassName '{0}'", t.Name);
            foreach (PropertyInfo pInfo in t.GetProperties())
            {
                Console.WriteLine("Has Property '{0}'", pInfo.ToString());
            }
            Console.WriteLine("");
        }

        public static string Builder(string className, string nameSpace, string MotorTypeId, string baseClassName, StringBuilder str)
        {
            var desc = new StringBuilder();
            desc.Append($"   /// <summary>\n");
            desc.Append($"   /// {MotorTypeId}\n");
            desc.Append($"   /// </summary>\n");
            desc.Append($"   [DataContract]\n");
            desc.Append($"   [Serializable]\n");
            desc.Append($"   [ProtoContract(SkipConstructor = true)]\n");

            var usings =
                "using System;\nusing System.ComponentModel;\nusing System.Runtime.Serialization;\nusing ProtoBuf;\n\n";
            if (nameSpace.EqualIgnoreCase("Yunt.Device.Domain.Model"))
            {
                usings= "using System;\nusing System.ComponentModel;\nusing System.Runtime.Serialization;\nusing ProtoBuf;\nusing Yunt.Device.Domain.BaseModel;\n\n";
            }
            var head = usings + "namespace " + nameSpace + " \n{\n  " + desc + " public class " + className + $":{baseClassName}\n   {{\n";

            var foot = "\n   }\n}\n";

            return head + str + foot;
        }

        public static void InitClassFile(string baseClassName, string spaceName, TableType table)
        {
            var types = Motorparams.FindAll().Select(e => e.MotorTypeId);
            foreach (var t in types)
            {
                if(t.EqualIgnoreCase("IC"))
                    ClassFactory(t, baseClassName, spaceName, table);
            }

        }

        public static void ClassFactory(string MotorTypeId, string baseClassName, string spaceName, TableType table)
        {

            var className = "";
            switch (MotorTypeId)
            {
                case "CC":
                    className = "ConeCrusher";
                    break;
                case "CY":
                    className = "Conveyor";
                    break;
                case "IC":
                    className = "ImpactCrusher";
                    break;
                case "JC":
                    className = "JawCrusher";
                    break;
                case "MF":
                    className = "MaterialFeeder";
                    break;
                case "PUL":
                    className = "Pulverizer";
                    break;
                case "SCC":
                    className = "SimonsConeCrusher";
                    break;
                case "VC":
                    className = "VerticalCrusher";
                    break;
                case "VB":
                    className = "Vibrosieve";
                    break;
                case "HVB":
                    className = "HVib";
                    break;
            }

            #region CreaterClassFile

            //生成一个类t。
            //Type x = ClassHelper.BuildType(className);

            var str = new StringBuilder();


            //先定义两个属性。
            //List<CustPropertyInfo> lcpix = new List<CustPropertyInfo>();
            //CustPropertyInfo cpix;
            var param = Motorparams.FindAll("MotorTypeId", MotorTypeId).ToList();
            if (!param.Any()) return;

            var i = 1;
            param.ForEach(p =>
            {
                if (!table.Equals(TableType.Instant))
                {
                    //参数名称不变
                    if (p.Param.Contains("AccumulativeWeight") || p.Param.Contains("ActivePower") || p.Param.Contains("WearValue"))
                    {
                        str.Append($"      /// <summary>\n");
                        str.Append($"      /// {p.Description}\n");
                        str.Append($"      /// </summary>\n");
                        str.Append($"      [DataMember]\n");
                        str.Append($"      [DisplayName(\"{p.Description}\")]\n");
                        str.Append($"      [ProtoMember({i})]\n");
                        str.Append($"      public float " + p.Param + "{get;set;}\n");
                        i++;
                        return;
                    }
                    ////参数不要
                    //if (p.Param.Contains("InstantWeight"))
                    //{
                    //    return;
                    //}
                    str.Append($"      /// <summary>\n");
                    str.Append($"      /// {p.Description}\n");
                    str.Append($"      /// </summary>\n");
                    str.Append($"      [DataMember]\n");
                    str.Append($"      [DisplayName(\"{p.Description}\")]\n");
                    str.Append($"      [ProtoMember({i})]\n");
                    str.Append($"      public float Avg" + p.Param + "{get;set;}\n");
                    i++;
                }
                else
                {
                    str.Append($"      /// <summary>\n");
                    str.Append($"      /// {p.Description}\n");
                    str.Append($"      /// </summary>\n");
                    str.Append($"      [DataMember]\n");
                    str.Append($"      [DisplayName(\"{p.Description}\")]\n");
                    str.Append($"      [ProtoMember({i})]\n");
                    str.Append($"      public float " + p.Param + "{get;set;}\n");
                    i++;
                }

            });
            str.Append($"     /// <summary>\n");
            str.Append($"     /// 电机设备编号\n");
            str.Append($"     /// </summary>\n");
            str.Append($"     [ProtoMember({i})]\n");
            str.Append("      public string MotorId { get; set; }\n");
            //str.Append($"     /// <summary>\n");
            //str.Append($"     /// 是否已删除\n");
            //str.Append($"     /// </summary>\n");
            //str.Append($"     [ProtoMember({i + 1})]\n");
            //str.Append("\n");

            if (!table.Equals(TableType.Instant))
            {
                str.Append($"     /// <summary>\n");
                str.Append($"     /// 开机时间\n");
                str.Append($"     /// </summary>\n");
                str.Append($"     [ProtoMember({i + 2})]\n");
                str.Append("      public float RunningTime { get; set; }\n");
                str.Append($"     /// <summary>\n");
                str.Append($"     /// 负荷\n");
                str.Append($"     /// </summary>\n");
                str.Append($"     [ProtoMember({i + 3})]\n");
                str.Append("      public float LoadStall { get; set; }\n");

            }


            var name = className;
            switch (table)
            {
                case TableType.Day:
                    name = className + "ByDay";
                    break;
                case TableType.Hour:
                    name = className + "ByHour";
                    break;
            }

            var fileStr = Builder(name, spaceName, MotorTypeId, baseClassName, str);

            var path = AppDomain.CurrentDomain.BaseDirectory + "\\Models\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = path + $"{name}.cs";//AppDomain.CurrentDomain.BaseDirectory
            FileEx.Write(path, fileStr);

            Console.WriteLine($"{name}生成完毕！");


            #endregion
        }
    }
}
