using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingStudy.Services.AyxFileLogger
{
    public static class AyxLoggerFactoryExtensions
    {
        public static ILoggerFactory AddAyxFileLogger(this ILoggerFactory factory)
        {
            factory.AddProvider(new AyxFileLoggerProvider());
            return factory;
        }
    }
}
