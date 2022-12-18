// See https://aka.ms/new-console-template for more information
using EAGetMail;
using EmailManager;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

Console.WriteLine("Hello, World!");
Email();


static void Email()
{
    Dictionary<string, string> unsubscribeLinks = new Dictionary<string, string>();
    try
    {
        string email = string.Empty;
        string password = string.Empty;
        // Create app password in Google account
        // https://support.google.com/accounts/answer/185833?hl=en
        // Gmail IMAP4 server is "imap.gmail.com"
        MailServer oServer = new MailServer("imap.gmail.com",
                        email,
                        password,
                        ServerProtocol.Imap4);

        // Enable SSL connection.
        oServer.SSLConnection = true;

        // Set 993 SSL port
        oServer.Port = 993;

        MailClient oClient = new MailClient("TryIt");
        oClient.Connect(oServer);

        MailInfo[] infos = oClient.GetMailInfos();
        Console.WriteLine("Total {0} email(s)\r\n", infos.Length);
        //from most recent email to last ones. Because oldest email have expaired/old links.
        for (int i = infos.Length-1; i >= 0; i--)
        {
            MailInfo info = infos[i];
            // Receive email from IMAP4 server
            Mail oMail = oClient.GetMail(info);

            var supplier = oMail.From.Address;
            //start count
            string link = string.Empty;

            if (unsubscribeLinks.TryGetValue(supplier, out link))
            {
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
            }
            unsubscribeLinks[supplier] = link == null ? string.Empty : link;


            // Mark email as deleted from IMAP4 server.
            //oClient.Delete(info);
        }
        WriteDownTheList(unsubscribeLinks);

        // Quit and expunge emails marked as deleted from IMAP4 server.
        oClient.Quit();
        Console.WriteLine("Completed!");
    }
    catch (Exception ep)
    {
        Console.WriteLine(ep.Message);
    }
}

static void WriteDownTheList(Dictionary<string, string> emailsFromReceipts)
{
    var filePath = "C:\\Users\\Custom\\Desktop\\NewExcels\\emailsFromReceipts.txt";
    //before your loop
    var csv = new StringBuilder();
    foreach (var sender in emailsFromReceipts)
    {
        //in your loop
        var first = sender.Key;
        var second = sender.Value.ToString();
        //Suggestion made by KyleMit
        var newLine = string.Format("{0},{1}", first, second);
        csv.AppendLine(newLine);
    }
    

    //after your loop
    File.WriteAllText(filePath, csv.ToString());
}

static async void Unsubscribe(string unsubscribeLink)
{
    var webReader = new WebReader();
    var success = await webReader.ReadLink(unsubscribeLink);
    if (success)
    {
        Console.WriteLine("Success {0}", unsubscribeLink);
    }
    else
    {
        Console.WriteLine("Failed {0}", unsubscribeLink);
    }
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
//method for stripping link html string

//textOperations
//emailOperations
//webReader
//UI 

