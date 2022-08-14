using System;
using System.Collections.Generic;
using System.Text;

namespace Blogify.Models
{
     class Role : Base
    {
        public string name;

        public Role(string name):base(Guid.NewGuid())
        {
            this.name = name;
        }
    }
}
