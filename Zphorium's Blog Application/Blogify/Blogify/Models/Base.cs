using System;
using System.Collections.Generic;
using System.Text;

namespace Blogify.Models
{
    class Base
    {
        public Guid id;

        public Base(Guid _id)
        {
            this.id = _id;
        }
    }
}
