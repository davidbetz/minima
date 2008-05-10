using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
//+
using Minima.Configuration;
using Minima.Service;
using Minima.Web;
using Minima.Web.Agent;
//+
public partial class Default : Minima.Web.Control.MinimaBlogPageBase
{
    //- #OnInit -//
    protected override void OnInit(EventArgs e)
    {
        this.Load += new EventHandler(Page_Load);
        base.OnInit(e);
    }

    //- #Page_Load -//
    protected void Page_Load(Object sender, EventArgs e)
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
            rptPosts.ItemDataBound += new RepeaterItemEventHandler(rptPosts_ItemDataBound);
            rptPosts.DataBind();
            //+
            hfBlogEntryGuid.Value = this.BlogEntryGuid;
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
                    rptComments.ItemDataBound += new RepeaterItemEventHandler(rptComments_ItemDataBound);
                    rptComments.DataBind();
                    //+
                    hfBlogEntryGuid.Value = blogEntry.Guid.ToString();
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
    //- $rptComments_ItemDataBound -//
    private void rptComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (!String.IsNullOrEmpty(((HyperLink)((RepeaterItem)e.Item).FindControl("hlCommentWebsite")).NavigateUrl))
        {
            ((MultiView)((RepeaterItem)e.Item).FindControl("mvCommentAuthor")).SetActiveView(((View)((RepeaterItem)e.Item).FindControl("vWithWebsite")));
        }
    }

    //- $rptPosts_ItemDataBound -//
    private void rptPosts_ItemDataBound(Object sender, RepeaterItemEventArgs e)
    {

        if (!String.IsNullOrEmpty(((Literal)e.Item.FindControl("litBlogEntryLabelSeries")).Text))
        {
            ((PlaceHolder)e.Item.FindControl("phLabels")).Visible = true;
        }

        MultiView mvCommentSummary = (MultiView)e.Item.FindControl("mvCommentSummary");
        if (!String.IsNullOrEmpty(this.Link))
        {
            mvCommentSummary.Visible = false;
        }
        else
        {
            HiddenField hfBlogEntryCommentAllowStatusId = (HiddenField)e.Item.FindControl("hfBlogEntryCommentAllowStatusId");
            if (hfBlogEntryCommentAllowStatusId.Value == "Disabled")
            {
                View vCommentsDisabled = (View)mvCommentSummary.FindControl("vCommentsDisabled");
                mvCommentSummary.SetActiveView(vCommentsDisabled);
            }
        }
    }

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
}