using System;
using ImageService.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logger
{
    public interface ILogger
    {
        event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        void Log(string message, MessageTypeEnum type);
        void Log(MessageRecievedEventArgs message);
    }
}
