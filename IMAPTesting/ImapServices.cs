using MailKit.Search;
using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using IMAPTesting.Helper;

namespace IMAPTesting
{
    public class ImapServices
    {
        public static void CreateImapClient(ref ImapClient client, string userEmail, string appPassword, string sendersEmail, int selectedOptionNumber, string gmailImapServer, int imapPort)
        {   
            CreateClient(ref client, userEmail, appPassword, gmailImapServer, imapPort);
            ProcessEmails(client, sendersEmail, selectedOptionNumber);
        }

        private static void CreateClient(ref ImapClient client ,string userEmail, string appPassword, string gmailImapServer, int imapPort)
        {
            client.Connect(gmailImapServer, imapPort, true);

            client.Authenticate(userEmail, appPassword);

            client.Inbox.Open(FolderAccess.ReadOnly);
        }

        private static void ProcessEmails(ImapClient client, string sendersEmail, int selectedOptionNumber)
        {
            var searchQueryForSender = SearchQuery.FromContains(sendersEmail);
            var uniqueIdentifiers = client.Inbox.Search(searchQueryForSender);
            int maxEmailCount = uniqueIdentifiers.Count;
            int totalEmailsToRead = GetCountOfEmailsToRead(selectedOptionNumber, maxEmailCount);

            Console.WriteLine($"\nTotal Number of Emails received from this EmailID : {maxEmailCount}\n");

            uniqueIdentifiers = uniqueIdentifiers.Reverse().ToList();

            ConsoleHelper.DisplayOutputEmails(client, uniqueIdentifiers, totalEmailsToRead);
        }

        public static int GetCountOfEmailsToRead(int optionNumber, int maxEmailCount)
        {
            int emailsToRead = 0;
            switch (optionNumber)
            {
                case 1:
                    emailsToRead = 5;
                    break;
                case 2:
                    emailsToRead = 10;
                    break;
                case 3:
                    emailsToRead = 20;
                    break;
                case 4:
                    emailsToRead = maxEmailCount;
                    break;
                case 5:
                    Console.Write("Enter No: ");
                    emailsToRead = InputValidation.GetInputInt(int.MaxValue);
                    break;
                default:
                    emailsToRead = 0;
                    Console.WriteLine("invalid option number");
                    break;
            }
            if (emailsToRead > maxEmailCount)
            {
                Console.WriteLine($"\nThere are only {maxEmailCount} Emails from this sender\n");
                emailsToRead = maxEmailCount;
            }
            Console.WriteLine(ConsoleHelper.DisplayFetchingString);
            return emailsToRead;
        }
    }
}
