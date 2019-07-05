using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using Tms_Travel.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tms_Travel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage ()
		{
			InitializeComponent ();
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();

			var locator= CrossGeolocator.Current;
			locator.PositionChanged += Locator_PositionChanged;
			await locator.StartListeningAsync(TimeSpan.FromSeconds(0),100);

			var position=await locator.GetPositionAsync();

			var center= new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
			var span=new Xamarin.Forms.Maps.MapSpan(center,2,2);
			locationMaps.MoveToRegion(span);

			/*using (SQLiteConnection conn = new SQLiteConnection(App.DataBaseLocation))
			{
				conn.CreateTable<Post>();
				var posts = conn.Table<Post>().ToList();

				DisplayInMap(posts);
			}*/

			//var posts =await Post.Read();
			var posts = await Post.GetPostByUserId(App.user.Id);
			DisplayInMap(posts);
		}

		protected override async void OnDisappearing()
		{
			base.OnDisappearing();

			var locator = CrossGeolocator.Current;
			locator.PositionChanged -= Locator_PositionChanged;

			await locator.StopListeningAsync();
		}

		private void DisplayInMap(List<Post> posts)
		{
			foreach(var post in posts)
			{
				try
				{

				var position = new Xamarin.Forms.Maps.Position(post.Latitude,post.Longitude);

				var pin= new Xamarin.Forms.Maps.Pin()
				{
					Type = Xamarin.Forms.Maps.PinType.SavedPin,
					Position=position,
					//Label=post.VenueName,
					//Address=post.Address
				};
				locationMaps.Pins.Add(pin);
				}
				catch(NullReferenceException nre)
				{

				}
				catch (Exception ex)
				{
				   
				}
			}
		}

		private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
		{
			var center = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude);
			var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);
			locationMaps.MoveToRegion(span);
		}
	}
}