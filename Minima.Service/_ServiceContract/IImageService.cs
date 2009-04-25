﻿#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.ServiceModel;
//+
namespace Minima.Service
{
    [ServiceContract(Namespace = Information.Namespace.Minima)]
    public interface IImageService
    {
        //- SaveImage -//
        [OperationContract]
        String SaveImage(BlogImage blogImage, String blogGuid);

        //- GetImage -//
        [OperationContract]
        BlogImage GetImage(String blogImageGuid);
    }
}