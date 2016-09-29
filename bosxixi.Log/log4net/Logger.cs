using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Log.log4net
{
    public class Logger : ILogger
    {
        private readonly ILog log;

        public Logger([CallerFilePath]string currentClassName = null)
        {
            this.log = LogManager.GetLogger(currentClassName);
        }

        public void Debug(object message)
        {
            this.log.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {

            this.log.Debug(message, exception);
        }

        public Task DebugAsync(object message)
        {
            var task = new Task(() => { this.log.Debug(message); });
            task.Start();
            return task;
        }

        public Task DebugAsync(object message, Exception exception)
        {
            var task = new Task(() => { this.log.Debug(message, exception); });
            task.Start();
            return task;
        }

        public void Error(object message)
        {
            this.log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            this.log.Error(message, exception);
        }

        public Task ErrorAsync(object message)
        {
            var task = new Task(() => { this.log.Error(message); });
            task.Start();
            return task;
        }

        public Task ErrorAsync(object message, Exception exception)
        {
            var task = new Task(() => { this.log.Error(message, exception); });
            task.Start();
            return task;
        }

        public void Fatal(object message)
        {
            this.log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            this.log.Fatal(message, exception);
        }

        public Task FatalAsync(object message)
        {
            var task = new Task(() => { this.log.Fatal(message); });
            task.Start();
            return task;
        }

        public Task FatalAsync(object message, Exception exception)
        {
            var task = new Task(() => { this.log.Fatal(message, exception); });
            task.Start();
            return task;
        }

        public void Info(object message)
        {
            this.log.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            this.log.Info(message, exception);
        }

        public Task InfoAsync(object message)
        {
            var task = new Task(() => { this.log.Info(message); });
            task.Start();
            return task;
        }

        public Task InfoAsync(object message, Exception exception)
        {
            var task = new Task(() => { this.log.Info(message, exception); });
            task.Start();
            return task;
        }

        public void Warn(object message)
        {
            this.log.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            this.log.Warn(message, exception);
        }

        public Task WarnAsync(object message)
        {

            var task = new Task(() => { this.log.Warn(message); });
            task.Start();
            return task;
        }

        public Task WarnAsync(object message, Exception exception)
        {
            var task = new Task(() => { this.log.Warn(message, exception); });
            task.Start();
            return task;
        }
    }
}
