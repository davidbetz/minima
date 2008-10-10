#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Configuration;
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

        //- @Domain -//
        public static String Domain
        {
            get
            {
                return Themelia.Web.UrlCleaner.FixWebPath(ConfigAccessor.ApplicationSettings("Domain"));
            }
        }

        //- @GenericErrorMessage -//
        public static String GenericErrorMessage
        {
            get
            {
                String value = ConfigAccessor.ApplicationSettings("GenericErrorMessage");
                if (String.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("GenericErrorMessage is required.");
                }
                //+
                return value;
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