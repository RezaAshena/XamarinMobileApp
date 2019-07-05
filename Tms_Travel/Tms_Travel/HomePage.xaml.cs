using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms_Travel.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tms_Travel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : TabbedPage
	{
		HomeVM viewModel;

		public HomePage ()
		{
			InitializeComponent ();
			var tabclr = Color.FromHex("#FF5733");
			this.BarBackgroundColor=tabclr;

			viewModel = new HomeVM();
			BindingContext = viewModel;
		}


    }
}