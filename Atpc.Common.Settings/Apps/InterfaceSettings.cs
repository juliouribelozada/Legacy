// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InterfaceSettings.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common.Settings.Apps
{
    using System.Configuration;

    using ATPC.Common.Settings;

    [SettingsProvider(typeof(AtpcSettingsProvider))]
    [SettingsGroupName("InterfaceSettings")]
    public sealed class InterfaceSettings : ApplicationSettingsBase
    {
        private static readonly InterfaceSettings defaultInstance = (InterfaceSettings)Synchronized(new InterfaceSettings());

        public static InterfaceSettings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool LogFrames
        {
            get
            {
                return (bool)this["LogFrames"];
            }

            set
            {
                this["LogFrames"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool UseHwAcceleration
        {
            get
            {
                return (bool)this["UseHwAcceleration"];
            }

            set
            {
                this["UseHwAcceleration"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool DebugMode
        {
            get
            {
                return (bool)this["DebugMode"];
            }

            set
            {
                this["DebugMode"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("127.0.0.1")]
        public string DatabaseAddress
        {
            get
            {
                return (string)this["DatabaseAddress"];
            }

            set
            {
                this["DatabaseAddress"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("Neuron")]
        public string DatabaseName
        {
            get
            {
                return (string)this["DatabaseName"];
            }

            set
            {
                this["DatabaseName"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("****")]
        public string DatabasePass
        {
            get
            {
                return (string)this["DatabasePass"];
            }

            set
            {
                this["DatabasePass"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("sa")]
        public string DatabaseUser
        {
            get
            {
                return (string)this["DatabaseUser"];
            }

            set
            {
                this["DatabaseUser"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("HI")]
        public string Prefijo1
        {
            get
            {
                return (string)this["Prefijo1"];
            }

            set
            {
                this["Prefijo1"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("PDO")]
        public string TestIdUnico
        {
            get
            {
                return (string)this["TestIdUnico"];
            }

            set
            {
                this["TestIdUnico"] = value;
            }
        }
    }
}
