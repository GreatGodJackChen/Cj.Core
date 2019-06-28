using System;
using Microsoft.Extensions.Logging;

namespace CJ.Data.Logger
{
    public class NullLogge: ILogger
    {
        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        { }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}