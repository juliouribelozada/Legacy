// -----------------------------------------------------------------------
// <copyright file="DatosEnColumnas.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DatosEnColumnas : ComplexObject
    {
        private PRO_CargaCapturaCuatroCampos_Result columna0;
        private PRO_CargaCapturaCuatroCampos_Result columna1;
        private PRO_CargaCapturaCuatroCampos_Result columna2;

        public PRO_CargaCapturaCuatroCampos_Result Columna0
        {
            get
            {
                return this.columna0;
            }

            set
            {
                if (value != this.columna0)
                {
                    this.columna0 = value;
                    this.ReportPropertyChanged("Columna0");
                }
            }
        }

        public PRO_CargaCapturaCuatroCampos_Result Columna1
        {
            get
            {
                return this.columna1;
            }

            set
            {
                if (value != this.columna1)
                {
                    this.columna1 = value;
                    this.ReportPropertyChanged("Columna1");
                }
            }
        }

        public PRO_CargaCapturaCuatroCampos_Result Columna2
        {
            get
            {
                return this.columna2;
            }

            set
            {
                if (value != this.columna2)
                {
                    this.columna2 = value;
                    this.ReportPropertyChanged("Columna2");
                }
            }
        }
    }
}
