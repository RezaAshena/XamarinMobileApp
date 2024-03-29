﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms_Travel.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tms_Travel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();

			//using (SQLiteConnection conn = new SQLiteConnection(App.DataBaseLocation))
			//{
				var postTable = await Post.Read();

				//var categoriesCount = Post.PostCategories(postTable);

				//categoryListView.ItemsSource=categoriesCount;

				postCountLabel.Text=postTable.Count.ToString();
			//}
		}
	}
}