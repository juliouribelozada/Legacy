// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAnalyteResult.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IAnalyteResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.HIS.Models.Common
{
    using System;

    public interface IAnalyteResult
    {
        Analyte Analyte { get; set; }

        Type Type { get; }

        void SetValue(object newValue);

        void SetAlternateValue(object newValue);

        void SetStatus(string newValue);
    }
}