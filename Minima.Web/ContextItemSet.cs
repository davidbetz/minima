using System;
//+
namespace Minima.Web
{
    public static class ContextItemSet
    {
        //- @BlogGuid -//
        public static String BlogGuid
        {
            get
            {
                return System.Web.HttpContext.Current.Items["BlogGuid"] as String;
            }
        }

        //- @BlogPage -//
        public static String BlogPage
        {
            get
            {
                return System.Web.HttpContext.Current.Items["BlogPage"] as String;
            }
        }
    }
}