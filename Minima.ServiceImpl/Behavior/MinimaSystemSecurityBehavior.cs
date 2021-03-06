﻿#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
//+
namespace Minima.Service.Behavior
{
    public class MinimaSystemSecurityBehavior : Attribute, IOperationBehavior
    {
        //- @PermissionRequired -//
        public BlogPermission PermissionRequired { get; set; }

        //+
        //- @AddBindingParameters -//
        public void AddBindingParameters(OperationDescription operationDescription, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            //+ blank
        }

        //- @ApplyClientBehavior -//
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            //+ blank
        }

        //- @ApplyDispatchBehavior -//
        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.Invoker = new MinimaSecureOperationInvoker(dispatchOperation.Invoker, PermissionLevel.System, this.PermissionRequired);
        }

        //- @Validate -//
        public void Validate(OperationDescription operationDescription)
        {
            //+ blank
        }
    }
}