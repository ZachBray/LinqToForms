using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace LinqToForms
{
   public class Form<T> : ObservableObject
   {
      private readonly string _submitAction;
      private readonly Action<T> _onSubmit;
      private readonly Action _onCancel;
      private ICommand _submit;
      private ICommand _cancel;
      private IFormlet<T> _formlet;
      private ValueChanged<T> _currentValue;
      private IDictionary<string, ObservableObject> _fields;

      public Form(string submitAction, Action<T> onSubmit, Action onCancel)
      {
         _submitAction = submitAction;
         _onSubmit = onSubmit;
         _onCancel = onCancel;
      }

      public IFormlet<T> Definition
      {
         get
         {
            return _formlet;
         }
         set
         {
            _formlet = value;

            var submit = new Command(
               () => _onSubmit(_currentValue.Value),
               () => _currentValue != null && _currentValue.HasValue);
            Submit = submit;

            // This class owns all the parts of the formlet. We don't need to
            // worry about disposing any resources here.
            _formlet.ValueChanged.Subscribe(v =>
            {
               _currentValue = v;
               submit.TriggerCanExecuteChanged();
               Fields = v.Fields;
            });

            Cancel = new Command(() => _onCancel());

            TriggerPropertyChanged();
         }
      }

      public IDictionary<string, ObservableObject> Fields
      {
         get { return _fields; }
         set 
         { 
            _fields = value;
            TriggerPropertyChanged();
         }
      }

      public ICommand Submit
      {
         get { return _submit; }
         set 
         { 
            _submit = value;
            TriggerPropertyChanged();
         }
      }

      public ICommand Cancel
      {
         get { return _cancel; }
         set 
         { 
            _cancel = value;
            TriggerPropertyChanged();
         }
      }

      public string SubmitAction
      {
         get { return _submitAction; }
      }
   }
}