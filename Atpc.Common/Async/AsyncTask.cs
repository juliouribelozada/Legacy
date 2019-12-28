// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncTask.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common.Async
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    public class AsyncTask : BackgroundWorker
    {
        private RunWorkerCompletedEventArgs resultadoConsulta;

        public AsyncTask(object parametro, DoWorkEventHandler accion)
        {
            this.RunWorkerCompleted += this.AsyncTaskRunWorkerCompleted;
            this.ProgressChanged += this.AsyncTaskProgressChanged;
            if (accion != null)
            {
                this.DoWork += accion;
            }
            else
            {
                this.DoWork += this.AsyncTaskDoWork;
            }

            this.Disposed += this.AsyncTaskDisposed;
            this.RunWorkerAsync(parametro);
        }

        public AsyncTask(object parametro, DoWorkEventHandler accion, AsyncTaskCompletedEventHandler resultadoOperacion)
            : this(parametro, accion)
        {
            this.ResultadoOperacion += resultadoOperacion;
        }

        public event AsyncTaskCompletedEventHandler ResultadoOperacion;

        public void InvokeResultadoOperacion(RunWorkerCompletedEventArgs e)
        {
            AsyncTaskCompletedEventHandler handler = this.ResultadoOperacion;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void AsyncTaskDoWork(object sender, DoWorkEventArgs e)
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

        protected virtual void AsyncTaskDisposed(object sender, EventArgs e)
        {
            // Debug.WriteLine(string.Format("Tarea terminanda AsyncTask()"));
        }

        private void AsyncTaskProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void AsyncTaskRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
