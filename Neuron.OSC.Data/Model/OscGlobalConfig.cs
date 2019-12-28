using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuron.OSC.Data.Model
{
    public static class OscGlobalConfig
    {
        public static string NomSede { get; private set; }

        public static void SetNomSede(string nomSede)
        {
            NomSede = nomSede;
        }
    }
}
