using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;


namespace Neuron.OSC.Data.Model
{



    public static class OscClaims
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
