// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseResultActions.cs" company="">
//   TODO: Update copyright text.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.UI.Controls.Legacy.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;
    using System.Windows.Input;

    public static class BaseResultActions
    {
        private static readonly RoutedCommand deleteRecordCommand = new RoutedCommand();
        private static readonly RoutedCommand selectAllCommand = new RoutedCommand();
        private static readonly RoutedCommand clearSelectionCommand = new RoutedCommand();
        private static readonly RoutedCommand getPacientPersonalInfoCommand = new RoutedCommand();
        private static readonly RoutedCommand getNotValidatedResultsCommand = new RoutedCommand();
        private static readonly RoutedCommand updateObservationsCommand = new RoutedCommand();
        private static readonly RoutedCommand showAdditionalPropertiesCommand = new RoutedCommand();
        private static readonly RoutedCommand updateAdditionalPropertiesCommand = new RoutedCommand();

        public static RoutedCommand UpdateAdditionalPropertiesCommand
        {
            get
            {
                return updateAdditionalPropertiesCommand;
            }
        }

        public static RoutedCommand ShowAdditionalPropertiesCommand
        {
            get
            {
                return showAdditionalPropertiesCommand;
            }
        }

        public static RoutedCommand GetNotValidatedResultsCommand
        {
            get
            {
                return getNotValidatedResultsCommand;
            }
        }

        public static RoutedCommand GetPacientPersonalInfoCommand
        {
            get
            {
                return getPacientPersonalInfoCommand;
            }
        }

        public static RoutedCommand ClearSelectionCommand
        {
            get
            {
                return clearSelectionCommand;
            }
        }

        public static RoutedCommand DeleteRecordCommand
        {
            get
            {
                return deleteRecordCommand;
            }
        }

        public static RoutedCommand UpdateObservationsCommand
        {
            get
            {
                return updateObservationsCommand;
            }
        }

        public static RoutedCommand SelectAllCommand
        {
            get
            {
                return selectAllCommand;
            }
        }

    ////    public static void CanExecuteDeleteRecordCommand(object sender, CanExecuteRoutedEventArgs e)
    ////    {
    ////        if (e.Source is DataGrid)
    ////        {
    ////            DataGrid datalList = e.Source as DataGrid;

    ////            BaseResult currentResult = datalList.SelectedItem as BaseResult;
    ////            if (currentResult != null)
    ////            {
    ////                e.CanExecute = currentResult.IsSaved;
    ////            }
    ////        }
    ////        else if (e.Source is ListView)
    ////        {
    ////            ListView datalList = e.Source as ListView;

    ////            BaseResult currentResult = datalList.SelectedItem as BaseResult;
    ////            if (currentResult != null)
    ////            {
    ////                e.CanExecute = currentResult.IsSaved;
    ////            }
    ////        }
    ////        else
    ////        {
    ////            e.CanExecute = false;
    ////        }
    ////    }

    ////    public static void CanExecuteSelectionCommand(object sender, CanExecuteRoutedEventArgs e)
    ////    {
    ////        var baseResults = e.Parameter as IEnumerable<BaseResult>;

    ////        if (baseResults != null)
    ////        {
    ////            int count = baseResults.Count();
    ////            e.CanExecute = count > 0;
    ////        }
    ////    }

    ////    public static void SelectAllAction(object sender, ExecutedRoutedEventArgs e)
    ////    {
    ////        var baseResults = e.Parameter as IEnumerable<IBaseResult>;

    ////        if (baseResults != null)
    ////        {
    ////            foreach (IBaseResult resultado in baseResults.Where(resultado => resultado.IsSaved && resultado.PatientPersonalInfo != null))
    ////            {
    ////                resultado.Validate = true;
    ////            }
    ////        }
    ////    }

    ////    public static void ClearSelectionAction(object sender, ExecutedRoutedEventArgs e)
    ////    {
    ////        var baseResults = e.Parameter as IEnumerable<BaseResult>;

    ////        if (baseResults != null)
    ////        {
    ////            foreach (BaseResult resultado in baseResults)
    ////            {
    ////                resultado.Validate = false;
    ////            }
    ////        }
    ////    }

    ////    public static void CanExecuteGetPacientPersonalInfoCommand(object sender, CanExecuteRoutedEventArgs e)
    ////    {
    ////        var control = e.Source as Button;
    ////        if (control != null)
    ////        {
    ////            var dato = control.DataContext as IBaseResult;
    ////            e.CanExecute = dato != null;
    ////        }
    ////        else
    ////        {
    ////            e.CanExecute = false;
    ////        }
    ////    }
    ////
    }
}
