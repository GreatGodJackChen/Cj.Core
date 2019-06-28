using System;
using CJ.Core.Log4net;
using Microsoft.Extensions.Logging;

namespace CJ.Data.Logger
{
    public class EfLogger: ILogger
    {
        private readonly string _categoryName;

        public EfLogger(string categoryName)
        {
            this._categoryName = categoryName;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Information:
                    MyLogManager.Info(formatter(state, null), this.GetType());
                    break;
                case LogLevel.Debug:
                    MyLogManager.Debug(formatter(state, exception), this.GetType());
                    break;
                case LogLevel.Error:
                    MyLogManager.Error(formatter(state, exception), exception, this.GetType());
                    break;
                case LogLevel.Warning:
                    MyLogManager.Warning(formatter(state, exception), this.GetType());
                    break;
            }
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}