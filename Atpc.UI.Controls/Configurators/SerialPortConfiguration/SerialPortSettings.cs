// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerialPortSettings.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the SerialPortSettings type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.UI.Controls.Configurators.SerialPortConfiguration
{
    using System.Configuration;
    using System.IO.Ports;

    using Atpc.Common.Settings;

    [SettingsProvider(typeof(AtpcSettingsProvider))]
    [SettingsGroupName("SerialPortSettings")]
    public sealed class SerialPortSettings : ApplicationSettingsBase
    {
        private static readonly SerialPortSettings defaultInstance = (SerialPortSettings)Synchronized(new SerialPortSettings());

        public static SerialPortSettings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("COM2")]
        public string PortNameSetting
        {
            get
            {
                return (string)this["PortNameSetting"];
            }

            set
            {
                this["PortNameSetting"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("None")]
        public Handshake HandshakeSetting
        {
            get
            {
                return (Handshake)this["HandshakeSetting"];
            }

            set
            {
                this["HandshakeSetting"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("One")]
        public StopBits StopBitsSetting
        {
            get
            {
                return (StopBits)this["StopBitsSetting"];
            }

            set
            {
                this["StopBitsSetting"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("None")]
        public Parity ParitySetting
        {
            get
            {
                return (Parity)this["ParitySetting"];
            }

            set
            {
                this["ParitySetting"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("9600")]
        public int BaudRateSetting
        {
            get
            {
                return (int)this["BaudRateSetting"];
            }
            
            set
            {
                this["BaudRateSetting"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("8")]
        public int DataBitsSetting
        {
            get
            {
                return (int)this["DataBitsSetting"];
            }

            set
            {
                this["DataBitsSetting"] = value;
            }
        }
    }
}
