using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LinqToForms
{
   public class ObservableObject : INotifyPropertyChanged
   {
      protected void TriggerPropertyChanged([CallerMemberName] string name = null)
      {
         var e = PropertyChanged;
         if (e != null) e(this, new PropertyChangedEventArgs(name));
      }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
