using System;
using System.IO;
using System.ServiceModel;
//+
namespace Minima.Service.Client
{
    public class ImageClient : MinimaClientBase<IImageService>, IImageService
    {
        //- @Ctor -//
        public ImageClient(String endpointConfigurationName)
            : base(endpointConfigurationName) { }

        public BlogImage GetImage(String blogImageGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogImageGuid, blogImageGuid);
                //+
                return base.Channel.GetImage(blogImageGuid);
            }
        }

        public String SaveImage(BlogImage blogImage, String blogGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.SaveImage(blogImage, blogGuid);
            }
        }
    }
}