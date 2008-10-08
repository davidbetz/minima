#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
//+
namespace Minima.Web.Service
{
    [ServiceContract(Namespace = Information.Namespace.Minima)]
    public interface IFeedService
    {
        //- GetRssFeed -//
        [OperationContract]
        [WebGet(UriTemplate = "GetFeed/{blogGuid}/{maxCount}")]
        Rss20FeedFormatter GetRssFeed(String blogGuid, String maxCount);
    }
}