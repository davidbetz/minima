#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
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
            : base(endpointConfigurationName)
        {
            if (System.Net.ServicePointManager.ServerCertificateValidationCallback == null)
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ServerCertificateValidator.Validate);
            }
        }

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