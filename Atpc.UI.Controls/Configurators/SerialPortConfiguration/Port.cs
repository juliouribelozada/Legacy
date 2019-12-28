// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Port.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the StopBit type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.UI.Controls.Configurators.SerialPortConfiguration
{
    public struct Port
    {
        public Port(string value)
            : this()
        {
            this.Name = value;
        }

        public string Name { get; private set; }
    }
}
