
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using ImageCircle.Forms.Plugin.Droid;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Permissions;
using System.IO;

namespace Tms_Travel.Droid
{
    [Activity(Label = "Tms_Travel", Icon = "@drawable/logotmsgry", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("FastRenders_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            CurrentPlatform.Init();

            string dbName="Tms_Travel_db.sqlite";
            string folderPath= System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath=Path.Combine(folderPath,dbName);


            LoadApplication(new App(fullPath));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode,permissions,grantResults);

        }
    }
}