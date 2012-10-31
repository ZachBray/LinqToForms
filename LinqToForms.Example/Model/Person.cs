namespace LinqToForms.Example.Model
{
   class Person
   {
      private readonly string _name;
      private readonly Address _address;
      private readonly int _age;

      public Person(string name, Address address, int age)
      {
         _name = name;
         _address = address;
         _age = age;
      }

      public string Name
      {
         get { return _name; }
      }

      public Address Address
      {
         get { return _address; }
      }

      public int Age
      {
         get { return _age; }
      }
   }
}
