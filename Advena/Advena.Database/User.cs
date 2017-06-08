using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Database
{
    public class User: Basic
    {
        public User()
        {
            Comments = new List<Comment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string City { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
