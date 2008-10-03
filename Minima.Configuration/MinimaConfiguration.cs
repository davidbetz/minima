#region Copyright
//+ Copyright � Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Themelia.Configuration;
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

        //- @ActiveImageServiceEndpoint -//
        public static String ActiveImageServiceEndpoint
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("ActiveImageServiceEndpoint");
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

        //- @EnableActivityLogging -//
        public static Boolean EnableActivityLogging
        {
            get
            {
                return ConfigAccessor.ApplicationSettings<Boolean>("EnableActivityLogging", false);
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

        //- @IndexHeadingSuffix -//
        public static String IndexHeadingSuffix
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("IndexHeadingSuffix", "Index");
            }
        }

        //- @ArchiveHeadingSuffix -//
        public static String ArchiveHeadingSuffix
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("ArchiveHeadingSuffix", "Contents");
            }
        }

        //- @LabelHeadingSuffix -//
        public static String LabelHeadingSuffix
        {
            get
            {
                return ConfigAccessor.ApplicationSettings("LabelHeadingSuffix", "Label Contents");
            }
        }
    }
}