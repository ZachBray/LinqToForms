using System;
using System.Reactive.Linq;

namespace LinqToForms
{
   class FilterFormlet<T> : IFormlet<T>
   {
      private readonly IObservable<ValueChanged<T>> _valueChanged;

      public FilterFormlet(IFormlet<T> formlet, Func<T, bool> filter)
      {
         _valueChanged = formlet.ValueChanged.Select(v => v.Where(filter));
      }

      public IObservable<ValueChanged<T>> ValueChanged
      {
         get { return _valueChanged; }
      }
   }
}