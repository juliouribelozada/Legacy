// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataBits.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the StopBit type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.UI.Controls.Configurators.SerialPortConfiguration
{
    public struct DataBits
    {
        public DataBits(int value)
            : this()
        {
            this.Value = value;
        }

        public int Value { get; private set; }
    }
}
