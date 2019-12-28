// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogListener.cs" company="ATPC.co">
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common.Legacy
{
    using System;
    using System.ComponentModel;

    public interface ILogListener
    {
        void WriteLogLine(string eventText);

        void WriteLogLine(Exception e);

        bool VerifyRunWorkerCompletedEventArgsErrors(object sender, RunWorkerCompletedEventArgs e);

        void WriteLog(string eventText);
    }
}
