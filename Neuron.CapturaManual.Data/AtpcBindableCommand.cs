namespace ATPC.Controls
{
    using System;
    using System.Windows.Input;

    public class AtpcBindableCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public AtpcBindableCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public AtpcBindableCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this._execute = execute;
            this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

        public void RaiseCanExecuteChanged()
        {
             this.CanExecuteChanged(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return this._canExecute == null || this._canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this._execute(parameter);
        }
    }
}
