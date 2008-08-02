using System;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [DataContract(Namespace = Information.Minima.Namespace)]
    public class BlogImage : IMinimaEntity
    {
        //- @Guid -//
        [DataMember]
        public String Guid { get; set; }

        //- @Data -//
        [DataMember]
        public Byte[] Data { get; set; }

        //- @ContentType -//
        [DataMember]
        public String ContentType { get; set; }

        //- @LastAction -//
        [DataMember]
        public LastAction LastAction { get; set; }
    }
}