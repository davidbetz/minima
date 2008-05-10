using System;
using System.Collections.Generic;
using Minima.Service;
using Minima.Service.Client;
using Minima.Configuration;
//+
namespace Minima.Web.Agent
{
    public class LabelAgent
    {
        //- @ApplyLabel -//
        public static void ApplyLabel(String blogEntryGuid, String labelGuid)
        {
            ApplyLabel(blogEntryGuid, labelGuid, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static void ApplyLabel(String blogEntryGuid, String labelGuid, String username, String password)
        {
            using (LabelClient labelClient = new LabelClient(MinimaConfiguration.ActiveLabelServiceEndpoint))
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
            return GetBlogLabelList(blogGuid, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static List<Label> GetBlogLabelList(String blogGuid, String username, String password)
        {
            using (LabelClient labelClient = new LabelClient(MinimaConfiguration.ActiveLabelServiceEndpoint))
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
            return GetLabelByTitle(title, MinimaConfiguration.DefaultServiceUserName, MinimaConfiguration.DefaultServicePassword);
        }
        public static Label GetLabelByTitle(String title, String username, String password)
        {
            using (LabelClient labelClient = new LabelClient(MinimaConfiguration.ActiveLabelServiceEndpoint))
            {
                labelClient.ClientCredentials.UserName.UserName = username;
                labelClient.ClientCredentials.UserName.Password = password;
                //+
                return labelClient.GetLabelByTitle(title);
            }
        }
    }
}