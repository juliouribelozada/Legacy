// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PoblationGroup.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the PoblationGroup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.HIS.Models.Common
{
    using System;

    /// <summary>
    /// Defines the Poblation Group.
    /// </summary>
    [Flags]
    public enum PoblationGroup
    {
        UndefinedGender = 0,
        Male = 2,
        Female = 4,
        Both = Male | Female,
        Adult = 64,
        Geriatric = 128,
        Pediatric = 256
    }
}