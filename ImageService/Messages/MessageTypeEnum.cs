using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public enum MessageTypeEnum : int
    {
        L_INFO,
        L_WARNING,
        L_FAIL,
        REMOVE_HANDLER,
        UNKNOWN
    }
}
