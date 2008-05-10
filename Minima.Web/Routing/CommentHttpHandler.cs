using System;
using System.Text.RegularExpressions;
using System.Web;
using Minima.Web.Agent;
//+
namespace Minima.Web.HttpExtensions
{
    public class MinimaCommentHttpHandler : IHttpHandler
    {
        //- @IsReusable -//
        public Boolean IsReusable
        {
            get { return true; }
        }

        //- @ProcessRequest -//
        public void ProcessRequest(HttpContext context)
        {
            String pathSignature = "Service/Comment/";
            String[] parts = Regex.Split(context.Request.Url.AbsoluteUri, pathSignature);

            if (parts.Length > 1 && parts[1].Length > 0)
            {
                AuthorizeComment(context, parts[1]);
            }
            else
            {
                context.Response.Write("Invalid comment guid.");
            }
        }

        //- $AuthorizeComment -//
        private void AuthorizeComment(HttpContext context, String commentGuid)
        {
            try
            {
                CommentAgent.AuthorizeComment(commentGuid);
            }
            catch
            {
                context.Response.Write("Invalid comment guid or error.");
                //+
                return;
            }
            //+
            context.Response.Write("Comment authorized");
        }
    }
}