// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncTaskCompletedEventHandler.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the AsyncTaskCompletedEventHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common.Async
{
    using System.ComponentModel;

    public delegate void AsyncTaskCompletedEventHandler(object sender, RunWorkerCompletedEventArgs e);
}