using System;
using System.Windows.Input;

namespace LinqToForms
{
   class Command : ICommand
   {
      private readonly Action _execute;
      private readonly Func<bool> _canExecute;

      public Command(Action execute, Func<bool> canExecute = null)
      {
         _execute = execute;
         _canExecute = canExecute ?? (() => true);
      }

      public event EventHandler CanExecuteChanged;

      public bool CanExecute(object parameter)
      {
         return _canExecute();
      }

      public void Execute(object parameter)
      {
         _execute();
      }

      public void TriggerCanExecuteChanged()
      {
         var canExecuteChanged = CanExecuteChanged;
         if (canExecuteChanged != null)
            canExecuteChanged(this, new EventArgs());
      }
   }
}
