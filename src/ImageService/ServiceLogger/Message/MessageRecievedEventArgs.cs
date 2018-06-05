using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Message
{
    /// <summary>
    /// log details
    ///     /// </summary>
    public class MessageRecievedEventArgs : EventArgs
    {
        public MessageTypeEnum Status { get; set; }
        public string Message { get; set; }

        public MessageRecievedEventArgs(MessageTypeEnum messageType, string message)
        {
            Status = messageType;
            Message = message;
        }

    }
}
