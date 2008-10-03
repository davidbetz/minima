﻿using System;
using System.IdentityModel.Selectors;
using System.Security;
#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
//+
namespace Minima.Service.Validation
{
    public class MinimaUserNamePasswordValidator : UserNamePasswordValidator
    {
        //- @Validate -//
        public override void Validate(String userName, String password)
        {
            try
            {
                SecurityValidator.ValidateUserNameAndPassword(userName, password);
            }
            catch (SecurityException exception)
            {
                FaultThrower.Throw<SecurityException>(exception);
            }
            catch (ArgumentException exception)
            {
                FaultThrower.Throw<ArgumentException>(exception);
            }
        }
    }
}