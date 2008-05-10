using System;
using System.Collections.Generic;
using System.ServiceModel;
//+
namespace Minima.Service.Client
{
    public class CommentClient : MinimaClientBase<ICommentService>, ICommentService
    {
        //- @Ctor -//
        public CommentClient(String endpointConfigurationName)
            : base(endpointConfigurationName) { }

        public void AuthorizeComment(String commentGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.CommentGuid, commentGuid);
                //+
                base.Channel.AuthorizeComment(commentGuid);
            }
        }

        public List<Comment> GetCommentList(String blogEntryGuid, Boolean showEveryComment)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                return base.Channel.GetCommentList(blogEntryGuid, showEveryComment);
            }
        }

        public String PostNewComment(String blogEntryGuid, String text, String author, String email, String website, DateTime dateTime, String emailBodyTemplate, String emailSubject)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                return base.Channel.PostNewComment(blogEntryGuid, text, author, email, website, dateTime, emailBodyTemplate, emailSubject);
            }
        }

        public void DeleteComment(String commentGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.CommentGuid, commentGuid);
                //+
                base.Channel.DeleteComment(commentGuid);
            }
        }
    }
}