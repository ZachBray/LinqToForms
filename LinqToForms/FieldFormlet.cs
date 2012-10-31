using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace LinqToForms
{
   class FieldFormlet<T> : ObservableObject, IFormlet<T>
   {
      private readonly ISubject<T> _value;
      private T _currentValue;
      private readonly T _minValue;
      private readonly T _maxValue;
      private readonly T _stepping;
      private readonly IDictionary<string, ObservableObject> _fields;

      public FieldFormlet(
         string id, 
         T initialValue = default(T),
         T minValue = default(T),
         T maxValue = default(T),
         T stepping = default(T))
      {
         _value = new ReplaySubject<T>(1);
         _currentValue = initialValue;
         _minValue = minValue;
         _maxValue = maxValue;
         _stepping = stepping;
         _value.OnNext(initialValue);
         _fields = new Dictionary<string, ObservableObject> { { id, this } };
      }

      public IObservable<ValueChanged<T>> ValueChanged
      {
         get { return _value.Select(v => new ValueChanged<T>(_fields, v)); }
      }

      public T Value
      {
         get { return _currentValue; }
         set
         {
            _currentValue = value;
            _value.OnNext(value);
            TriggerPropertyChanged();
         }
      }

      public T MinValue
      {
         get { return _minValue; }
      }

      public T MaxValue
      {
         get { return _maxValue; }
      }

      public T Stepping
      {
         get { return _stepping; }
      }
   }
}