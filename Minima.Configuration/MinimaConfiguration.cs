using System;
//+
using General;
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
                return ConfigurationFacade.ApplicationSettings("ActiveAuthorServiceEndpoint");
            }
        }

        //- @ActiveBlogServiceEndpoint -//
        public static String ActiveBlogServiceEndpoint
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings("ActiveBlogServiceEndpoint");
            }
        }

        //- @ActiveCommentServiceEndpoint -//
        public static String ActiveCommentServiceEndpoint
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings("ActiveCommentServiceEndpoint");
            }
        }
        
        //- @ActiveLabelServiceEndpoint -//
        public static String ActiveLabelServiceEndpoint
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings("ActiveLabelServiceEndpoint");
            }
        }

        //- @BlankLabelMessage -//
        public static String BlankLabelMessage
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings("BlankLabelMessage");
            }
        }

        //- @BlogGuid -//
        public static String BlogGuid
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings("BlogGuid");
            }
        }
        //- @CommentNotificationSubject -//
        public static String CommentNotificationSubject
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings("CommentNotificationSubject");
            }
        }

        //- @EnableTracingViaSerialization -//
        public static Boolean EnableTracingViaSerialization
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings<Boolean>("EnableTracingViaSerialization");
            }
        }

        //- @DefaultMaterialsPhysicalPath -//
        public static String DefaultMaterialsPhysicalPath
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings("DefaultMaterialsPhysicalPath");
            }
        }

        //- @MaterialsRelativePath -//
        public static String MaterialsRelativePath
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings("MaterialsRelativePath");
            }
        }

        //- @SupportImagePhysicalLocation -//
        public static String SupportImagePhysicalLocation
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings("SupportImagePhysicalLocation");
            }
        }

        //- @ForceSpecifiedPath -//
        public static Boolean ForceSpecifiedPath
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings<Boolean>("ForceSpecifiedPath");
            }
        }

        //- @SupportImageFullWebPath -//
        public static String SupportImageFullWebPath
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings("SupportImageFullWebPath");
            }
        }

        //- @SupportImageWebRelativePath -//
        public static String SupportImageWebRelativePath
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings("SupportImageWebRelativePath");
            }
        }

        //- @RecentEntriesToShow -//
        public static Int32 RecentEntriesToShow
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings<Int32>("RecentEntriesToShow");
            }
        }

        //- @ViewableBlogEntryCount -//
        public static Int32 ViewableBlogEntryCount
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings<Int32>("ViewableBlogEntryCount");
            }
        }

        //- @DefaultServiceUserName -//
        public static String DefaultServiceUserName
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings("DefaultServiceUserName");
            }
        }

        //- @DefaultServicePassword -//
        public static String DefaultServicePassword
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings("DefaultServicePassword");
            }
        }

        //- @LinkAuthorToEmail -//
        public static Boolean LinkAuthorToEmail
        {
            get
            {
                return ConfigurationFacade.ApplicationSettings<Boolean>("LinkAuthorToEmail");
            }
        }
    }
}