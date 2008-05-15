using System;
//+
using General.Configuration;
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
                return ConfigAccessor.ConnectionString("Minima.Service.Properties.Settings.MinimaConnectionString");
            }
        }
    }
}