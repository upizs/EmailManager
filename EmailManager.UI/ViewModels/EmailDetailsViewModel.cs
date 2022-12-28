using EmailManager.Operations;
using EmailManager.Operations.Models;
using EmailManager.UI.Base;
using EmailManager.UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EmailManager.UI.ViewModels
{
    public class EmailDetailsViewModel : BindableBase
    {
        private EmailOperations _emailOperations;
        public EmailDetailsViewModel(EmailOperations emailOperations)
        {
            _emailOperations = emailOperations;
            LoadEmailDetailsCommand = new RelayCommand(LoadEmailContacts, CanLoadContacts);
        }

        private int _numberOfContacts;
        public int NumberOfContacts
        {
            get { return _numberOfContacts; }
            set 
            { 
                SetProperty(ref _numberOfContacts, value);
                RaiseCanExecuteChanged();
            }
        }

        public RelayCommand LoadEmailDetailsCommand { get; private set; }

        private ObservableCollection<EmailContactModel> _emailContacts;
        public ObservableCollection<EmailContactModel> EmailContacts
        {
            get { return _emailContacts; }
            set { SetProperty(ref _emailContacts, value); }
        }

        private bool CanLoadContacts()
        {
            return NumberOfContacts > 0;
        }

        public async void LoadEmailContacts()
        {
            var contacts = await _emailOperations.GetAllEmailDetails(_numberOfContacts);
            EmailContacts = new ObservableCollection<EmailContactModel>();
            foreach (var contact in contacts)
            {
                EmailContacts.Add(new EmailContactModel
                {
                    EmailAddress = contact.EmailAddress,
                    UnsubscribeLink = contact.UnsubscribeLink,
                    DeleteAllEmails = contact.DeleteAllEmails
                });
            }
        }

        private void RaiseCanExecuteChanged()
        {
            LoadEmailDetailsCommand.RaiseCanExecuteChanged();
        }
    }
}
