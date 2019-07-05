using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Linq;
using Tms_Travel.Model;
using Tms_Travel.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tms_Travel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTravelPage : ContentPage
	{

        Position savedPosition;

        NewTravelVM viewModel;

		public NewTravelPage ()
		{
			InitializeComponent ();

			viewModel = new NewTravelVM();
			BindingContext = viewModel;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			try
			{
				var status =await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Location);
				if(status != PermissionStatus.Granted)
				{
					if(await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
					{
						   await DisplayAlert("Need permission", "we will have to access your location","Ok") ;
					}

					var results = await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location);

					

				}

				if(status == PermissionStatus.Granted)
				{
					var locator=CrossGeolocator.Current;
					var position= await locator.GetPositionAsync();

					var venues = await Venue.GetVenues(position.Latitude,position.Longitude);

					venueListView.ItemsSource=venues;
				}
				else
				{
					await DisplayAlert("No permission", "you didn't granted permission to access your location,we can not proceed", "Ok");
				}


			}
			catch (Exception ex)
			{

				
			}
		}

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			

			try
			{
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync(null, null, false);
                if (position == null)
                {
                    return;
                }
                savedPosition = position;

                var address = await locator.GetAddressesForPositionAsync(savedPosition, "RJHqIE53Onrqons5CNOx~FrDr3XhjDTyEXEjng-CRoA~Aj69MhNManYUKxo6QcwZ0wmXBtyva0zwuHB04rFYAPf7qqGJ5cHb03RCDw1jIW8l");
                if (address == null || address.Count() == 0)
                {
                    DisplayAlert("Error", "Unable to find address", "Ok");
                }

                var add = address.FirstOrDefault();

                Guid guid = Guid.NewGuid();
				Post post = new Post()
				{
					Id = guid.ToString(),
					UserId = App.user.Id,
					Experience = experienceEntery.Text,
                    Latitude= position.Latitude,
                    Longitude=position.Longitude,
                    AdminArea= add.AdminArea,
                    CountryCode=add.CountryCode,
                    FeatureName=add.FeatureName,
                    PostalCode=add.PostalCode,
                    SubAdminArea=add.SubAdminArea,
                    SubLocality=add.SubLocality,
                    SubThroughfare=add.SubThoroughfare,
                    Throughfare=add.Thoroughfare,
                    Locality=add.Locality,
					CREATEDAT= DateTime.Now
				};

				var result = await Post.PostAsync(post);

				await DisplayAlert("Success", "Post created successfully", "Ok");
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "Something wrong.", "Ok");
				throw;
			}
		}
	}
}