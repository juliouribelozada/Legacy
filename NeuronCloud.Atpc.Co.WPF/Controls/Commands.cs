// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Commands.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the Commands type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF.Controls
{
    using System.Windows.Input;

    public class Commands
    {
        private static readonly RoutedCommand pasteCommand = new RoutedCommand();
        private static readonly RoutedCommand printCommand = new RoutedCommand();
        private static readonly RoutedCommand upNavCommand = new RoutedCommand();
        private static readonly RoutedCommand downNavCommand = new RoutedCommand();
        private static readonly RoutedCommand anulaCommand = new RoutedCommand();

        public static RoutedCommand PasteCommand
        {
            get
            {
                return pasteCommand;
            }
        }

        public static RoutedCommand AnulaCommand
        {
            get
            {
                return anulaCommand;
            }
        }

        public static RoutedCommand PrintCommand
        {
            get
            {
                return printCommand;
            }
        }

        public static RoutedCommand UpNavCommand
        {
            get
            {
                return upNavCommand;
            }
        }

        public static RoutedCommand DownNavCommand
        {
            get
            {
                return downNavCommand;
            }
        }
    }
}
