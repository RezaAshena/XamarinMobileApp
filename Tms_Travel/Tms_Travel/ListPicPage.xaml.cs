using System;
using Tms_Travel.Model;
using Tms_Travel.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tms_Travel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListPicPage : ContentPage
	{
        PicVM viewModel;
		public ListPicPage ()
		{
			InitializeComponent ();
            viewModel = new PicVM();
            BindingContext = viewModel;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.Updatepics();
        }

        private async void ButtonDetails_Clicked(object sender, EventArgs e)
        {
            var pic = viewModel.Picselected;
            await Navigation.PushAsync(new DetailPicPage(pic.FileName, pic.URL, pic.Content));
        }

        private async void DeleteBtn_Clicked(object sender,EventArgs e)
        {
            var pic = viewModel.Picselected;
            var result = await Picture.DeleteAsync(pic);
            viewModel.PreviousPicSelected = null;
            await viewModel.Updatepics();
        }

        private async void listPictures_Refreshing(object sender, EventArgs e)
        {
            await viewModel.Updatepics();
            listPictures.IsRefreshing = false;
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;
            if (isToggled)
            {
                viewModel.IsSwitchToggled = true;
            }

            else viewModel.IsSwitchToggled = false;
        }

    }

}