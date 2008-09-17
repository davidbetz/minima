using System;
using System.IO;
using System.Text;
//+
using Themelia.Tracing;
//+
namespace Minima.Web.Tracing
{
    public static class StreamConverter
    {
        //- @GetBytes -//
        public static Byte[] GetBytes(Stream stream)
        {
            Byte[] buffer = new Byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            //+
            return buffer;
        }

        //- @GetString -//
        public static String GetString(Stream stream)
        {
            Byte[] buffer = new Byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            ReportFacade.Send(buffer.Length.ToString());
            //+
            return Encoding.UTF8.GetString(buffer);
        }
    }
}