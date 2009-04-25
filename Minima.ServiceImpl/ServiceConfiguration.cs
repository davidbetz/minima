#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
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