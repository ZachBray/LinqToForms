using System;

namespace LinqToForms
{
   public static class Formlet
   {
      public static IFormlet<T> Return<T>(this T value)
      {
         return new StaticFormlet<T>(value);
      }

      public static IFormlet<string> Text(string id, string initialValue = "")
      {
         return new FieldFormlet<string>(id, initialValue);
      }

      public static IFormlet<int> Int(string id, int initialValue = 0, int minValue = int.MinValue,
                                      int maxValue = int.MaxValue, int stepping = 1)
      {
         return new FieldFormlet<int>(id, initialValue, minValue, maxValue, stepping);
      }

      public static IFormlet<decimal> Int(string id, decimal initialValue = 0, decimal minValue = decimal.MinValue,
                                      decimal maxValue = decimal.MaxValue, decimal stepping = 0.01m)
      {
         return new FieldFormlet<decimal>(id, initialValue, minValue, maxValue, stepping);
      }

      public static IFormlet<B> Bind<A, B>(this IFormlet<A> value, Func<A, IFormlet<B>> continuation)
      {
         return new CombineFormlet<A, B>(value, continuation);
      }

      public static IFormlet<C> SelectMany<A, B, C>(this IFormlet<A> childForm, Func<A, IFormlet<B>> restFunc, Func<A, B, C> select)
      {
         return childForm
            .Bind(a => 
               restFunc(a).Bind(b =>
                  select(a, b).Return()));
      }

      public static IFormlet<B> Select<A, B>(this IFormlet<A> childForm, Func<A, B> selector)
      {
         return childForm.Bind(a => selector(a).Return());
      }

      public static IFormlet<T> Where<T>(this IFormlet<T> formlet, Func<T, bool> filter)
      {
         return new FilterFormlet<T>(formlet, filter);
      }
   }
}
