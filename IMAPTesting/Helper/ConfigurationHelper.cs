using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMAPTesting.Helper
{
    public class ConfigurationHelper
    {
        public static void LoadConfiguration(out string userEmail, out string appPassword, out string gmailImapServer, out int imapPort)
        {
            userEmail = "";
            appPassword = "";
            gmailImapServer = "";
            imapPort = 0;

            try
            {
                userEmail = ConfigurationManager.AppSettings["UserEmail"] ?? throw new ArgumentException("User Email not found!!");
                appPassword = ConfigurationManager.AppSettings["AppPassword"] ?? throw new ArgumentException("AppPassword not found!!");
                gmailImapServer = ConfigurationManager.AppSettings["GmailImapServer"] ?? throw new ArgumentException("GmailImapServer not found!!");
                if (!int.TryParse(ConfigurationManager.AppSettings["ImapPort"], out imapPort))
                {
                    throw new ArgumentNullException("ImapPort is invalid or not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
