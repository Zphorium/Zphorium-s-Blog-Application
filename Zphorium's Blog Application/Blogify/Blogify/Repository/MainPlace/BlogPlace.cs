using Blogify.Models;
using Blogify.Repository.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogify.Repository.Services
{
    class BlogPlace : IServiceProvider<Blog>
    {



        static List<Blog> blogs = new List<Blog>();
        public Blog CREATE(Blog t)
        {
            blogs.Add(t);
            return t;
        }

        public Blog DELETE(Blog t)
        {
            blogs.Remove(t);
            return t;
        }

        public Blog Get(Guid ID)
        {
            Blog blog = blogs.Find(el => el.id == ID);
            if(blog != null)
            {
                return blog;
            }
            return null;
        }

        public List<Blog> GetALL()
        {
            return blogs;
        }

        public Blog Update(Blog t, Blog t1)
        {
            blogs.Remove(t);
            blogs.Add(t1);
            return t1;
        }
    }
}
