using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceWebApp.Models.Messages
{
    public class LogMessage
    {
        public static Dictionary<MessageTypeEnum, string> messages = new Dictionary<MessageTypeEnum, string>()
        { { MessageTypeEnum.L_FAIL, "ERROR" }, { MessageTypeEnum.L_INFO, "INFO" }, { MessageTypeEnum.L_WARNING, "WARNING" } };

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string Type { get; private set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Message")]
        public string Message { get; private set; }

        public LogMessage(MessageRecievedEventArgs message) : this(message.Status, message.Message) { }

        public LogMessage(MessageTypeEnum t, string m)
        {
            Type = messages[t];
            Message = m;
        }
    }
}
