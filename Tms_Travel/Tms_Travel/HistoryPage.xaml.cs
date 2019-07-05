using System;
using Tms_Travel.Model;
using Tms_Travel.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tms_Travel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
		HistoryVM viewModel;
		public HistoryPage ()
		{
			InitializeComponent ();

			viewModel = new HistoryVM();
            postListView.BindingContext = viewModel;
            //BindingContext = viewModel;
		}

		protected async  override  void OnAppearing()
		{
 			 base.OnAppearing();

			await viewModel.UpdatePosts();
           // viewModel.IsAttending = await viewModel.PostAttend();
		}

        private async void Mine_Clicked(object sender, EventArgs e)
        {
            await viewModel.MyPosts();
            postListView.IsRefreshing = false;
        }

        private async  void Delete_Clicked(object sender, EventArgs e)
		{
			var post = (Post)((MenuItem)sender).CommandParameter;

			var result = await Post.DeleteAsync(post); 
			//await DisplayAlert("Success", "Post deleted successfully", "Ok");
            await viewModel.UpdatePosts();

		}


        private async void postListView_Refreshing(object sender, EventArgs e)
		{
			//await AzureAppServiceHelper.SyncAsync();
			postListView.IsRefreshing = false;
		}

        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;
            if (isToggled)
            {
                try
                {

                    var att = new Attendance()
                    {
                        AttendeeId = App.user.Id,
                        PostId = viewModel.Postselected.Id
                    };

                    var result = await Attendance.AttendAsync(att);
                    viewModel.IsAttending= isToggled = await Post.PostAttend(viewModel.Postselected.Id, App.user.Id);
                }
                catch (Exception ex)
                {
                    var m = ex.Message;
                }
            }
               
            else
            {
                try
                {
                    var att = new Attendance()
                    {
                        AttendeeId = App.user.Id,
                        PostId = viewModel.Postselected.Id
                    };

                    var result = await Attendance.DeleteAsync(att);
                }
                catch (Exception ex)
                {
                    var m = ex.Message;
                    throw;
                }
            }
                


        }

    }
}