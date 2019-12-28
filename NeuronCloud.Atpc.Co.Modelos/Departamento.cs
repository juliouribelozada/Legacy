// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Departamento.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Describe un Departamento.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos
{
    using Microsoft.Practices.Prism.ViewModel;

    /// <summary>
    /// Describe un Departamento.
    /// </summary>
    public class Departamento : NotificationObject
    {
        public string Nombre { get; set; }

        public string Codigo { get; set; }

        public string CodigoPais { get; set; }

        public Pais Pais { get; set; }
    }
}
