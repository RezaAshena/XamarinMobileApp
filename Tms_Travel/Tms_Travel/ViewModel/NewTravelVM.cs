﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Tms_Travel.Model;
using Tms_Travel.ViewModel.Commands;

namespace Tms_Travel.ViewModel
{
    public class NewTravelVM:INotifyPropertyChanged
    {
        public PostCommand PostCommand { get;set;}

        private Post post;
        public Post Post
        {
            get { return post; }
            set {
                  post = value;
                  OnPropertyChanged("Post");
                }
        }

        private string experience;
        public string Experience
        {
            get { return experience; }
            set
            {
                experience = value;
                Post = new Post()
                {
                    Experience = this.Experience,
                    Venue = this.venue
                };
                OnPropertyChanged("Experience");
            }
        }

        private Venue venue;
        public Venue Venue
        {
            get { return venue; }
            set {
                  venue = value;
                  Post = new Post()
                  {
                        Experience = this.Experience,
                        Venue = this.venue
                  };
                  OnPropertyChanged("Venue");
                 }
        }



        public NewTravelVM()
        {
            PostCommand = new PostCommand(this);
            Post = new Post();
            Venue = new Venue();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void PublishPost(Post post)

        {
            try
            {
                Post.Insert(post);
                await App.Current.MainPage.DisplayAlert("Success", "Experience succesfully inserted", "Ok");
            }
            catch (NullReferenceException nre)
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Experience Failed to be insert", "Ok");
            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("Failure", "Experience Failed to be insert", "Ok");
            }
        }
    }
}
