using System;
using System.Collections.Generic;
using System.Text;

namespace Blogify.Models
{
    class Blog: Base
    {
        public string title;
        public string content;
        public DateTime publisDate;
        public string blogCode;
        public bool verified;
        public User user;
        public List<Comment> comments;

        //Code tracker
        private static int codeStartingPoint = 1000;

        public Blog(string title, string content, DateTime publisDate, bool verified, User user):base(Guid.NewGuid())
        {
            this.title = title;
            this.content = content;
            this.publisDate = publisDate;
            this.blogCode = $"BL{codeStartingPoint}";
            this.verified = verified;
            this.user = user;
            this.comments = new List<Comment>();

            //Increase unique code.
            codeStartingPoint++;
        }
    }
}
