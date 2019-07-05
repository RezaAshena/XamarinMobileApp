using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Tms_Travel.Model;

namespace Tms_Travel.ViewModel
{
    public  class HistoryVM: INotifyPropertyChanged
    {
        public ObservableCollection<Post> Posts { get;set;}

        private bool isAttending;
        public bool IsAttending
        {
            get { return isAttending; }
            set
            {
                //isAttending = Post.PostAttend("12c3dac4-7f02-460d-afe2-5fb051a76dab", App.user.Id).Result; ;
                OnPropertyChanged("IsAttending");
            }
        }

        public Post PreviousPostSelected { get; set; }

        private Post postselected;

        public event PropertyChangedEventHandler PropertyChanged;

        public Post Postselected
        {
            get { return postselected; }
            set
            {
                if (postselected != value)
                {
                    postselected = value;
                    ExpandOrCollapseSelectedItem();
                }
            }
        }

        
        private void ExpandOrCollapseSelectedItem()
        {
            if(PreviousPostSelected != null)
            {
                 Posts.Where(t => t.Id == PreviousPostSelected.Id).FirstOrDefault().IsVisible =false;
            }

            Posts.Where(t => t.Id == Postselected.Id).FirstOrDefault().IsVisible = true;
            PreviousPostSelected = Postselected;
        }



        public  HistoryVM()
        {
            Posts = new ObservableCollection<Post>();
            
        }

        public async Task<bool> UpdatePosts()
        {
            try
            {
               
                var posts = await Post.GetPosts();
               
                if (posts != null)
                {
                    Posts.Clear();
                    foreach(var post in posts)
                   
                    Posts.Add(post);
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                return false;
            }
            
        }
       
        public async Task<bool> MyPosts()
        {
            try
            {
                var posts = await Post.GetPostByUserId(App.user.Id);
                if (posts != null)
                {
                    Posts.Clear();
                    foreach (var post in posts)
                        Posts.Add(post);
                }
                return true;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                return false;
            }
        }

        public async void DeletePost(Post postToDelete)
        {
            await Post.Delete(postToDelete);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
