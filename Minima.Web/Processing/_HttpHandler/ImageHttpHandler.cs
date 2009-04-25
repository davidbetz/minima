#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using Minima.Service;
//+
using Minima.Service.Agent;
//+
using Themelia.Web;
//+
namespace Minima.Web.Processing
{
    public class ImageHttpHandler : Themelia.Web.ReusableHttpHandler
    {
        public class Info : Minima.Web.Info
        {
            public const String ImageContentType = "ImageContentType";
        }

        //+
        //- @ProcessRequest -//
        public override void Process()
        {
            if (Themelia.Web.Http.GetUrlPart(Http.Position.Penultima) == "imagestore")
            {
                Byte[] buffer = HttpData.GetInputHttpByteArray();
                String contentType = HttpData.GetHeaderItem(Info.ImageContentType);
                if (buffer != null && buffer.Length > 0 && !String.IsNullOrEmpty(contentType))
                {
                    String blogGuid = Themelia.Web.Http.GetUrlPart(Http.Position.Ultima);
                    BlogImage blogImage = new BlogImage
                    {
                        ContentType = contentType,
                        Data = buffer
                    };
                    //+
                    try
                    {
                        Response.Write(ImageAgent.SaveImage(blogImage, blogGuid));
                    }
                    catch
                    {
                    }
                }
                else
                {
                    String blogImageGuid = Themelia.Web.Http.GetUrlPart(Http.Position.Ultima);
                    if (!String.IsNullOrEmpty(blogImageGuid))
                    {
                        BlogImage blogImage = ImageAgent.GetImage(blogImageGuid);
                        if (blogImage.Data != null && blogImage.Data.Length > 0)
                        {
                            ContentType = blogImage.ContentType;
                            Response.BinaryWrite(blogImage.Data);
                        }
                    }
                }
            }
        }
    }
}