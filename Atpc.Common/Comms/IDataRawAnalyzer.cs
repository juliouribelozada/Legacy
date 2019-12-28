// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataRawAnalyzer.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the IDataRawAnalyzer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common.Comms
{
    using Atpc.Common.Legacy;

    public delegate void FrameReadyEvent(object sender, RawResult result);

    public delegate void SendMessageEvent(object sender, string message);

    public delegate void DebugMessageEvent(object sender, string message);

    public interface IDataRawAnalyzer
    {
        event FrameReadyEvent FrameReady;

        event SendMessageEvent SendMessage;

        event DebugMessageEvent DebugMessage;

        string Frame { get; set; }

        string DuttyLoad { get; }

        string Buffer { get; }

        ILogListener Log { get; set; }

        string BufferRaw { get; }

        bool Debug { get; set; }

        bool AnalyzeData(int[] inputBuffer);
    }
}