﻿using EAGetMail;
using EmailManager;
using EmailManager.Operations.Models;
using System;
using System.Text;

namespace EmailManager.Operations
{
    public class EmailOperations
    {
        private MailServer? _mailServer;
        private MailClient? _mailClient;
        private List<EmailContact> _contactsWithDetails = new List<EmailContact>();

        public bool Login(string email, string password)
        {
            _mailServer = new MailServer("imap.gmail.com",
                        email,
                        password,
                        ServerProtocol.Imap4);
            // Enable SSL connection.
            _mailServer.SSLConnection = true;

            // Set 993 SSL port
            _mailServer.Port = 993;
            MailClient _mailClient = new MailClient("TryIt");
            _mailClient.Connect(_mailServer);

            return _mailClient.Connected;
        }

        public List<EmailContact> GetAllEmailDetails()
        {
            MailInfo[] infos = _mailClient.GetMailInfos();
            for (int i = infos.Length - 1; i >= 0; i--)
            {
                MailInfo info = infos[i];
                // Receive email from IMAP4 server
                Mail oMail = _mailClient.GetMail(info);

                var emailAddress = oMail.From.Address;
                //start count
                string link = string.Empty;
                //if new contact, add to list
                bool addToList = true;

                var emailContact = _contactsWithDetails.Where(x => x.EmailAddress == emailAddress)
                                                        .FirstOrDefault();
                if (emailContact is not null)
                {
                    link = emailContact.UnsubscribeLink;
                    addToList = false;
                }
                else
                {
                    emailContact = new EmailContact
                    {
                        EmailAddress = emailAddress
                    };
                }
                //if link is empty, we dont need to look for it.
                if (!string.IsNullOrEmpty(link))
                {
                    continue;
                }
                else
                {
                    var body = oMail.HtmlBody;
                    if (body.Contains("Unsubscribe"))
                    {
                        link = GetUnsubscribeLink(body);
                    }
                }
                emailContact.UnsubscribeLink = link == null ? string.Empty : link;
                if (addToList)
                {
                    _contactsWithDetails.Add(emailContact);
                }

            }
            return _contactsWithDetails;
        }

        static string GetUnsubscribeLink(string body)
        {
            string unsubscribeLink = string.Empty;
            var index = body.IndexOf("Unsubscribe");
            var partBeforeUnsubscribe = body.Substring(0, index);
            index = partBeforeUnsubscribe.LastIndexOf("<a");
            unsubscribeLink = body.Substring(index);
            unsubscribeLink = CleanLink(unsubscribeLink);

            return unsubscribeLink;
        }

        static string CleanLink(string link)
        {
            int index = link.IndexOf("http");
            link = link.Substring(index);
            index = link.IndexOf("\"");
            link = link.Substring(0, index);
            return link;
        }

    }
}