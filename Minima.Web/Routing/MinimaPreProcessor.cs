using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
//+
using Themelia;
using Themelia.Web;
//+
using Minima.Service.Agent;
using Minima.Web.Routing.Component;
//+
namespace Minima.Web.Routing
{
    public class MinimaPreProcessor : Themelia.Web.Routing.PreProcessorBase
    {
        //- @OnPreHttpHandlerExecute -//
        public override void OnPreHttpHandlerExecute(HttpContext context, params Object[] parameterArray)
        {
            List<MinimaComponentSetting.MinimaInfo> parameterList = MinimaComponentSetting.CurrentComponentSetting.GetParameterList();
            MinimaComponentSetting.MinimaInfo currentInfo = parameterList.FirstOrDefault(u => u.WebSection != null && Themelia.Web.WebSection.Current.ToLower().Contains(u.WebSection.ToLower()));
            if (currentInfo != null)
            {
                HttpData.SetScopedItem<String>("Minima", "BlogGuid", currentInfo.BlogGuid);
            }
            //+ url
            DetectDestination();
            //+ blog page
            currentInfo = parameterList.FirstOrDefault(u => u.WebSection != null && Http.Url.AbsolutePath.ToLower().Contains(u.WebSection.ToLower()));
            if (currentInfo != null)
            {
                HttpData.SetScopedItem<String>("Minima", "BlogPage", currentInfo.Page);
            }
            else
            {
                currentInfo = parameterList.FirstOrDefault(u => u.WebSection != null && u.WebSection.ToLower().Equals("Root", StringComparison.InvariantCultureIgnoreCase));
                if (currentInfo != null)
                {
                    HttpData.SetScopedItem<String>("Minima", "BlogPage", currentInfo.Page);
                }
                else
                {
                    HttpData.SetScopedItem<String>("Minima", "BlogPage", "~/default.aspx");
                }
            }
        }

        //- $DetectDestination -//
        private void DetectDestination()
        {
            //+
            String finderLabel = "/label/";
            String uri = Http.Url.ToString().ToLower();
            //+ label
            if (Http.Url.ToString().ToLower().Contains(finderLabel))
            {
                String label = uri.Substring(uri.IndexOf(finderLabel) + finderLabel.Length, uri.Length - (uri.IndexOf(finderLabel) + finderLabel.Length));
                if (!String.IsNullOrEmpty(label))
                {
                    Int32 extraSlash = label.IndexOf("/");
                    if (extraSlash > -1)
                    {
                        label = label.Substring(0, extraSlash);
                    }
                    String labelTitle = String.Empty;
                    Map labelMap = HttpData.GetScopedCacheItem<Map>("Minima", "LabelMap");
                    if (labelMap == null)
                    {
                        labelMap = new Map();
                    }
                    labelTitle = labelMap.Pull(label);
                    if (String.IsNullOrEmpty(labelTitle))
                    {
                        Minima.Service.Label labelEntity = LabelAgent.GetLabelByFriendlyTitle(label);
                        if (labelEntity != null)
                        {
                            labelTitle = labelEntity.Title;
                            labelMap.Add(label, labelTitle);
                            HttpData.SetScopedCacheItem<Map>("Minima", "LabelMap", labelMap);
                        }
                    }
                    HttpData.SetScopedItem<String>("Minima", "Label", label);
                    HttpData.SetScopedItem<String>("Minima", "LabelTitle", labelTitle);
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
                    HttpData.SetScopedItem<String>("Minima", "Link", link);
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
                    HttpData.SetScopedItem<String>("Minima", "Link", link);
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
                String archive = m.Captures[0].Value;
                if (!String.IsNullOrEmpty(archive))
                {
                    HttpData.SetScopedItem<String>("Minima", "Archive", archive);
                    //+
                    String[] parts = archive.Split('/');
                    HttpData.SetScopedItem<Int32>("Minima", "ArchiveYear", Parser.ParseInt32(parts[0], 0));
                    HttpData.SetScopedItem<Int32>("Minima", "ArchiveMonth", Parser.ParseInt32(parts[1], 0));
                    return;
                }
            }
            //+ index
            String indexCapturePattern = "/index/(?<year>20[0-9]{2})";
            String indexMatchPattern = indexCapturePattern;
            if (new Regex(indexMatchPattern).IsMatch(uri))
            {
                Regex a = new Regex(indexCapturePattern);
                Match m = a.Match(uri);
                String index = m.Groups["year"].Value;
                if (!String.IsNullOrEmpty(index))
                {
                    HttpData.SetScopedItem<Int32>("Minima", "Index", Parser.ParseInt32(index, 0));
                    return;
                }
            }
        }
    }
}