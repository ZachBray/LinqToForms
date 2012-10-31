using LinqToForms.Example.Model;

namespace LinqToForms.Example.ViewModel.Formlets
{
   class PersonFormletFactory
   {
      public IFormlet<Person> Create()
      {
         return from name in Formlet.Text("Name")
                from age in Formlet.Int("Age", 18, 0, 120, 1)
                from address in new AddressFormletFactory().Create()
                where name != ""
                select new Person(name, address, age);
      }
   }
}
