#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.IO;
using System.Web;
//+
namespace Minima.Web.Helper
{
    public static class MediaHelper
    {
        //- @DetectAppropriateContentType -//
        public static String DetectAppropriateContentType(String fileName)
        {
            FileInfo fi = new FileInfo(fileName);

            if (fi.Extension == ".htm" || fi.Extension == ".htm" || fi.Extension == ".xhtml")
            {
                return "text/html";
            }
            else if (fi.Extension == ".png")
            {
                return "image/png";
            }
            else if (fi.Extension == ".jpg" || fi.Extension == ".jpeg")
            {
                return "image/jpeg";
            }
            else if (fi.Extension == ".gif")
            {
                return "image/gif";
            }
            else if (fi.Extension == ".txt")
            {
                return "image/plain";
            }
            else
            {
                return "application/octet-stream";
            }
        }

        //- ~SendFileIfExists -//
        internal static Boolean SendFileIfExists(String filename)
        {
            if (File.Exists(filename))
            {
                HttpContext.Current.Response.ContentType = MediaHelper.DetectAppropriateContentType(filename);
                HttpContext.Current.Response.TransmitFile(filename);
                return true;
            }
            else
            {
                return false;
            }
        }

        //- ~SendFileIfExistsWith404OnFail -//
        internal static void SendFileIfExistsWith404OnFail(String filename)
        {
            if (!SendFileIfExists(filename))
            {
                HttpContext.Current.Response.StatusCode = 404;
                HttpContext.Current.Response.StatusDescription = "Not found";
                HttpContext.Current.Response.Write("That file does not exist");
                HttpContext.Current.Response.End();
            }
        }

        //- ~SendFileIfExistsWithRedirectOnFail -//
        internal static void SendFileIfExistsWithRedirectOnFail(String filename)
        {
            if (!SendFileIfExists(filename))
            {
                HttpContext.Current.Response.Redirect(Minima.Configuration.BlogSection.GetConfigSection().Domain);
            }
        }
    }
}