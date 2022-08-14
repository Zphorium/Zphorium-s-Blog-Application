using System;
using System.Collections.Generic;
using System.Text;

namespace Blogify.Models
{
    class User : Base
    {
        public string name;
        public string surname;
        public string email;
        public string password;
        public Role role;
        public Inbox inbox;

        public User(string _name, string _surname, string _email, string _password, Role _role) : base(Guid.NewGuid())
        {
            this.name = _name;
            this.surname = _surname;
            this.email = _email;
            this.password = _password;
            this.role = _role;
            this.inbox = new Inbox(this.id, new List<Notification>());
        }
    }
}
