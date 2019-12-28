// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogWriter.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the ILogWriter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common.Logging
{
    using System;

    public interface ILogWriter
    {
        void WriteLogLine(string textToLog);

        void WriteLogLine(object objectToLog);

        void WriteLogLine(Exception exceptionToLog, bool includeInnerExceptions);
    }
}