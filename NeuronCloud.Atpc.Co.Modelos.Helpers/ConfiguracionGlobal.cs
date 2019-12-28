// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionGlobal.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the ConfiguracionGlobal type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;

    public static class ConfiguracionGlobal
    {
        private static IPrincipal principalActual;

        /// <summary>
        /// Gets the usuario actual.
        /// </summary>
        [Obsolete("Usar IPrincipal")]
        public static string UsuarioActual { get; private set; }

        public static IPrincipal IPrincipalActual
        {
            get
            {
                return principalActual ?? new GenericPrincipal(new GenericIdentity("Neuron", "Ninguna"), new[] { "Ninguno" });
            }

            private set
            {
                principalActual = value;
            }
        }

        public static void SetUsuarioActual(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                return;
            }

            UsuarioActual = nombreUsuario;
        }

        public static void SetIPrincipalActual(IPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal", "El IPrincipal no debe ser Nulo");
            }

            if (principal.Identity == null)
            {
                throw new ArgumentNullException("principal", "El Identity del IPrincipal no debe ser Nulo");
            }

            IPrincipalActual = principal;
        }
    }
}
