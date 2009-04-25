#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
//+
using Minima.Service.Behavior;
//+
namespace Minima.Service.Activation
{
    public class MinimaServiceHostFactory : ServiceHostFactory
    {
        //- @CreateServiceHost -//
        public ServiceHost CreateServiceHost(Uri[] baseAddresses)
        {
            return (ServiceHost)CreateServiceHost(typeof(ServiceHost), baseAddresses);
        }

        //- #CreateServiceHost -//
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            ServiceHost host = new ServiceHost(serviceType, baseAddresses);
            //+ add fault handler
            host.Description.Behaviors.Add(new Themelia.ServiceModel.Behavior.FaultHandlingBehavior());
            //+ add metadata exchange
            ServiceMetadataBehavior serviceMetadataBehavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (serviceMetadataBehavior == null)
            {
                serviceMetadataBehavior = new ServiceMetadataBehavior();
                host.Description.Behaviors.Add(serviceMetadataBehavior);
            }
            serviceMetadataBehavior.HttpGetEnabled = true;
            ServiceEndpoint serviceEndpoint = host.Description.Endpoints.Find(typeof(IMetadataExchange));
            if (serviceEndpoint == null)
            {
                host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
            }
            //+
            return host;
        }
    }
}