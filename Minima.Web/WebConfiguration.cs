using System;
using System.Configuration;
using System.Diagnostics;
//+
using General.Configuration;
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
                return General.Web.UrlHelper.FixWebPath(ConfigAccessor.ApplicationSettings("Domain"));
            }
        }

        //- @SiteName -//
        public static String SiteName
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("SiteName");
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