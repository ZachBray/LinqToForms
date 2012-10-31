namespace LinqToForms.Example.Model
{
   class Company
   {
      private readonly string _name;
      private readonly Address _address;

      public Company(string name, Address address)
      {
         _name = name;
         _address = address;
      }

      public string Name
      {
         get { return _name; }
      }

      public Address Address
      {
         get { return _address; }
      }
   }
}
