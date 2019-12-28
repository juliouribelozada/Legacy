// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Municipio.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Describe un Municipio.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos
{
    using Microsoft.Practices.Prism.ViewModel;

    /// <summary>
    /// Describe un Municipio.
    /// </summary>
    public class Municipio : NotificationObject
    {
        public string Nombre { get; set; }

        public string Codigo { get; set; }

        public Pais Pais { get; set; }

        public Departamento Departamento { get; set; }

        public string CodigoPais { get; set; }

        public string CodigoDepartamento { get; set; }
    }
}
