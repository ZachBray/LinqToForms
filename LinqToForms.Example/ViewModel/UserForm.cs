using System;
using LinqToForms.Example.Model;
using LinqToForms.Example.ViewModel.Formlets;

namespace LinqToForms.Example.ViewModel
{
   class UserForm : Form<Person>
   {
      public UserForm(string submitAction, Action<Person> onSubmit, Action onCancel) 
         : base(submitAction, onSubmit, onCancel)
      {
         Definition = new PersonFormletFactory().Create();
      }
   }
}
