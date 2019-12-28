// -----------------------------------------------------------------------
// <copyright file="CheckSum.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Atpc.Common.Comms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CheckSum
    {
        public static string CalculateCheckSum(string data)
        {
            string checksum = "00";

            int sumOfChars = data.Sum(t => Convert.ToInt32(t));

            if (sumOfChars > 0)
            {
                checksum = Convert.ToString(sumOfChars % 256, 16).ToUpper();
            }

            return checksum.Length == 1 ? "0" + checksum : checksum;
        }
    }
}
