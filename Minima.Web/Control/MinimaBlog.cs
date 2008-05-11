using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
//+
using Minima.Activation;
using Minima.Configuration;
using Minima.Service;
using Minima.Web.Agent;
using Minima.Web.Control.Base;
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
        protected HiddenField hfBlogEntryGuid;

        //+
        //- @CustomCommentInputControl -//
        public Minima.Web.Control.Base.CommentInputBase CustomCommentInputControl { get; set; }

        //- @CustomPostTemplateType -//
        public Type CustomPostTemplateType { get; set; }

        //- @CustomCommentTemplateType -//
        public Type CustomCommentTemplateType { get; set; }

        //- @CaptchaControl -//
        public CaptchaBase CaptchaControl { get; set; }

        //+
        //- @Ctor -//
        public MinimaBlog() {
            //+ default
            this.CaptchaControl = new MathCaptcha();
        }

        //+
        //- #OnInit -//
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        //- #OnPreRender -//
        protected override void OnPreRender(EventArgs e)
        {
            List<BlogEntry> blogEntryList = this.DataSource;
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
                    Url = p.BlogEntryUri.AbsoluteUri,
                    Content = p.Content,
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
                //hfBlogEntryGuid.Value = this.BlogEntryGuid;
                //+
                if (this.IsLinkAccess)
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
                        hfBlogEntryGuid = new HiddenField
                        {
                            Value = blogEntry.Guid.ToString()
                        };
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

        //- #Render -//
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.Write(@"
<script type=""text/javascript"">
document.observe('dom:loaded', Initialization.init);
</script>
");
        }

        //+
        #region Builder

        //- $GetCommentInputControl -//
        private System.Web.UI.Control GetCommentInputControl()
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
                return TemplateFactory.CreateTemplate(TemplateFactory.TemplateType.Post, this.IsLinkAccess);
            }
            //+
            return ObjectCreator.CreateAs<ITemplate>(this.CustomPostTemplateType, this.IsLinkAccess);
        }

        //- $GetCommentTemplate -//
        private ITemplate GetCommentTemplate()
        {
            if (this.CustomCommentTemplateType == null)
            {
                return TemplateFactory.CreateTemplate(TemplateFactory.TemplateType.Comment);
            }
            //+
            return ObjectCreator.CreateAs<ITemplate>(this.CustomCommentTemplateType);
        }

        //- $__BuildCommentFormViewControl -//
        private View __BuildCommentFormViewControl()
        {
            View v = new View();
            //+ comment input
            v.Controls.Add(GetCommentInputControl());
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

        //- #CreateChildControls -//
        protected override void CreateChildControls()
        {
            phNoEntries = __BuildNoEntryPlaceHolderControl();
            phNoEntries.ID = "phNoEntries";
            this.Controls.Add(phNoEntries);
            //+
            rptPosts = this.__BuildPostRepeaterControl();
            rptPosts.ID = "rptPosts";
            this.Controls.Add(rptPosts);
            //+
            mvCommentContent = this.__BuildCommentMultiViewControl();
            this.Controls.Add(mvCommentContent);
            //+
            base.CreateChildControls();
        }

        #endregion

        #region Support

        //+
        //- $GetBlogEntryLabelSeries -//
        private String GetBlogEntryLabelSeries(BlogEntry blogEntry)
        {
            StringBuilder labelSeries = new StringBuilder();
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

                labelSeries.Append(String.Format("<a href=\"{1}\">{0}</a>", label.Title, GetLabelUrl(label)));
                first = false;
            }
            labelSeries.Append("}");
            //+
            return labelSeries.ToString();
        }

        //- $GetBlogEntryAuthorSeries -//
        private String GetBlogEntryAuthorSeries(BlogEntry blogEntry)
        {
            StringBuilder authorSeries = new StringBuilder();
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
            //+
            return authorSeries.ToString();
        }

        //- $GetLabelUrl -//
        private String GetLabelUrl(Minima.Service.Label label)
        {
            return WebConfiguration.Domain + "/label/" + (!String.IsNullOrEmpty(label.FriendlyTitle) ? label.FriendlyTitle : label.Title).ToLower();
        }

        #endregion
    }
}