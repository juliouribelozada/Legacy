// -----------------------------------------------------------------------
// <copyright file="Commands.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Neuron.Satellite.CapturaManual.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Commands
    {
        private static readonly RoutedCommand pasteCommand = new RoutedCommand();
        private static readonly RoutedCommand printCommand = new RoutedCommand();
        private static readonly RoutedCommand upNavCommand = new RoutedCommand();
        private static readonly RoutedCommand downNavCommand = new RoutedCommand();
        private static readonly RoutedCommand anulaCommand = new RoutedCommand();
        private static readonly RoutedCommand closeMenuCommand = new RoutedCommand();

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
        public static RoutedCommand CloseMenuCommand
        {
            get
            {
                return closeMenuCommand;
            }
        }
    }
}
