#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Collections.Generic;
using System.ServiceModel;
//+
namespace Minima.Service
{
    [ServiceContract(Namespace = Information.Namespace.Minima)]
    public interface ICommentService
    {
        //- AuthorizeComment -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void AuthorizeComment(String commentGuid);

        //- GetCommentList -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        List<Comment> GetCommentList(String blogEntryGuid, Boolean showEveryComment);

        //- PostNewComment -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        String PostNewComment(String blogEntryGuid, String text, String author, String email, String website, DateTime dateTime, String emailBodyTemplate, String emailSubject);

        //- DeleteComment -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void DeleteComment(String commentGuid);
    }
}