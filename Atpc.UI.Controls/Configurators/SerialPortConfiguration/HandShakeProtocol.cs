// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandShakeProtocol.cs" company="ATPC.co">
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

    public struct HandShakeProtocol
    {
        public HandShakeProtocol(Handshake value, string name)
            : this()
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; private set; }

        public Handshake Value { get; private set; }
    }
}
