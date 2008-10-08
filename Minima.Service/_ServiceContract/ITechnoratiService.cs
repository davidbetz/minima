#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.ServiceModel;
//+
namespace Minima.Service
{
    [ServiceContract(Namespace = Information.Namespace.Minima)]
    public interface ITechnoratiService
    {
        //- PingTechnorati -//
        [OperationContract]
        void PingTechnorati(String blogGuid);
    }
}