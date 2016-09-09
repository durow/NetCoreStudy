using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingStudy.Services.AyxFileLogger
{
    public class AyxFileLogger : ILogger
    {
        private string _name;

        public AyxFileLogger(string name)
        {
            _name = name;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!_name.StartsWith("LoggingStudy")) return;
            //获取日志信息
            var message = formatter?.Invoke(state,exception);
            //日志写入文件
            LogToFile(logLevel, message);
            //Console.WriteLine("from ayxLog " + message);
        }

        //记录日志
        private void LogToFile(LogLevel level, string message)
        {
            var filename = GetFilename();
            var logContent = GetLogContent(level, message);
            File.AppendAllLines(filename, new List<string> { logContent },Encoding.UTF8);
        }
        //获取日志内容
        private string GetLogContent(LogLevel level,string message)
        {
            return $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.h3")}]{level}|{_name}|{message}";
        }
        //获取文件名
        private static string GetFilename()
        {
            var dir = "C:\\Log";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            var result = $"{dir}\\AyxFileLog-{DateTime.Now.ToString("yyyy-MM-dd")}.txt";

            return result;
        }
    }
}
