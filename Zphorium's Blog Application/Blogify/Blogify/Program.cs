using Blogify.MenuSystem;
using System;

namespace Blogify
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Creating Instance of MenuSysHandler and Calling Start Method to run the App
            MenySystemHandler menuStart = new MenySystemHandler();
            menuStart.Start();
        }
    }
}
