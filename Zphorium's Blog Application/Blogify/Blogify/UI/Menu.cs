using System;
using System.Collections.Generic;
using System.Text;

namespace Blogify.MenuSystem
{
     class Menu
    {
        string prompt;
        List<string> options;
        int selectedIndex;
        string prefix;

        public Menu(string Prompt, List<string> Options)
        {
            this.prompt = Prompt;
            this.options = Options;
            selectedIndex = 0;
        }

        private void DisplayInfo()
        {
            //Console Color
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Title = "Blogify";

            Console.WriteLine(prompt);
            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                {

                    prefix = "*";
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {

                    prefix = " ";
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.SetCursorPosition((Console.WindowWidth - "{prefix} << {options[i]} >>".Length) / 2, Console.CursorTop + 1);
                Console.WriteLine($"{prefix} << {options[i]} >>");
            }
            Console.ResetColor();
        }

        public int Run()
        {
            Console.CursorVisible = false;

            ConsoleKey keyPressed;
            do
            {
                Console.Clear();

                DisplayInfo();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex == -1)
                    {
                        selectedIndex = options.Count - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex > options.Count - 1)
                    {
                        selectedIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            return selectedIndex;
        }
    }
}
