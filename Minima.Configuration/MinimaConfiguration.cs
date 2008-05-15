using System;
//+
using General.Configuration;
//+
namespace Minima.Configuration
{
    public static class MinimaConfiguration
    {
        //- @ActiveAuthorServiceEndpoint -//
        public static String ActiveAuthorServiceEndpoint
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("ActiveAuthorServiceEndpoint");
            }
        }

        //- @ActiveBlogServiceEndpoint -//
        public static String ActiveBlogServiceEndpoint
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("ActiveBlogServiceEndpoint");
            }
        }

        //- @ActiveCommentServiceEndpoint -//
        public static String ActiveCommentServiceEndpoint
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("ActiveCommentServiceEndpoint");
            }
        }

        //- @ActiveLabelServiceEndpoint -//
        public static String ActiveLabelServiceEndpoint
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("ActiveLabelServiceEndpoint");
            }
        }

        //- @BlankLabelMessage -//
        public static String BlankLabelMessage
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("BlankLabelMessage");
            }
        }

        //- @CommentNotificationSubject -//
        public static String CommentNotificationSubject
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("CommentNotificationSubject");
            }
        }

        //- @EnableTracingViaSerialization -//
        public static Boolean EnableTracingViaSerialization
        {
            get
            {
                return ConfigAccessor.ApplicationSettings<Boolean>("EnableTracingViaSerialization");
            }
        }

        //- @DefaultMaterialsPhysicalPath -//
        public static String DefaultMaterialsPhysicalPath
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("DefaultMaterialsPhysicalPath");
            }
        }

        //- @MaterialsRelativePath -//
        public static String MaterialsRelativePath
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("MaterialsRelativePath");
            }
        }

        //- @SupportImagePhysicalLocation -//
        public static String SupportImagePhysicalLocation
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("SupportImagePhysicalLocation");
            }
        }

        //- @ForceSpecifiedPath -//
        public static Boolean ForceSpecifiedPath
        {
            get
            {
                return ConfigAccessor.ApplicationSettings<Boolean>("ForceSpecifiedPath");
            }
        }

        //- @SupportImageFullWebPath -//
        public static String SupportImageFullWebPath
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("SupportImageFullWebPath");
            }
        }

        //- @SupportImageWebRelativePath -//
        public static String SupportImageWebRelativePath
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("SupportImageWebRelativePath");
            }
        }

        //- @RecentEntriesToShow -//
        public static Int32 RecentEntriesToShow
        {
            get
            {
                return ConfigAccessor.ApplicationSettings<Int32>("RecentEntriesToShow");
            }
        }

        //- @ViewableBlogEntryCount -//
        public static Int32 ViewableBlogEntryCount
        {
            get
            {
                return ConfigAccessor.ApplicationSettings<Int32>("ViewableBlogEntryCount");
            }
        }

        //- @DefaultServiceUserName -//
        public static String DefaultServiceUserName
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("DefaultServiceUserName");
            }
        }

        //- @DefaultServicePassword -//
        public static String DefaultServicePassword
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("DefaultServicePassword");
            }
        }

        //- @LinkAuthorToEmail -//
        public static Boolean LinkAuthorToEmail
        {
            get
            {
                return ConfigAccessor.ApplicationSettings<Boolean>("LinkAuthorToEmail");
            }
        }
    }
}