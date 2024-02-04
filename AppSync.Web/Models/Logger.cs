using log4net;
using System.Reflection;

namespace AppSync.Web.Models
{
    public sealed class Logger
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Lazy<Logger> _loggerInstance = new Lazy<Logger>(() => new Logger());

        private const string ExceptionName = "Exception";
        private const string InnerExceptionName = "Inner Exception";
        private const string ExceptionMessageWithoutInnerException = "{0}{1}: {2}Message: {3}{4}StackTrace: {5}.";
        private const string ExceptionMessageWithInnerException = "{0}{1}{2}";

        //Gets the Logger instance.
        public static Logger Instance
        {
            get { return _loggerInstance.Value; }
        }

        //Logs a message object with the log4net.Core.Level.Debug level.
        public void Debug(object message)
        {
            if (_logger.IsDebugEnabled)
                _logger.Debug(message);
        }

        //Logs a message object with the log4net.Core.Level.Debug level including the exception.
        public void Debug(object message, Exception exception)
        {
            if (_logger.IsDebugEnabled)
                _logger.Debug(message, exception);
        }

        //Logs a message object with the log4net.Core.Level.Error level.
        public void Error(object message)
        {
            _logger.Error(message);
        }

        //Logs a message object with the log4net.Core.Level.Error level including the exception.
        public void Error(object message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        //Log an exception with the log4net.Core.Level.Error level including the stack trace of the System.Exception passed as a parameter.
        public void Error(Exception exception)
        {
            _logger.Error(SerializeException(exception, ExceptionName));
        }

        //Logs a message object with the log4net.Core.Level.Fatal level.
        public void Fatal(object message)
        {
            _logger.Fatal(message);
        }

        //Logs a message object with the log4net.Core.Level.Fatal level including the exception.
        public void Fatal(object message, Exception exception)
        {
            _logger.Fatal(message, exception);
        }

        //Log an exception with the log4net.Core.Level.Fatal level including the stack trace of the System.Exception passed as a parameter.
        public void Fatal(Exception exception)
        {
            _logger.Fatal(SerializeException(exception, ExceptionName));
        }

        //Logs a message object with the log4net.Core.Level.Info level.
        public void Info(object message)
        {
            if (_logger.IsInfoEnabled)
                _logger.Info(message);
        }

        //Logs a message object with the log4net.Core.Level.Info level including the exception.
        public void Info(object message, Exception exception)
        {
            if (_logger.IsInfoEnabled)
                _logger.Info(message, exception);
        }

        //Logs a message object with the log4net.Core.Level.Info Warning.
        public void Warn(object message)
        {
            if (_logger.IsWarnEnabled)
                _logger.Warn(message);
        }

        //Logs a message object with the log4net.Core.Level.Warn level including the exception.
        public void Warn(object message, Exception exception)
        {
            if (_logger.IsWarnEnabled)
                _logger.Info(message, exception);
        }

        //Serialize Exception to get the complete message and stack trace.
        private static string SerializeException(Exception ex, string exceptionMessage)
        {
            var mesgAndStackTrace = string.Format(ExceptionMessageWithoutInnerException, Environment.NewLine,
                exceptionMessage, Environment.NewLine, ex.Message, Environment.NewLine, ex.StackTrace);

            if (ex.InnerException != null)
            {
                mesgAndStackTrace = string.Format(ExceptionMessageWithInnerException, mesgAndStackTrace,
                    Environment.NewLine,
                    SerializeException(ex.InnerException, InnerExceptionName));
            }

            return mesgAndStackTrace + Environment.NewLine;
        }
    }
}
