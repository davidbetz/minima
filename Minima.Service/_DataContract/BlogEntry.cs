﻿using System;
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

        //- @CommentList -//
        [DataMember]
        public List<Comment> CommentList { get; set; }

        //- @PostDateTime -//
        [DataMember]
        public DateTime PostDateTime { get; set; }

        //- @ModifyDateTime -//
        [DataMember]
        public DateTime ModifyDateTime { get; set; }

        [DataMember]
        public LastAction LastAction { get; set; }

        [DataMember]
        public Uri BlogEntryUri { get; set; }
    }
}