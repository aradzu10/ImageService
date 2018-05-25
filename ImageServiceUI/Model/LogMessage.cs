using ImageServiceUI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceUI.Model
{
    class LogMessage
    {
        private static Dictionary<MessageTypeEnum, string> messages = new Dictionary<MessageTypeEnum, string>()
        { { MessageTypeEnum.L_FAIL, "ERROR" }, { MessageTypeEnum.L_INFO, "INFO" }, { MessageTypeEnum.L_WARNING, "WARNINGS" } };

        public string Type { get; private set; }
        public string Message { get; private set; }

        public LogMessage(MessageRecievedEventArgs message) : this(message.Status, message.Message) { }

        public LogMessage(MessageTypeEnum t, string m)
        {
            Type = messages[t];
            Message = m;
        }
    }
}
