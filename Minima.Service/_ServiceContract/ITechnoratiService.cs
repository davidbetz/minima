using System;
using System.ServiceModel;
//+
namespace Minima.Service
{
    [ServiceContract(Namespace = Information.Minima.Namespace)]
    public interface ITechnoratiService
    {
        //- PingTechnorati -//
        [OperationContract]
        void PingTechnorati(String blogGuid);
    }
}