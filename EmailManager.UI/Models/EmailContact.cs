
using EmailManager.UI.Base;

namespace EmailManager.UI.Models
{
    public class EmailContactModel : BindableBase
    {
        private string _email;
        public string EmailAddress
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        private string _unsubscribeLink;
        public string UnsubscribeLink {
            get { return _unsubscribeLink; }
            set { SetProperty(ref _unsubscribeLink, value); }
        }
        private bool _deleteAllEmails;

        public bool DeleteAllEmails {
            get { return _deleteAllEmails; }
            set { SetProperty(ref _deleteAllEmails, value); }
        }
    }
}
