using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceWebApp.Models.Messages
{
    public enum MessageTypeEnum : int
    {
        SETTINGS,
        L_INFO,
        L_WARNING,
        L_FAIL,
        REMOVE_HANDLER,
        P_SEND,
        P_SENDALL,
        P_DELETE,
        UNKNOWN
    }
}
