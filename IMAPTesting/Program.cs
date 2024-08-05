using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using System;
using System.Text.RegularExpressions;
using MailKit.Search;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Reflection;
using System.Configuration;
using System.Net.Mail;
using IMAPTesting.Helper;

namespace IMAPTesting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userEmail = "";
            var appPassword = "";
            var gmailImapServer = "";
            int imapPort = 0;

            ConfigurationHelper.LoadConfiguration(out userEmail, out appPassword, out gmailImapServer, out imapPort);
            
            Console.Write("Enter Sender's EmailId : ");
            string? sendersEmail = Console.ReadLine();

            if (InputValidation.IsEmailValid(sendersEmail))
            {
                ConsoleHelper.DisplayOptions();
                int selectedOptionNumber = InputValidation.GetInputInt(5);

                ImapClient client = new ImapClient();
                try
                {
                    ImapServices.CreateImapClient(ref client, userEmail, appPassword, sendersEmail, selectedOptionNumber, gmailImapServer, imapPort);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    client.Dispose();
                }
            }
            else
            {
                Console.WriteLine("Email Not Valid");
            }

            Console.ReadLine();
        }
    }
}
