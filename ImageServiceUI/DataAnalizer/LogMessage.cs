using ImageServiceUI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceUI.DataAnalizer
{
    class LogMessage
    {
        private static Dictionary<MessageTypeEnum, string> messages = new Dictionary<MessageTypeEnum, string>()
        { { MessageTypeEnum.L_FAIL, "ERROR" }, { MessageTypeEnum.L_INFO, "INFO" }, { MessageTypeEnum.L_WARNING, "WARNINGS" } };

        public string type { get; set; }
        public string message { get; set; }

        public LogMessage(MessageTypeEnum t, string m)
        {
            type = messages[t];
            message = m;
        }
    }
}
