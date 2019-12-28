using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuron.CapturaManual.Data
{
    using System.Data.EntityClient;

    public class NeuronStorage
    {
        public static string EntityConnectionString { get; private set; }

        public static void SetConnectionString(string dbConnectionString, string providerName)
        {
            EntityConnectionStringBuilder connectionStringBuilder = new EntityConnectionStringBuilder
            {
                Metadata = @"res://Neuron.CapturaManual.Data/NeuronCapturaManualModel.csdl|res://Neuron.CapturaManual.Data/NeuronCapturaManualModel.ssdl|res://Neuron.CapturaManual.Data/NeuronCapturaManualModel.msl",
                Provider = providerName,
                ProviderConnectionString = dbConnectionString
            };
            EntityConnectionString = connectionStringBuilder.ConnectionString;
        }
    }
}
