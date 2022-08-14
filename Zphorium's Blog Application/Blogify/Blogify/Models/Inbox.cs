using System;
using System.Collections.Generic;
using System.Text;

namespace Blogify.Models
{
    class Inbox : Base
    {
        public Guid userId;
        public List<Notification> notifications;
        public Inbox(Guid _userId, List<Notification> _notifications) : base(Guid.NewGuid())
        {
            this.userId = _userId;
            this.notifications = _notifications;
        }
    }
}
