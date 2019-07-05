using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Tms_Travel.Model;
using Tms_Travel.ViewModel.Commands;

namespace Tms_Travel.ViewModel
{
    public class MainVM:INotifyPropertyChanged
    {

        private User user;
        public User User
        {
            get { return user; }
            set {
                  user = value;
                  OnPropertChanged("User");
                }
        }

        public RegisterNavigationCommand RegisterNavigationCommand { get;set;}

        

        public LoginCommand LoginCommand { get; set; }

        private string email;
        public string Email
        {
            get { return email; }
            set {
                    email = value;
                    User = new User()
                    {
                        Email=this.Email,
                        Password=this.Password
                    };
                    OnPropertChanged("Email");
                }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set {
                    password = value;
                    User = new User()
                    {
                        Email = this.Email,
                        Password = this.Password
                    };
                    OnPropertChanged("Password");
                 }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
        }

        public MainVM()
        {
            User = new User();
            LoginCommand = new LoginCommand(this);
            RegisterNavigationCommand = new RegisterNavigationCommand(this);
        }

        public  async void Login()
        {
            bool canLogin = await User.Login(User.Email, User.Password);

            if (canLogin)
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            else
                await App.Current.MainPage.DisplayAlert("Error", "Try again", "Ok");
        }

        public async void Navigate()
        {
          await  App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}
