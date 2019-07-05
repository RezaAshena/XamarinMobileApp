using System;
using Tms_Travel.Model;
using Tms_Travel.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tms_Travel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        UserVM viewModel;
        public UserPage()
        {
            InitializeComponent();

            viewModel = new UserVM();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            viewModel.UpdateUsers();

            //var users = await User.GetUsers();

            //userListView.ItemsSource = users;

        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var user = (User)((MenuItem)sender).CommandParameter;
            var result = await User.DeleteAsync(user);
            //await DisplayAlert("Success", "User deleted successfully", "Ok");
            await viewModel.UpdateUsers();

        }

        private async void userListView_Refreshing(object sender, EventArgs e)
        {
            await viewModel.UpdateUsers();
            userListView.IsRefreshing = false;
        }
    }
}