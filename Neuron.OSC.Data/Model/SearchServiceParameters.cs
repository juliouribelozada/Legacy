// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchServiceParameters.cs" company="NeuronCloud.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.Data.Model
{
    using Neuron.HIS.Models.Common;

    public class SearchServiceParameters
    {
        public ServiceAgreement ServiceAgreement { get; set; }

        public ServiceRequester ServiceRequester { get; set; }

        public ServiceProvider Provider { get; set; }

        public string Code { get; set; }

        public string SearchChars { get; set; }

        public Gender Gender { get; set; }

        public string GenderAsString
        {
            get
            {
                switch (Gender)
                {
                    case Gender.NotGiven:
                        return "AMBOS";

                    case Gender.Male:
                        return "MASCULINO";

                    case Gender.Female:
                        return "FEMENINO";

                    default:
                        return "AMBOS";
                }
            }
        }
    }
}
