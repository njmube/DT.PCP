using System;

namespace DT.PCP.Logging
{
    public interface ILogger
    {
        void Debug(string msg);
        void Error(string msg, Exception exception);
    }
}
