// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DetalleOrdenDeServicio.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Detalle de Orden de Servicio.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using Microsoft.Practices.Prism.ViewModel;

    /// <summary>
    /// Detalle de Orden de Servicio.
    /// </summary>
    public class DetalleOrdenDeServicio : NotificationObject
    {
        [Required]
        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Osc_NumeroDocumento_Caption")]
        public string DocUnicoOsc { get; set; }

        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public decimal Cantidad { get; set; }

        public decimal ValorUnitario { get; set; }

        public decimal Total { get; set; }
    }
}
