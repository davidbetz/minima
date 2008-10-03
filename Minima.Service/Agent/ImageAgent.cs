#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
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
            using (ImageClient imageClient = new ImageClient(MinimaConfiguration.ActiveImageServiceEndpoint))
            {
                imageClient.ClientCredentials.UserName.UserName = MinimaConfiguration.DefaultServiceUserName;
                imageClient.ClientCredentials.UserName.Password = MinimaConfiguration.DefaultServicePassword;
                //+
                return imageClient.SaveImage(blogImage, blogGuid);
            }
        }

        //- @SaveImage -//
        public static BlogImage GetImage(String blogImageGuid)
        {
            using (ImageClient imageClient = new ImageClient(MinimaConfiguration.ActiveImageServiceEndpoint))
            {
                imageClient.ClientCredentials.UserName.UserName = MinimaConfiguration.DefaultServiceUserName;
                imageClient.ClientCredentials.UserName.Password = MinimaConfiguration.DefaultServicePassword;
                //+
                return imageClient.GetImage(blogImageGuid);
            }
        }
    }
}