using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NewLife.Log;
using Yunt.Common.Log;

namespace Yunt.Common
{
    public class Logger
    {
        private static ILogger _log;
        public static void Create(IConfiguration config, ILoggerFactory factory, string categoryName)
        {
            factory.AddConsole(config.GetSection("Logging"));
            factory.AddDebug();
            factory.AddFile(config.GetSection("FileLogging"));
            factory.AddEventSourceLogger();
            _log = factory.CreateLogger(categoryName);
        }
        public static void Info(string str, params object[] args)
        {
            // Console.ForegroundColor = GetColor(Thread.CurrentThread.ManagedThreadId);
            //_log.LogInformation(str, args);
            XTrace.Log.Info(str,args);
        }
        public static void Debug(string str, params object[] args)
        {
            // Console.ForegroundColor = GetColor(Thread.CurrentThread.ManagedThreadId);
            //_log.LogDebug(str, args);
            XTrace.Log.Debug(str, args);
        }
        public static void Warn(string str, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            //_log.LogWarning(str, args);
            XTrace.Log.Warn(str, args);
        }
        public static void Error(string str, params object[] args)
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            //_log.LogError(str, args);
            XTrace.Log.Error(str, args);
        }
        public static void Exception(Exception ex, string message = null)
        {
            //Console.ForegroundColor = ConsoleColor.DarkRed;
            //_log.LogCritical(ex.Message, message);
            XTrace.Log.Fatal(ex.Message);
        }
        public static void Trace(string str, params object[] args)
        {
            //Console.ForegroundColor = GetColor(Thread.CurrentThread.ManagedThreadId);
            // _log.LogTrace(str, args);
            XTrace.Log.Info(str, args);
        }

        static ConcurrentDictionary<Int32, ConsoleColor> dic = new ConcurrentDictionary<Int32, ConsoleColor>();
        static ConsoleColor[] colors = new ConsoleColor[] {
            ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.White, ConsoleColor.Yellow,
            ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta, ConsoleColor.DarkRed, ConsoleColor.DarkYellow };
        private static ConsoleColor GetColor(Int32 threadid)
        {
            if (threadid == 1) return ConsoleColor.Gray;

            return dic.GetOrAdd(threadid, k => colors[dic.Count % colors.Length]);
        }
    }
}
