#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Diagnostics;
//+
using Themelia.Configuration;
//+
namespace Minima.Web
{
    public static class WebConfiguration
    {
        //- @ConnectionString -//
        public static String ConnectionString
        {
            get
            {
                return ConfigAccessor.ConnectionString("Minima.Web.Properties.Settings.MinimaConnectionString");
            }
        }

        //- @GlobalTraceSwitch -//
        public static TraceSwitch GlobalTraceSwitch
        {
            get
            {
                return new TraceSwitch("TracingSwitch", "Global Tracing Switch");
            }
        }
    }
}