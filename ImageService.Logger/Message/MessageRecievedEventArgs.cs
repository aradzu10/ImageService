using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging.Modal
{
    public class MessageRecievedEventArgs : EventArgs
    {
        private MessageTypeEnum Status { get; set; }
        private string Message { get; set; }

        public MessageRecievedEventArgs(MessageTypeEnum messageType, string message)
        {
            Status = messageType;
            Message = message;
        }

    }
}
