#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
namespace Minima.Service.Helper
{
    internal static class GuidCreator
    {
        //- @NewDatabaseGuid -//
        public static String NewDatabaseGuid
        {
            get
            {
                return Guid.NewGuid().ToString().Replace("{", "").Replace("}", "");
            }
        }
    }
}