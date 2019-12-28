// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Provider.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the Provider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common.Settings
{
    using System;
    using System.Configuration;

    using Atpc.Common.Settings.Resources;

    public static class Provider
    {
        private static SettingsProvider defaultProvider = new XMLSettingsProvider();

        public static SettingsProvider Default
        {
            get
            {
                return defaultProvider;
            }
        }

        public static void SetDefaultProvider(Type type)
        {
            if (typeof(SettingsProvider).IsAssignableFrom(type))
            {
                defaultProvider = (SettingsProvider)Activator.CreateInstance(type);
            }
            else
            {
                throw new ArgumentException(LocalizableStrings.Provider_SetDefaultProvider_ExceptionMessage, "type");
            }
        }
    }
}
