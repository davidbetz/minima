using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
//+
namespace Minima.Service.Behavior
{
    public class FaultHandlingBehavior : Attribute, IServiceBehavior
    {
        //- @AddBindingParameters -//
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            //+ blank
        }

        //- @ApplyDispatchBehavior -//
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                dispatcher.ErrorHandlers.Add(new ErrorHandler(serviceDescription.ServiceType));
            }
        }

        //- @Validate -//
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            //+ blank
        }
    }
}