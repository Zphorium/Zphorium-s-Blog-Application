using System;
using System.Collections.Generic;
using System.Text;

namespace Blogify.Utilities
{
    static class Login
    {

        //Combining 2 private methods to return the User input as List.
        internal static List<string> LoginProcess(string loginPrompt, string promptUserEmail, string promptPWD)
        {
            CenterTXTL(loginPrompt);
            string userEmail = Login_USR_CHECKER(promptUserEmail);
            string pwd = Login_PWD_CHECKER(promptPWD);

            List<string> Log_Info = new List<string>() { userEmail, pwd };

            return Log_Info;

        }


        //Gathering User email
        private static string Login_USR_CHECKER(string promptUserEmail)
        {
            string userEmail = "";

            CenterTXT(promptUserEmail);

            userEmail = Console.ReadLine();

            return userEmail;

        }



        //Gathering pwd in a secure format.
        private static string Login_PWD_CHECKER(string promptPWD)
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
