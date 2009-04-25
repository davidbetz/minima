#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
//+
using Minima.Service;
//+
using Themelia;
using Themelia.Activation;
//+
namespace Minima.Web.Controls
{
    public static class BlogControlTemplateFactory
    {
        //- ~TemplateType -//
        internal enum TemplateType
        {
            Comment,
            Post
        }

        //+
        //- @CommentTemplate -//
        public class CommentTemplate : ITemplate
        {
            //- @Ctor -//
            public CommentTemplate(params Object[] parameterArray)
            {
                //+ blank
            }

            //- @InstantiateIn -//
            public void InstantiateIn(System.Web.UI.Control container)
            {
                PlaceHolder ph = new PlaceHolder();
                ph.DataBinding += new EventHandler(delegate(Object sender, EventArgs ea)
                {
                    IDataItemContainer item = (IDataItemContainer)container;
                    String name = DataBinder.Eval(item.DataItem, "Name") as String;
                    String website = DataBinder.Eval(item.DataItem, "Website") as String;
                    String text = DataBinder.Eval(item.DataItem, "Text") as String;
                    String dateTime = DataBinder.Eval(item.DataItem, "DateTime") as String;
                    //+
                    //+ div#commentBlock
                    HtmlGenericControl divCommentBlock = new HtmlGenericControl("div");
                    divCommentBlock.Attributes.Add("id", "commentBlock");
                    ph.Controls.Add(divCommentBlock);
                    //+ p.comment-person
                    HtmlGenericControl pCommentPerson = new HtmlGenericControl("p");
                    pCommentPerson.Attributes.Add("class", "comment-person");
                    divCommentBlock.Controls.Add(pCommentPerson);
                    //+
                    String authorName = name;
                    if (String.IsNullOrEmpty(authorName))
                    {
                        authorName = "{ Anonymous }";
                    }
                    if (!String.IsNullOrEmpty(website))
                    {
                        authorName = @"<a href=""{Website}"">{Name}</a>"
                            .Replace("{Website}", website)
                            .Replace("{Name}", authorName);
                    }
                    pCommentPerson.Controls.Add(new System.Web.UI.WebControls.Literal
                    {
                        Text = authorName
                    });
                    //+ p.comment-body
                    HtmlGenericControl pCommentBody = new HtmlGenericControl("p");
                    pCommentBody.Attributes.Add("class", "comment-body");
                    divCommentBlock.Controls.Add(pCommentBody);
                    pCommentBody.Controls.Add(new System.Web.UI.WebControls.Literal
                    {
                        Text = text
                    });
                    //+ p.comment-timestamp
                    HtmlGenericControl pCommentTimestamp = new HtmlGenericControl("p");
                    pCommentTimestamp.Attributes.Add("class", "comment-timestamp");
                    divCommentBlock.Controls.Add(pCommentTimestamp);
                    pCommentTimestamp.Controls.Add(new System.Web.UI.WebControls.Literal
                    {
                        Text = dateTime
                    });
                });
                container.Controls.Add(ph);
            }
        }

        //+
        //- @PostTemplate -//
        public class PostTemplate : ITemplate
        {
            //- $IsLink -//
            private Boolean IsLink { get; set; }

            //- $SupportCommenting -//
            private Boolean SupportCommenting { get; set; }

            //- $DisabledCommentText -//
            public String DisabledCommentText { get; set; }

            //- $ShowAuthorSeries -//
            private Boolean ShowAuthorSeries { get; set; }

            //- $ShowAuthorSeries -//
            private Boolean ShowLabelSeries { get; set; }

            //- $HidePostTime -//
            private Boolean HidePostDateTime { get; set; }

            //- $LinkHeader -//
            private Boolean LinkHeader { get; set; }

            //- $PostFooterTypeInfo -//
            private TypeInfo PostFooterTypeInfo { get; set; }

            //- @Ctor -//
            public PostTemplate(params Object[] parameterArray)
            {
                this.PostFooterTypeInfo = (TypeInfo)parameterArray[0];
                this.LinkHeader = (Boolean)parameterArray[1];
                this.IsLink = ((AccessType)parameterArray[2]) == AccessType.Link;
                this.SupportCommenting = (Boolean)parameterArray[3];
                this.DisabledCommentText = (String)parameterArray[4];
                this.ShowAuthorSeries = (Boolean)parameterArray[5];
                this.ShowLabelSeries = (Boolean)parameterArray[6];
                this.HidePostDateTime = (Boolean)parameterArray[7];
            }

            //- @InstantiateIn -//
            public void InstantiateIn(System.Web.UI.Control container)
            {
                PlaceHolder ph = new PlaceHolder();
                ph.DataBinding += new EventHandler(delegate(Object sender, EventArgs ea)
                {
                    IDataItemContainer item = (IDataItemContainer)container;
                    String guid = DataBinder.Eval(item.DataItem, "Guid") as String;
                    String url = DataBinder.Eval(item.DataItem, "Url") as String;
                    String title = DataBinder.Eval(item.DataItem, "Title") as String;
                    String content = DataBinder.Eval(item.DataItem, "Content") as String;
                    String authorSeries = DataBinder.Eval(item.DataItem, "AuthorSeries") as String;
                    String labelSeries = DataBinder.Eval(item.DataItem, "LabelSeries") as String;
                    String dateTimeString = DataBinder.Eval(item.DataItem, "DateTimeString") as String;
                    String dateTimeDisplay = DataBinder.Eval(item.DataItem, "DateTimeDisplay") as String;
                    AllowCommentStatus allowCommentStatus = (AllowCommentStatus)(DataBinder.Eval(item.DataItem, "AllowCommentStatus") ?? 0);
                    Int32 viewableCommentCount = (DataBinder.Eval(item.DataItem, "ViewableCommentCount") as Int32?) ?? 0;
                    //+
                    HiddenField hBlogEntryGuid = new HiddenField
                    {
                        ID = "hBlogEntryGuid",
                        Value = guid
                    };
                    ph.Controls.Add(hBlogEntryGuid);
                    //+
                    String postFooterData = String.Empty;
                    if (this.PostFooterTypeInfo != null)
                    {
                        PostFooterBase postFooter = ObjectCreator.CreateAs<PostFooterBase>(this.PostFooterTypeInfo);
                        if (postFooter != null)
                        {
                            postFooter.Data = item.DataItem;
                            System.Text.StringBuilder builder = new System.Text.StringBuilder();
                            System.IO.StringWriter writer = new System.IO.StringWriter(builder);
                            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);
                            postFooter.RenderControl(htmlWriter);
                            postFooterData = builder.ToString();
                        }
                    }
                    //+
                    Themelia.Template template = DataTemplateTemplateFactory.CreatePostTemplate(this.LinkHeader, this.HidePostDateTime, this.ShowAuthorSeries, this.ShowLabelSeries, labelSeries, this.IsLink, this.SupportCommenting, allowCommentStatus, !String.IsNullOrEmpty(DisabledCommentText), postFooterData);
                    //+
                    String output = template.Interpolate(new Map(
                            new MapEntry("$Url$", url),
                            new MapEntry("$Title$", title),
                            new MapEntry("$DateTimeString$", dateTimeString),
                            new MapEntry("$Content$", content),
                            new MapEntry("$AuthorSeries$", authorSeries),
                            new MapEntry("$DateTimeDisplay$", dateTimeDisplay)
                        )
                    );
                    if (!String.IsNullOrEmpty(labelSeries))
                    {
                        output = new Template(output).Interpolate(new Map(
                             new MapEntry("$LabelSeries$", labelSeries)
                        ));
                    }
                    if (!this.IsLink && this.SupportCommenting)
                    {
                        if (allowCommentStatus == AllowCommentStatus.Disabled)
                        {
                            if (!String.IsNullOrEmpty(this.DisabledCommentText))
                            {
                                output = new Template(output).Interpolate(new Map(
                                     new MapEntry("$DisabledCommentText$", this.DisabledCommentText)
                                ));
                            }
                        }
                        else
                        {
                            output = new Template(output).Interpolate(new Map(
                              new MapEntry("$Url$", url),
                              new MapEntry("$ViewableCommentCount$", viewableCommentCount.ToString()),
                              new MapEntry("$Plural$", viewableCommentCount != 1 ? "s" : String.Empty)));
                        }
                    }
                    if (!String.IsNullOrEmpty(postFooterData))
                    {
                        output = new Template(output).Interpolate(new Map(
                             new MapEntry("$PostFooterData$", postFooterData)
                        ));
                    }
                    ph.Controls.Add(new System.Web.UI.WebControls.Literal
                    {
                        Text = output
                    });
                });
                container.Controls.Add(ph);
            }
        }

        //- ~CreateTemplate -//
        internal static ITemplate CreateTemplate(TemplateType templateType, params Object[] parameterArray)
        {
            switch (templateType)
            {
                case TemplateType.Comment:
                    return new CommentTemplate(parameterArray);
                case TemplateType.Post:
                    return new PostTemplate(parameterArray);
            }
            //+
            return null;
        }
    }
}