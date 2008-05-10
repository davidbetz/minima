using System;
using System.Collections.Generic;
using System.ServiceModel;
//+
namespace Minima.Service
{
    [ServiceContract(Namespace = Information.Minima.Namespace)]
    public interface ICommentService
    {
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void AuthorizeComment(String commentGuid);
        
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        List<Comment> GetCommentList(String blogEntryGuid, Boolean showEveryComment);

        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        String PostNewComment(String blogEntryGuid, String text, String author, String email, String website, DateTime dateTime, String emailBodyTemplate, String emailSubject);

        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void DeleteComment(String commentGuid);
    }
}