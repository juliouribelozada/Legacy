
namespace Atpc.Common.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Log to System Event Log
    /// </summary>
    public class EventLogWriter : ILogWriter
    {
        public EventLogWriter(string eventSource)
        {
            if (EventLog.SourceExists(eventSource))
            {
                ///EventLog.CreateEventSource();
                var c = new EventLog("ATPC");
               // c.Source
            }
        }

        public void WriteLogLine(string textToLog)
        {

        }

        public void WriteLogLine(object objectToLog)
        {

        }

        public void WriteLogLine(Exception exceptionToLog, bool includeInnerExceptions)
        {

        }
    }
}
