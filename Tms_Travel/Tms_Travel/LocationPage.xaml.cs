using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms_Travel.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tms_Travel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationPage : ContentPage
	{
		int count;
		bool tracking;
		Position savedPosition;

		public ObservableCollection<Position> positions { get; } = new ObservableCollection<Position>();
		public LocationPage ()
		{
			InitializeComponent ();
		}

		private async void ButtonCached_Clicked(object sender, EventArgs e)
		{
			try
			{
				var hasPermission = await Utils.CheckPermissions(Permission.Location);
				if (!hasPermission)
					return;

				ButtonCached.IsEnabled = false;

				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = DesiredAccuracy.Value;
				LabelCached.Text = "Getting gps...";

				var position = await locator.GetLastKnownLocationAsync();

				if (position == null)
				{
					LabelCached.Text = "null cached location :(";
					return;
				}

				savedPosition = position;
				ButtonAddressForPosition.IsEnabled = true;
				LabelCached.Text = string.Format("Time: {0} \nLat: \b{1} \nLong: \b{2} Altitude: {3} Altitude Accuracy: {4} Accuracy: {5} Heading: {6} nSpeed: {7}",
					position.Timestamp, position.Latitude, position.Longitude,
					position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

			}
			catch (Exception ex)
			{
				await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
			}

			finally
			{
				ButtonCached.IsEnabled = true;
			}

		}

		private async void ButtonGetGPS_Clicked(object sender, EventArgs e)
		{
			try
			{
				var hasPermission = await Utils.CheckPermissions(Permission.Location);
				if (!hasPermission)
					return;

				ButtonGetGPS.IsEnabled = false;

				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = DesiredAccuracy.Value;
				labelGPS.Text = "Getting gps...";

				var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(Timeout.Value), null, IncludeHeading.IsToggled);

				if (position == null)
				{
					labelGPS.Text = "null gps :(";
					return;
				}
				savedPosition = position;
				ButtonAddressForPosition.IsEnabled = true;
				labelGPS.Text = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
					position.Timestamp, position.Latitude, position.Longitude,
					position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

			}
			catch (Exception ex)
			{
				await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
			}
			finally
			{
				ButtonGetGPS.IsEnabled = true;
			}

		}

		private async void ButtonAddressForPosition_Clicked(object sender, EventArgs e)
		{
			try
			{
				if (savedPosition == null)
					return;

				var hasPermission = await Utils.CheckPermissions(Permission.Location);
				if (!hasPermission)
					return;

				ButtonAddressForPosition.IsEnabled = false;

				var locator = CrossGeolocator.Current;

				var address = await locator.GetAddressesForPositionAsync(savedPosition, "RJHqIE53Onrqons5CNOx~FrDr3XhjDTyEXEjng-CRoA~Aj69MhNManYUKxo6QcwZ0wmXBtyva0zwuHB04rFYAPf7qqGJ5cHb03RCDw1jIW8l");
				if (address == null || address.Count() == 0)
				{
					LabelAddress.Text = "Unable to find address";
				}

				var a = address.FirstOrDefault();
				LabelAddress.Text = $"Address: Thoroughfare = {a.Thoroughfare}\nLocality = {a.Locality}\nCountryCode = {a.CountryCode}\nCountryName = {a.CountryName}\nPostalCode = {a.PostalCode}\nSubLocality = {a.SubLocality}\nSubThoroughfare = {a.SubThoroughfare}";

			}
			catch (Exception ex)
			{
				await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
			}
			finally
			{
				ButtonAddressForPosition.IsEnabled = true;
			}

		}
	}
}