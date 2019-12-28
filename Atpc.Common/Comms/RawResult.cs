// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RawResult.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common.Comms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RawResult
    {
        public string FullFrame { get; set; }

        public IEnumerable<string> RecordList { get; set; }
    }
}
