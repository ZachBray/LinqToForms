namespace LinqToForms.Example.Model
{
   class Address
   {
      private readonly string _firstLine;
      private readonly string _secondLine;
      private readonly string _postCode;

      public Address(string firstLine, string secondLine, string postCode)
      {
         _firstLine = firstLine;
         _secondLine = secondLine;
         _postCode = postCode;
      }

      public string FirstLine
      {
         get { return _firstLine; }
      }

      public string SecondLine
      {
         get { return _secondLine; }
      }

      public string PostCode
      {
         get { return _postCode; }
      }
   }
}
