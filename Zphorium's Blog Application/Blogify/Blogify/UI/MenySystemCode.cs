using Blogify.Models;
using Blogify.Repository.Services;
using Blogify.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;

namespace Blogify.MenuSystem
{
    class MenySystemHandler
    {

        //logged in user
        static User loggedInUser;

        //DateTime Culture
        public static CultureInfo cultInfo = CultureInfo.InvariantCulture;

        //Creating Service Instances
        static UserPlace userService = new UserPlace();
        static BlogPlace blogService = new BlogPlace();




        //Static Counter for Login Validation
        internal static byte log_counter = 0;
        public void Start()
        {
            //Setting up Console environment
            Console.SetWindowSize(200, 40);
            Console.Title = "Zphorium's Blog Application / Most Toxic Community";

            //Animations Starting
            AnimationIntro();

            DashBoardMENU();
        }



        //DashBoard Menu
        private static void DashBoardMENU()
        {
            Menu dashboardMenu;
            Console.Clear();
            string prompt = @"

                                                               ______                         ____                                        _____     ____        
                                                |         |  .~      ~.        .'. .`.       |            |`````````,       .'.        .-~     ~.  |            
                                                |_________| |          |     .'   `   `.     |______      |'''''''''      .''```.     :            |______      
                                                |         | |          |   .'           `.   |            |             .'       `.   :     _____  |            
                                                |         |  `.______.'  .'               `. |___________ |           .'           `.  `-._____.'| |___________ 
                                                                                                                                                             ";

            if (loggedInUser != null)
            {
                if (loggedInUser.role.name == "User")
                {
                    dashboardMenu = new Menu(prompt, new List<string>() { "Signout", "Inbox", "Blogs", "Add Comment", "Add Blog", "Delete Blog", "Show-Blogs-With-Comments", "Show-Filtered-Blogs-With-Comments", "Find-Blog-By-Code", "Exit" });

                }
                else
                {
                    dashboardMenu = new Menu(prompt, new List<string>() { "Signout", "Show Users", "Show Admins", "Show Auditing Blogs", "Approve Blog", "Reject Blog", "Show-Blogs-With-Comments", "Show-Filtered-Blogs-With-Comments", "Find-Blog-By-Code", "Exit" });
                }
            }
            else
            {
                dashboardMenu = new Menu(prompt, new List<string>() { "Register", "Login", "Show-Blogs-With-Comments", "Show-Filtered-Blogs-With-Comments", "Find-Blog-By-Code", "Exit" });
            }

            int selectedIndex = dashboardMenu.Run();

            if (loggedInUser == null)
            {
                switch (selectedIndex)
                {
                    case 0:
                        RegisterMenu();
                        break;
                    case 1:
                        LoginMenu();
                        break;
                    case 2:
                        ShowBlogsWithComments();
                        break;
                    case 3:
                        ShowFilteredBlogsWithComments();
                        break;
                    case 4:
                        FindBlogByCode();
                        break;
                    case 5:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        CenterTXTL("Program Terminated");
                        Console.ReadKey(true);
                        break;
                }
            }
            else
            {
                CenterTXTL(loggedInUser.role.name);
                if (loggedInUser.role.name == "User")
                {
                    switch (selectedIndex)
                    {
                        case 0:
                            SignOut();
                            break;
                        case 1:
                            InboxMenu();
                            break;
                        case 2:
                            BlogsMenu();
                            break;
                        case 3:
                            AddCommentMenu();
                            break;
                        case 4:
                            AddBlogMenu();
                            break;
                        case 5:
                            DeleteBlogMenu();
                            break;
                        case 6:
                            ShowBlogsWithComments();
                            break;
                        case 7:
                            ShowFilteredBlogsWithComments();
                            break;
                        case 8:
                            FindBlogByCode();
                            break;
                        case 9:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            CenterTXTL("Program Terminated");
                            Console.ReadKey(true);
                            break;
                    }
                }
                else
                {
                    switch (selectedIndex)
                    {
                        case 0:
                            SignOut();
                            break;
                        case 1:
                            ShowUsers();
                            break;
                        case 2:
                            ShowAdmins();
                            break;
                        case 3:
                            ShowAuditingBlogs();
                            break;
                        case 4:
                            ApproveBlog();
                            break;
                        case 5:
                            RejectBlog();
                            break;
                        case 6:
                            ShowBlogsWithComments();
                            break;
                        case 7:
                            ShowFilteredBlogsWithComments();
                            break;
                        case 8:
                            FindBlogByCode();
                            break;
                        case 9:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            CenterTXTL("Program Terminated");
                            Console.ReadKey(true);
                            break;
                    }
                }
            }


        }

        //Sign out
        public static void SignOut()
        {
            Console.Clear();
            CenterTXTL("You have been signed out");
            loggedInUser = null;
            DashBoardMENU();
        }

        //Register Menu
        public static void RegisterMenu()
        {
            Console.Clear();
            CenterTXTL("Welcome to registiration ");
            RunRegistration();
        }

        //Registration Procedure Handler Method
        private static void RunRegistration(string registrationPrompt = "Press enter to Register", string promptUserEmail = "Email: ", string promptUserName = "Name: ", string promptUserSurname = "Surname: ", string promptPWD = "Password: ")
        {
            List<string> Reg_Info = Register.RegisterProcess(registrationPrompt, promptUserEmail, promptUserName, promptUserSurname, promptPWD);

            foreach (string log in Reg_Info)
            {
                if (log.Trim() == "")
                {
                    CenterTXTL("Cant be empty");
                    CenterTXTL("Press any key to start again");
                    Console.ReadLine();
                    RunRegistration();
                }
            }

            dynamic result = userService.RegistrationValidation(Reg_Info);

            if (result.status)
            {
                CenterTXTL("You have been successfully registered!");
                User newUser = new User(Reg_Info[2], Reg_Info[3], Reg_Info[0], Reg_Info[1], new Role("User"));
                userService.AddUser(newUser);
                Console.ReadLine();
                DashBoardMENU();
            }
            else
            {
                CenterTXTL(result.message);
                CenterTXTL("Press any key to navigate back to menu");
                Console.ReadLine();
                DashBoardMENU();
            }
        }
        public static void LoginMenu()
        {
            Console.Clear();
            CenterTXTL("Welcome to login ");
            RunLogin();
        }

        //Login Procedure Handler method
        private static void RunLogin(string loginPrompt = "Press Enter to Login", string promptUserEmail = "Email: ", string promptPWD = "Password: ")
        {
            List<string> Log_Info = Login.LoginProcess(loginPrompt, promptUserEmail, promptPWD);
            foreach (string log in Log_Info)
            {
                if (log.Trim() == "")
                {
                    CenterTXTL("Cant be empty");
                    CenterTXTL("Press any key to start again");
                    Console.ReadLine();
                    Console.Clear();
                    RunLogin();
                }
            }
            dynamic result = (User)userService.VALIDATION(Log_Info);
            if (result != null)
            {
                CenterTXTL("You are logged in ! Navigating to menu");
                Thread.Sleep(1000);
                loggedInUser = result;
                Console.Clear();
                DashBoardMENU();
            }
            else
            {
                log_counter++;
                if (log_counter < 3)
                {
                    Console.Clear();
                    RunLogin("Failed Login, Try Again", "Re-enter Username: ", "Re-enter Password: ");
                }
                else
                {

                    Console.Clear();
                    CenterTXTL("Program Terminated");
                    CenterTXTL("Press any key to close app");
                    Console.ReadKey(true);

                }

            }


        }


        //Blogs with Comments
        public static void ShowBlogsWithComments()
        {
            Console.Clear();
            List<Blog> blogsList = blogService.GetALL();
            CenterTXTL("Welcome to ShowBlogsWithComments ");
            foreach (Blog blog in blogsList)
            {
                CenterTXTL($"[{blog.blogCode}] - [{blog.user.name} {blog.user.surname}] - [{blog.publisDate.ToString("dd.MM.yyyy")}] --[{blog.title}] -- [{blog.content}]");
                CenterTXTL("");
                foreach (Comment comment in blog.comments)
                {
                    CenterTXTL($"[{comment.publishDate.ToString("dd.MM.yyyy")}] -- [{comment.user.name} {comment.user.surname}] -- [{comment.content}]");
                }
            }
            CenterTXTL("Press any key to go back menu");
            Console.ReadLine();
            DashBoardMENU();
        }

        //Filter Blogs
        public static void ShowFilteredBlogsWithComments()
        {
            Console.Clear();
            List<Blog> blogList = blogService.GetALL();
            CenterTXTL("Welcome to ShowFilteredBlogsWithComments ");
            CenterTXTL("Enter 1 to filter by title, Enter 2 to filter by Author Firstname");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    CenterTXTL("Enter Title: ");
                    string title = Console.ReadLine();
                    Blog blog = blogList.Find(el => el.title == title);
                    if(blog == null)
                    {
                        CenterTXTL("No such blog!");
                        CenterTXTL("Press any key to navigate back to menu");
                        Console.ReadLine();
                        DashBoardMENU();
                    }
                    CenterTXTL($"[{blog.blogCode}] -- [{blog.publisDate.ToString("dd.MM.yyyy")}] -[{blog.user.name} {blog.user.surname}]- [{blog.title}] -- [{blog.content}]");
                    if(blog.comments.Count > 0)
                    {
                        foreach(Comment comment in blog.comments)
                        {
                            CenterTXTL($"[{comment.publishDate.ToString("dd.MM.yyyy")}] -- [{comment.user.name} {comment.user.surname}] -- [{comment.content}]");
                        }
                    }
                    else
                    {
                        CenterTXTL("No Comments !");
                    }
                    break;
                case "2":
                    Console.Clear();
                    CenterTXTL("Enter Firsname: ");
                    string firstname = Console.ReadLine();
                    Blog result_blog = blogList.Find(el => el.title == firstname);
                    if (result_blog == null)
                    {
                        CenterTXTL("No such blog!");
                        CenterTXTL("Press any key to navigate back to menu");
                        Console.ReadLine();
                        DashBoardMENU();
                    }
                    CenterTXTL($"[{result_blog.blogCode}] -- [{result_blog.publisDate.ToString("dd.MM.yyyy")}] -- [{result_blog.title}] -- [{result_blog.content}]");
                    if (result_blog.comments.Count > 0)
                    {
                        foreach (Comment comment in result_blog.comments)
                        {
                            CenterTXTL($"[{comment.publishDate.ToString("dd.MM.yyyy")}] -- [{comment.user.name} {comment.user.surname}] -- [{comment.content}]");
                        }
                    }
                    else
                    {
                        CenterTXTL("No Comments !");
                    }
                    break;
                default:
                    ShowFilteredBlogsWithComments();
                    break;
            }
            CenterTXTL("Press any key to go back menu");
            Console.ReadLine();
            DashBoardMENU();
        }


        //Find by code
        public static void FindBlogByCode()
        {
            Console.Clear();
            List<Blog> blogsList = blogService.GetALL();
            CenterTXT("Enter Blog Code: ");
            string blogCode = Console.ReadLine();
            Blog blog = blogsList.Find(el => el.blogCode == blogCode);
            if (blog == null)
            {
                CenterTXTL("No result!");
                CenterTXTL("Press any key to go menu");
                Console.ReadLine();
                DashBoardMENU();
            }
            CenterTXTL($"[{blog.blogCode}] -- [{blog.publisDate.ToString("dd.MM.yyyy")}] -- [{blog.title}] -- [{blog.content}]");
            CenterTXTL("Press any key to go back to menu");
            Console.ReadLine();
            DashBoardMENU();
        }

        //Show all users
        public static void ShowUsers()
        {
            Console.Clear();
            CenterTXTL("Users");
            List<User> usersList = userService.GetALL();
            foreach (User user in usersList)
            {
                if (user.role.name == "User")
                {
                    CenterTXTL($"[{user.email}] |=> [{user.name}]|[{user.surname}] |=> [{user.role.name}]");
                }
            }
            Console.ReadLine();
            DashBoardMENU();
        }

        //Show all admins
        public static void ShowAdmins()
        {
            Console.Clear();
            CenterTXTL("Admins");
            List<User> usersList = userService.GetALL();
            foreach (User user in usersList)
            {
                if (user.role.name == "Admin")
                {
                    CenterTXTL($"[{user.email}] |=> [{user.name}]|[{user.surname}] |=> [{user.role.name}]");
                }
            }
            Console.ReadLine();
            DashBoardMENU();
        }

        //Show Auditing Blogs
        public static void ShowAuditingBlogs()
        {
            Console.Clear();
            CenterTXTL("Showing Auditing Blogs");
            List<Blog> blogsList = blogService.GetALL();
            foreach (Blog blog in blogsList)
            {
                if (!blog.verified)
                {
                    CenterTXTL($"[{blog.blogCode}]--[{blog.publisDate.ToString("dd.MM.yyyy")}] -- [{blog.title}] -- [{blog.content}]");
                }
            }
            Console.ReadLine();
            DashBoardMENU();
        }

        //Approve Blog
        public static void ApproveBlog()
        {
            Console.Clear();
            List<Blog> blogsList = blogService.GetALL();
            CenterTXTL("Approve Blog by Code");
            CenterTXT("Enter Blog Code for approval: ");
            string blogCode = Console.ReadLine();
            Blog blog = blogsList.Find(el => el.blogCode == blogCode);
            if (blog == null)
            {
                CenterTXTL("Incorrect blog code !");
                CenterTXTL("Pressa any key to go back to menu");
                Console.ReadLine();
                DashBoardMENU();
            }
            Blog updatedBlog = new Blog(blog.title, blog.content, blog.publisDate, true, blog.user);
            User blogUser = blog.user;

            User updatedBlogUser = new User(blogUser.name, blogUser.surname,blogUser.email , blogUser.password, blogUser.role);
            List<Notification> updatedNotifications = new List<Notification>() { 
            new Notification("Approved", blog)
            };
            if(blogUser.inbox.notifications.Count > 0)
            {
                foreach (Notification _notification in blogUser.inbox.notifications)
                {
                    updatedNotifications.Add(_notification);
                }
            }
            updatedBlogUser.inbox = new Inbox(Guid.NewGuid(),updatedNotifications);

            userService.Update(blogUser, updatedBlogUser);
            blogService.Update(blog, updatedBlog);
            CenterTXTL("Press any key to return back to menu");
            Console.ReadLine();
            DashBoardMENU();
        }

        //Reject Blog
        public static void RejectBlog()
        {
            Console.Clear();
            List<Blog> blogsList = blogService.GetALL();
            CenterTXTL("Reject Blog by Code");
            CenterTXTL("Enter Blog Code for rejection");
            string blogCode = Console.ReadLine();
            Blog blog = blogsList.Find(el => el.blogCode == blogCode);
            if (blog == null)
            {
                CenterTXTL("Incorrect blog code !");
                CenterTXTL("Pressa any key to go back to menu");
                Console.ReadLine();
                DashBoardMENU();
            }
            Blog updatedBlog = new Blog(blog.title, blog.content, blog.publisDate, false, blog.user);
            User blogUser = blog.user;

            User updatedBlogUser = new User(blogUser.name, blogUser.surname, blogUser.email, blogUser.password, blogUser.role);
            List<Notification> updatedNotifications = new List<Notification>() {
            new Notification("Rejected", blog)
            };
            if (blogUser.inbox.notifications.Count > 0)
            {
                foreach (Notification _notification in blogUser.inbox.notifications)
                {
                    updatedNotifications.Add(_notification);
                }
            }
            updatedBlogUser.inbox = new Inbox(Guid.NewGuid(), updatedNotifications);

            userService.Update(blogUser, updatedBlogUser);
            blogService.Update(blog, updatedBlog);
            CenterTXTL("Press any key to return back to menu");
            Console.ReadLine();
            DashBoardMENU();
        }

        //Inbox
        public static void InboxMenu()
        {
            Console.Clear();
            CenterTXTL("Inbox");
            if (loggedInUser.inbox.notifications.Count > 0)
            {
                foreach (Notification notification in loggedInUser.inbox.notifications)
                {
                    CenterTXTL($"[{notification.content}] - [{notification.blog.title}]");
                }
            }
            else
            {
                CenterTXTL("You dont have any notifications");
            }
            CenterTXTL("Press any key to navigate back to menu");
            Console.ReadLine();
            DashBoardMENU();
        }

        //Blogs
        public static void BlogsMenu()
        {
            Console.Clear();
            CenterTXTL("Blogs");
            List<Blog> blogsList = blogService.GetALL();
            foreach (Blog blog in blogsList)
            {
                if (blog.user.id == loggedInUser.id)
                {
                    CenterTXTL($"[{blog.publisDate}]--[{blog.blogCode}] -- [{blog.title}] -- [{blog.content}] -- [{blog.verified}]");
                }
            }
            Console.ReadLine();
            DashBoardMENU();
        }

        //Add Comment
        public static void AddCommentMenu()
        {
            Console.Clear();
            List<Blog> blogsList = blogService.GetALL();
            CenterTXTL("Add Comment");
            CenterTXT("Enter Blog Code: ");
            string blogCode = Console.ReadLine();
            Blog blog = blogsList.Find(el => el.blogCode == blogCode);

            if (blog == null)
            {
                Console.Clear();
                CenterTXTL("No such blog found !");
                CenterTXTL("navigagte back to menu");
                DashBoardMENU();
                   
            }
            CenterTXT("Enter your Comment: ");
            string comment = Console.ReadLine();
            if (comment.Length < 10 || comment.Length > 35)
            {
                CenterTXTL("Comment length is incorrect");
                CenterTXTL("Press any key to start again");
                Console.ReadLine();
                AddCommentMenu();
            }
            Blog updatedBlog = new Blog(blog.title, blog.content, blog.publisDate, blog.verified, blog.user);
            updatedBlog.comments.Add(new Comment(loggedInUser, DateTime.Now, comment));
            blogService.Update(blog, updatedBlog);

            User blogUser = blog.user;

            User updatedBlogUser = new User(blogUser.name, blogUser.surname, blogUser.email, blogUser.password, blogUser.role);
            List<Notification> updatedNotifications = new List<Notification>() {
            new Notification("New Comment", blog)
            };
            if (blogUser.inbox.notifications.Count > 0)
            {
                foreach (Notification _notification in blogUser.inbox.notifications)
                {
                    updatedNotifications.Add(_notification);
                }
            }
            updatedBlogUser.inbox = new Inbox(Guid.NewGuid(), updatedNotifications);

            userService.Update(blogUser, updatedBlogUser);
            CenterTXTL("Comment added !");
            CenterTXTL("Press any key to go back to menu");
            Console.ReadLine();
            DashBoardMENU();
        }
        //Add Blog
        public static void AddBlogMenu()
        {
            Console.Clear();
            CenterTXTL("Add Blog");

            CenterTXT("Blog title: ");
            string blogTitle = Console.ReadLine();
            if (blogTitle.Length < 10 || blogTitle.Length > 35)
            {
                CenterTXTL("Blog title length incorrect! ");
                Console.Clear();
                AddBlogMenu();
            }
            CenterTXT("Blog Content: ");
            string blogContent = Console.ReadLine();
            if (blogContent.Length < 20 || blogContent.Length > 45)
            {
                CenterTXTL("Blog content length incorrect! ");
                Console.Clear();
                AddBlogMenu();
            }


            Blog newBlog = new Blog(blogTitle, blogContent, DateTime.Now, false, loggedInUser);

            blogService.CREATE(newBlog);
            Console.Clear();
            CenterTXTL("Blog Created Successfully");
            CenterTXT("Press any key to go back menu");
            Console.ReadLine();
            DashBoardMENU();
        }
        //Delete Blog
        public static void DeleteBlogMenu()
        {
            Console.Clear();
            List<Blog> blogsList = blogService.GetALL();
            CenterTXTL("Delete Blog");
            CenterTXTL("Enter Blog Code: ");
            string blogCode = Console.ReadLine();
            Blog blog = blogsList.Find(text => text.blogCode == blogCode && text.user.id == loggedInUser.id);
            if (blog == null)
            {
                Console.Clear();
                CenterTXTL("No such blog found !");
            }
            blogService.DELETE(blog);

            CenterTXTL("Blog successfully deleted !");

            Console.ReadLine();
            DashBoardMENU();
        }
        //Blogify Introduction Animation
        private static void AnimationIntro()
        {
            Console.CursorVisible = false;

            ConsoleKeyInfo keyAvail;
            string s = @"Welcome to ~~Blogify~~
most toxic community you can find on internet 3:)";

            for (int i = 0; i < s.Length; i++)
            {
                Console.Write(s[i]);
                Thread.Sleep(50);
                if (Console.KeyAvailable)
                {
                    keyAvail = Console.ReadKey(true);
                    Console.Write(s.Substring(i));
                    break;
                }
            }
            Console.CursorVisible = true;
            Console.WriteLine("\n\nPress Any Key to Login");
            Console.ReadKey(true);
            Console.Clear();
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
