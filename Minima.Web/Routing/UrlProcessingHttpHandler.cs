using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
//+
namespace Minima.Web.Routing
{
    /// <summary>
    /// HttpHandler to detect if a URL is accessing a label, a link or archive.
    /// </summary>
    public class UrlProcessingHttpHandler : IHttpHandler, IRequiresSessionState
    {
        public Boolean IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            Route(context);

            IHttpHandler h = PageParser.GetCompiledPageInstance("~/default.aspx", null, context);
            h.ProcessRequest(context);
        }

        private static void Route(HttpContext context)
        {
            String finderLabel = "/label/";
            String uri = context.Request.Url.ToString().ToLower();
            //+ label
            if (context.Request.Url.ToString().ToLower().Contains(finderLabel))
            {
                String label = uri.Substring(uri.IndexOf(finderLabel) + finderLabel.Length, uri.Length - (uri.IndexOf(finderLabel) + finderLabel.Length));
                if (!String.IsNullOrEmpty(label))
                {
                    context.Items.Add("label", label);
                    return;
                }
            }
            //+ link
            String linkCapturePattern = "[\\-a-z0-9]+$";
            String linkMatchPattern = "\\/200\\d\\/\\d{2}\\/[\\-a-z0-9]+$";
            String linkMatchUri = String.Empty;
            if (uri[uri.Length - 1] == '/')
            {
                linkMatchUri = uri.Substring(0, uri.Length - 1);
            }
            else
            {
                linkMatchUri = uri;
            }
            if (new Regex(linkMatchPattern).IsMatch(linkMatchUri))
            {
                Regex a = new Regex(linkCapturePattern);
                Match m = a.Match(linkMatchUri);
                String link = m.Captures[0].Value;
                // link = link.Substring(0, link.Length - 5);
                if (!String.IsNullOrEmpty(link))
                {
                    context.Items.Add("link", link);
                    return;
                }
            }
            linkCapturePattern = "[\\-a-z0-9]+\\.aspx";
            linkMatchPattern = "\\/200\\d\\/\\d{2}\\/[\\-a-z0-9]+\\.aspx";
            if (new Regex(linkMatchPattern).IsMatch(uri))
            {
                Regex a = new Regex(linkCapturePattern);
                Match m = a.Match(uri);
                String link = m.Captures[0].Value;
                link = link.Substring(0, link.Length - 5);
                if (!String.IsNullOrEmpty(link))
                {
                    context.Items.Add("link", link);
                    return;
                }
            }
            //+ archive
            String archiveCapturePattern = "200(\\d)\\/(\\d{2})";
            String archiveMatchPattern = "\\/" + archiveCapturePattern;
            if (new Regex(archiveMatchPattern).IsMatch(uri))
            {
                Regex a = new Regex(archiveCapturePattern);
                Match m = a.Match(uri);
                String label = m.Captures[0].Value;
                if (!String.IsNullOrEmpty(label))
                {
                    context.Items.Add("archive", label);
                    return;
                }
            }
        }
    }
}