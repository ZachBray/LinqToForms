using System;
using LinqToForms.Example.Model;
using LinqToForms.Example.ViewModel.Formlets;

namespace LinqToForms.Example.ViewModel
{
   class CompanyForm : Form<Company>
   {
      public CompanyForm(string submitAction, Action<Company> onSubmit, Action onCancel) 
         : base(submitAction, onSubmit, onCancel)
      {
         Definition = new CompanyFormletFactory().Create();
      }
   }
}
