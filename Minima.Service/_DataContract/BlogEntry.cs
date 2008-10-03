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
    public class BlogEntry : IMinimaEntity
    {
        //- @Guid -//
        [DataMember]
        public String Guid { get; set; }

        //- @Title -//
        [DataMember]
        public String Title { get; set; }

        //- @AllowCommentStatus -//
        [DataMember]
        public AllowCommentStatus AllowCommentStatus { get; set; }

        //- @Content -//
        [DataMember]
        public String Content { get; set; }

        //- @Status -//
        [DataMember]
        public Int32 Status { get; set; }

        //- @AuthorList -//
        [DataMember]
        public List<Author> AuthorList { get; set; }

        //- @LabelList -//
        [DataMember]
        public List<Label> LabelList { get; set; }

        //- @BlogEntryTypeGuid -//
        [DataMember]
        public String BlogEntryTypeGuid { get; set; }

        //- @CommentList -//
        [DataMember]
        public List<Comment> CommentList { get; set; }

        //- @MappingNameList -//
        [DataMember]
        public List<String> MappingNameList { get; set; }

        //- @PostDateTime -//
        [DataMember]
        public DateTime PostDateTime { get; set; }

        //- @ModifyDateTime -//
        [DataMember]
        public DateTime ModifyDateTime { get; set; }

        //- @LastAction -//
        [DataMember]
        public LastAction LastAction { get; set; }
    }
}