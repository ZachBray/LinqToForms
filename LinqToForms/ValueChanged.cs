using System;
using System.Collections.Generic;

namespace LinqToForms
{
   public class ValueChanged<T>
   {
      private readonly bool _hasValue;
      private readonly IDictionary<string, ObservableObject> _fields;
      private readonly T _value;

      public ValueChanged(IDictionary<string, ObservableObject> fields, T value)
      {
         _hasValue = true;
         _fields = fields;
         _value = value;
      }

      public ValueChanged(IDictionary<string, ObservableObject> fields)
      {
         _fields = fields;
         _hasValue = false;
      }

      public bool HasValue
      {
         get { return _hasValue; }
      }

      public T Value
      {
         get { return _value; }
      }

      public IDictionary<string, ObservableObject> Fields
      {
         get { return _fields; }
      }
   }

   public static class ValueChanged
   {
      public static ValueChanged<A> Where<A>(this ValueChanged<A> valueChanged, Func<A, bool> filter)
      {
         return valueChanged.HasValue
            ? filter(valueChanged.Value)
               ? valueChanged
               : new ValueChanged<A>(valueChanged.Fields)
            : valueChanged;
      }
   }
}