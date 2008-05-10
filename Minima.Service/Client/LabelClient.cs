using System;
using System.Collections.Generic;
using System.ServiceModel;
//+
namespace Minima.Service.Client
{
    public class LabelClient : MinimaClientBase<ILabelService>, ILabelService
    {
        //- @Ctor -//
        public LabelClient(String endpointConfigurationName)
            : base(endpointConfigurationName) { }

        public void ApplyLabel(String blogEntryGuid, String labelGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                base.Channel.ApplyLabel(blogEntryGuid, labelGuid);
            }
        }

        public String CreateLabel(String blogGuid, String title)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.CreateLabel(blogGuid, title);
            }
        }

        public void RemoveLabel(String labelGuid, String blogEntryGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                base.Channel.RemoveLabel(labelGuid, blogEntryGuid);
            }
        }

        public void UpdateLabel(String labelGuid, String title)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.LabelGuid, labelGuid);
                //+
                base.Channel.UpdateLabel(labelGuid, title);
            }
        }

        public List<Label> GetBlogEntryLabelList(String blogEntryGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                return base.Channel.GetBlogEntryLabelList(blogEntryGuid);
            }
        }

        public List<Label> GetBlogLabelList(String blogGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                return base.Channel.GetBlogLabelList(blogGuid);
            }
        }

        public Label GetLabelByTitle(String title)
        {
            return base.Channel.GetLabelByTitle(title);
        }
    }
}