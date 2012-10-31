using System;
using System.Windows;
using LinqToForms.Example.Model;
using LinqToForms.Example.View;
using LinqToForms.Example.ViewModel;

namespace LinqToForms.Example
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnShowCompanyForm(object sender, RoutedEventArgs e)
      {
         var window = new Window();
         var view = new CompanyFormView();

         Action<Company> onSubmit = co =>
         {
            window.Close();
            MessageBox.Show(string.Format("{0} added!", co.Name));
         };

         Action onCancel = window.Close;

         var viewModel = new CompanyForm("Add company", onSubmit, onCancel);
         view.DataContext = viewModel;
         window.Content = view;
         window.Show();
         window.BringIntoView();
      }

      private void OnShowUserForm(object sender, RoutedEventArgs e)
      {
         var window = new Window();
         var view = new UserFormView();

         Action<Person> onSubmit = p =>
         {
            window.Close();
            MessageBox.Show(string.Format("{0} (aged: {1} years) added!", p.Name, p.Age));
         };

         Action onCancel = window.Close;

         var viewModel = new UserForm("Add user", onSubmit, onCancel);
         view.DataContext = viewModel;
         window.Content = view;
         window.Show();
         window.BringIntoView();
      }
   }
}
