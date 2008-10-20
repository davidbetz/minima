#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.ServiceModel.Activation;
using System.Web;
//+
using Themelia.Web;
//+
using Minima.Configuration;
using Minima.Service.Agent;
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
                CommentReportCreator creator = new CommentReportCreator();
                creator.Formatter = new Themelia.Reporting.HtmlFormatter();
                Themelia.Map map = new Themelia.Map();
                map.Add("BlogEntryTitle", "{BlogEntryTitle}");
                map.Add("Guid", blogEntryGuid);
                map.Add("Author", author);
                map.Add("Email", email);
                map.Add("WebSite", website);
                map.Add("DateTime", DateTime.Now.ToString());
                map.Add("Text", text);
                map.Add("Link", WebConfiguration.Domain + @"services/comment/{CommentGuid}");
                String emailBodyTemplate = creator.Create(map);
                Themelia.Configuration.SystemSection systemSection = Themelia.Configuration.SystemSection.GetConfigSection();
                String emailSubject = String.Format("{0} ({1})", BlogSection.GetConfigSection().Comment.Subject, systemSection.AppInfo.Name);
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