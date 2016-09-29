using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Log
{
    public interface ILogger
    {
        void Debug(object message);
        void Debug(object message, Exception exception);
        Task DebugAsync(object message);
        Task DebugAsync(object message, Exception exception);

        void Error(object message);
        void Error(object message, Exception exception);
        Task ErrorAsync(object message);
        Task ErrorAsync(object message, Exception exception);
        void Fatal(object message);
        void Fatal(object message, Exception exception);
        Task FatalAsync(object message);
        Task FatalAsync(object message, Exception exception);
        void Info(object message);
        void Info(object message, Exception exception);
        Task InfoAsync(object message);
        Task InfoAsync(object message, Exception exception);
        void Warn(object message);
        void Warn(object message, Exception exception);
        Task WarnAsync(object message);
        Task WarnAsync(object message, Exception exception);
    }
}
