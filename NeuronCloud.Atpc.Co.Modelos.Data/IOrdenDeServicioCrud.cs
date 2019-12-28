
namespace NeuronCloud.Atpc.Co.Modelos.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICrud : INotifyPropertyChanged
    {
        ICommand Crear { get; }

        ICommand Actualizar { get; }

        ICommand Borrar { get; }
    }
}
