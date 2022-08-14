using System;
using System.Collections.Generic;
using System.Text;

namespace Blogify.Models
{
    class Comment : Base
    {
        public User user;
        public DateTime publishDate;
        public string content;

        public Comment(User user, DateTime publishDate, string content):base(Guid.NewGuid())
        {
            this.user = user;
            this.publishDate = publishDate;
            this.content = content;
        }
    }
}
