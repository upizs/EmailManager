using EmailManager.Operations;
using EmailManager.UI.Base;

namespace EmailManager.UI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private BindableBase _currentViewModel;
        private EmailDetailsViewModel _emailDetailsViewModel;
        private LoginViewModel _loginView = new LoginViewModel();
        

        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }


        public MainWindowViewModel()
        {
            _currentViewModel = _loginView;
            _loginView.EmailDetailsRequested += NavToEmailDetails;
        }

        private void NavToEmailDetails(EmailOperations emailOperations)
        {
            _emailDetailsViewModel = new EmailDetailsViewModel(emailOperations);
            CurrentViewModel = _emailDetailsViewModel;
        }
    }
}
