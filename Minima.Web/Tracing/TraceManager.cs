using System;
using System.IO;
using Minima.Configuration;
//+
namespace Minima.Web.Tracing
{
    internal class TraceManager
    {
        //- ~Record -//
        internal static void Record(params Object[] items)
        {
            if (MinimaConfiguration.EnableTracingViaSerialization)
            {
                TraceStorage.RecordInformationMessage(TraceSerializer.Serialize(items));
            }
        }

        //- ~RecordMethodCall -//
        internal static void RecordMethodCall(String methodName, params Object[] items)
        {
            if (MinimaConfiguration.EnableTracingViaSerialization)
            {
                String message = TraceSerializer.Serialize(items);
                TraceStorage.RecordInformationMessage(methodName, message);
            }
        }

        //- ~TraceRequest -//
        internal static void TraceRequest(String managedMethod, Stream stream)
        {
            if (MinimaConfiguration.EnableTracingViaSerialization)
            {
                TraceStorage.RecordInformationMessage(managedMethod, StreamConverter.GetString(stream));
            }
        }
    }
}