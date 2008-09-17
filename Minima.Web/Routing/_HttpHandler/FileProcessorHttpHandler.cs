using System;
using System.Linq;
using System.Web;
//+
using Minima.Configuration;
using Minima.Web.Data.Context;
using Minima.Web.Data.Entity;
using Minima.Web.Helper;
//+
using Themelia.Tracing;
using Themelia.Web;
//+
namespace Minima.Web.Routing
{
    public class FileProcessorHttpHandler : Themelia.Web.Routing.ReusableNonSessionHttpHandler
    {
        //- @ProcessRequest -//
        public override void ProcessRequest(HttpContext context)
        {
            String basePhysicalPath = MinimaConfiguration.DefaultMaterialsPhysicalPath;
            String baseVirtualPath = WebConfiguration.Domain + MinimaConfiguration.MaterialsRelativePath;
            String path = Http.Url.AbsoluteUri.Substring(baseVirtualPath.Length, Http.Url.AbsoluteUri.Length - baseVirtualPath.Length);
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
                    ReportFacade.Send("File mapping problem: " + path);
                }
            }
        }
    }
}
