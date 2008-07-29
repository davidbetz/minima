using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
//+
using Themelia.Tracing;
//+
namespace Minima.Web.Tracing
{
    internal class TraceSerializer
    {
        //- ~Serialize -//
        internal static String Serialize(params Object[] items)
        {
            List<Object> l = new List<Object>(items);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Object>));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            StringBuilder b = new StringBuilder();
            TextWriter writer = new StringWriter(b);
            try
            {
                serializer.Serialize(writer, l, ns);
            }
            catch (Exception ex)
            {
                EmailReporter.Send(ex);
            }
            return b.ToString();
        }
    }
}