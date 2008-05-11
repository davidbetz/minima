using System;
//+
using Minima.Service.Behavior;
using Minima.Service.Technorati;
using Minima.Service.Validation;
//+
using BlogLINQ = Minima.Service.Data.Entity.Blog;
//+
using MinimaServiceLINQDataContext = Minima.Service.Data.Context.MinimaServiceLINQDataContext;
//+
namespace Minima.Service
{
    public class TechnoratiService : ITechnoratiService
    {
        //- @PingTechnorati -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Update)]
        public void PingTechnorati(String blogGuid)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog entry exists
                BlogLINQ blogLinq;
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                //+
                TechnoratiNotifier.Ping(blogLinq.BlogTitle, new Uri(blogLinq.BlogPrimaryUrl));
            }
        }
    }
}