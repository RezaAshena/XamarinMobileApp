using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tms_Travel.Helpers;

namespace Tms_Travel.Model
{
    public class Picture : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private DateTimeOffset createdat;
        public DateTimeOffset CREATEDAT
        {
            get { return createdat; }
            set
            {
                createdat = value;
                OnPropertyChanged("CREATEDAT");
            }
        }

        private string filename;
        public string FileName
        {
            get { return filename; }
            set
            {
                filename = value;
                OnPropertyChanged("FileName");
            }
        }

        private string size;
        public string Size
        {
            get { return size; }
            set
            {
                size = value;

                OnPropertyChanged("Size");
            }
        }

        private string userId;
        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }

        private string url;
        public string URL
        {
            get { return url; }
            set
            {
                url = value;
                OnPropertyChanged("URL");
            }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                OnPropertyChanged("Content");
                OnPropertyChanged("Image");
            }
        }

        private bool _isVisible { get; set; }
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged("IsVisible");
                }
            }
        }


        private Xamarin.Forms.ImageSource image;
        [JsonIgnore]
        public Xamarin.Forms.ImageSource Image
        {
            get
            {
                if (image == null)
                {
                    image = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(Content)));
                }
                return image;
            }

        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public async static Task<bool> PostPicAsync(Picture pic)
        {
            var url = Constants.ApiPicture;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(pic);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.PostAsync(url, httpContent);
                return result.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {

                var m = ex.Message;
                throw;
            }
        }

        public async static Task<List<Picture>> GetPicturesByUserId(string id)
        {
            var url = Constants.ApiPicture+id;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Picture>>(json);
            }

        }

        public async static Task<bool> DeleteAsync(Picture pic)
        {
            var url = Constants.ApiPicture + "Delete/" + pic.Id;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.DeleteAsync(url);
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
