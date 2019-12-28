// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NeuronCloudBaseStorage.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the NeuronCloudBaseStorage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.Entity
{
    using System;
    using System.Data.EntityClient;

    public class NeuronCloudBaseStorage
    {
        private static string entityConnectionString;

        public static string EntityConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(entityConnectionString))
                {
                    throw new InvalidOperationException("Debe Llamar NeuronCloudBaseStorage.SetConnectionString() Primero.");
                }

                return entityConnectionString;
            }

            private set
            {
                entityConnectionString = value;
            }
        }

        public static void SetConnectionString(string dbConnectionString, string providerName)
        {
            EntityConnectionStringBuilder connectionStringBuilder = new EntityConnectionStringBuilder
            {
                Metadata = @"res://NeuronCloud.Atpc.Co.Modelos.Data/SqlServer.Entity.NeuronCloudBaseModel.csdl|res://NeuronCloud.Atpc.Co.Modelos.Data/SqlServer.Entity.NeuronCloudBaseModel.ssdl|res://NeuronCloud.Atpc.Co.Modelos.Data/SqlServer.Entity.NeuronCloudBaseModel.msl",
                Provider = providerName,
                ProviderConnectionString = dbConnectionString
            };
            EntityConnectionString = connectionStringBuilder.ConnectionString;
        }
    }
}
