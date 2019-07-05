using Microsoft.WindowsAzure.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms_Travel.Helpers;
using Tms_Travel.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tms_Travel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImagePage : ContentPage
	{
		public ImagePage ()
		{
			InitializeComponent ();
		}


	   public async void SelectImageButton_Clicked (object sender ,EventArgs e)
		{

            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "This is not supported on your device", "Ok");
                return;
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Small
            };
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectedImageFile == null)
            {
                await DisplayAlert("Error", "there was an error when trying to get your image.", "Ok");
                return;
            }

            selectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());

            try
            {
                Guid guid = Guid.NewGuid();
                var stream = selectedImageFile.GetStream();
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                string base64 = System.Convert.ToBase64String(bytes);

                Picture pic = new Picture()
                {
                    Id = guid.ToString(),
                    FileName = ($"{guid}.jpg"),
                    Size=PhotoSize.Small.ToString(),
                    URL = Constants.ApiPicture+"pic/"+($"{guid}"),
                    Content = base64,
                    CREATEDAT = DateTime.Now,
                    UserId = App.user.Id
                };

                var result = await Picture.PostPicAsync(pic);
                await DisplayAlert("Success", "Picture saved successfully", "Ok");
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Something wrong.", "Ok");
                throw;
            }

        }

	}
}