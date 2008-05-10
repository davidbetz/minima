using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
//+
namespace Minima.Service.Behavior
{
    public class ErrorHandler : IErrorHandler
    {
        //- @ServiceType -//
        public Type ServiceType { get; set; }

        //+
        //- @Ctor -//
        public ErrorHandler(Type serviceType)
        {
            ServiceType = serviceType;
        }

        //+
        //- @HandleError -//
        public bool HandleError(Exception error)
        {
            return false;
        }

        //- @ProvideFault -//
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            FaultCreator.CreateFault(this.ServiceType, error, version, ref fault);
        }
    }
}