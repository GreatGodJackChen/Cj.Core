using System;
using Microsoft.Extensions.Logging;

namespace CJ.Data.Logger
{
    public class MyFilteredLoggerProvider: ILoggerProvider
    {
        private Func<string, LogLevel, bool> p;

        public MyFilteredLoggerProvider(Func<string, LogLevel, bool> func)
        {
            p = func;
        }

        public MyFilteredLoggerProvider()
        {

        }

        public ILogger CreateLogger(string categoryName)
        {
            if (p.Invoke(categoryName, LogLevel.Information))
            {
                return new EfLogger(categoryName);
            }
            return new NullLogge();
        }
        public void Dispose()
        { }
    }
}