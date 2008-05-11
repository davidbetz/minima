using System;
//+
namespace Minima.Web.Helper
{
    public class UrlHelper
    {
        //- @FixWebPath -//
        public static String FixWebPath(String path)
        {
            if (path.EndsWith("/"))
            {
                path = path.Substring(0, path.Length - 1);
            }
            if (path.StartsWith("/"))
            {
                path = path.Substring(1, path.Length - 1);
            }
            //+
            return path;
        }
    }
}