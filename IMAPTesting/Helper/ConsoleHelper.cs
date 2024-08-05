using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IMAPTesting.Helper
{
    public class ConsoleHelper
    {
        public static string DisplayStartLineString = "\n\n----------------------------------------Start-------------------------------------------------\n\n";
        public static string DisplayEndLineString = "\n\n----------------------------------------End-------------------------------------------------\n\n";
        public static string DisplayFetchingString = "\n\n----------------------------------------- Fetching.... -------------------------------------------\n\n";
        public static void DisplayOptions()
        {
            Console.WriteLine("Select From the Following Option");
            Console.WriteLine("[1]. Read 5 Emails");
            Console.WriteLine("[2]. Read 10 Emails");
            Console.WriteLine("[3]. Read 20 Emails");
            Console.WriteLine("[4]. Read All Emails");
            Console.WriteLine("[5]. Custom");
        }

        public static void DisplayOutputEmails(ImapClient client, IList<UniqueId> uniqueIdentifiers, int totalEmailsToRead)
        {

            for (int i = 0; i < totalEmailsToRead; i++)
            {
                var message = client.Inbox.GetMessage(uniqueIdentifiers[i]);

                if (string.IsNullOrEmpty(message.TextBody) || string.IsNullOrEmpty(message.Subject))
                {
                    continue;
                }

                Console.WriteLine("\n" + DisplayStartLineString);

                Console.WriteLine($"Subject: {message.Subject}\n");

                Console.WriteLine($"Body: {CleanUpBodyOfOutputEmail(message, message.TextBody)}\n");

                Console.WriteLine(DisplayEndLineString);
            }
        }

        public static string CleanUpBodyOfOutputEmail(MimeMessage message, string body)
        {
            if (string.IsNullOrEmpty(body))
                return string.Empty;

            // Remove HTML tags
            if (message.Body.ContentType.MimeType.Equals("text/html"))
            {
                body = Regex.Replace(body, "<.*?>", string.Empty);
            }

            // Remove URLs
            body = Regex.Replace(body, @"http[^\s]+", string.Empty);

            // Replace multiple spaces with a single space
            body = Regex.Replace(body, @"\s{2,}", " ");

            // Remove any non-printable characters
            body = Regex.Replace(body, @"[^\x20-\x7E]", string.Empty);

            // Trim leading and trailing whitespace
            body = body.Trim();

            return body;
        }
    }
}
