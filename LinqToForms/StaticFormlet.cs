using System;
using System.Reactive.Linq;

namespace LinqToForms
{
   class StaticFormlet<T> : IFormlet<T>
   {
      private readonly T _value;

      public StaticFormlet(T value)
      {
         _value = value;
      }

      public IObservable<ValueChanged<T>> ValueChanged
      {
         get 
         { 
            return Observable.Return(
               new ValueChanged<T>(Dictionary.Empty<string, ObservableObject>(), _value)); 
         }
      }
   }
}