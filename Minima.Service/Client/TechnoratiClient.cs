﻿#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.ServiceModel;
//+
namespace Minima.Service.Client
{
    public class TechnoratiClient : MinimaClientBase<ITechnoratiService>, ITechnoratiService
    {
        //- @Ctor -//
        public TechnoratiClient(String endpointConfigurationName)
            : base(endpointConfigurationName) { }

        public void PingTechnorati(String blogGuid)
        {
            using (OperationContextScope scope = new OperationContextScope(this.InnerChannel))
            {
                AddGuidToMessageHeader(MinimaMessageHeaderType.BlogGuid, blogGuid);
                //+
                base.Channel.PingTechnorati(blogGuid);
            }
        }
    }
}