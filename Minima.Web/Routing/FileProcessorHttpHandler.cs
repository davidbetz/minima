using System;
using System.Linq;
using System.Web;
//+
using Minima.Web.Data.Context;
using Minima.Web.Data.Entity;
using Minima.Web.Helper;
using Minima.Web.Agent;
using General.ExceptionHandling;
using Minima.Configuration;
//+
namespace Minima.Web.Routing
{
    public class FileProcessorHttpHandler : IHttpHandler
    {
        //- @IsReusable -//
        public Boolean IsReusable
        {
            get { return true; }
        }

        //- @ProcessRequest -//
        public void ProcessRequest(HttpContext context)
        {
            String basePhysicalPath = MinimaConfiguration.DefaultMaterialsPhysicalPath;
            String baseVirtualPath = WebConfiguration.Domain + MinimaConfiguration.MaterialsRelativePath;
            String path = context.Request.Url.AbsoluteUri.Substring(baseVirtualPath.Length, context.Request.Url.AbsoluteUri.Length - baseVirtualPath.Length);
            //+
            using (MinimaWebLINQDataContext db = new MinimaWebLINQDataContext(WebConfiguration.ConnectionString))
            {
                FileMapping fileMapping = db.FileMappings.SingleOrDefault(p => p.FileMappingUrl == path);
                if (fileMapping != null)
                {
                    if (fileMapping != null)
                    {
                        MediaHelper.SendFileIfExistsWith404OnFail(fileMapping.FileMappingName);
                    }
                    else
                    {
                        MediaHelper.SendFileIfExistsWith404OnFail(basePhysicalPath + path.Replace("/", "\\"));
                    }
                }
                else
                {
                    ExceptionManager.Report("File mapping problem: " + path);
                }
            }
        }
    }
}
