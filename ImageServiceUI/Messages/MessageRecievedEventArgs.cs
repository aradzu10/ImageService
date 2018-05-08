using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Messages
{
    public class MessageRecievedEventArgs : EventArgs
    {
        public MessageTypeEnum Status { get; set; }
        public string Message { get; set; }

        public MessageRecievedEventArgs(MessageTypeEnum messageType, string message)
        {
            Status = messageType;
            Message = message;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
