using System;
using System.ServiceModel.Activation;
using System.Web;
//+
using Minima.Configuration;
using Minima.Service.Agent;
using Themelia.Web;
//+
namespace Minima.Web.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class CommentService : ICommentService
    {
        //- @InitCaptchaMath -//
        public CaptchMathInformation InitCaptchaMath()
        {
            Random r = new Random((Int32)DateTime.Now.Ticks);
            Int32 a = r.Next(10);
            Int32 b = r.Next(10);
            //+
            HttpData.SetScopedSessionItem<Int32>("Captcha", "ExpectedValue", a + b);
            return new CaptchMathInformation
            {
                A = a,
                B = b
            };
        }

        //- @AuthorizeComment -//
        public String AuthorizeComment(String commentGuid)
        {
            try
            {
                CommentAgent.AuthorizeComment(commentGuid);
            }
            catch
            {
                return "Invalid guid or error.";
            }
            //+
            return "Comment authorized.";
        }

        //- @PostNewComment -//
        public Int32 PostNewComment(Int32 captchaValue, String blogEntryGuid, String author, String email, String website, String text)
        {
            Int32 returnStatus;
            //+
            if (captchaValue == HttpData.GetScopedSessionItem<Int32>("Captcha", "ExpectedValue"))
            {
                String emailBodyTemplate = @"                
<h4>New Comment!</h4>
<p>This comment has been moderated.</p>
<p>Blog EntryTitle: {0}</p>
<p>BlogEntry Guid: " + blogEntryGuid + @"</p>
<p>Comment author: " + author + @"</p>
<p>Comment email: " + email + @"</p>
<p>Comment website: " + website + @"</p>
<p>Comment date/time: " + DateTime.Now.ToString() + @"</p>
<p>Comment text: " + text + @"</p>
<p><b>This link is moderated, <a href=\""" + WebConfiguration.Domain + @"services/comment/{1}\"">click here to unmoderate</a>.</b></p>";
                Themelia.Configuration.SystemSection systemSection = Themelia.Configuration.SystemSection.GetConfigSection();
                String emailSubject = String.Format("{0} ({1})", MinimaConfiguration.CommentNotificationSubject, systemSection.AppInfo.Name);
                //+
                String commentGuid = String.Empty;
                returnStatus = 0;
                try
                {
                    commentGuid = CommentAgent.PostNewComment(blogEntryGuid, text, author, email, website, DateTime.Now, emailBodyTemplate, emailSubject);
                }
                catch(Exception ex)
                {
                    if (ex.Message != "Failure sending mail.")
                    {
                        returnStatus = 1;
                    }
                }
            }
            else
            {
                returnStatus = 2;
            }
            //+
            return returnStatus;
        }
    }
}