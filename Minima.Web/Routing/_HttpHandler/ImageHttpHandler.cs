#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.IO;
using System.Web;
using Minima.Service;
//+
using Minima.Service.Agent;
//+
using Themelia.Web;
//+
namespace Minima.Web.Routing
{
    public class ImageHttpHandler : Themelia.Web.Routing.ReusableHttpHandler
    {
        public class Info : Minima.Web.Info
        {
            public const String ImageContentType = "ImageContentType";
        }

        //+
        //- @ProcessRequest -//
        public override void ProcessRequest(HttpContext context)
        {
            if (Themelia.Web.Http.GetHttpPart(Http.Position.Penultima) == "imagestore")
            {
                Byte[] buffer = HttpData.InputByteArray;
                String contentType = HttpData.GetHeaderItem(Info.ImageContentType);
                if (buffer != null && buffer.Length > 0 && !String.IsNullOrEmpty(contentType))
                {
                    String blogGuid = Themelia.Web.Http.GetHttpPart(Http.Position.Ultima);
                    BlogImage blogImage = new BlogImage
                    {
                        ContentType = contentType,
                        Data = buffer
                    };
                    //+
                    try
                    {
                        context.Response.Write(ImageAgent.SaveImage(blogImage, blogGuid));
                    }
                    catch
                    {
                    }
                }
                else
                {
                    String blogImageGuid = Themelia.Web.Http.GetHttpPart(Http.Position.Ultima);
                    if (!String.IsNullOrEmpty(blogImageGuid))
                    {
                        BlogImage blogImage = ImageAgent.GetImage(blogImageGuid);
                        if (blogImage.Data != null && blogImage.Data.Length > 0)
                        {
                            context.Response.ContentType = blogImage.ContentType;
                            context.Response.BinaryWrite(blogImage.Data);
                        }
                    }
                }
            }
        }
    }
}