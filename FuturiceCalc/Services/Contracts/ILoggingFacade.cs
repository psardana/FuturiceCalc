using System;

namespace FuturiceCalc.Services.Contracts
{
    /// <summary>
    /// facade to log errors and info
    /// </summary>
    public interface ILoggingFacade
    {
        /// <summary>
        /// logs error
        /// </summary>
        /// <param name="exception"></param>
        void LogError(Exception exception);

        /// <summary>
        /// logs infor
        /// </summary>
        /// <param name="message"></param>
        void LogInfo(string message);
    }
}
