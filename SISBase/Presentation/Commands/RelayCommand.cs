using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SISBase.Presentation.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;

        public RelayCommand(Action<object?> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
