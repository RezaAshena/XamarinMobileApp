using System;
using Tms_Travel.Model;
using Tms_Travel.ViewModel;
using Xamarin.Forms;

namespace Tms_Travel
{
    public partial class MainPage : ContentPage
    {
        MainVM viewModel;

        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            viewModel = new MainVM();
            BindingContext = viewModel;

            iconImage.Source = ImageSource.FromResource("Tms_Travel.Assets.Images.TMSlogo3.PNG", assembly);
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
           bool canLogin = await User.Login(emailEntry.Text, passwordEntry.Text);
            if (canLogin)
                await Navigation.PushAsync(new HomePage());
            else
                await DisplayAlert("Error", "Email or password are incorrect Try again.", "Ok");

        }
    }
}
