using System;
using System.IO;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace CJ.Core.Log4net
{
    public class MyLogManager
    {
        private static ILoggerRepository _loggerRepository;


        private static ILoggerRepository LoggerRepository
        {
            get
            {
                //_loggerRepository = LogManager.GetRepository("NETCoreRepository");
                if (_loggerRepository != null)
                {
                    return _loggerRepository;
                }
                _loggerRepository = LogManager.CreateRepository("NETCoreRepository");
                XmlConfigurator.ConfigureAndWatch(_loggerRepository, new FileInfo("Configs/log4net.config"));
                return _loggerRepository;
            }
        }
        private static ILog GetMyLog<T>(T t)
        {
            return LogManager.GetLogger(LoggerRepository.Name, t.GetType());
        }

        private static ILog GetMyLog(object obj)
        {
            return LogManager.GetLogger(LoggerRepository.Name, obj.GetType());
        }

        private static ILog GetMyLog(Type type)
        {
            return LogManager.GetLogger(LoggerRepository.Name, type);
        }

        private static ILog GetMyLog()
        {
            return LogManager.GetLogger(LoggerRepository.Name, nameof(GetMyLog));
        }


        public static void Info(string str, Type type)
        {
            ILog log = LogManager.GetLogger(LoggerRepository.Name, type);
            log.Info(str);
        }
        public static void Error(string str, Type type)
        {
            ILog log = LogManager.GetLogger(LoggerRepository.Name, type);

            log.Error(str);
        }
        public static void Error(string str, System.Exception ex, Type type)
        {
            ILog log = LogManager.GetLogger(LoggerRepository.Name, type);

            string errorMsg = string.Format("【抛出信息】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2}", new object[] {
                str,ex.GetType().Name, ex.Message});
            //errorMsg = errorMsg.Replace("\r\n", "<br>");
            //errorMsg = errorMsg.Replace("位置", "<strong style=\"color:red\">位置</strong>");
            log.Error(errorMsg);
        }
        public static void Debug(string str, Type type)
        {
            ILog log = LogManager.GetLogger(LoggerRepository.Name, type);
            log.Debug(str);
        }
        public static void Warning(string str, Type type)
        {
            ILog log = LogManager.GetLogger(LoggerRepository.Name, type);
            log.Warn(str);
        }
    }
}