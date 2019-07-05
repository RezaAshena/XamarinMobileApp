using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Tms_Travel.Model;

namespace Tms_Travel.ViewModel
{
    public  class UserVM
    {
        public ObservableCollection<User> Users { get; set; }
        public UserVM()
        {
            Users = new ObservableCollection<User>();
        }

        public async Task<bool> UpdateUsers()
        {
            try
            {
                var users = await User.GetUsers();
                if (users != null)
                {
                    Users.Clear();
                    foreach (var user in users)
                        Users.Add(user);
                }
                return true;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                return false;
            }
        }
    }
}
