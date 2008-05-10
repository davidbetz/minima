using System;
using System.Web;
//+
namespace Minima.Web.Routing
{
    public class MinimaBaseHttpHandler : IHttpHandler
    {
        //- @IsReusable -//
        public Boolean IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        //+
        //- @ProcessRequest -//
        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}