// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SaveOSCParameters.cs" company="NeuronCloud.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Neuron.HIS.Models.Common;
    using Neuron.UI.Controls.Models;

    using NeuronCloud.Atpc.Co.Modelos;
    using NeuronCloud.Atpc.Co.Modelos.Auxiliares;

    [XmlInclude(typeof(CRUD_TerceroSeleccionaExacto_Result))]
    public class SaveOSCParameters
    {
        public List<ItemPago> Pagos { get; set; }

        public List<CRUDEntity<Service>> SelectedServices { get; set; }

        public PersonalAsistencial PersonalAsistencial { get; set; }

        public Diagnose CurrentDiagnosis { get; set; }

        public ServiceAgreement CurrentServiceAgreement { get; set; }

        public ServiceProvider CurrentProvider { get; set; }

        public ServiceRequester CurrentServiceRequester { get; set; }

        public PatientInfo CurrentPatient { get; set; }

        public string OSCPrefix { get; set; }

        public string VrDcto { get; set; }

        public string IdSistema { get; set; }

        public string NumeroAutorizacion { get; set; }

        public string Observaciones { get; set; }

        public decimal Total { get; set; }

        public string NoOrdenMedica { get; set; }

        public AsigNoMuestra AsigNoMuestra { get; set; }

        public NumeroDeMuestra NumeroDeMuestraAsignado { get; set; }

        public DateTime? FUR { get; set; }

        public string Nivel { get; set; }

        public bool EnviarResultadoPorCorreo { get; set; }

        public string CorreoElectronico { get; set; }

        public bool ProgramarCita { get; set; }

        public DateTime? FechaHoraCita { get; set; }

        public string IdProgramaAgenda { get; set; }

        public ServiceUnit CurrentServiceUnit { get; set; }

        public string Cama { get; set; }

        public string ServicioUbicacion { get; set; }
        public string Sede { get; set; }

        public string BookingNumber { get; set; }
    }
}
