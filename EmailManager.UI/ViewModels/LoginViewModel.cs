using EmailManager.UI.Base;
using System.Windows.Controls;

namespace EmailManager.UI.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private string _email;
        private string _password;
        private bool _loginSuccess;

        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
                RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                RaiseCanExecuteChanged();
            }
        }

        public bool LoginSuccess
        {
            get { return _loginSuccess; }
            set
            {
                SetProperty(ref _loginSuccess, value);
            }
        }

        public RelayCommand<PasswordBox> LoginCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<PasswordBox>(OnLogin,CanLogin);
            _email = string.Empty;
            _password = string.Empty;
        }

        private void RaiseCanExecuteChanged()
        {
            LoginCommand.RaiseCanExecuteChanged();
        }

        public void OnLogin(PasswordBox passwordBox)
        {
            //Execute EmailOperations.Login(email,password);
            //set LoginSuccess
        }
        //if either email or password is empthy dont allow to login
        public bool CanLogin(PasswordBox passwordBox)
        {
            return !string.IsNullOrEmpty(_email) && !string.IsNullOrEmpty(passwordBox.Password);
        }


    }
}
