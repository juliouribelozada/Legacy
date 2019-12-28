// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NeuronCloudIdentity.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   NeuronCloud: Identity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Providers.Desktop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;

    using NeuronCloud.Atpc.Co.Modelos.Auxiliares;

    /// <summary>
    /// NeuronCloud: Identity.
    /// </summary>
    public class NeuronCloudIdentity : IIdentity
    {
        public NeuronCloudIdentity()
        {
        }

        public NeuronCloudIdentity(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public string AuthenticationType { get; private set; }

        public bool IsAuthenticated { get; private set; }
    }
}
