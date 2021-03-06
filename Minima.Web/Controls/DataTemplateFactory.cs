#region Copyright
//+ Copyright � Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using Minima.Service;
//+
using Themelia;
//+
namespace Minima.Web.Controls
{
    public static class DataTemplateTemplateFactory
    {
        public static Template CreatePostTemplate(Boolean linkHeader, Boolean hidePostDateTime, Boolean showAuthorSeries, Boolean showLabelSeries, String labelSeries, Boolean isLink, Boolean supportCommenting, AllowCommentStatus allowCommentStatus, Boolean showDisabledText, String postFooterData)
        {
            Themelia.Template template = new Themelia.Template();
            if (linkHeader)
            {
                template.AppendText(@"
<div class=""post"">
    <h3><a href=""{$Url$}"">{$Title$}</a></h3>");
            }
            else
            {
                template.AppendText(@"
<div class=""post"">
    <h3>{$Title$}</h3>");
            }
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
            if (showLabelSeries && !String.IsNullOrEmpty(labelSeries))
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
        </p>");
                }
            }
            if (!String.IsNullOrEmpty(postFooterData))
            {
                template.AppendText(@"
{$PostFooterData$}
");
            }
            template.AppendText(@"
    </p>
</div>");
            //+
            return template;
        }
    }
}