using System;
using System.Web;
//+
using Minima.Configuration;
using Minima.Web.Helper;
//+
namespace Minima.Web.Routing
{
    public class BlogEntryImageHttpHandler : IHttpHandler
    {
        //- @IsReusable -//
        public Boolean IsReusable
        {
            get { return true; }
        }

        //+
        //- @ProcessRequest -//
        public void ProcessRequest(HttpContext context)
        {
            String basePhysicalPath = MinimaConfiguration.SupportImagePhysicalLocation;
            if (basePhysicalPath.EndsWith("\\"))
            {
                basePhysicalPath = basePhysicalPath.Substring(0, basePhysicalPath.Length - 1);
            }
            String baseVirtualPath = WebConfiguration.Domain + MinimaConfiguration.SupportImageWebRelativePath;
            String path = context.Request.Url.AbsoluteUri.Substring(baseVirtualPath.Length, context.Request.Url.AbsoluteUri.Length - baseVirtualPath.Length);
            //+
            MediaHelper.SendFileIfExistsWith404OnFail(basePhysicalPath + "\\" + path.Replace("%20", " ").Replace("/", "\\"));
        }
    }
}