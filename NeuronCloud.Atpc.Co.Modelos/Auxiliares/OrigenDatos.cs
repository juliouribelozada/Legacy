// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrigenDatos.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the OrigenDatos type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Auxiliares
{
    public enum OrigenDatos
    {
        /// <summary>Indica que los Datos deben ser traidos desde una base de Datos</summary>
        DesdeBD,

        /// <summary>Indica que los Datos deben ser traidos desde una cadena de texto con valores separados por comas</summary>
        DesdeCSV,

        /// <summary>Indica que los Datos deben ser traidos desde IList</summary>
        DesdeLista
    }
}