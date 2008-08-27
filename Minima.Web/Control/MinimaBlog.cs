using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
//+
using Themelia.Activation;
//+
using Minima.Configuration;
using Minima.Parsing;
using Minima.Service;
using Minima.Service.Agent;
using Minima.Web.Control.Base;
using Minima.Web.Helper;
//+
namespace Minima.Web.Control
{
    public class MinimaBlog : MinimaBlogBase
    {
        protected PlaceHolder phNoEntries;
        protected System.Web.UI.WebControls.Literal litNoEntriesMessage;
        //+
        protected Repeater rptPosts;
        //+
        protected IndexEntryList indexEntryList;
        protected IndexListSeries indexListSeries;
        protected MultiView mvCommentContent;
        protected View vNothing;
        protected View vShowComments;
        protected HtmlGenericControl htmlCommentListHeader;
        protected System.Web.UI.WebControls.Literal litCommentCount;
        protected Repeater rptComments;
        protected View vCommentsDisabled;
        //+
        protected MultiView mvCommentInput;
        protected View vCommentForm;
        protected View vCommentClosed;

        //+
        //- @CustomCommentInputControl -//
        public CommentInputBase CustomCommentInputControl { get; set; }

        //- @CustomPostTemplateType -//
        public Type CustomPostTemplateType { get; set; }

        //- @CustomCommentTemplateType -//
        public Type CustomCommentTemplateType { get; set; }

        //- @CaptchaControl -//
        public CaptchaBase CaptchaControl { get; set; }

        //- @SupportCommenting -//
        public Boolean SupportCommenting { get; set; }

        //- @ShowAuthorSeries -//
        public Boolean ShowAuthorSeries { get; set; }

        //- @CodeParserSeries -//
        public Themelia.CodeParsing.CodeParserSeries CodeParserSeries { get; set; }

        //+
        //- @Ctor -//
        public MinimaBlog()
        {
            //+ default
            this.CaptchaControl = new MathCaptcha();
            this.SupportCommenting = true;
            this.ShowAuthorSeries = true;
            //+ parser
            this.CodeParserSeries = new Themelia.CodeParsing.CodeParserSeries()
            {
                CodeParserId = Info.Scope
            };
            this.CodeParserSeries.Add(new BlogEntryCodeParser());
            this.CodeParserSeries.Add(new AmazonAffiliateCodeParser());
        }

        //+
        //- #OnInit -//
        protected override void OnInit(EventArgs e)
        {
            Func<BlogEntry, IndexEntry> indexTransformation = p => new IndexEntry
            {
                Url = BlogEntryHelper.BuildBlogEntry(p.PostDateTime, p.MappingNameList.FirstOrDefault(), Themelia.Web.WebDomain.Current),
                Title = p.Title,
                TypeGuid = p.BlogEntryTypeGuid,
                PostDateTime = p.PostDateTime,
                LabelList = p.LabelList,
                DateTimeString = String.Format("{0}, {1} {2}, {3}", p.PostDateTime.DayOfWeek, p.PostDateTime.ToString("MMMM"), p.PostDateTime.Day, p.PostDateTime.Year),
                DateTimeDisplay = String.Format("{0}/{1}/{2} {3}", p.PostDateTime.Month, p.PostDateTime.Day, p.PostDateTime.Year, p.PostDateTime.ToShortTimeString())
            };
            List<IndexEntry> indexDataSource;
            //+ index
            if (this.Index > 0)
            {
                String blogGuid = Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.BlogGuid);
                DateTime startDateTime = new DateTime(this.Index, 1, 1, 0, 0, 0);
                DateTime endDateTime = new DateTime(this.Index, 12, 31, 23, 59, 59);
                //+
                indexDataSource = BlogAgent.GetBlogEntryListByDateRange(blogGuid, startDateTime, endDateTime, true).Select(indexTransformation).ToList();
                List<Int32> yearDataSource = BlogAgent.GetBlogEntryList(blogGuid, 0, BlogEntryRetreivalType.VeryBasic)
                    .Where(p => p.PostDateTime.Year != this.Index)
                    .Select(p => p.PostDateTime.Year)
                    .Distinct()
                    .OrderByDescending(p => p)
                    .ToList();
                indexListSeries = new IndexListSeries()
                {
                    HeadingSuffix = MinimaConfiguration.IndexHeadingSuffix,
                    BlogEntryDataSource = indexDataSource,
                    YearDataSource = yearDataSource,
                    Year = this.Index
                };
                indexListSeries.ID = "indexListSeries";
                this.Controls.Add(indexListSeries);
            }
            //+ blog
            else
            {
                if (this.AccessType == AccessType.Archive || this.AccessType == AccessType.Label)
                {
                    if (this.DataSource != null && this.DataSource.Count > 0)
                    {
                        indexDataSource = this.DataSource.Select(indexTransformation).ToList();
                        indexEntryList = new IndexEntryList(this.AccessType, indexDataSource);
                        indexEntryList.ID = "indexEntryList";
                        this.Controls.Add(indexEntryList);
                    }
                }
                //+
                phNoEntries = __BuildNoEntryPlaceHolderControl();
                phNoEntries.ID = "phNoEntries";
                this.Controls.Add(phNoEntries);
                //+
                rptPosts = this.__BuildPostRepeaterControl();
                rptPosts.ID = "rptPosts";
                this.Controls.Add(rptPosts);
                //+
                if (this.SupportCommenting)
                {
                    mvCommentContent = this.__BuildCommentMultiViewControl();
                    this.Controls.Add(mvCommentContent);
                }
            }
            //+
            base.OnInit(e);
        }

        //- #OnLoad -//
        protected override void OnLoad(EventArgs e)
        {
            List<BlogEntry> blogEntryList = this.DataSource;
            if (this.AccessType != AccessType.Index)
            {
                //+ were there any entries at all?
                if (blogEntryList == null || blogEntryList.Count < 1)
                {
                    rptPosts.Visible = false;
                    phNoEntries.Visible = true;
                    litNoEntriesMessage.Text = MinimaConfiguration.BlankLabelMessage;
                }
                else
                {
                    rptPosts.DataSource = blogEntryList.Select(p => new
                    {
                        Url = BlogEntryHelper.BuildBlogEntry(p.PostDateTime, p.MappingNameList.First(), Themelia.Web.WebDomain.Current),
                        Content = this.CodeParserSeries.ParseCode(p.Content),
                        Title = p.Title,
                        AuthorList = p.AuthorList,
                        AllowCommentStatus = p.AllowCommentStatus,
                        CommentList = p.CommentList,
                        Guid = p.Guid,
                        LabelList = p.LabelList,
                        AuthorSeries = GetBlogEntryAuthorSeries(p),
                        LabelSeries = GetBlogEntryLabelSeries(p),
                        ViewableCommentCount = p.CommentList != null ? p.CommentList.Count : 0,
                        DateTimeString = String.Format("{0}, {1} {2}, {3}", p.PostDateTime.DayOfWeek, p.PostDateTime.ToString("MMMM"), p.PostDateTime.Day, p.PostDateTime.Year),
                        DateTimeDisplay = String.Format("{0}/{1}/{2} {3}", p.PostDateTime.Month, p.PostDateTime.Day, p.PostDateTime.Year, p.PostDateTime.ToShortTimeString())
                    });
                    rptPosts.DataBind();
                    //+
                    if (this.AccessType == AccessType.Link && this.SupportCommenting)
                    {
                        BlogEntry blogEntry = blogEntryList[0];
                        if (blogEntry.AllowCommentStatus == AllowCommentStatus.Disabled)
                        {
                            mvCommentContent.SetActiveView(vCommentsDisabled);
                        }
                        else
                        {
                            mvCommentContent.SetActiveView(vShowComments);
                            List<Comment> commentList = CommentAgent.GetCommentList(this.BlogEntryGuid, false);
                            if (commentList.Count > 0)
                            {
                                htmlCommentListHeader.Visible = true;
                                litCommentCount.Text = commentList.Count.ToString();
                            }
                            rptComments.DataSource = commentList;
                            rptComments.DataBind();
                            //+
                            CommentInputBase commentInput = vCommentForm.FindControl("CommentInput") as CommentInputBase;
                            if (commentInput != null)
                            {
                                HiddenField hfBlogEntryGuid = commentInput.FindControl("hfBlogEntryGuid") as HiddenField;
                                if (hfBlogEntryGuid != null)
                                {
                                    hfBlogEntryGuid.Value = this.BlogEntryGuid;
                                }
                            }
                            //+
                            if (blogEntry.AllowCommentStatus == AllowCommentStatus.Closed)
                            {
                                mvCommentInput.SetActiveView(vCommentClosed);
                            }
                            else
                            {
                                mvCommentInput.SetActiveView(vCommentForm);
                            }
                        }
                    }
                }
            }
            //+
            SetPageTitle();
            //+
            base.OnLoad(e);
        }

        //- $SetPageTitle -//
        private void SetPageTitle( )
        {
            //+ title
            String pageTitle = String.Empty;
            switch (this.AccessType)
            {
                case AccessType.Index:
                    pageTitle = String.Format("{0} {1}", this.Index, MinimaConfiguration.IndexHeadingSuffix);
                    break;
                case AccessType.Link:
                    if (this.DataSource != null && this.DataSource.Count == 1)
                    {
                        String blogEntryTitle = Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.BlogEntryTitle);
                        pageTitle = blogEntryTitle;
                    }
                    else
                    {
                        pageTitle = GetDefaultHeader();
                    }
                    break;
                case AccessType.Label:
                    String labelName = Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.LabelTitle);
                    pageTitle = String.Format("{0} {1}", labelName, MinimaConfiguration.LabelHeadingSuffix);
                    break;
                case AccessType.Archive:
                    String monthName = Themelia.Web.HttpData.GetScopedItem<String>(Info.Scope, Info.ArchiveMonth);
                    Int32 year = Themelia.Web.HttpData.GetScopedItem<Int32>(Info.Scope, Info.ArchiveYear);
                    pageTitle = String.Format("{0} {1} {2}", monthName, year, MinimaConfiguration.ArchiveHeadingSuffix);
                    break;
                default:
                    pageTitle = GetDefaultHeader();
                    break;
            }
            if (!String.IsNullOrEmpty(pageTitle))
            {
                Themelia.Web.HttpData.SetScopedItem<String>(Info.Scope, Info.PageTitle, pageTitle);
            }
        }

        //- $SetDefaultHeader -//
        private String GetDefaultHeader()
        {
            String pageTitle = String.Empty;
            BlogMetaData blogMetaData = Themelia.Web.HttpData.GetScopedCacheItem<BlogMetaData>(Info.Scope, Info.BlogMetaData);
            if (blogMetaData != null)
            {
                pageTitle = blogMetaData.Title;
            }
            //+
            return pageTitle;
        }

        //+
        #region Builder

        //- $GetCommentInputControl -//
        private CommentInputBase GetCommentInputControl()
        {
            if (this.CustomCommentInputControl == null)
            {
                return new CommentInput(this.CaptchaControl);
            }
            //+
            return this.CustomCommentInputControl;
        }

        //- $GetPostTemplate -//
        private ITemplate GetPostTemplate()
        {
            if (this.CustomPostTemplateType == null)
            {
                return BlogTemplateFactory.CreateTemplate(BlogTemplateFactory.TemplateType.Post, this.AccessType, this.SupportCommenting, this.ShowAuthorSeries);
            }
            //+
            return (ITemplate)ObjectCreator.Create(this.CustomPostTemplateType, this.AccessType, this.SupportCommenting, this.ShowAuthorSeries);
        }

        //- $GetCommentTemplate -//
        private ITemplate GetCommentTemplate()
        {
            if (this.CustomCommentTemplateType == null)
            {
                return BlogTemplateFactory.CreateTemplate(BlogTemplateFactory.TemplateType.Comment);
            }
            //+
            return (ITemplate)ObjectCreator.CreateAs<ITemplate>(this.CustomCommentTemplateType);
        }

        //- $__BuildCommentFormViewControl -//
        private View __BuildCommentFormViewControl()
        {
            View v = new View();
            //+ comment input
            CommentInputBase commentInput = GetCommentInputControl();
            commentInput.ID = "CommentInput";
            v.Controls.Add(commentInput);
            return v;
        }

        //- $__BuildShowCommentsViewControl -//
        private View __BuildShowCommentsViewControl()
        {
            View v = new View();
            //+ div#comments
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("id", "comments");
            v.Controls.Add(div);
            //+ h4, comment count
            htmlCommentListHeader = new HtmlGenericControl("h4") { ID = "htmlCommentListHeader", Visible = false };
            div.Controls.Add(htmlCommentListHeader);
            //+ comment count
            litCommentCount = new System.Web.UI.WebControls.Literal() { ID = "litCommentCount" };
            htmlCommentListHeader.Controls.Add(new System.Web.UI.WebControls.Literal
            {
                Text = "Comments ("
            });
            htmlCommentListHeader.Controls.Add(litCommentCount);
            htmlCommentListHeader.Controls.Add(new System.Web.UI.WebControls.Literal
            {
                Text = ")"
            });
            //+ comment repeater
            rptComments = new Repeater() { ID = "rptComments" };
            rptComments.ItemTemplate = GetCommentTemplate();
            div.Controls.Add(rptComments);
            if (this.SupportCommenting)
            {
                //+ comment input multiview
                mvCommentInput = new MultiView() { ID = "mvCommentInput", ActiveViewIndex = 0 };
                vCommentForm = __BuildCommentFormViewControl();
                vCommentForm.ID = "vCommentForm";
                vCommentClosed = new View() { ID = "vCommentClosed" };
                vCommentClosed.Controls.Add(new System.Web.UI.WebControls.Literal
                {
                    Text = @"<p class=""comment-status"">Comments have been closed for this entry.</p>"
                });
                mvCommentInput.Controls.Add(vCommentForm);
                mvCommentInput.Controls.Add(vCommentClosed);
                div.Controls.Add(mvCommentInput);
            }
            //+
            return v;
        }

        //- $__BuildCommentMultiViewControl -//
        private MultiView __BuildCommentMultiViewControl()
        {
            MultiView mv = new MultiView();
            mv.ID = "mvCommentContent";
            //+
            vNothing = new View() { ID = "vNothing" };
            mv.Controls.Add(vNothing);
            //+
            vShowComments = __BuildShowCommentsViewControl();
            vShowComments.ID = "vShowComments";
            mv.Controls.Add(vShowComments);
            //+
            vCommentsDisabled = new View() { ID = "vCommentsDisabled" };
            vCommentsDisabled.Controls.Add(new System.Web.UI.WebControls.Literal
            {
                Text = @"<p class=""comment-status"">Comments have been disabled for this entry.</p>"
            });
            mv.Controls.Add(vCommentsDisabled);
            //+
            return mv;
        }

        //- $__BuildPostRepeaterControl -//
        private Repeater __BuildPostRepeaterControl()
        {
            Repeater rpt = new Repeater();
            rpt.ItemTemplate = GetPostTemplate();
            return rpt;
        }

        //- $__BuildNoEntryPlaceHolder -//
        private PlaceHolder __BuildNoEntryPlaceHolderControl()
        {
            PlaceHolder ph = new PlaceHolder();
            //+
            litNoEntriesMessage = new System.Web.UI.WebControls.Literal() { ID = "litNoEntriesMessage" };
            ph.Controls.Add(litNoEntriesMessage);
            //+
            return ph;
        }

        #endregion

        #region Support

        //+
        //- $GetBlogEntryLabelSeries -//
        private String GetBlogEntryLabelSeries(BlogEntry blogEntry)
        {
            StringBuilder labelSeries = new StringBuilder();
            if (blogEntry.LabelList != null)
            {
                Boolean first = true;
                labelSeries.Append("{");
                if (blogEntry.LabelList.Count < 1)
                {
                    return String.Empty;
                }
                foreach (Minima.Service.Label label in blogEntry.LabelList)
                {
                    if (blogEntry.LabelList.Count > 1 && !first)
                    {
                        labelSeries.Append(", ");
                    }

                    labelSeries.Append(String.Format("<a href=\"{1}\">{0}</a>", label.Title, LabelHelper.GetLabelUrl(label)));
                    first = false;
                }
                labelSeries.Append("}");
            }
            //+
            return labelSeries.ToString();
        }

        //- $GetBlogEntryAuthorSeries -//
        private String GetBlogEntryAuthorSeries(BlogEntry blogEntry)
        {
            StringBuilder authorSeries = new StringBuilder();
            if (blogEntry.AuthorList != null)
            {
                Boolean first = true;
                if (blogEntry.AuthorList.Count < 1)
                {
                    return String.Empty;
                }
                else if (blogEntry.AuthorList.Count > 1)
                {
                    authorSeries.Append("{");
                }
                foreach (Author author in blogEntry.AuthorList)
                {
                    if (blogEntry.AuthorList.Count > 1 && !first)
                    {
                        authorSeries.Append(", ");
                    }
                    if (MinimaConfiguration.LinkAuthorToEmail)
                    {
                        authorSeries.Append(String.Format("<a href=\"mailto:{1}\">{0}</a>", author.Name, author.Email));
                    }
                    else
                    {
                        authorSeries.Append(author.Name);
                    }
                    first = false;
                }
                if (blogEntry.AuthorList.Count > 1)
                {
                    authorSeries.Append("}");
                }
            }
            //+
            return authorSeries.ToString();
        }

        #endregion
    }
}