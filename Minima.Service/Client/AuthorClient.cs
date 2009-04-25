#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.ServiceModel;
//+
namespace Minima.Service.Client
{
    public class AuthorClient : MinimaClientBase<IAuthorService>, IAuthorService
    {
        //- @Ctor -//
        public AuthorClient(String endpointConfigurationName)
            : base(endpointConfigurationName) { }


        public void ApplyAuthor(String blogEntryGuid, String authorEmail)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                base.Channel.ApplyAuthor(blogEntryGuid, authorEmail);
            }
        }

        public String CreateAuthor(String authorEmail, String authorName)
        {
            return base.Channel.CreateAuthor(authorEmail, authorName);
        }

        public void RemoveAuthor(String blogEntryGuid, String authorEmail)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogEntryGuid, blogEntryGuid);
                //+
                base.Channel.RemoveAuthor(blogEntryGuid, authorEmail);
            }
        }

        public void UpdateAuthor(String authorEmail, String authorName)
        {
            base.Channel.UpdateAuthor(authorEmail, authorName);
        }
    }
}