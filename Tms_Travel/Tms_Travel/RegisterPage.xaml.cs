using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms_Travel.Model;
using Tms_Travel.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tms_Travel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		RegisterVM viewModel;

		public RegisterPage ()
		{
			InitializeComponent ();

			viewModel = new RegisterVM();
			BindingContext = viewModel;
		}

		private async void  registerButton_Clicked(object sender, EventArgs e)
		{
			if(passwordEntry.Text == ConfirmpasswordEntry.Text)
			{
				Guid guid = Guid.NewGuid();
				User user = new User()
				{
					Id = guid.ToString(),
					Email = emailEntry.Text,
					Password = passwordEntry.Text,
					ConfirmPassword=passwordEntry.Text,
                    CREATEDAT=DateTime.Now
				};

				var result = await User.PostAsync(user);
				//User.Register(user);
                //User.PostAsync(user);
				await DisplayAlert("Success", "User created successfully", "Ok");
				emailEntry.Text = null;
				passwordEntry.Text = null;
				ConfirmpasswordEntry.Text = null;
				registerButton.IsVisible = false;

			}
			else
			{
			  await  DisplayAlert("Error", "Password don't match.", "Ok");
			}
		}
	}
}