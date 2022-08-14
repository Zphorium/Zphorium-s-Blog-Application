using System;
using System.Collections.Generic;
using System.Text;

namespace Blogify.Models
{
    class Notification: Base
    {
        public string content;
        public Blog blog;

        public Notification(string _content, Blog _blog):base(Guid.NewGuid())
        {
            this.content = _content;
            this.blog = _blog;
        }
    }
}
