﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
//+
namespace Minima.Service
{
    [ServiceContract(Namespace = Information.Minima.Namespace)]
    public interface IBlogService
    {
        //- PostBlogEntry -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        [FaultContract(typeof(ArgumentNullException))]
        String PostBlogEntry(String blogGuid, List<Author> authorList, String title, String content, DateTime dateTime, String blogEntryTypeGuid, List<Label> labelList, Boolean publish);

        //- DisableBlogEntry -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void DisableBlogEntry(String blogEntryGuid);

        //- UpdateBlogEntry -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void UpdateBlogEntry(String blogEntryGuid, String title, String content, String blogEntryTypeGuid, List<Label> labelList, DateTime dateTime, Boolean publish);

        //- GetSingleBlogEntry -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        BlogEntry GetSingleBlogEntry(String blogEntryGuid, Boolean metaDataOnly);

        //- GetNetBlogEntryList -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        List<BlogEntry> GetNetBlogEntryList(String blogGuid, String label, String archive, String link, Int32 maxBlogEntryCount);

        //- GetBlogEntryList -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        List<BlogEntry> GetBlogEntryList(String blogGuid, Int32 count, Boolean activeOnly, BlogEntryRetreivalType blogEntryRetreivalType);

        //- GetArchivedEntryList -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        List<ArchiveCount> GetArchivedEntryList(String blogGuid);

        //- GetBlogMetaData -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        BlogMetaData GetBlogMetaData(String blogGuid);

        //- GetBlogListForAssociatedAuthor -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        List<BlogMetaData> GetBlogListForAssociatedAuthor(String authorEmail);

        //- GetBlogListForAssociatedAuthor -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        List<BlogEntryType> GetBlogEntryTypeList(String blogGuid, List<String> guidList);

        //- GetBlogEntryListByDateRange -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        List<BlogEntry> GetBlogEntryListByDateRange(String blogGuid, DateTime startDateTime, DateTime endDateTime, Boolean metaDataOnly);

    }
}