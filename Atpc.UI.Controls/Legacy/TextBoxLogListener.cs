// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextBoxLogListener.cs" company="ATPC.co">
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.UI.Controls.Legacy
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Controls;

    using Atpc.Common.Comms;
    using Atpc.Common.Legacy;

    public class TextBoxLogListener : ILogListener
    {
        private readonly TextBox logDestination;

        private readonly bool convertControlChars;

        public TextBoxLogListener(TextBox logDestination)
        {
            this.logDestination = logDestination;
            this.convertControlChars = false;
        }

        public TextBoxLogListener(TextBox logDestination, bool convertControlChars)
        {
            this.logDestination = logDestination;
            this.convertControlChars = convertControlChars;
        }

        public void WriteLogLine(string eventText)
        {
            if (this.convertControlChars)
            {
                eventText = ControlChars.ControlCharsReadable(eventText);
            }

            this.logDestination.Dispatcher.BeginInvoke((Action)(() =>
            {
                this.logDestination.AppendText(eventText + "\n");
                this.logDestination.ScrollToEnd();
            }));
        }

        public void WriteLogLine2(string eventText)
        {
            this.logDestination.Dispatcher.BeginInvoke((Action)(() =>
            {
                this.logDestination.AppendText(eventText + "\n");
                this.logDestination.ScrollToEnd();
            }));
        }

        public void WriteLogLine(Exception e)
        {
            this.WriteLogLine2(string.Format("Error: {0}\n{1}", e.Message, e.InnerException != null ? string.Format("\t Inner: {0}\n", e.InnerException.Message) : string.Empty));
        }

        public bool VerifyRunWorkerCompletedEventArgsErrors(object sender, RunWorkerCompletedEventArgs e)
        {
            bool salida;

            if (e.Cancelled)
            {
                this.WriteLogLine("Accion Cancelada");
                salida = true;
            }
            else if (e.Error != null)
            {
                this.WriteLogLine(e.Error);
                salida = true;
            }
            else
            {
                salida = false;
            }

            return salida;
        }

        public void WriteLog(string eventText)
        {
            this.logDestination.Dispatcher.BeginInvoke((Action)(() =>
            {
                this.logDestination.AppendText(eventText);
                this.logDestination.ScrollToEnd();
            }));
        }
    }
}
