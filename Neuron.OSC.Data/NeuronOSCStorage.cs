// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NeuronOSCStorage.cs" company="NeuronCloud.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.Data
{
    using System.Data.EntityClient;

    public class NeuronOSCStorage
    {
        public static string EntityConnectionString { get; private set; }

        public static void SetConnectionString(string dbConnectionString, string providerName)
        {
            EntityConnectionStringBuilder connectionStringBuilder = new EntityConnectionStringBuilder
            {
                Metadata = @"res://Neuron.OSC.Data/NeuronOsc.csdl|res://Neuron.OSC.Data/NeuronOsc.ssdl|res://Neuron.OSC.Data/NeuronOsc.msl",
                Provider = providerName,
                ProviderConnectionString = dbConnectionString
            };
            EntityConnectionString = connectionStringBuilder.ConnectionString;
        }
    }
}
