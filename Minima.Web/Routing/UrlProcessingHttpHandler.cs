using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
//+
using General.Web;
//+
using Minima.Web.Configuration;
//+
namespace Minima.Web.Routing
{
    public class UrlProcessingHttpHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //- @IsReusable -//
        public Boolean IsReusable
        {
            get { return true; }
        }

        //- @ProcessRequest -//
        public void ProcessRequest(HttpContext context)
        {
            Route(context);
            //+
            String pageLocation = String.Empty;
            //+
            List<InstanceElement> instanceElementList = MinimaConfigurationFacade.GetWebConfiguration().Registration.OrderBy(p => p.Priority).ToList();
            InstanceElement t = instanceElementList.FirstOrDefault(u => u.WebSection != null && Http.Url.AbsolutePath.ToLower().Contains(u.WebSection.ToLower()));
            if (t != null)
            {
                pageLocation = t.Page;
            }
            else
            {
                t = instanceElementList.FirstOrDefault(u => u.WebSection != null && u.WebSection.Equals("Root", StringComparison.InvariantCultureIgnoreCase));
                if (t != null)
                {
                    pageLocation = t.Page;
                }
                else
                {
                    pageLocation = "~/default.aspx";
                }
            }
            //+
            IHttpHandler h = System.Web.UI.PageParser.GetCompiledPageInstance(pageLocation, null, context);
            h.ProcessRequest(context);
        }

        //- $Route -//
        private static void Route(HttpContext context)
        {
            String finderLabel = "/label/";
            String uri = Http.Url.ToString().ToLower();
            //+ label
            if (Http.Url.ToString().ToLower().Contains(finderLabel))
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