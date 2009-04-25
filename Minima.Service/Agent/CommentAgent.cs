#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Collections.Generic;
//+
using Minima.Configuration;
using Minima.Service.Client;
//+
namespace Minima.Service.Agent
{
    public class CommentAgent
    {
        //- @AuthorizeComment -//
        public static void AuthorizeComment(String commentGuid)
        {
            using (CommentClient commentClient = new CommentClient(BlogSection.GetConfigSection().Service.Endpoint.Comment))
            {
                commentClient.ClientCredentials.UserName.UserName = BlogSection.GetConfigSection().Service.Authentication.DefaultUserName;
                commentClient.ClientCredentials.UserName.Password = BlogSection.GetConfigSection().Service.Authentication.DefaultPassword;
                //+
                commentClient.AuthorizeComment(commentGuid);
            }
        }

        //- @GetCommentList -//
        public static List<Comment> GetCommentList(String blogEntryGuid, Boolean showEveryComment)
        {
            using (CommentClient commentClient = new CommentClient(BlogSection.GetConfigSection().Service.Endpoint.Comment))
            {
                commentClient.ClientCredentials.UserName.UserName = BlogSection.GetConfigSection().Service.Authentication.DefaultUserName;
                commentClient.ClientCredentials.UserName.Password = BlogSection.GetConfigSection().Service.Authentication.DefaultPassword;
                //+
                return commentClient.GetCommentList(blogEntryGuid, showEveryComment);
            }
        }

        //- @PostNewComment -//
        public static String PostNewComment(String blogEntryGuid, String text, String author, String email, String website, DateTime dateTime, String emailBodyTemplate, String emailSubject)
        {
            using (CommentClient commentClient = new CommentClient(BlogSection.GetConfigSection().Service.Endpoint.Comment))
            {
                commentClient.ClientCredentials.UserName.UserName = BlogSection.GetConfigSection().Service.Authentication.DefaultUserName;
                commentClient.ClientCredentials.UserName.Password = BlogSection.GetConfigSection().Service.Authentication.DefaultPassword;
                //+
                return commentClient.PostNewComment(blogEntryGuid, text, author, email, website, dateTime, emailBodyTemplate, emailSubject);
            }
        }
    }
}