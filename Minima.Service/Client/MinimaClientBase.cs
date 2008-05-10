using System;
using System.ServiceModel;
//+
namespace Minima.Service.Client
{
    public abstract class MinimaClientBase<T> : ClientBase<T> where T : class
    {
        //- @Ctor -//
        public MinimaClientBase(String endpointConfigurationName)
            : base(endpointConfigurationName) { }

        //+
        //- #AddGuidToMessageHeader -//
        protected void AddGuidToMessageHeader(MinimaMessageHeaderType minimaMessageHeaderType, String guid)
        {
            MessageHeaderHelper<MinimaMessageHeader>.AddOutgoingMessageHeader(new MinimaMessageHeader
            {
                Content = guid,
                HeaderType = minimaMessageHeaderType
            }, "MinimaHeader", "Minima");
        }
    }
}