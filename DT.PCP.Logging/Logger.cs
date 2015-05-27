using System;
using System.IO;
using NLog;
using NLog.Config;


namespace DT.PCP.Logging
{
    /// <summary>
    /// Представляет логгер
    /// </summary>
    public class Logger:ILogger
    {
        private static NLog.Logger _logger;

        public void Debug(string msg)
        {
            _logger.Debug(msg);
            
        }

        public void Error(string msg, Exception exception)
        {
            try
            {
                _logger.Error(msg, exception);
            }
            catch (Exception)
            {}
            
        }
        
        public static void Configure(string configPath)
        {
            LoggingConfiguration conf = new XmlLoggingConfiguration(configPath);
            LogManager.Configuration = conf;
            _logger = LogManager.GetCurrentClassLogger();
        }
    }
}
