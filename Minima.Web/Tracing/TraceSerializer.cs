using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
//+
using Themelia.ExceptionHandling;
//+
namespace Minima.Web.Tracing
{
    internal class TraceSerializer
    {
        //- $CombineObjects -//
        private static List<Object> CombineObjects(params Object[] items)
        {
            List<Object> l = new List<Object>();
            foreach (Object o in items)
            {
                l.Add(o);
            }
            return l;
        }

        //- ~Serialize -//
        internal static String Serialize(params Object[] items)
        {
            List<Object> l = CombineObjects(items);
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
                Reporter.Send(ex);
            }
            return b.ToString();
        }
    }
}