using EmailManager.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.UI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private BindableBase _currentViewModel;

        

        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }


        public MainWindowViewModel()
        {
            _currentViewModel = new LoginViewModel();
        }
    }
}
