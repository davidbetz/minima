#region Copyright
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
    public class LabelAgent
    {
        //- @ApplyLabel -//
        public static void ApplyLabel(String blogEntryGuid, String labelGuid)
        {
            ApplyLabel(blogEntryGuid, labelGuid, BlogSection.GetConfigSection().Service.Authentication.DefaultUserName, BlogSection.GetConfigSection().Service.Authentication.DefaultPassword);
        }
        public static void ApplyLabel(String blogEntryGuid, String labelGuid, String username, String password)
        {
            using (LabelClient labelClient = new LabelClient(BlogSection.GetConfigSection().Service.Endpoint.Label))
            {
                labelClient.ClientCredentials.UserName.UserName = username;
                labelClient.ClientCredentials.UserName.Password = password;
                //+
                labelClient.ApplyLabel(blogEntryGuid, labelGuid);
            }
        }

        //- @GetBlogLabelList -//
        public static List<Label> GetBlogLabelList(String blogGuid)
        {
            return GetBlogLabelList(blogGuid, BlogSection.GetConfigSection().Service.Authentication.DefaultUserName, BlogSection.GetConfigSection().Service.Authentication.DefaultPassword);
        }
        public static List<Label> GetBlogLabelList(String blogGuid, String username, String password)
        {
            using (LabelClient labelClient = new LabelClient(BlogSection.GetConfigSection().Service.Endpoint.Label))
            {
                labelClient.ClientCredentials.UserName.UserName = username;
                labelClient.ClientCredentials.UserName.Password = password;
                //+
                return labelClient.GetBlogLabelList(blogGuid);
            }
        }

        //- @GetLabelByTitle -//
        public static Label GetLabelByTitle(String title)
        {
            return GetLabelByTitle(title, BlogSection.GetConfigSection().Service.Authentication.DefaultUserName, BlogSection.GetConfigSection().Service.Authentication.DefaultPassword);
        }
        public static Label GetLabelByTitle(String title, String username, String password)
        {
            using (LabelClient labelClient = new LabelClient(BlogSection.GetConfigSection().Service.Endpoint.Label))
            {
                labelClient.ClientCredentials.UserName.UserName = username;
                labelClient.ClientCredentials.UserName.Password = password;
                //+
                return labelClient.GetLabelByTitle(title);
            }
        }

        //- @GetLabelByNetTitle -//
        public static Label GetLabelByNetTitle(String friendlyTitle)
        {
            return GetLabelByNetTitle(friendlyTitle, BlogSection.GetConfigSection().Service.Authentication.DefaultUserName, BlogSection.GetConfigSection().Service.Authentication.DefaultPassword);
        }
        public static Label GetLabelByNetTitle(String friendlyTitle, String username, String password)
        {
            using (LabelClient labelClient = new LabelClient(BlogSection.GetConfigSection().Service.Endpoint.Label))
            {
                labelClient.ClientCredentials.UserName.UserName = username;
                labelClient.ClientCredentials.UserName.Password = password;
                //+
                return labelClient.GetLabelByNetTitle(friendlyTitle);
            }
        }
    }
}