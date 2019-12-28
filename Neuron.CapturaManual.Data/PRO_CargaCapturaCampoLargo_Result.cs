namespace Neuron.Satelite.CapturaManual.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using ATPC.Controls;

    using Neuron.Satelite.CapturaManual.Data.AsyncOps;
    using Neuron.Satelite.CapturaManual.Data.Model;

    public partial class PRO_CargaCapturaCampoLargo_Result
    {
        private readonly ObservableCollection<SugerenciasAutoTexto> suggestionsDatoLargo = new ObservableCollection<SugerenciasAutoTexto>();
        private AtpcBindableCommand verHistoricoCommand;

        public PRO_CargaCapturaCampoLargo_Result()
        {
            StoreProceduresOps.GetDatoLargoSugestions(
                this,
                (s, e) =>
                {
                    if (e.Cancelled)
                    {
                    }
                    else if (e.Error != null)
                    {
                    }
                    else
                    {
                        var sugerenciasAutoTextos = e.Result as List<SugerenciasAutoTexto>;

                        if (sugerenciasAutoTextos != null)
                        {
                            foreach (var sugerenciasAutoTexto in sugerenciasAutoTextos)
                            {
                                this.SuggestionsDatoLargo.Add(sugerenciasAutoTexto);
                            }
                        }
                    }
                });

        }

        public event EventHandler DatoLargoChanged;

        public event EventHandler VerHistorico;

        public AtpcBindableCommand VerHistoricoCommand
        {
            get
            {
                return this.verHistoricoCommand ?? (this.verHistoricoCommand = new AtpcBindableCommand(this.OnVerHistorico, obj => true));
            }
        }

        public ObservableCollection<SugerenciasAutoTexto> SuggestionsDatoLargo
        {
            get
            {
                return this.suggestionsDatoLargo;
            }
        }

        public void OnVerHistorico(object o)
        {
            EventHandler handler = this.VerHistorico;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        partial void OnDatoLargoChanged()
        {
            EventHandler handler = this.DatoLargoChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}
