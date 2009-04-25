#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.ServiceModel;
//+
namespace Minima.Service
{
    [ServiceContract(Namespace = Information.Namespace.Minima)]
    public interface IAuthorService
    {
        //- ApplyAuthor -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void ApplyAuthor(String blogEntryGuid, String authorEmail);

        //- CreateAuthor -//
        [OperationContract]
        String CreateAuthor(String authorEmail, String authorName);

        //- RemoveAuthor -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void RemoveAuthor(String blogEntryGuid, String authorEmail);

        //- UpdateAuthor -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void UpdateAuthor(String authorEmail, String authorName);
    }
}