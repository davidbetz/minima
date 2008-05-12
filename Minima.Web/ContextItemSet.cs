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

        //- @WebSection -//
        public static String WebSection
        {
            get
            {
                return System.Web.HttpContext.Current.Items["WebSection"] as String;
            }
        }
    }
}