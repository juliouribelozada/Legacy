// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermisosOsc.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the PermisosOsc type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Auxiliares
{
    using System;

    [Flags]
    public enum PermisosOsc
    {
        Crear = 2,
        Anular = 64
    }
}