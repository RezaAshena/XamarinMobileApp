using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tms_Travel.Helpers;

namespace Tms_Travel.Model
{
    public class Post:INotifyPropertyChanged
    {
        private string id;
        public string Id
        {
            get { return id; }
            set {
                   id = value;
                   OnPropertyChanged("Id");
                }
        }


        private string experience;
        public string Experience
        {
            get { return experience; }
            set {
                   experience = value;
                   OnPropertyChanged("Experience");
                }
        }

        private string adminArea;
        public string AdminArea
        {
            get { return adminArea; }
            set {
                    adminArea = value;
                    OnPropertyChanged("AdminArea");
                }
        }

        private string countryCode;
        public string CountryCode
        {
            get { return countryCode; }
            set
            {
                countryCode = value;
                OnPropertyChanged("CountryCode");
            }
        }

        private string countryName;
        public string CountryName
        {
            get { return countryName; }
            set
            {
                countryName = value;
                OnPropertyChanged("CountryName");
            }
        }

        private string featureName;
        public string FeatureName
        {
            get { return featureName; }
            set {
                featureName = value;
                 OnPropertyChanged("FeatureName");
                }
        }

        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set {
                    latitude = value;
                    OnPropertyChanged("Latitude");
                }
        }


        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set {
                    longitude = value;
                    OnPropertyChanged("Longitude");
                }
        }

        private string locality;
        public string Locality
        {
            get { return locality; }
            set { locality = value;
                  OnPropertyChanged("Locality");
                }
        }

        private string postalCode;
        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value;
                  OnPropertyChanged("PostalCode");
                }
        }

        private string subAdminArea;
        public string SubAdminArea
        {
            get { return subAdminArea; }
            set { subAdminArea = value;
                  OnPropertyChanged("SubAdminArea");
                }
        }

        private string subLocality;
        public string SubLocality
        {
            get { return subLocality; }
            set { subLocality = value;
                  OnPropertyChanged("SubLocality");
                }
        }

        private string subThroughfare;
        public string SubThroughfare
        {
            get { return subThroughfare; }
            set { subThroughfare = value;
                  OnPropertyChanged("SubThroughfare");
                }
        }

        private string throughfare;
        public string Throughfare
        {
            get { return throughfare; }
            set { throughfare = value;
                  OnPropertyChanged("Throughfare");
               }
        }


        private string userId;
        public string UserId
        {
            get { return userId; }
            set { userId = value;
                  OnPropertyChanged("UserId");
                }
        }

        private Venue venue;
        [JsonIgnore]
        public Venue Venue
        {
            get { return venue; }
            set {
                    venue = value;
                    
                   if(venue.categories != null)
                   { 

                      var firstCategory = venue.categories.FirstOrDefault();
                      if(firstCategory != null)
                      { 
                         //CategoryId = firstCategory.id;
                         //CategoryName = firstCategory.name;
                      }
                   }
                if (venue.location != null)
                    { 
                        //Address = venue.location.address;
                        //Distance = venue.location.distance;
                        Latitude = venue.location.lat;
                        Longitude = venue.location.lng;
                    }
                   // VenueName = venue.name;
                    UserId = App.user.Id.ToString();

                    OnPropertyChanged("Venue");
                }

        }

        private DateTimeOffset createdat;
        public DateTimeOffset CREATEDAT
        {
            get { return createdat; }
            set {
                    createdat = value;
                    OnPropertyChanged("CREATEDAT");
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

        public event PropertyChangedEventHandler PropertyChanged;

        public static async void Insert(Post post)
        {
            //await App.MobileService.GetTable<Post>().InsertAsync(post);
            await App.postsTable.InsertAsync(post);
            await App.MobileService.SyncContext.PushAsync();

        }

        public static async Task<bool> Delete(Post post)
        {
            try
            {
                //await App.MobileService.GetTable<Post>().DeleteAsync(post);
                await App.postsTable.DeleteAsync(post);
                await App.MobileService.SyncContext.PushAsync();
                return true;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                return false;
            }

        }

        public static async Task<List<Post>> Read()
        {
            var posts = await App.postsTable.Where(p => p.UserId == App.user.Id).ToListAsync();
            return posts;
        }

        //public static Dictionary<string,int> PostCategories(List<Post> posts)
        //{
        //    //var categories = (from p in posts
        //    //                  orderby p.CategoryId
        //    //                  select p.CategoryName).Distinct().ToList();

        //    //Dictionary<string, int> categoriesCount = new Dictionary<string, int>();
        //    //foreach (var category in categories)
        //    //{
        //    //    var count = (from post in posts
        //    //                 where post.CategoryName == category
        //    //                 select post).ToList().Count;

        //    //    categoriesCount.Add(category, count);
        //    //}

        //    //return categoriesCount;
        //}

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
        }

        public async static Task<List<Post>> GetPosts()
        {
            var url = Constants.ApiPost;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Post>>(json);
            }

        }

        public async static Task<bool> PostAsync(Post post)
        {
            var url = Constants.ApiPost;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(post);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.PostAsync(url, httpContent);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }

        }

        public async static Task<bool> DeleteAsync(Post post)
        {
            var url = Constants.ApiPost + "Delete/" + post.Id;
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

        public async static Task<List<Post>> GetPostByUserId(string id)
        {
            Post posts = new Post();
            var url = Constants.ApiPost + id;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Post>>(json);


                }



            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }
        }

        public async static Task<List<Post>> GetPostId(string id)
        {
            Post posts = new Post();
            var url = Constants.ApiPost + "GetbyId/" + id;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Post>>(json);


                }



            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }
        }


        public async static Task<bool> PostAttend(string postId, string attendeeid)
        {
            var url = Constants.ApiPost + "/" + postId + "/" + attendeeid;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<bool>(json);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
