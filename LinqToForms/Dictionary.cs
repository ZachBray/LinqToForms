using System.Collections.Generic;

namespace LinqToForms
{
   static class Dictionary
   {
      public static IDictionary<K, V> Empty<K, V>()
      {
         return new Dictionary<K, V>();
      }

      public static IDictionary<K, V> Combine<K, V>(this IDictionary<K, V> xs, IDictionary<K, V> ys)
      {
         var combined = Empty<K, V>();

         foreach (var kvp in xs)
            combined.Add(kvp.Key, kvp.Value);

         foreach (var kvp in ys)
            combined.Add(kvp.Key, kvp.Value);

         return combined;
      }
   }
}