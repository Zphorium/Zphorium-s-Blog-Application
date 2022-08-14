using Blogify.Models;
using Blogify.Repository.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Blogify.Repository.Services
{
    class UserPlace : IServiceProvider<User>
    {
        //Default Database for ADMINS
        public static List<User> users = new List<User>()
        {
           new User("Mahmood", "Garibov", "mahmoodgaribov@code.edu.az", "12345" ,new Role("Admin")),
           new User("Zulfuqar", "Mammadli", "zulfuqarr@code.edu.az", "12345" ,new Role("User")),
           new User("Ali", "Aliev", "alialiev@code.edu.az", "12345" ,new Role("User")),

        };

        public User CREATE(User t)
        {
            users.Add(t);
            return t;
        }

        public User DELETE(User t)
        {
            users.Remove(t);
            return t;
        }

        public User Get(Guid ID)
        {
            User user = users.Find(el => el.id == ID);
            if(user != null)
            {
                return user;
            }
            return null;

        }

        public List<User> GetALL()
        {
            return users;
        }

        public User Update(User t, User t1)
        {
            users.Remove(t);
            users.Add(t1);
            return t1;
        }

        public User AddUser(User t)
        {
            users.Add(t);
            return t;
        }
        //Validation Method being used for Login Procedure
        public object VALIDATION(List<string> Log_Info)
        {
            User result = users.Find(el => el.email == Log_Info[0] && el.password == Log_Info[1]);
            if (result != null)
            {
                return result;
            }
            return null;

        }

        //Validation Method being used for Registration Procedure

        public object RegistrationValidation(List<string> Log_Info)
        {
            Regex emailRegex = new Regex(@"^[A-Za-z0-9._%+-]+@code\.edu\.az$");
            Regex nameRegex = new Regex(@"^[A-Z]+[a-zA-Z]{3,30}$");
            Regex surnameRegex = new Regex(@"^[A-Z]+[a-zA-Z]{4,29}$");
            Regex passwordRegex = new Regex(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])");

            
            string useremail = Log_Info[0];
            string userpwd = Log_Info[1];
            string username = Log_Info[2];
            string usersurname = Log_Info[3];

            //If exists in db
            User result = users.Find(el => el.email == useremail);

            if(result != null)
            {
                return new { message = "Email exists!", status=false }; 
            }

            //Email Regex
            if (!emailRegex.IsMatch(useremail)) {
                return new { message = "Email format is wrong!", status = false };
            }

            //Name Regex
            if (!nameRegex.IsMatch(username))
            {
                return new { message = "Name format is wrong!", status = false };
            }

            //Surname Regex
            if (!surnameRegex.IsMatch(usersurname))
            {
                return new { message = "Surname format is wrong!", status = false };
            }

            //Password Regex
            if (!passwordRegex.IsMatch(userpwd))
            {
                return new { message = "Password format is wrong!", status = false };
            }

            //Username Length
            if(username.Length < 3 || username.Length > 30)
            {
                return new { message = "Name length is incorrect!", status = false };
            }

            //Usersurname Length
            if (usersurname.Length < 4 || usersurname.Length > 29)
            {
                return new { message = "Surname length is incorrect!", status = false };
            }

            return new { message = "Succesfully registered!", status = true };
        }
    }
}
