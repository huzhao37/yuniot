using ExcelInfoImport.YuntModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelInfoImport
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            XCode.Setting.Current.Migration = XCode.DataAccessLayer.Migration.Off;
            Start();

        }
        //启动
        static void Start()
        {
            //1.表单初始化
            //Dataformmodel.FromExcel("JXD001", 50);
            // Dataformmodel.FromExcel("JXD001", 51);
            Dataformmodel.Motors();
            Console.WriteLine("初始化完成...");
            Console.ReadKey();
            //2.motor初始化

            //3.dataconfig初始化
        }
    }
}
