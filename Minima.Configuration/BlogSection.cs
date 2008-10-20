#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.ComponentModel;
using System.Configuration;
//+
namespace Minima.Configuration
{
    /// <summary>
    /// Provides access to the configuration section.
    /// </summary>
    public class BlogSection : ConfigurationSection
    {
        //- @EntriesToShow -//
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ConfigurationProperty("entriesToShow")]
        public Int32 EntriesToShow
        {
            get
            {
                return (Int32)this["entriesToShow"];
            }
        }

        //- @Domain -//
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ConfigurationProperty("domain")]
        public String Domain
        {
            get
            {
                return (String)this["domain"];
            }
        }

        //- @Service -//
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ConfigurationProperty("service")]
        public ServiceElement Service
        {
            get
            {
                return (ServiceElement)this["service"];
            }
        }

        //- @Suffix -//
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ConfigurationProperty("suffix")]
        public SuffixElement Suffix
        {
            get
            {
                return (SuffixElement)this["suffix"];
            }
        }

        //- @Display -//
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ConfigurationProperty("display")]
        public DisplayElement Display
        {
            get
            {
                return (DisplayElement)this["display"];
            }
        }

        //- @Comment -//
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ConfigurationProperty("comment")]
        public CommentElement Comment
        {
            get
            {
                return (CommentElement)this["comment"];
            }
        }

        //- @CodeParsers -//
        [ConfigurationProperty("codeParsers")]
        [ConfigurationCollection(typeof(CodeParserCollection), AddItemName = "add")]
        public CodeParserCollection CodeParsers
        {
            get
            {
                return (CodeParserCollection)this["codeParsers"];
            }
            set
            {
                this["codeParsers"] = value;
            }
        }

        //+
        //- @GetConfigSection -//
        /// <summary>
        /// Gets the config section.
        /// </summary>
        /// <returns>Configuration section</returns>
        public static BlogSection GetConfigSection()
        {
            return (BlogSection)ConfigurationManager.GetSection("minima.blog") ?? new BlogSection();
        }
    }
}