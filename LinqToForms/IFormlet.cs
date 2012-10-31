using System;

namespace LinqToForms
{
   public interface IFormlet<T>
   {
      IObservable<ValueChanged<T>> ValueChanged { get; }
   }
}