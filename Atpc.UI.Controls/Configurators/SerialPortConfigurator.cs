// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerialPortConfigurator.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the SerialPortConfigurator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.UI.Controls.Configurators
{
    using System.IO.Ports;
    using System.Windows;

    using Atpc.UI.Controls.Configurators.SerialPortConfiguration;

    public class SerialPortConfigurator
    {
        private static readonly SerialPortSettings settings = SerialPortSettings.Default;

        internal static SerialPortSettings Settings
        {
            get
            {
                return settings;
            }
        }

        public static SerialPort GetPortFromConfiguration()
        {
            return new SerialPort
            {
                PortName = Settings.PortNameSetting,
                BaudRate = Settings.BaudRateSetting,
                DataBits = Settings.DataBitsSetting,
                Parity = Settings.ParitySetting,
                StopBits = Settings.StopBitsSetting,
                Handshake = Settings.HandshakeSetting
            };
        }

        public static void ConfigurePort(SerialPort puerto, Window mainWindow)
        {
            var test = new SerialPortConfiguratorWindow(puerto) { Owner = mainWindow };
            test.ShowDialog();
        }
    }
}
