// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NeuronCloudPrincipal.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Principal De NeuronCloud.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Providers.Desktop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Web.Security;

    using NeuronCloud.Atpc.Co.Modelos.Auxiliares;

    /// <summary>
    /// Principal De NeuronCloud.
    /// </summary>
    public class NeuronCloudPrincipal : IPrincipal
    {
        private string roleProviderName;

        public NeuronCloudPrincipal(IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }

            this.Identity = identity;
            this.Inicializar();
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return false;
        }

        public bool PermisosOsc(PermisosOsc permisosOsc)
        {
            return true;
        }

        public bool PermisosOsc(Func<bool> func)
        {
            if (func != null)
            {
                var f = func();
                return f;
            }
            else return false;

        }

        private void Inicializar()
        {
            if (string.IsNullOrWhiteSpace(this.roleProviderName))
            {
                this.roleProviderName = Roles.Provider.Name;
            }
        }
    }
}
