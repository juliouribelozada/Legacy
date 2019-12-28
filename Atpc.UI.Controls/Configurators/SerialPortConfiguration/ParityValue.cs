// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParityValue.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the StopBit type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.UI.Controls.Configurators.SerialPortConfiguration
{
    using System.IO.Ports;

    public struct ParityValue
    {
        public ParityValue(Parity parity, string name)
            : this()
        {
            this.Name = name;
            this.Value = parity;
        }

        public string Name { get; private set; }

        public Parity Value { get; private set; }
    }
}
