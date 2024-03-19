using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Model;
using WpfApp1.Repositories;

namespace WpfApp1.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        private IUserRepository userRepository;

        //Properties
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

        public ViewModelBase CurrentChildView 
        { 
            get 
            {
                return _currentChildView;
            }
            
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
            
        }
        public string Caption  
        { 
            get
            {  
                return _caption; 
            }
            
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
                
        }
        public IconChar Icon 
        {
            get
            { 
                return _icon; 
            }
            set
            { 
                _icon = value; 
                OnPropertyChanged(nameof(Icon));
            }
        }

        //--> Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);

            //Default View
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowCustomerViewCommand(object obj)
        {
            CurrentChildView = new CustomerViewModel();
            Caption = "Customers";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
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
