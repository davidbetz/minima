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