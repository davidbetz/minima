using System;
using System.Runtime.Serialization;
//+
namespace Minima.Service
{
    [DataContract]
    public class MinimaMessageHeader
    {
        [DataMember]
        public MinimaMessageHeaderType HeaderType { get; set; }

        [DataMember]
        public String Content { get; set; }
    }
}