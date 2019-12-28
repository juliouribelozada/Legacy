// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestSqlCeSettingsProvider.cs" company="ATPC.co">
//   ATPC © 2012
// </copyright>
// <summary>
//   Defines the SqlCeSettingsProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATPC.Controls.Settings
{
    using System.Configuration;

    public class TestSqlCeSettingsProvider : SettingsProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            throw new NotImplementedException();
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            throw new NotImplementedException();
        }
    }
}
