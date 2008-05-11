using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
//+
namespace Minima.Web.Control
{
    internal static class TemplateFactory
    {
        //- ~TemplateType -//
        internal enum TemplateType
        {
            Comment,
            Post
        }

        //+
        //- ~CommentTemplate -//
        internal class CommentTemplate : ITemplate
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
                    String name = DataBinder.Eval(item.DataItem, "Name").ToString();
                    String website = DataBinder.Eval(item.DataItem, "Website").ToString();
                    String text = DataBinder.Eval(item.DataItem, "Text").ToString();
                    String dateTime = DataBinder.Eval(item.DataItem, "DateTime").ToString();
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
                    String authorName;
                    if (!String.IsNullOrEmpty(website))
                    {
                        authorName = @"<a href=""{Website}"">{Name}</a>"
                            .Replace("{Website}", website)
                            .Replace("{Name}", name);
                    }
                    else
                    {
                        authorName = name;
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
                    HtmlGenericControl pCommentTimestamp= new HtmlGenericControl("p");
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
        //- ~PostTemplate -//
        internal class PostTemplate : ITemplate
        {
            //- $IsLink -//
            private Boolean IsLink { get; set; }

            //- @Ctor -//
            public PostTemplate(params Object[] parameterArray)
            {
                this.IsLink = (parameterArray[0] as Boolean?) ?? false;
            }

            //- @InstantiateIn -//
            public void InstantiateIn(System.Web.UI.Control container)
            {
                PlaceHolder ph = new PlaceHolder();
                ph.DataBinding += new EventHandler(delegate(Object sender, EventArgs ea)
                {
                    IDataItemContainer item = (IDataItemContainer)container;
                    String guid = DataBinder.Eval(item.DataItem, "Guid").ToString();
                    String url = DataBinder.Eval(item.DataItem, "Url").ToString();
                    String title = DataBinder.Eval(item.DataItem, "Title").ToString();
                    String content = DataBinder.Eval(item.DataItem, "Content").ToString();
                    String authorSeries = DataBinder.Eval(item.DataItem, "AuthorSeries").ToString();
                    String labelSeries = DataBinder.Eval(item.DataItem, "LabelSeries").ToString();
                    String dateTimeString = DataBinder.Eval(item.DataItem, "DateTimeString").ToString();
                    String dateTimeDisplay = DataBinder.Eval(item.DataItem, "DateTimeDisplay").ToString();
                    String allowCommentStatus = DataBinder.Eval(item.DataItem, "AllowCommentStatus").ToString();
                    Int32 viewableCommentCount = Convert.ToInt32(DataBinder.Eval(item.DataItem, "ViewableCommentCount"));
                    //+
                    HiddenField hBlogEntryGuid = new HiddenField
                    {
                        ID = "hBlogEntryGuid",
                        Value = guid
                    };
                    ph.Controls.Add(hBlogEntryGuid);
                    ph.Controls.Add(new System.Web.UI.WebControls.Literal
                    {
                        Text = @"
<div class=""post"">
    <h3><a href=""{Url}"">{Title}</a></h3>
    <h2 class=""date-header"">{DateTimeString}</h2>
    <div class=""post-body"">
        <div>{Content}</div>
    </div>
    <p class=""post-footer"">
        <em>posted by {AuthorSeries} at {DateTimeDisplay}</em>".Replace("{Url}", url)
                                                               .Replace("{Title}", title)
                                                               .Replace("{DateTimeString}", dateTimeString)
                                                               .Replace("{Content}", content)
                                                               .Replace("{AuthorSeries}", authorSeries)
                                                               .Replace("{DateTimeDisplay}", dateTimeDisplay)
                    });
                    if (!String.IsNullOrEmpty(labelSeries))
                    {
                        ph.Controls.Add(new System.Web.UI.WebControls.Literal
                        {
                            Text = @"
        <p class=""post-labels"">{LabelSeries}</p>".Replace("{LabelSeries}", labelSeries)
                        });
                    }
                    if (!this.IsLink)
                    {
                        if (allowCommentStatus == "Disabled")
                        {
                            ph.Controls.Add(new System.Web.UI.WebControls.Literal
                            {
                                Text = @"
        <p class=""comment-count""><i>Comments are disabled for this entry.</i> </p>"
                            });
                        }
                        else
                        {
                            ph.Controls.Add(new System.Web.UI.WebControls.Literal
                            {
                                Text = @"
        <p class=""comment-count"">
            <a href=""{Url}"">({ViewableCommentCount} Comment{Plural})</a>
".Replace("{Url}", url)
         .Replace("{ViewableCommentCount}", viewableCommentCount.ToString())
         .Replace("{Plural}", viewableCommentCount != 1 ? "s" : String.Empty)
                            });
                            ph.Controls.Add(new System.Web.UI.WebControls.Literal
                            {
                                Text = @"
        </p>"
                            });
                        }
                    }
                    ph.Controls.Add(new System.Web.UI.WebControls.Literal()
                    {
                        Text = @"
    </p>
</div>"
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