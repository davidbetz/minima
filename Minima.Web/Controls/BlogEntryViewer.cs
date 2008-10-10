#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Themelia;
//+
using Minima.Service.Agent;
using Minima.Web.Helper;
using Themelia.Web.Routing.Data;
using Themelia.Web;
//+
namespace Minima.Web.Controls
{
    public class BlogViewer : System.Web.UI.Control
    {
        //- ~Info -//
        internal class Info : Minima.Web.Info
        {
            public const String CustomLink = "CustomLink";
        }

        //+
        private String webDomain;
        private String blogGuid;

        //+
        //- @BlogGuid -//
        public String BlogGuid
        {
            get
            {
                if (!String.IsNullOrEmpty(blogGuid))
                {
                    return blogGuid;
                }
                WebDomainData webDomain = WebDomain.CurrentData;
                if (webDomain != null && this.WebDomainName != webDomain.Name)
                {
                    return WebDomainDataList.AllWebDomainData[this.WebDomainName].ComponentDataList[Info.Scope].ParameterDataList[Info.BlogGuid].Value;
                }
                //+
                return HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
            }
            set
            {
                blogGuid = value;
            }
        }

        //- @Link -//
        public String Link { get; set; }

        //- @BlogEntryGuid -//
        public String BlogEntryGuid { get; set; }

        //- @ShowAuthorSeries -//
        public Boolean ShowAuthorSeries { get; set; }

        //- @ShowLabelSeries -//
        public Boolean ShowLabelSeries { get; set; }

        //- @HidePostDateTime -//
        public Boolean HidePostDateTime { get; set; }

        //- @CodeParserSeries -//
        public Themelia.CodeParsing.CodeParserSeries CodeParserSeries { get; set; }

        //- @WebDomain -//
        public String WebDomainName
        {
            get
            {
                if (!String.IsNullOrEmpty(webDomain))
                {
                    return webDomain;
                }
                if (Themelia.Web.WebDomain.CurrentData != null)
                {
                    return Themelia.Web.WebDomain.CurrentData.Name;
                }
                //+
                return String.Empty;
            }
            set
            {
                webDomain = value;
            }
        }

        //+
        //- @Ctor -//
        public BlogViewer()
        {
            //+ default
            this.ShowAuthorSeries = true;
            this.HidePostDateTime = false;
            //+ parser
            this.CodeParserSeries = new Themelia.CodeParsing.CodeParserSeries()
            {
                CodeParserId = Info.Scope
            };
            this.CodeParserSeries.Add(new Minima.Parsing.BlogEntryCodeParser());
            this.CodeParserSeries.Add(new Minima.Parsing.AmazonAffiliateCodeParser());
        }

        //+
        //- #OnPreRender -//
        protected override void OnPreRender(EventArgs e)
        {
            String link = Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.CustomLink);
            //+
            if (!String.IsNullOrEmpty(link))
            {
                Minima.Service.BlogEntry blogEntry = BlogAgent.GetSingleBlogEntryByLink(this.BlogGuid, link);
                if (blogEntry != null)
                {
                    String labelSeries = SeriesHelper.GetBlogEntryLabelSeries(blogEntry);
                    Template template = DataTemplateTemplateFactory.CreatePostTemplate(false, this.HidePostDateTime, this.ShowAuthorSeries, this.ShowLabelSeries, labelSeries, true, false, Minima.Service.AllowCommentStatus.Disabled, false, String.Empty);
                    String output = template.Interpolate(new Map(
                            new MapEntry("$Title$", blogEntry.Title),
                            new MapEntry("$DateTimeString$", String.Format("{0}, {1} {2}, {3}", blogEntry.PostDateTime.DayOfWeek, blogEntry.PostDateTime.ToString("MMMM"), blogEntry.PostDateTime.Day, blogEntry.PostDateTime.Year)),
                            new MapEntry("$Content$", blogEntry.Content),
                            new MapEntry("$AuthorSeries$", SeriesHelper.GetBlogEntryAuthorSeries(blogEntry)),
                            new MapEntry("$DateTimeDisplay$", String.Format("{0}/{1}/{2} {3}", blogEntry.PostDateTime.Month, blogEntry.PostDateTime.Day, blogEntry.PostDateTime.Year, blogEntry.PostDateTime.ToShortTimeString())),
                            new MapEntry("$LabelSeries$", labelSeries)
                        )
                    );
                    output = this.CodeParserSeries.ParseCode(output);
                    //+
                    this.Controls.Add(new System.Web.UI.WebControls.Literal
                    {
                        Text = output
                    });
                }
            }
            //+
            base.OnPreRender(e);
        }
    }
}