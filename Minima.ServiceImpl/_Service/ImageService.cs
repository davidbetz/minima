using System;
using System.Data.Linq;
//+
using Minima.Service.Behavior;
using Minima.Service.Validation;
using BlogImageLINQ = Minima.Service.Data.Entity.BlogImage;
//+
using BlogLINQ = Minima.Service.Data.Entity.Blog;
//+
using MinimaServiceLINQDataContext = Minima.Service.Data.Context.MinimaServiceLINQDataContext;
//+
namespace Minima.Service
{
    public class ImageService : IImageService
    {
        //- @Message -//
        public class Message
        {
            public const string ImageDataNull = "Image data may not be null";
            public const string ContentTypeBlank = "Image data may not be null";
        }

        //- @SaveImage -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Create)]
        public String SaveImage(BlogImage blogImage, String blogGuid)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog exists
                BlogLINQ blogLinq;
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                Validator.EnsureIsNotNull(blogImage.Data, Message.ImageDataNull);
                Validator.IsNotBlank(blogImage.ContentType, Message.ContentTypeBlank);
                //+
                Binary imageBinary = new Binary(blogImage.Data);
                BlogImageLINQ blogImageLinq = new BlogImageLINQ();
                blogImageLinq.BlogId = blogLinq.BlogId;
                blogImageLinq.BlogImageContentType = blogImage.ContentType;
                blogImageLinq.BlogImageData = imageBinary;
                blogImageLinq.BlogImageGuid = Themelia.GuidCreator.NewDatabaseGuid;
                //+
                db.BlogImages.InsertOnSubmit(blogImageLinq);
                db.SubmitChanges();
                //+
                return blogImageLinq.BlogImageGuid;
            }
        }

        //- @GetImage -//
        [MinimaBlogSecurityBehavior(PermissionRequired = BlogPermission.Retrieve)]
        public BlogImage GetImage(String blogImageGuid)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                //+ ensure blog  image exists
                BlogImageLINQ blogImageLinq;
                Validator.EnsureBlogImageExists(blogImageGuid, out blogImageLinq, db);
                //+
                Binary imageBinary = blogImageLinq.BlogImageData;
                //+
                return new BlogImage
                {
                    ContentType = blogImageLinq.BlogImageContentType,
                    Data = imageBinary.ToArray()
                };
            }
        }
    }
}