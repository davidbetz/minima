#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
namespace Minima.Service
{
    public interface IMinimaEntity
    {
        //- LastAction -//
        LastAction LastAction { get; set; }
    }
}