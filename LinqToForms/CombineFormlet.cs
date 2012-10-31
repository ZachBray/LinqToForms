using System;
using System.Reactive.Linq;

namespace LinqToForms
{
   class CombineFormlet<A, B> : ObservableObject, IFormlet<B>
   {
      private readonly IObservable<ValueChanged<B>> _valueChanged;

      public CombineFormlet(IFormlet<A> formlet, Func<A, IFormlet<B>> continuation)
      {
         _valueChanged =
            formlet.ValueChanged.Select(v => 
               v.HasValue
                  ? continuation(v.Value).ValueChanged.Select(mappedV =>
                     mappedV.HasValue
                        ? new ValueChanged<B>(mappedV.Fields.Combine(v.Fields), mappedV.Value)
                        : new ValueChanged<B>(mappedV.Fields.Combine(v.Fields)))
                  : Observable.Return(new ValueChanged<B>(v.Fields)))
            .Switch();
      }

      public IObservable<ValueChanged<B>> ValueChanged 
      {
         get { return _valueChanged; } 
      }
   }
}