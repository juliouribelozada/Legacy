namespace Neuron.OSC.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    public class AccionAsincronaGenerica : BackgroundWorker
    {
        private RunWorkerCompletedEventArgs resultadoConsulta;

        public AccionAsincronaGenerica(object parametro, DoWorkEventHandler accion)
        {
            this.RunWorkerCompleted += this.AccionAsincronaGenericaRunWorkerCompleted;
            this.ProgressChanged += this.AccionAsincronaGenericaProgressChanged;
            if (accion != null)
            {
                this.DoWork += accion;
            }
            else
            {
                this.DoWork += this.AccionAsincronaGenericaDoWork;
            }

            this.Disposed += this.AccionAsincronaGenericaDisposed;
            this.RunWorkerAsync(parametro);
        }

        public AccionAsincronaGenerica(object parametro, DoWorkEventHandler accion, OperacionAsincronaEventHandler resultadoOperacion)
            : this(parametro, accion)
        {
            this.ResultadoOperacion += resultadoOperacion;
        }

        public event OperacionAsincronaEventHandler ResultadoOperacion;

        public void InvokeResultadoOperacion(RunWorkerCompletedEventArgs e)
        {
            OperacionAsincronaEventHandler handler = this.ResultadoOperacion;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void AccionAsincronaGenericaDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                throw new Exception("Debe sobrecarcar el methodo DoWork ");
            }
            catch (Exception exc)
            {
                this.resultadoConsulta = new RunWorkerCompletedEventArgs(null, exc, false);
            }
        }

        protected virtual void AccionAsincronaGenericaDisposed(object sender, EventArgs e)
        {
            // Debug.WriteLine(string.Format("Tarea terminanda AccionAsincronaGenerica()"));
        }

        private void AccionAsincronaGenericaProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void AccionAsincronaGenericaRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.resultadoConsulta != null)
            {
                this.InvokeResultadoOperacion(this.resultadoConsulta);
            }
            else
            {
                if (e.Cancelled)
                {
                    Trace.WriteLine("Cancelado");
                    this.resultadoConsulta = new RunWorkerCompletedEventArgs(null, null, true);
                }
                else if (e.Error != null)
                {
                    this.resultadoConsulta = new RunWorkerCompletedEventArgs(null, e.Error, false);
                }
                else
                {
                    this.resultadoConsulta = new RunWorkerCompletedEventArgs(e.Result, null, false);
                }

                this.InvokeResultadoOperacion(this.resultadoConsulta);
            }

            this.Dispose();
        }
    }
}
