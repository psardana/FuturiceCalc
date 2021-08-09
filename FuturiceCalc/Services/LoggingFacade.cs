using System;
using FuturiceCalc.Services.Contracts;
using Serilog;

namespace FuturiceCalc.Services
{
    /// <summary>
    /// implements custom log facade
    /// </summary>
    public class LoggingFacade : ILoggingFacade
    {
        public void LogError(Exception exception)
        {
            WriteLogEntry($"{exception.Message}", exception);
        }

        public void LogInfo(string message)
        {
            Log.Information(message);
        }

        private void WriteLogEntry(string message, Exception ex)
        {
            Log.Error(ex, message);
        }
    }
}
