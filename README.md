# LinqToForms

## Overview

* LinqToForms is a *library* for composing [WPF](http://en.wikipedia.org/wiki/Windows_Presentation_Foundation) forms using [LINQ](http://msdn.microsoft.com/en-us/library/bb397926.aspx).
* It only helps you build a `ViewModel`. It doesn't generate a `View`. Although this is something you could easily do yourself.
* It works with [XAML](http://msdn.microsoft.com/en-us/library/ms752059.aspx).
* It is a *toy* project and hasn't been well tested yet.

## Goals
* To make the construction of forms using [MVVM](http://en.wikipedia.org/wiki/Model_View_ViewModel) feel more natural.
* To simplify `ViewModel` validation logic by removing things like mutable state.
* To make it possible to compose forms.

## Structure
#### Formlets
Tomas Petricek wrote a great blog entry [here](http://tomasp.net/blog/formlets-in-linq.aspx) on *formlets*. The basic idea is that *a formlet represents part of a form*. For example, a text field or an address section. Also, *a formlet can include other formlets*. For example, a company formlet might include an address formlet.

###### Construction and validation

The code below demonstrates how you might construct an address formlet with LinqToForms.

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

In the first three lines from the `return` statement we declare some text fields that should be available on the address form using the `from` operation. We pass a `string` into the `Fomlet.Text(string id)` method for each field. Later we will use these `string` values as keys into a dictionary of fields so that we can interact with the form from XAML.

The lines starting with `where` clauses constitute the validation logic for the formlet. Here we check that the "First Line" and "Second Line" fields are filled and that the "Post Code" field matches a regular expression.

###### Composition

The code below demonstrates how you might construct a company formlet that makes use of the address formlet above.

      public IFormlet<Company> Create()
      {
         return from name in Formlet.Text("Name")
                from address in new AddressFormletFactory().Create()
                where name != ""
                select new Company(name, address);
      }
 
Once again we declare a field, "Name", that we expect in the form. In the following line we use the `from` operation again but this time to import a child formlet and all of its validation logic. The `address` variable has the type `Address` and it is used in the construction of a new `Company` instance.

**Note: At present, LinqToForms only supports `where` clauses after all fields have been declared.** This includes where clauses in child formlets. This needs to be fixed. It limits composability.

#### Forms and ViewModels

The value of a formlet changes over time. More specifically, it changes when the user updates a field in the form. Furthermore, any change in a child formlet propagates to its parents. For example, when a user changes the "First Line" field in the "Address Section" the address formlet produces a new `Address` instance if it passes validation. This new `Address` causes the company formlet to produce a new `Company` instance with an updated `Address` field.

A `Form` is wrapper around a formlet. It listens to the formlet values over time and provides a command for submitting the current value of the formlet. This command is only executable when the formlet passes validation. It also exposes a dictionary from identifier to form field. This allows the view to interact with the form values.

Below is an example of a form for the company formlet.

   	class CompanyForm : Form<Company>
   	{
      	public CompanyForm(string submitAction, Action<Company> onSubmit, Action onCancel) 
         : base(submitAction, onSubmit, onCancel)
      	{
         	Definition = new CompanyFormletFactory().Create();
      	}
   	}

We assign the formlet to the `Definition` property of the `Form` sub type. In the constructor of `Form` we pass in several arguments. The `submitAction` argument is the label we give the submit button on the form, e.g., "Update Company" or "Add Company". The `onSubmit` argument is a continuation for when the user submits a validated form.

#### Views

###### Text field bindings

		<Label Grid.Column="0">Name</Label>
		<TextBox Grid.Column="1" Text="{Binding Fields[Name].Value, Mode=TwoWay}" />

* `Name` is the identifier provided in the formlet for the field.

###### Int/decimal field bindings

		<Label Grid.Column="0">Age</Label>
 		<xctk:IntegerUpDown Grid.Column="1"
			Value="{Binding Fields[Age].Value, Mode=TwoWay}" 
			Increment="{Binding Fields[Age].Stepping}" 
			Maximum="{Binding Fields[Age].MaxValue}" 
			Minimum="{Binding Fields[Age].MinValue}" />

* `Age` is the identifier provided in the formlet for the field.
* The initial `Value`, `Stepping`, `Minimum` and `Maximum` can be defined in the formlet. See the `UserFormletFactory` in the example project.

###### Other field bindings

Feel free to add more and send us a pull request.

## Gotchas
* Where clause problem. See **bold** note above.
* Identifier re-use (in child formlets) is not supported.
* Probably some other stuff too!
