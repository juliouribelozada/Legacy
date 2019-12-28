// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtpcSettingsProvider.cs" company="ATPC.co">
//   ATPC © 2012
// </copyright>
// <summary>
//   Defines the SqlCeSettingsProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common.Settings
{
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics;

    /// <summary>
    /// Get the ProviderSettings
    /// To change the Back repository call: Provider.SetDefaultProvider
    /// </summary>
    public sealed class AtpcSettingsProvider : SettingsProvider
    {
        private readonly SettingsProvider settingsProvider = Provider.Default;

        public AtpcSettingsProvider()
        {
        }

        public override string ApplicationName
        {
            get { return "test"; }
            set { }
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            Debug.WriteLine("Llamando GetPropertyValues de \"" + this.settingsProvider.GetType() + "\"");
            return this.settingsProvider.GetPropertyValues(context, collection);
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            Debug.WriteLine("Llamando SetPropertyValues de \"" + this.settingsProvider.GetType() + "\"");
            this.settingsProvider.SetPropertyValues(context, collection);
        }

        public override void Initialize(string name, NameValueCollection col)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "AtpcSettingsProvider";
            }

            base.Initialize(name, col);
        }
    }
}
