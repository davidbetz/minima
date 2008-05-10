using System;
//+
using General;
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
                return ConfigurationFacade.ConnectionString("Minima.Service.Properties.Settings.MinimaConnectionString");
            }
        }
    }
}