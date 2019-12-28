// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressCircle.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the ProgressCircle type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    public class ProgressCircle : ContentControl
    {
        static ProgressCircle()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressCircle), new FrameworkPropertyMetadata(typeof(ProgressCircle)));
        }
    }
}
