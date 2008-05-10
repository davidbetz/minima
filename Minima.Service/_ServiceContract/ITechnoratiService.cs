using System;
using System.ServiceModel;
//+
namespace Minima.Service
{
    [ServiceContract(Namespace = Information.Minima.Namespace)]
    public interface ITechnoratiService
    {
        void PingTechnorati(String blogGuid);
    }
}