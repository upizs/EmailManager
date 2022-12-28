using EmailManager.Operations;
using EmailManager.UI.Base;
using System;
using System.Windows.Controls;

namespace EmailManager.UI.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private string _email;
        private string _password;
        private bool _loginSuccess;
        private string _error;
        private string _loginInstructions = "login here";

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

        public string LoginInstructions
        {
            get { return _loginInstructions; }
            set
            {
                SetProperty(ref _loginInstructions, value);
            }
        }

        public string Error 
        { 
            get { return _error; } 
            set { SetProperty(ref _error, value);}
        }

        public RelayCommand<PasswordBox> LoginCommand { get; private set; }

        public event Action<EmailOperations> EmailDetailsRequested = delegate { };

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
            string error  = string.Empty;
            var emailOperations = new EmailOperations();
            _loginSuccess = emailOperations.Login(_email,passwordBox.Password, out error);
            Error = error;
            if (_loginSuccess)
            {
                EmailDetailsRequested(emailOperations);
            }
        }

        //if either email or password is empthy dont allow to login
        public bool CanLogin(PasswordBox passwordBox)
        {
            return !string.IsNullOrEmpty(_email) 
                //&& !string.IsNullOrEmpty(passwordBox.Password)
                ;
        }


    }
}
