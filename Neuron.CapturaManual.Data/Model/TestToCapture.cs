// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestToCapture.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   Defines the TestToCapture type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Text;

    public class TestToCapture<T> : PRO_CargaCapturaSeccion_Result where T : ComplexObject
    {
    }
}
