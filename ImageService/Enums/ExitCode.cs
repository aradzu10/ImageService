using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Enums
{
    public enum ExitCode : int
    {
        Failed,
        Success,
        F_Invalid_Input,
        F_Missing_Date,
        F_Create_Dir,
        F_Create_Thumb,
        F_Move
    }
}
