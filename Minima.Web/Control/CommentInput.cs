using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
//+
namespace Minima.Web.Control
{
    public class CommentInput : CommentInputBase
    {
        //- @Captcha -//
        public CaptchaBase CaptchaControl { get; set; }

        //- @Ctor -//
        public CommentInput(CaptchaBase captcha)
        {
            this.CaptchaControl = captcha;
        }

        //- #CreateChildControls -//
        protected override void CreateChildControls()
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("id", "commentInput");
            //+ name
            HtmlGenericControl p1 = new HtmlGenericControl("p");
            System.Web.UI.WebControls.Label lblCommentAuthorName = new System.Web.UI.WebControls.Label
            {
                AssociatedControlID = "txtCommentAuthorName",
                Text = "Name *"
            };
            TextBox txtCommentAuthorName = new TextBox()
            {
                ID = "txtCommentAuthorName",
                CssClass = "comment-input"
            };
            RequiredFieldValidator rfvCommentAuthorName = new RequiredFieldValidator
            {
                ID = "rfvCommentAuthorName",
                Display = ValidatorDisplay.Dynamic,
                Text = "(required)",
                ControlToValidate = "txtCommentAuthorName"
            };
            p1.Controls.Add(lblCommentAuthorName);
            p1.Controls.Add(rfvCommentAuthorName);
            div.Controls.Add(p1);
            div.Controls.Add(txtCommentAuthorName);
            //+ email
            HtmlGenericControl p2 = new HtmlGenericControl("p");
            System.Web.UI.WebControls.Label lblCommentAuthorEmail = new System.Web.UI.WebControls.Label
            {
                AssociatedControlID = "txtCommentAuthorEmail",
                Text = "E-mail *"
            };
            TextBox txtCommentAuthorEmail = new TextBox()
            {
                ID = "txtCommentAuthorEmail",
                CssClass = "comment-input"
            };
            RequiredFieldValidator rfvCommentAuthorEmail = new RequiredFieldValidator
            {
                ID = "rfvCommentAuthorEmail",
                Display = ValidatorDisplay.Dynamic,
                Text = "(required)",
                ControlToValidate = "txtCommentAuthorEmail"
            };
            p2.Controls.Add(lblCommentAuthorEmail);
            p2.Controls.Add(rfvCommentAuthorEmail);
            div.Controls.Add(p2);
            div.Controls.Add(txtCommentAuthorEmail);
            //+ website
            System.Web.UI.WebControls.Label lblCommentWebsite = new System.Web.UI.WebControls.Label
            {
                AssociatedControlID = "txtCommentWebsite",
                Text = "Website"
            };
            TextBox txtCommentWebsite = new TextBox()
            {
                ID = "txtCommentWebsite",
                CssClass = "comment-input"
            };
            div.Controls.Add(lblCommentWebsite);
            div.Controls.Add(txtCommentWebsite);
            //+ text
            HtmlGenericControl p3 = new HtmlGenericControl("p");
            System.Web.UI.WebControls.Label lblCommentText = new System.Web.UI.WebControls.Label
            {
                AssociatedControlID = "txtCommentText",
                Text = "Comment *"
            };
            TextBox txtCommentText = new TextBox()
            {
                ID = "txtCommentText",
                Rows = 10,
                Columns = 50,
                TextMode = TextBoxMode.MultiLine,
                CssClass = "comment-input"
            };
            RequiredFieldValidator rfvCommentText = new RequiredFieldValidator
            {
                ID = "rfvCommentText",
                Display = ValidatorDisplay.Dynamic,
                Text = "(required -- isn't that the entire point?)",
                ControlToValidate = "txtCommentAuthorEmail"
            };
            p3.Controls.Add(lblCommentText);
            p3.Controls.Add(rfvCommentText);
            div.Controls.Add(p3);
            div.Controls.Add(txtCommentText);
            //+
            div.Controls.Add(this.CaptchaControl);
            //+
            div.Controls.Add(new System.Web.UI.WebControls.Label
            {
                Text = @"
<input type=""button"" id=""btnSubmitComment"" value=""Submit"" />
<p> Notice: all comments are subject to moderation.</p>"
            });
            div.Controls.Add(new System.Web.UI.WebControls.Label
            {
                ID = "lblStatusMessage",
                CssClass = "comment-status"
            });
            HiddenField hfBlogEntryGuid = new HiddenField { ID = "hfBlogEntryGuid" };
            div.Controls.Add(hfBlogEntryGuid);
            //+
            this.Controls.Add(div);
            //+
            this.Controls.Add(new System.Web.UI.WebControls.Label
            {
                Text = @"
<div id=""commentInputCompleted"" style=""display: none;"">
    <p>Comment saved. All comments are moderated and may not show up for some time.</p>
</div>"
            });
            this.Controls.Add(new System.Web.UI.WebControls.Literal
            {
                Text = @"
<script type=""text/javascript"">
document.observe('dom:loaded', function( ) {
    Prominax.AspNet.registerObject('hfBlogEntryGuid', '" + hfBlogEntryGuid.ClientID + @"');
    Prominax.AspNet.registerObject('txtCommentAuthorName', '" + txtCommentAuthorName.ClientID + @"');
    Prominax.AspNet.registerObject('txtCommentAuthorName', '" + txtCommentAuthorName.ClientID + @"');
    Prominax.AspNet.registerObject('txtCommentAuthorEmail', '" + txtCommentAuthorEmail.ClientID + @"');
    Prominax.AspNet.registerObject('txtCommentWebsite', '" + txtCommentWebsite.ClientID + @"');
    Prominax.AspNet.registerObject('txtCommentText', '" + txtCommentText.ClientID + @"');
    Prominax.AspNet.registerObject('rfvCommentAuthorName', '" + rfvCommentAuthorName.ClientID + @"');
    Prominax.AspNet.registerObject('rfvCommentAuthorEmail', '" + rfvCommentAuthorEmail.ClientID + @"');
    Prominax.AspNet.registerObject('rfvCommentText', '" + rfvCommentText.ClientID + @"');
    //+
    Prominax.AspNet.registerObject('lblStatusMessage');
});
</script>"
            });
            //+
            base.CreateChildControls();
        }
    }
}