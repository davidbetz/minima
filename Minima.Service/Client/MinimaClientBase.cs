#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
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