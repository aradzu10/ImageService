using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceUI.Messages
{
    public enum MessageTypeEnum : int
    {
        SETTINGS,
        L_INFO,
        L_WARNING,
        L_FAIL,
        REMOVE_HANDLER,
        UNKNOWN
    }
}
