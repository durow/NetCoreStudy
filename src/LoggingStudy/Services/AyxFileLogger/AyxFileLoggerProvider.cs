using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingStudy.Services.AyxFileLogger
{
    public class AyxFileLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new AyxFileLogger(categoryName);
        }

        public void Dispose()
        {
        }
    }
}
