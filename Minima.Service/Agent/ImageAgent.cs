#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Minima.Configuration;
using Minima.Service.Client;
//+
namespace Minima.Service.Agent
{
    public class ImageAgent
    {
        //- @SaveImage -//
        public static String SaveImage(BlogImage blogImage, String blogGuid)
        {
            using (ImageClient imageClient = new ImageClient(BlogSection.GetConfigSection().Service.Endpoint.Image))
            {
                imageClient.ClientCredentials.UserName.UserName = BlogSection.GetConfigSection().Service.Authentication.DefaultUserName;
                imageClient.ClientCredentials.UserName.Password = BlogSection.GetConfigSection().Service.Authentication.DefaultPassword;
                //+
                return imageClient.SaveImage(blogImage, blogGuid);
            }
        }

        //- @SaveImage -//
        public static BlogImage GetImage(String blogImageGuid)
        {
            using (ImageClient imageClient = new ImageClient(BlogSection.GetConfigSection().Service.Endpoint.Image))
            {
                imageClient.ClientCredentials.UserName.UserName = BlogSection.GetConfigSection().Service.Authentication.DefaultUserName;
                imageClient.ClientCredentials.UserName.Password = BlogSection.GetConfigSection().Service.Authentication.DefaultPassword;
                //+
                return imageClient.GetImage(blogImageGuid);
            }
        }
    }
}