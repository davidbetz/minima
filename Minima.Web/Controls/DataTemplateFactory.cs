#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
//+
using Themelia;
using Themelia.Activation;
using Minima.Service;
//+
namespace Minima.Web.Controls
{
    public static class DataTemplateTemplateFactory
    {
        public static Template CreatePostTemplate(Boolean hidePostDateTime, Boolean showAuthorSeries, String labelSeries, Boolean isLink, Boolean supportCommenting, AllowCommentStatus allowCommentStatus, Boolean showDisabledText, String postFooterData)
        {
            Themelia.Template template = new Themelia.Template(@"
<div class=""post"">
    <h3><a href=""{$Url$}"">{$Title$}</a></h3>");
            if (hidePostDateTime == false)
            {
                template.AppendText(@"
    <h2 class=""date-header"">{$DateTimeString$}</h2>");
            }
            template.AppendText(@"
    <div class=""post-body"">
        <div>{$Content$}</div>
    </div>
    <p class=""post-footer"">");
            if (showAuthorSeries)
            {
                template.AppendText("<em>posted by {$AuthorSeries$}");
                //+
                if (hidePostDateTime == false)
                {
                    template.AppendText(" at {$DateTimeDisplay$}</em>");
                }
            }
            if (!String.IsNullOrEmpty(labelSeries))
            {
                template.AppendText(@"
    <p class=""post-labels"">{$LabelSeries$}</p>");
            }
            if (!isLink && supportCommenting)
            {
                if (allowCommentStatus == AllowCommentStatus.Disabled)
                {
                    if (showDisabledText)
                    {
                        template.AppendText(@"<p class=""comment-count"">{$DisabledCommentText$}</p>");
                    }
                }
                else
                {
                    template.AppendText(@"
        <p class=""comment-count"">
            <a href=""{$Url$}"">({$ViewableCommentCount$} Comment{$Plural$})</a>
");
                    template.AppendText(@"
        </p>");
                }
            }
            template.AppendText(@"
{$PostFooterData$}
");
            template.AppendText(@"
    </p>
</div>");
            //+
            return template;
        }
    }
}