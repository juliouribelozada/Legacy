// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CapturaManualClaims.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   Defines the CapturaManualClaims type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    public static class CapturaManualClaims
    {
        private static bool sealedClaims = false;

        private static ReadOnlyCollection<string> internalClaims;
       
        public static bool SetClaims(string[] claimsStrings)
        {
            if (sealedClaims)
            {
                return false;
            }

            internalClaims = new ReadOnlyCollection<string>(claimsStrings);
            sealedClaims = true;
            return true;
        }

        public static void ClearClaims()
        {
            sealedClaims = false;
            internalClaims = null;
        }

        public static bool ValidateClaims(string claim)
        {
            if (internalClaims == null || sealedClaims == false)
            {
                return false;
            }

            return internalClaims.Any(internalClaim => string.Equals(internalClaim, claim, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
