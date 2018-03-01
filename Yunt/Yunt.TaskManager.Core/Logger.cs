using System;
using NewLife.Log;

namespace Yunt.TaskManager.Core
{
    public class Logger
    {
        public static void Info(string str, params object[] args)
        {
            XTrace.Log.Info(str, args);
        }
        public static void Debug(string str, params object[] args)
        {
            XTrace.Log.Debug(str, args);
        }
        public static void Warn(string str, params object[] args)
        {
            XTrace.Log.Warn(str, args);
        }
        public static void Error(string str, params object[] args)
        {
            XTrace.Log.Error(str, args);
        }
        public static void Exception(Exception ex, string message = null)
        {
            XTrace.Log.Fatal(ex.Message, message);
        }
    }
}
