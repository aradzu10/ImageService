using System;

namespace ImageService.Enums
{
    public enum SC_MessageType : int
    {
        S_outputDir,
        S_sourceName,
        S_logName,
        S_thumbSize,
        S_dirListener,
        L_info,
        L_warning,
        L_fail,
        RemoveHandler,
        Unknown
    }
}