﻿using System;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [DataContract(Namespace = Information.Minima.Namespace)]
    public class Comment : IMinimaEntity
    {
        //- @Guid -//
        [DataMember]
        public String Guid { get; set; }

        //- @BlogEntryGuid -//
        [DataMember]
        public String BlogEntryGuid { get; set; }

        //- @Name -//
        [DataMember]
        public String Name { get; set; }

        //- @Email -//
        [DataMember]
        public String Email { get; set; }

        //- @Website -//
        [DataMember]
        public String Website { get; set; }

        //- @Text -//
        [DataMember]
        public String Text { get; set; }

        //- @IsModerated -//
        [DataMember]
        public Boolean IsModerated { get; set; }

        //- @DateTime -//
        [DataMember]
        public DateTime DateTime { get; set; }

        //- @LastAction -//
        [DataMember]
        public LastAction LastAction { get; set; }
    }
}