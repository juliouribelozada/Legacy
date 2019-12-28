// -----------------------------------------------------------------------
// <copyright file="ObtenerDatos.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ObtenerDatos : IObtenerDatos
    {
        public IEnumerable<T> TraerTodos<T>()
        {
            return new List<T>();
        }
    }
}
