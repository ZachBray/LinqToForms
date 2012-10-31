using System.Text.RegularExpressions;
using LinqToForms.Example.Model;

namespace LinqToForms.Example.ViewModel.Formlets
{
   class AddressFormletFactory
   {
      public IFormlet<Address> Create()
      {
         var postCodeRegex = new Regex(@"[A-Za-z]+[0-9]+\s+[0-9]+[A-Za-z]+");

         return from firstLine in Formlet.Text("First Line")
                from secondLine in Formlet.Text("Second Line")
                from postCode in Formlet.Text("Post Code")
                where firstLine != ""
                where secondLine != ""
                where postCodeRegex.IsMatch(postCode)
                select new Address(firstLine, secondLine, postCode);
      }
   }
}
