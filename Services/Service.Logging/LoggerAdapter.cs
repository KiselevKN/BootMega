using Microsoft.Practices.Prism.Logging;
using NLog;

namespace Service.Logging
{
    public class LoggerAdapter : ILoggerFacade
    {
        #region Fields

        private static Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region ILoggerFacade Members

        public void Log(string message, Category category, Priority priority = Priority.None)
        {
            switch (category)
            {
                case Category.Debug:
                        logger.Trace(message);
                    break;
                case Category.Warn:
                        logger.Warn(message);
                    break;
                case Category.Exception:
                        logger.Error(message);
                    break;
                case Category.Info:
                        logger.Info(message);
                    break;
            }
        }

        #endregion
    }
}
