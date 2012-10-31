using LinqToForms.Example.Model;

namespace LinqToForms.Example.ViewModel.Formlets
{
   class CompanyFormletFactory
   {
      public IFormlet<Company> Create()
      {
         return from name in Formlet.Text("Name")
                from address in new AddressFormletFactory().Create()
                where name != ""
                select new Company(name, address);
      }
   }
}
