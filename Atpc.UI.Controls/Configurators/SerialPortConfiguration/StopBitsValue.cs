// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StopBitsValue.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the StopBitsValue type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.UI.Controls.Configurators.SerialPortConfiguration
{
    using System.IO.Ports;

    public struct StopBitsValue
    {
        public StopBitsValue(StopBits value, string name)
            : this()
        {
            this.Value = value;
            this.Name = name;
        }

        public string Name { get; private set; }

        public StopBits Value { get; private set; }
    }
}
