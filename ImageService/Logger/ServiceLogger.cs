using Logger.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class ServiceLogger : ILogger
    {
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;

        public void log(string message, MessageTypeEnum type)
        {
            MessageRecieved?.Invoke(this, new MessageRecievedEventArgs(type, message));
        }
    }
}
