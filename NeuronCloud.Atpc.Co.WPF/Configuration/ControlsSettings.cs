// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlsSettings.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the SerialPortSettings type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF.Configuration
{
    using System.Configuration;

    using global::Atpc.Common.Settings;

    [SettingsProvider(typeof(AtpcSettingsProvider))]
    [SettingsGroupName("ControlsSettings")]
    public sealed class ControlsSettings : ApplicationSettingsBase
    {
        private static readonly ControlsSettings DefaultInstance = (ControlsSettings)Synchronized(new ControlsSettings());

        public static ControlsSettings Default
        {
            get
            {
                return DefaultInstance;
            }
        }

        [ApplicationScopedSetting]
        [DefaultSettingValue("NumeroDeResultados")]
        public int NumeroDeResultados
        {
            get
            {
                return (int)this["NumeroDeResultados"];
            }

            set
            {
                this["NumeroDeResultados"] = value;
            }
        }
    }
}
