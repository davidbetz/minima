#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Text;
using System.Web;
//+
using Minima.Web.Data.Context;
//+
using TraceLINQ = Minima.Web.Data.Entity.Trace;
//+
namespace Minima.Web.Tracing
{
    internal class TraceStorage
    {
        //- ~RecordExceptionMessage -//
        internal static void RecordExceptionMessage(String message)
        {
            Exception ex = HttpContext.Current.Server.GetLastError();
            RecordException(message, ex);
        }

        //- ~RecordInformationMessage -//
        internal static void RecordInformationMessage(String message)
        {
            TraceLINQ trace = new TraceLINQ();
            RecordMessage(trace, message, 2);
        }

        //- ~RecordInformationMessage -//
        internal static void RecordInformationMessage(String managedMethod, String message)
        {
            TraceLINQ trace = new TraceLINQ
            {
                TraceExtra = message
            };
            RecordMessage(trace, managedMethod, 2);
        }

        //- $RecordException -//
        private static void RecordException(String message, Exception ex)
        {
            TraceLINQ trace = new TraceLINQ();
            trace.TraceStackTrace = ex.StackTrace;
            trace.TraceExtra = ex.Source;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("==Message==");
            builder.AppendLine(message);
            builder.AppendLine("==Exception==");
            builder.AppendLine(ex.Message);
            for (Exception innerException = ex.InnerException; innerException != null; innerException = innerException.InnerException)
            {
                builder.AppendLine("==InnerException.Message===");
                builder.AppendLine("===" + innerException.Message + "===");
                if ((innerException.Data != null) && (innerException.Data.Count > 0))
                {
                    builder.AppendLine("===InnerException.Data===");
                    foreach (String data in innerException.Data)
                    {
                        builder.AppendLine(string.Format("{0}, {1}", data, innerException.Data[data]));
                    }
                }
                builder.AppendLine("==InnerException.StackTrace==");
                builder.AppendLine(innerException.StackTrace);
            }
            //+
            RecordMessage(trace, message, 1);
        }

        //- $RecordMessage -//
        private static void RecordMessage(TraceLINQ trace, String message, int typeId)
        {
            HttpRequest request = HttpContext.Current.Request;
            trace.TraceAddress = request.ServerVariables["REMOTE_ADDR"];
            trace.TraceMessage = message;
            trace.TraceTypeId = typeId;
            trace.TraceUserAgent = request.UserAgent;
            trace.TraceUrl = request.Url.AbsoluteUri;
            using (MinimaWebLINQDataContext db = new MinimaWebLINQDataContext(WebConfiguration.ConnectionString))
            {
                db.Traces.InsertOnSubmit(trace);
                db.SubmitChanges();
            }
        }
    }
}