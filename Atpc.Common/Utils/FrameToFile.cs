// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameToFile.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common.Utils
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    public static class FrameToFile
    {
        private static readonly object lockWriteFile = new object();
        private static readonly string defaultDirectory;

        static FrameToFile()
        {
            FileInfo fileInfo = new FileInfo(Assembly.GetEntryAssembly().Location);
            defaultDirectory = fileInfo.DirectoryName;
            if (string.IsNullOrWhiteSpace(defaultDirectory))
            {
                defaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory, Environment.SpecialFolderOption.None);
            }
        }

        public static void WriteFrame(string frame, string prefix)
        {
            ThreadPool.QueueUserWorkItem(s => WriteFrameAction(frame, prefix));
        }

        private static void WriteFrameAction(string frame, string prefix)
        {
            string logFilePath = defaultDirectory + Path.DirectorySeparatorChar + prefix;
            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }

            lock (lockWriteFile)
            {
                BinaryWriter tw = new BinaryWriter(File.Open(logFilePath + Path.DirectorySeparatorChar + prefix + "-" + DateTime.Today.ToString("yyyyMMdd") + ".txt", FileMode.Append));

                byte[] array = frame.ToCharArray().Select(Convert.ToByte).ToArray();
                tw.Write(array);
                tw.Close();
            }
        }
    }
}
