#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
//+
using Minima.Service.Agent;
//+
using Themelia;
using Themelia.Web;
using Themelia.Web.Routing.Data;
//+
namespace Minima.Web.Routing
{
    public class PreProcessor : Themelia.Web.Routing.PreProcessorBase
    {
        //- @LabelMap -//
        public static Map LabelMap { get; set; }

        //- @Ctor -//
        static PreProcessor()
        {
            LabelMap = new Map();
        }

        //+
        //- @OnPreProcessorExecute -//
        public override void OnPreProcessorExecute(HttpContext context, params Object[] parameterArray)
        {
            WebDomainData webDomainData = WebDomain.Current;
            ComponentData componentData = webDomainData.ComponentDataList[Info.Key];
            if (componentData != null)
            {
                ParameterData blogGuidParameter = componentData.ParameterDataList[Info.BlogGuid];
                if (blogGuidParameter != null)
                {
                    HttpData.SetScopedItem<String>(Info.Scope, Info.BlogGuid, blogGuidParameter.Value);
                }
            }
            //+ url
            if (!Themelia.Web.Routing.PassThroughHttpHandler.ForceUse)
            {
                DetectDestination();
            }
            //+ blog page
            if (componentData != null)
            {
                ParameterData blogPageParameter = componentData.ParameterDataList[Info.BlogPage];
                if (blogPageParameter != null)
                {
                    HttpData.SetScopedItem<String>(Info.Scope, Info.BlogPage, blogPageParameter.Value);
                    return;
                }
            }
            //+ blog page fallback
            HttpData.SetScopedItem<String>(Info.Scope, Info.BlogPage, "~/default.aspx");
        }

        //- $DetectDestination -//
        private void DetectDestination()
        {
            String finderLabel = "/label/";
            String uri = Http.AbsoluteUrl;
            //+ label
            if (uri.Contains(finderLabel))
            {
                String label = uri.Substring(uri.IndexOf(finderLabel) + finderLabel.Length, uri.Length - (uri.IndexOf(finderLabel) + finderLabel.Length));
                if (!String.IsNullOrEmpty(label))
                {
                    Int32 extraSlash = label.IndexOf("/");
                    if (extraSlash > -1)
                    {
                        label = label.Substring(0, extraSlash);
                    }
                    Int32 questionMark = label.IndexOf("?");
                    if (questionMark > -1)
                    {
                        label = label.Substring(0, questionMark);
                    }
                    String labelTitle = String.Empty;
                    ReaderWriterLock readerWriterLock = new ReaderWriterLock();
                    try
                    {
                        readerWriterLock.AcquireReaderLock(Timeout.Infinite);
                        //+
                        labelTitle = LabelMap.Pull(label);
                        if (String.IsNullOrEmpty(labelTitle))
                        {
                            Minima.Service.Label labelEntity = LabelAgent.GetLabelByNetTitle(label);
                            if (labelEntity != null)
                            {
                                labelTitle = labelEntity.Title;
                                LockCookie lockCookie = readerWriterLock.UpgradeToWriterLock(Timeout.Infinite);
                                try
                                {
                                    if (!LabelMap.ContainsKey(label))
                                    {
                                        LabelMap.Add(label, labelTitle);
                                    }
                                }
                                catch
                                {
                                    //+ doesn't matter
                                }
                                finally
                                {
                                    readerWriterLock.DowngradeFromWriterLock(ref lockCookie);
                                }
                            }
                        }
                    }
                    finally
                    {
                        readerWriterLock.ReleaseReaderLock();
                    }
                    HttpData.SetScopedItem<String>(Info.Scope, "Label", label);
                    HttpData.SetScopedItem<String>(Info.Scope, "LabelTitle", labelTitle);
                    return;
                }
            }
            //+ link
            String linkCapturePattern = "[\\-a-z0-9]+$";
            String linkMatchPattern = "\\/2\\d\\d\\d\\/\\d{2}\\/[\\-a-z0-9]+$";
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
                if (!String.IsNullOrEmpty(link))
                {
                    HttpData.SetScopedItem<String>(Info.Scope, "Link", WebDomain.RelativeUrl);
                    return;
                }
            }
            //++ due to how IIS6 works, this is only compatible with IIS7 integrated mode
            linkCapturePattern = "[\\-a-z0-9]+\\.aspx";
            linkMatchPattern = "\\/2\\d\\d\\d\\/\\d{2}\\/[\\-a-z0-9]+\\.aspx";
            if (new Regex(linkMatchPattern).IsMatch(uri))
            {
                Regex a = new Regex(linkCapturePattern);
                Match m = a.Match(uri);
                String link = m.Captures[0].Value;
                link = link.Substring(0, link.Length - 5);
                if (!String.IsNullOrEmpty(link))
                {
                    HttpData.SetScopedItem<String>(Info.Scope, "Link", WebDomain.RelativeUrl.Substring(0, WebDomain.RelativeUrl.Length - 5));
                    return;
                }
            }
            //+ archive
            String archiveCapturePattern = "2(\\d\\d\\d)\\/(\\d{2})";
            String archiveMatchPattern = "\\/" + archiveCapturePattern;
            if (new Regex(archiveMatchPattern).IsMatch(uri))
            {
                Regex a = new Regex(archiveCapturePattern);
                Match m = a.Match(uri);
                String archive = m.Captures[0].Value;
                if (!String.IsNullOrEmpty(archive))
                {
                    HttpData.SetScopedItem<String>(Info.Scope, "Archive", archive);
                    //+
                    String[] parts = archive.Split('/');
                    HttpData.SetScopedItem<Int32>(Info.Scope, "ArchiveYear", Parser.ParseInt32(parts[0], 0));
                    HttpData.SetScopedItem<Int32>(Info.Scope, "ArchiveMonth", Parser.ParseInt32(parts[1], 0));
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
                    HttpData.SetScopedItem<Int32>(Info.Scope, "Index", Parser.ParseInt32(index, 0));
                    return;
                }
            }
            //+
            HttpData.SetScopedItem<String>(Info.Scope, "CustomLink", WebDomain.RelativeUrl);
        }
    }
}