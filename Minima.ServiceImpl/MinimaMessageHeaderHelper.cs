#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
namespace Minima.Service
{
    public static class MinimaMessageHeaderHelper
    {
        //- @GetBlogGuidFromMessageHeader -//
        public static String GetBlogGuidFromMessageHeader()
        {
            MinimaMessageHeader minimaMessageHeader = MessageHeaderHelper<MinimaMessageHeader>.GetAddOutgoingMessageHeader("MinimaHeader", "Minima");
            switch (minimaMessageHeader.HeaderType)
            {
                case MinimaMessageHeaderType.BlogGuid:
                    return minimaMessageHeader.Content;
                case MinimaMessageHeaderType.BlogEntryGuid:
                    return BlogGuidFinder.ByBlogEntryGuid(minimaMessageHeader.Content);
                case MinimaMessageHeaderType.CommentGuid:
                    return BlogGuidFinder.ByCommentGuid(minimaMessageHeader.Content);
                case MinimaMessageHeaderType.LabelGuid:
                    return BlogGuidFinder.ByLabelGuid(minimaMessageHeader.Content);
                case MinimaMessageHeaderType.BlogImageGuid:
                    return BlogGuidFinder.ByBlogImageGuid(minimaMessageHeader.Content);
                default:
                    return null;
            }
        }
    }
}