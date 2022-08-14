using System;
using System.Collections.Generic;

namespace Blogify.Repository.IService
{
    interface IServiceProvider<T>
    {
        public T CREATE(T t);

        public T DELETE(T t);

        public T Update(T t, T t1);

        public T Get(Guid ID);
        public List<T> GetALL();
    }
}
