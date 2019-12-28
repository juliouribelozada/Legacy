// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataCaptureType.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   Defines the DataCaptureType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.Model
{
    using System;

    [Flags]
    public enum DataCaptureType
    {
        /// <summary>
        /// This Data Field is undefined.
        /// </summary>
        Undeffined = 0,

        /// <summary>
        /// This Data Field is optional.
        /// </summary>
        Optional = 1,

        /// <summary>
        /// This Data Field is the result of a formula.
        /// </summary>
        Formula = 2,

        /// <summary>
        /// This Data fiels is Standard Data.
        /// </summary>
        Data = 4,

        /// <summary>
        /// This Data Field is long Text.
        /// </summary>
        LongData = 8,

        /// <summary>
        /// The Field is a subtitle.
        /// </summary>
        Subtitle = 16,

        /// <summary>
        /// This Data Field is Null.
        /// </summary>
        Null = 256
    }
}