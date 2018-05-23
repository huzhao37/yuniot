

using System;
using NewLife.Log;

namespace Yunt.NetcoreTest
{
    class Program
    {
        //private static TimerX _timerX;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            XTrace.Log.Level = LogLevel.Info;
#if DEBUG
            XTrace.UseConsole(true, true);
#endif
            try
            {
                while (true)
                {
                    //if (_timerX == null)
                    //        _timerX = new TimerX(obj => { SendEmail(); }, null, 1000, 1 * 1000);
                    SendEmail();
                    System.Threading.Thread.Sleep(1000);
                }
            }
                catch (Exception e)
                {
                    throw;
                }
            //Console.ReadKey();
        }

        private static void SendEmail()
        {
           XTrace.Log.Info("success!");
        }


    }
}
