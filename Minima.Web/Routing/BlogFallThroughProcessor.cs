#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
namespace Minima.Web.Routing
{
    public class BlogFallThroughProcessor : Themelia.Web.Routing.FallThroughProcessorBase
    {
        //- @MatchHttpHandler -//
        public override System.Web.IHttpHandler GetHandler(System.Web.HttpContext context, String requestType, String virtualPath, String path, params Object[] parameterArray)
        {
            if (!String.IsNullOrEmpty(Themelia.Web.WebDomain.Current))
            {
                return new UrlProcessingHttpHandler();
            }
            //+
            return null;
        }
    }
}