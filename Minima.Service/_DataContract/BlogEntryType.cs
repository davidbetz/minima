using System;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [DataContract(Namespace = Information.Minima.Namespace)]
    public class BlogEntryType : IMinimaEntity
    {
        //- @Guid -//
        [DataMember]
        public String Guid { get; set; }

        //- @Name -//
        [DataMember]
        public String Name { get; set; }

        //- @Extra -//
        [DataMember]
        public String Extra { get; set; }

        //- @LastAction -//
        [DataMember]
        public LastAction LastAction { get; set; }
    }
}