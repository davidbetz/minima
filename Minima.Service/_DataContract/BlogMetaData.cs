#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [DataContract(Namespace = Information.Minima.Namespace)]
    public class BlogMetaData : IMinimaEntity
    {
        //- @Guid -//
        [DataMember]
        public String Guid { get; set; }

        //- @Title -//
        [DataMember]
        public String Title { get; set; }

        //- @Description -//
        [DataMember]
        public String Description { get; set; }

        //- @Uri -//
        [DataMember]
        public Uri Uri { get; set; }

        //- @FeedTitle -//
        [DataMember]
        public String FeedTitle { get; set; }

        //- @FeedUri -//
        [DataMember]
        public Uri FeedUri { get; set; }

        //- @LabelList -//
        [DataMember]
        public List<Label> LabelList { get; set; }

        //- @CreateDateTime -//
        [DataMember]
        public DateTime CreateDateTime { get; set; }

        //- @LastAction -//
        [DataMember]
        public LastAction LastAction { get; set; }
    }
}