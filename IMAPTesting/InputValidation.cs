using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IMAPTesting
{
    public class InputValidation
    {
        public static int GetInputInt(int maxValidInputNumber)
        {
            var frequency = 3;
            int inputNumber = default(int);
            while (frequency > 0)
            {
                bool isParsed = int.TryParse(Console.ReadLine(), out inputNumber) && inputNumber > 0 && inputNumber <= maxValidInputNumber;
                if (isParsed) return inputNumber;
                else Console.WriteLine("** Invalid Input **");
            }
            return inputNumber;
        }

        public static bool IsEmailValid(string emailaddress)
        {
            bool valid = true;
            try
            {
                if (emailaddress == null || emailaddress == "")
                {
                    return false;
                }
                MailAddress m = new MailAddress(emailaddress);
                string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|uk|in|ai|tech)$";
                return Regex.IsMatch(emailaddress, regex, RegexOptions.IgnoreCase);
            }
            catch (FormatException ex)
            {
                valid = false;
                Console.WriteLine(ex.Message);
            }
            return valid;
        }
    }
}
