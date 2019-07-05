using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tms_Travel.Helpers;

namespace Tms_Travel.Model
{
    public class User : INotifyPropertyChanged
    {
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

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
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




        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public static async Task<bool> Login(string email, string password)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(email);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);

            if (isEmailEmpty || isPasswordEmpty)
            {
                return false;
            }
            else
            {
                try
                {
                    //var user = (await App.MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();
                    var user = await User.GetUserByemailPassword(email, password);

                    if (user != null)
                    {
                        App.user = user;
                        if (user.Password == password)
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static  void Register(User user)
        {
            //await App.MobileService.GetTable<User>().InsertAsync(user);
             var result =  User.PostAsync(user);
        }

        public static async void Insert(User user)
        {
            var url = Constants.ApiUser;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(user);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.PostAsync(url, httpContent);
                //return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }
        }


        public async static Task<List<User>> GetUsers()
        {
            List<User> users = new List<User>();

            var url = Constants.ApiUser;

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<User>>(json);

            }
        }

        public async static Task<bool> PostAsync(User user)
        {
            //User.Insert(user);
            //return true;

            var url = Constants.ApiUser;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(user);
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


        public async Task<User> GetUserById(string id, User user)
        {
            var url = Constants.ApiUser;

            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(user);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.PostAsync(url + id, httpContent);
                return user;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }


        }

        public async Task<User> GetUserById(string id)
        {
            try
            {
                {
                    var url = Constants.ApiUser;

                    HttpClient client = new HttpClient();
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    HttpContent httpContent = new StringContent(json);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var result = await client.PostAsync(url + id, httpContent);
                    return JsonConvert.DeserializeObject<User>(json);

                }

            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }
        }

        public async static Task<User> GetUserByemailPassword(string email, string password)
        {
            try
            {
                User user = new User();
                var url = Constants.ApiUser + email + "/" + password;

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        response.EnsureSuccessStatusCode();
                        string json = await response.Content.ReadAsStringAsync();
                        return user = JsonConvert.DeserializeObject<User>(json);
                    }

                    else
                    {
                        return null;
                    }



                }

            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }
        }


        public async static Task<bool> DeleteAsync(User user)
        {
            var url = Constants.ApiUser + user.Id;
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
