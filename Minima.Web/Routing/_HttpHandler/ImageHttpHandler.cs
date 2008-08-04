﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
//+
using Themelia.Web;
//+
using Minima.Web.Agent;
using Minima.Service.Client;
using Minima.Service;
//+
namespace Minima.Web.Routing
{
    public class ImageHttpHandler : IHttpHandler
    {
        public class Data
        {
            public const string HeaderName = "ImageContentType";
        }

        //- @IsReusable -//
        public Boolean IsReusable
        {
            get { return true; }
        }

        //+
        //- @ProcessRequest -//
        public void ProcessRequest(HttpContext context)
        {
            if (Themelia.Web.Http.GetHttpPart(Http.Position.Penultima) == "blog"
                 && Themelia.Web.Http.GetHttpPart(Http.Position.Antepenultima) == "image")
            {
                Stream inputStream = context.Request.InputStream;
                if (inputStream != null && inputStream.Length > 0 && context.Request.Headers[Data.HeaderName] != null)
                {
                    String contentType = context.Request.Headers["ImageContentType"];
                    Byte[] buffer = new Byte[inputStream.Length];
                    inputStream.Read(buffer, 0, (Int32)inputStream.Length);
                    //+
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