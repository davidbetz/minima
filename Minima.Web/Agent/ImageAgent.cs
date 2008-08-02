using System;
using System.Collections.Generic;
using System.IO;
//+
using Minima.Configuration;
using Minima.Service;
using Minima.Service.Client;
//+
namespace Minima.Web.Agent
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