﻿using System;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
//+
namespace Minima.Web.Service
{
    [ServiceContract(Namespace = Information.Minima.Namespace)]
    public interface IFeedService
    {
        //- GetRssFeed -//
        [OperationContract]
        [WebGet(UriTemplate = "GetFeed/{blogGuid}/{maxCount}")]
        Rss20FeedFormatter GetRssFeed(String blogGuid, String maxCount);
    }
}