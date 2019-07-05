using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tms_Travel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPicPage : ContentPage
	{
		public DetailPicPage (string fileName,string url,string content)
		{
			InitializeComponent ();
            MyItemNameShow.Text = fileName;
            MyImagerediantItemShow.Text = url;

            byte[] Base64Stream = Convert.FromBase64String(content);
            MyImage.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
        }
	}
}