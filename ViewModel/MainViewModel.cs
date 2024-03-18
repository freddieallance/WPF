using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Model;
using WpfApp1.Repositories;

namespace WpfApp1.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;

        public UserAccountModel CurrentUserAccount 
        { get
            {  
                return _currentUserAccount; 
            
            }
            
            set 
            { 
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            
            }
           }
        public MainViewModel()
        {
            userRepository = new UserRepository();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetUserModel(Thread.CurrentPrincipal.Identity.Name);
            if(user!=null)
            {
                CurrentUserAccount = new UserAccountModel()
                { 
                    Username=user.Username,
                    DisplayName=$"Welcome {user.Name} {user.LastName} ;)",
                    ProfilePicture=null
                };
            }
            else 
            {
                MessageBox.Show("Invalid user, not logged in");
                Application.Current.Shutdown(); 
            }
        }
    }
}
