﻿#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
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
            using (CommentClient commentClient = new CommentClient(MinimaConfiguration.ActiveCommentServiceEndpoint))
            {
                commentClient.ClientCredentials.UserName.UserName = MinimaConfiguration.DefaultServiceUserName;
                commentClient.ClientCredentials.UserName.Password = MinimaConfiguration.DefaultServicePassword;
                //+
                commentClient.AuthorizeComment(commentGuid);
            }
        }

        //- @GetCommentList -//
        public static List<Comment> GetCommentList(String blogEntryGuid, Boolean showEveryComment)
        {
            using (CommentClient commentClient = new CommentClient(MinimaConfiguration.ActiveCommentServiceEndpoint))
            {
                commentClient.ClientCredentials.UserName.UserName = MinimaConfiguration.DefaultServiceUserName;
                commentClient.ClientCredentials.UserName.Password = MinimaConfiguration.DefaultServicePassword;
                //+
                return commentClient.GetCommentList(blogEntryGuid, showEveryComment);
            }
        }

        //- @PostNewComment -//
        public static String PostNewComment(String blogEntryGuid, String text, String author, String email, String website, DateTime dateTime, String emailBodyTemplate, String emailSubject)
        {
            using (CommentClient commentClient = new CommentClient(MinimaConfiguration.ActiveCommentServiceEndpoint))
            {
                commentClient.ClientCredentials.UserName.UserName = MinimaConfiguration.DefaultServiceUserName;
                commentClient.ClientCredentials.UserName.Password = MinimaConfiguration.DefaultServicePassword;
                //+
                return commentClient.PostNewComment(blogEntryGuid, text, author, email, website, dateTime, emailBodyTemplate, emailSubject);
            }
        }
    }
}