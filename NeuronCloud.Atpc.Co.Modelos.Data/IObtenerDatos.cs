// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IObtenerDatos.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the IObtenerDatos type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Data
{
    using System.Collections.Generic;

    public interface IObtenerDatos
    {
        IEnumerable<T> TraerTodos<T>();
    }
}