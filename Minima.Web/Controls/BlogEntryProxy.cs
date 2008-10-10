#region Copyright
//+ Copyright � Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Themelia;
//+
using Minima.Service.Agent;
using Minima.Web.Helper;
//+
namespace Minima.Web.Controls
{
    public class BlogEntryProxy : System.Web.UI.Control
    {
        //- @BlogGuid -//
        public String BlogGuid { get; set; }

        //- @Link -//
        public String Link { get; set; }

        //- @BlogEntryGuid -//
        public String BlogEntryGuid { get; set; }

        //- @ShowAuthorSeries -//
        public Boolean ShowAuthorSeries { get; set; }

        //- @HidePostDateTime -//
        public Boolean HidePostDateTime { get; set; }

        //- @CodeParserSeries -//
        public Themelia.CodeParsing.CodeParserSeries CodeParserSeries { get; set; }

        //+
        //- @Ctor -//
        public BlogEntryProxy()
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
            Minima.Service.BlogEntry blogEntry = null;
            if (!String.IsNullOrEmpty(this.BlogEntryGuid))
            {
                blogEntry = BlogAgent.GetSingleBlogEntry(this.BlogEntryGuid);

            }
            else
            {
                String blogGuid = this.BlogGuid;
                if (String.IsNullOrEmpty(blogGuid))
                {
                    blogGuid = Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
                }
                //+
                if (!String.IsNullOrEmpty(this.Link))
                {
                    blogEntry = BlogAgent.GetSingleBlogEntryByLink(blogGuid, this.Link);
                }
            }
            if (blogEntry != null)
            {
                String labelSeries = SeriesHelper.GetBlogEntryLabelSeries(blogEntry);
                Template template = DataTemplateTemplateFactory.CreatePostTemplate(false, this.HidePostDateTime, this.ShowAuthorSeries, labelSeries, true, false, Minima.Service.AllowCommentStatus.Disabled, false, String.Empty);
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
            //+
            base.OnPreRender(e);
        }
    }
}