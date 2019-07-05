using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Tms_Travel.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tms_Travel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrackingPage : ContentPage
	{
		int count;
		bool tracking;

		public ObservableCollection<Position> Positions { get; } = new ObservableCollection<Position>();

		public TrackingPage ()
		{
			InitializeComponent ();
			ListViewPositions.ItemsSource = Positions;

		}

		private async void ButtonTrack_Clicked(object sender, EventArgs e)
		{
			try
			{
				var hasPermission = await Utils.CheckPermissions(Permission.Location);
				if (!hasPermission)
					return;

				if (tracking)
				{
					CrossGeolocator.Current.PositionChanged -= CrossGeolocator_Current_PositionChanged;
					CrossGeolocator.Current.PositionError -= CrossGeolocator_Current_PositionError;
				}
				else
				{
					CrossGeolocator.Current.PositionChanged += CrossGeolocator_Current_PositionChanged;
					CrossGeolocator.Current.PositionError += CrossGeolocator_Current_PositionError;
				}

				if (CrossGeolocator.Current.IsListening)
				{
					await CrossGeolocator.Current.StopListeningAsync();
					labelGPSTrack.Text = "Stopped tracking";
					ButtonTrack.Text = "Start Tracking";
					tracking = false;
					count = 0;
				}
				else
				{
					Positions.Clear();
					if (await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(TrackTimeout.Value), TrackDistance.Value,
						TrackIncludeHeading.IsToggled, new ListenerSettings
						{
							ActivityType = (ActivityType)ActivityTypePicker.SelectedIndex,
							AllowBackgroundUpdates = AllowBackgroundUpdates.IsToggled,
							DeferLocationUpdates = DeferUpdates.IsToggled,
							DeferralDistanceMeters = DeferalDistance.Value,
							DeferralTime = TimeSpan.FromSeconds(DeferalTIme.Value),
							ListenForSignificantChanges = ListenForSig.IsToggled,
							PauseLocationUpdatesAutomatically = PauseLocation.IsToggled
						}))
					{
						labelGPSTrack.Text = "Started tracking";
						ButtonTrack.Text = "Stop Tracking";
						tracking = true;
					}
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
			}

		}

		private void CrossGeolocator_Current_PositionError(object sender, PositionErrorEventArgs e)
		{
			labelGPSTrack.Text = "Location error: " + e.Error.ToString();
		}

		private void CrossGeolocator_Current_PositionChanged(object sender, PositionEventArgs e)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				var position = e.Position;
				Positions.Add(position);
				count++;
				LabelCount.Text = $"{count} updates";
				labelGPSTrack.Text = string.Format("Time: {0} \nLat: \b{1} \nLong: \b{2} \nAltitude: {3} Altitude Accuracy: {4} Accuracy: {5} Heading: {6} Speed: {7}",
					position.Timestamp, position.Latitude, position.Longitude,
					position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

			});
		}
	}
}