using System;
using System.Linq;
using System.Security;
using System.ServiceModel;
//+
using Minima.Service.Validation;
//+
using MinimaServiceLINQDataContext = Minima.Service.Data.Context.MinimaServiceLINQDataContext;
using AuthorLINQ = Minima.Service.Data.Entity.Author;
using BlogLINQ = Minima.Service.Data.Entity.Blog;
//+
namespace Minima.Service
{
    internal class SecurityValidator
    {
        //- ~Message -//
        private class Message
        {
            internal const String Invalid = "Username or password is invalid";
        }

        //- ~ValidateBlogPermission -//
        internal static void ValidateBlogPermission(BlogPermission blogPermission, String blogGuid)
        {
            Char databasePermissionCode = GetDatabasePermissionCode(blogPermission);
            //+
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                String authorEmail = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
                //+ validate
                AuthorLINQ authorLinq;
                BlogLINQ blogLinq;
                Validator.EnsureAuthorExists(authorEmail, out authorLinq, db);
                Validator.EnsureBlogExists(blogGuid, out blogLinq, db);
                //+
                if (!authorLinq.UserRights.Any(ur => ur.UserRightLevel == PermissionLevel.Blog
                    && ur.BlogId == blogLinq.BlogId
                    && ur.UserRightType == databasePermissionCode))
                {
                    throw new SecurityException(Message.Invalid);
                }
            }
        }

        //- ~ValidateSystemPermission -//
        internal static void ValidateSystemPermission(BlogPermission blogPermission)
        {
            Char databasePermissionCode = GetDatabasePermissionCode(blogPermission);
            //+
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                String authorEmail = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
                //+ validate
                AuthorLINQ authorLinq;
                Validator.EnsureAuthorExists(authorEmail, out authorLinq, db);
                //+
                if (!authorLinq.UserRights.Any(ur => ur.UserRightLevel == PermissionLevel.System
                    && ur.UserRightType == databasePermissionCode))
                {
                    throw new SecurityException(Message.Invalid);
                }
            }
        }

        //- ~ValidateUserNameAndPassword -//
        internal static void ValidateUserNameAndPassword(String userName, String password)
        {
            using (MinimaServiceLINQDataContext db = new MinimaServiceLINQDataContext(ServiceConfiguration.ConnectionString))
            {
                String authorEmail = userName;
                //+ validate
                AuthorLINQ authorLinq;
                Validator.EnsureAuthorExists(authorEmail, out authorLinq, db);
                //+
                if (authorLinq == null || authorLinq.AuthorPassword != password)
                {
                    throw new SecurityException(Message.Invalid);
                }
            }
        }

        //- $GetDatabasePermissionCode -//
        private static Char GetDatabasePermissionCode(BlogPermission blogPermission)
        {
            switch (blogPermission)
            {
                case BlogPermission.Create:
                    return 'C';
                case BlogPermission.Retrieve:
                    return 'R';
                case BlogPermission.Update:
                    return 'U';
                case BlogPermission.Delete:
                    return 'D';
                default:
                    throw new InvalidOperationException("Invalid blog permission required.");
            }
        }
    }
}