// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CRUDCommands.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.UI.Controls.Models
{
    using System.Windows.Input;

    public class CRUDCommands
    {
        private static readonly RoutedCommand addCommand = new RoutedCommand();
        private static readonly RoutedCommand removeCommand = new RoutedCommand();
        private static readonly RoutedCommand editCommand = new RoutedCommand();
        private static readonly RoutedCommand newCommand = new RoutedCommand();

        public static RoutedCommand AddCommand
        {
            get
            {
                return addCommand;
            }
        }

        public static RoutedCommand RemoveCommand
        {
            get
            {
                return removeCommand;
            }
        }

        public static RoutedCommand EditCommand
        {
            get
            {
                return editCommand;
            }
        }

        public static RoutedCommand NewCommand
        {
            get
            {
                return newCommand;
            }
        }
    }
}
