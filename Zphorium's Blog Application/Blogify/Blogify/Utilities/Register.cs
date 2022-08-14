using System;
using System.Collections.Generic;
using System.Text;

namespace Blogify.Utilities
{
    static class Register
    {

        //Combining 2 private methods to return the User input as List.
        internal static List<string> RegisterProcess(string registrationPrompt, string promptUserEmail, string promptUserName, string promptUserSurname, string promptPWD)
        {
            CenterTXTL(registrationPrompt);
            string usrEmail = Register_Email_CHECKER(promptUserEmail);
            string usrName = Register_USR_CHECKER(promptUserName);
            string usrSurname = Register_USRS_CHECKER(promptUserSurname);
            string pwd = Register_PWD_CHECKER(promptPWD);

            List<string> Log_Info = new List<string>() { usrEmail , pwd ,usrName, usrSurname };

            return Log_Info;

        }


        //Gathering Username
        private static string Register_USR_CHECKER(string promptUserName)
        {
            string userName = "";

            CenterTXT(promptUserName);

            userName = Console.ReadLine();

            return userName;

        }

        //Gathering Username
        private static string Register_USRS_CHECKER(string promptUserSurnamee)
        {
            string userSurname = "";

            CenterTXT(promptUserSurnamee);

            userSurname = Console.ReadLine();

            return userSurname;

        }

        //Gathering Username
        private static string Register_Email_CHECKER(string promptUserEmail)
        {
            string userEmail = "";

            CenterTXT(promptUserEmail);

            userEmail = Console.ReadLine();

            return userEmail;

        }

        //Gathering pwd in a secure format.
        private static string Register_PWD_CHECKER(string promptPWD)
        {

            CenterTXT(promptPWD);
            string pwd = "";
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Backspace)
                {
                    pwd += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (keyInfo.Key == ConsoleKey.Backspace && pwd.Length > 0)
                    {
                        pwd = pwd.Substring(0, (pwd.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Console.Write("");
                        break;
                    }


                }


            } while (true);

            return pwd;
        }


        //Centering Text Content -- WriteLine
        public static void CenterTXTL(string s)
        {
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop + 1);
            Console.WriteLine(s);
        }

        //Centering Text Content -- Write
        public static void CenterTXT(string s)
        {
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop + 1);
            Console.Write(s);
        }
    }
}
