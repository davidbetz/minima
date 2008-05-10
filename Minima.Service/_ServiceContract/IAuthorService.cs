using System;
using System.ServiceModel;
//+
namespace Minima.Service
{
    [ServiceContract(Namespace = Information.Minima.Namespace)]
    public interface IAuthorService
    {
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void ApplyAuthor(String blogEntryGuid, String authorEmail);

        [OperationContract]
        String CreateAuthor(String authorEmail, String authorName);

        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void RemoveAuthor(String blogEntryGuid, String authorEmail);

        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void UpdateAuthor(String authorEmail, String authorName);
    }
}