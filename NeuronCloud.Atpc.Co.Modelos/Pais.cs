// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pais.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
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
    /// Describe una Entidad Tipo Pais.
    /// </summary>
    public class Pais : NotificationObject
    {
        [Key]
        [Required]
        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Pais_Codigo_Caption")]
        public string Codigo { get; set; }

        public string Nombre { get; set; }
    }
}
