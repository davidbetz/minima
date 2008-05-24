using System;
//+
//+
namespace Minima.Service
{
    public static class ServiceConfiguration
    {
        //- @ConnectionString -//
        public static String ConnectionString
        {
            get
            {
                return Themelia.Configuration.ConfigAccessor.ConnectionString("Minima.Service.Properties.Settings.MinimaConnectionString");
            }
        }
    }
}